using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using RTP.DAL;
using System.Net;
using System.Xml;
using System.Data.Entity;

namespace RTP.WinService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        public void OnDebug()
        {
            OnStart(null);
        }
        private Timer _informationTimer;

        public Timer InformationTimer
        {
            get
            {
                if (_informationTimer == null)
                {
                    _informationTimer = new Timer();
                    _informationTimer.Interval = 20000;
                    _informationTimer.Enabled = true;
                    _informationTimer.Elapsed += InformationTimer_Elapsed;
                }
                return _informationTimer;
            }
            set { _informationTimer = value; }
        }

        public async void InformationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InformationTimer.Stop();
            try
            {
                await GetData();
            }
            catch (Exception ex)
            {

            }
            InformationTimer.Start();
        }

        private async Task GetData()
        {
            using (InformationContext entity = new InformationContext())
            {
                List<InformationReport> reports = new List<InformationReport>();
                reports = entity.InformationReport.ToList();
                Uri uri = new Uri("https://data.dublinked.ie/cgi-bin/rtpi/realtimebusinformation?stopid=103381&format=xml");
                WebClient client = new WebClient();
                string xmlStr = await client.DownloadStringTaskAsync(uri);
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStr);

                DataSet ds = new DataSet();
                ds.ReadXml(new XmlNodeReader(xmlDoc));

                if (reports.Count == 0) await FillData(ds, entity, reports);
                else
                {
                    reports.ForEach(x => entity.InformationReport.Remove(x));
                    await FillData(ds, entity, reports);
                }
            }
        }
        private async Task FillData(DataSet ds, InformationContext entity, List<InformationReport> reports)
        {
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                InformationReport report = new InformationReport();
                report.ErrorCode = item["errorcode"] != null ? Convert.ToInt32(item["errorcode"]) : 0;
                report.ErrorMessage = item["errormessage"].ToString();
                report.NumberOfResults = item["numberofresults"].ToString();
                report.Results = item["results"].ToString();
                report.StopID = item["stopid"] != null ? Convert.ToInt32(item["stopid"]) : 0;
                report.TimeStamp = item["timestamp"] != null ? Convert.ToDateTime(item["timestamp"]) : DateTime.Now;
                reports.Add(report);
                entity.InformationReport.Add(report);
            }
            entity.SaveChanges();
        }
        protected override void OnStart(string[] args)
        {
            InformationTimer.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
