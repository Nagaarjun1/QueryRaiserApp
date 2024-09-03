using Microsoft.AspNetCore.Mvc;
using QueryRaiserApp.DB;
using QueryRaiserApp.Models;
using System.Diagnostics;

namespace QueryRaiserApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context1;

        static string path = @"D:\ms sql\QueryRaiserLog.txt";

        DateTime timelog = DateTime.Now;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context1)
        {
            _logger = logger;
            _context1 = context1;
        }

        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Student(string id)
        {
            Alldata data = new Alldata();
            try
            {
             
                var a = _context1.Register.Find(id);
                data.studentQueryTypes = _context1.studenttype.ToList();
                data.studentQueryTypes.RemoveAll((x) => x.roleid == a.usertypeid);
                data.prioritys = _context1.Priority.ToList();
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
                    writer.Write("College Webside Logged in" + " " + id + "-" + timelog);
                    writer.Close();
                }
            }

            return View(data);
        }
        public IActionResult HR_role()
        {

            return View();
        }
        public IActionResult Director_role()
        {
            return View();
        }
    
        public IActionResult Techadvisor_role()
        {
            return View();
        }
        public IActionResult Centerlead_role()
        {
            return View();
        }
        public IActionResult Infrahead_role()
        {
            return View();
        }
        public IActionResult Employ(string id)
        {
            Alldata data = new Alldata();
            try
            {
                var a = _context1.Register.Find(id);
                data.employQueryType = _context1.employQueryType.ToList();
                data.employQueryType.RemoveAll((x) => x.roleid == a.usertypeid);
                data.prioritys = _context1.Priority.ToList();
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
                    writer.Write("Company Webside Logged in" + " " + id + "-" + timelog);
                    writer.Close();
                }
            }

            return View(data);

           
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
