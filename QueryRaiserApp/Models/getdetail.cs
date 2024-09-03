using System.ComponentModel.DataAnnotations;

namespace QueryRaiserApp.Models
{
    public class getdetail
    {
        [Key]
        public int ?userqueryid { get; set; }
        public string ?query { get; set; }

        public string ?typename { get; set; }

        public string ?statustype { get; set; }
        public string ?priorityname { get; set; }

        public DateTime ?submittedon { get; set; }

        public string ?submittedby { get; set; }

        public string ?responses { get; set; }
        public string? modifiedon { get; set; }

        public string? modifiedby { get; set; }


    }
    public class responsegetdetail
    {
        [Key]
        public int userqueryid { get; set; }

        public string ?query { get; set; }
        public DateTime ?submittedon { get; set; }

        public string ?submittedby { get; set; }
        public string ?statustype { get; set; }

        public string ?priorityname { get; set; }

        public string ?responses { get; set; }

        public string ?responseon { get; set; }

        public string ?responseby { get; set; }

        public string  ?review { get; set; }

        public string ?modifiedon { get; set; }

        public string? modifiedby { get; set;}



    }
    public class reviewtable
    {
        [Key]
        public int queryid { get; set;}

        public string ?response { get; set; }

        public string ?review { get; set; }

        public string ?reviewer { get; set;}

        public string ?modifiedon { get; set; }
    }
    public class modalget
    {
        [Key]
        public int ?userqueryid { get; set; }

        public string ?responses { get; set; }

        public string ?submittedby { get; set; }
    }
}
