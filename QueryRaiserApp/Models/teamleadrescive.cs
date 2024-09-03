using System.ComponentModel.DataAnnotations;

namespace QueryRaiserApp.Models
{
    public class teamleadrescive
    {
        public string username { get; set; }
        public string sender { get; set; }
        [Key]
        public string email { get; set; }
        public int role { get; set; }

        public string priority { get; set; }

        public string question { get; set; }

        public string responselink { get; set; }

        public string status { get; set; }
    }
}
