


using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QueryRaiserApp.Models
{
    public class login1
    {
        public string email { get; set; }

        public string password { get; set; }
    }
    public class studentQueryType
    {
        [Key]
        public int roleid { get; set; }

        public string rolename { get; set; }
    }
    public class employQueryType
    {
        [Key]
        public int roleid { get; set; }

        public string rolename { get; set; }
    }

    public class prioritys
    {
        [Key]
        public int priorityid { get; set; }

        public string priorityname { get; set; }
    }
    public class Usertype
    {
        [Key]
        public int typeid { get; set; }

        public string typename {  get; set; }

    
      
    }
    public class Statustype
    {
        [Key]
        public int statusid { get; set; }

        public string statustype { get; set; }
    }
    public class Roletype1
    {
        public int id { get; set; }

        public string role { get; set; }

        public string priority1 {  get; set; }
    }
    public class registration
    {
        public int userid { get; set; }
        public string username { get; set; }
        [Key]
        public string emailid { get; set; }   
        public string userpassword { get; set; }

        public int usertypeid { get; set; }
    }
    public class Userqueries
    {
        [Key]
        public int userqueryid { get; set; }
        public string query {  get; set; }

        public int usertypeid { get; set; }

        public int ?querystatusid { get; set; }
        public int priorityid {  get; set; }

        public DateTime submittedon { get; set;}

        public string submittedby {  get; set; }

        public string ?modifiedon { get; set; }

        public string ?modifiedby { get; set; }

    }
    public class Userqueries1
    {
        [Key]
        public int userqueryid { get; set; }
     

        public string ?review { get; set; }

       

    }
    public class Responsequeries
    {
        [Key]
        public int responsequeryid { get; set; }

        public int userqueryid { get; set; }

        public int ?statustypeid { get; set; }

        public string ?responses { get; set; }
        public string ?responseby {  get; set; }

        public string ?responseon { get; set; }

        public string ?modifiedon { get; set; }

        public string ?modifiedby { get; set; }

    }
    public class Alldata
    {
        public login1 login1 { get; set; }

        public List<Usertype> Usertype { get; set; }

        public List<Statustype> Statustype { get; set; }

        public Userqueries Userqueries { get; set; }

        public Responsequeries Responsequeries { get; set; }

        public List<studentQueryType> studentQueryTypes { get; set; }
        public Roletype1 Roletype1 { get; set; }

        public List<prioritys> prioritys { get; set; }

        public registration registration { get; set; }

       public Userqueries1 Userqueries1 { get; set; }
        public List<employQueryType> employQueryType { get; set; }
    
    }
}
