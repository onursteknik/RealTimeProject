using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RTP.DAL;

namespace RTP.WebAPI.Controllers
{
    public class DataController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetInformations()
        {
            List<InformationReport> reports = new List<InformationReport>();
            try
            {
                using (InformationContext entity = new InformationContext())
                {
                    reports = entity.InformationReport.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(Ok(reports));
        }
    }
}
