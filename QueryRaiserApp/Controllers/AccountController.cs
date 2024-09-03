using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueryRaiserApp.DB;
using QueryRaiserApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace QueryRaiserApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        static string path = @"D:\ms sql\QueryRaiserLog.txt";

        DateTime timelog = DateTime.Now;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult login()
        {
     
            using(StreamWriter writer=new StreamWriter(path))
            {
                writer.Write("Login is Open" + "-" + timelog);
                writer.Close();
            }
            return View("login");
        }

        [HttpPost]
        public JsonResult hellofun(login1 a)
        {
            try
            {
                var registerdetail = _context.Register.ToList();
                if (registerdetail.Any(em => em.emailid == a.email))
                {
                    var data = _context.Register.Find(a.email);
                    if ((data.emailid == a.email) && (data.userpassword == a.password))
                    {
                        return Json(data);
                    }
                    else
                    {
                        return Json("Incorrect Password");
                    }
                }

                return Json("Incorrect Email");
            }
            catch(Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();
                }
                return Json("Database Problem");
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Login proceesing block"+" "+a.email + "-" + timelog);
                    writer.Close();
                }
            }
        }
        public IActionResult register(Alldata a)
        {
            try
            {
                //  a.Usertype = _context.UserType.ToList();
                a.employQueryType = _context.employQueryType.ToList();
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
                    writer.Write("Register page is Open" + "-" + timelog);
                    writer.Close();
                }
            }
            return View(a);
        }

        public JsonResult registerdata(registration a)
        {
            try
            {
                var registerdetail = _context.Register.ToList();
                if (registerdetail.Any(em => em.emailid == a.emailid))
                {
                    return Json("Email Already Have Account");
                }
                else
                {
                    _context.Database.ExecuteSqlRaw("insert into Register(username,emailid,userpassword,usertypeid) values({0},{1},{2},{3})", a.username, a.emailid, a.userpassword, a.usertypeid);

                    _context.SaveChanges();

                    return Json("Success");

                }
            }
            catch(Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();
                }
                return Json("Data not insert");
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Register data proceesing" + "-" + timelog);
                    writer.Close();
                }
            }



        }
    }
}
