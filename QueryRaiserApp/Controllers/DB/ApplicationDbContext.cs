using Microsoft.EntityFrameworkCore;
using QueryRaiserApp.Models;

namespace QueryRaiserApp.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<registration> Register { get; set; }

        public DbSet<Usertype> UserType { get; set; }

        public DbSet<prioritys> Priority { get; set; }

        public DbSet<studentQueryType> studenttype{  get; set; }


        public DbSet<Userqueries> UserQueries { get; set; }

        public DbSet<Responsequeries> ResponseQueries { get; set; }


        public DbSet<teamleadrescive> reportstudent1{  get; set; }



        public DbSet<Statustype> StatusTypes { get; set; }

        public DbSet<getdetail> getdetails { get; set; }
        
        public DbSet<responsegetdetail> responsegetdetails { get; set; }

        public DbSet<reviewtable> reviewtables { get; set; }

        public DbSet<modalget> modalgets { get; set; }

        public DbSet<Userqueries1> userqueries1s { get; set; }

        public DbSet<employQueryType> employQueryType { get; set; }
    }
}
