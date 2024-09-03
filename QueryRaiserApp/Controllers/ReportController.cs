using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueryRaiserApp.DB;
using QueryRaiserApp.Models;

namespace QueryRaiserApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context2;

        static string path = @"D:\ms sql\QueryRaiserLog.txt";

        DateTime timelog = DateTime.Now;

        public ReportController(ApplicationDbContext context2)
        {
            _context2 = context2;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult report(string id)
        {
       
            List<getdetail> data=new List<getdetail>();
            try
            {
                var a = _context2.Register.Find(id);
                data = _context2.getdetails.FromSqlRaw("select u.userqueryid,u.query,ut.typename,s.statustype,p.priorityname,rq.responses,u.submittedon,u.submittedby,u.modifiedon,u.modifiedby from Userqueries u left join StatusTypes s on u.querystatusid=s.statusid inner join UserType ut on u.usertypeid=ut.typeid inner join Priority p on p.priorityid=u.priorityid inner join ResponseQueries rq on u.userqueryid=rq.userqueryid where u.submittedby={0}", id).ToList();
                ViewData["registerdatas"] = a;
            }
            catch(Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();
                }
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("College Website Report Open" + " " + id + "-" + timelog);
                    writer.Close();
                }

            }
            return View(data);
        }
        public IActionResult response()
        {
            return View();
        }
        public IActionResult Employ_report(string id) 
        {

            List<getdetail> b = new List<getdetail>();
            try
            {
                var a = _context2.Register.Find(id);
                b = _context2.getdetails.FromSqlRaw("select u.userqueryid,u.query,ut.typename,s.statustype,p.priorityname,rq.responses,u.submittedon,u.submittedby,u.modifiedon,u.modifiedby from Userqueries u left join StatusTypes s on u.querystatusid=s.statusid inner join UserType ut on u.usertypeid=ut.typeid inner join Priority p on p.priorityid=u.priorityid inner join ResponseQueries rq on u.userqueryid=rq.userqueryid where u.submittedby={0}", id).ToList();
                ViewData["registerdatas"] = a;
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();
                }
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Company Website Report Open" + " " + id + "-" + timelog);
                    writer.Close();
                }

            }
            return View(b);
        }




    }
}
