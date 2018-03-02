using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTP.DAL
{
    public class InformationContext : DbContext
    {
        public InformationContext()
            : base("name=InformationContext")
        {

        }

        public virtual DbSet<InformationReport> InformationReport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InformationReport>().Property(x => x.ErrorCode).IsOptional();
            modelBuilder.Entity<InformationReport>().Property(x => x.ErrorMessage).IsOptional();
            modelBuilder.Entity<InformationReport>().Property(x => x.NumberOfResults).IsOptional();
            modelBuilder.Entity<InformationReport>().Property(x => x.Results).IsOptional();
            modelBuilder.Entity<InformationReport>().Property(x => x.StopID).IsOptional();
            modelBuilder.Entity<InformationReport>().Property(x => x.TimeStamp).IsOptional();
        }

    }
}
