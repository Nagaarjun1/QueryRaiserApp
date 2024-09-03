using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using QueryRaiserApp.DB;
using QueryRaiserApp.Models;



namespace QueryRaiserApp.Controllers
{
    public class QueryResponseController : Controller
    {
        private readonly ApplicationDbContext _context3;


        static string path = @"D:\ms sql\QueryRaiserLog.txt";

        DateTime timelog = DateTime.Now;

        public QueryResponseController(ApplicationDbContext context3)
        {
            _context3 = context3;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult reportsave(Userqueries b)
        {
            Alldata a = new Alldata();
            try
            {
                a.registration = _context3.Register.Find(b.submittedby);
                DateTime time = DateTime.Now;
                string times = time.ToString();


                _context3.Database.ExecuteSqlRaw("insert into UserQueries(query,usertypeid,priorityid,submittedby) values({0},{1},{2},{3})", b.query, b.usertypeid, b.priorityid, b.submittedby);

                Userqueries c = _context3.UserQueries.FromSqlRaw("select top 1  * from UserQueries order by submittedon desc").FirstOrDefault();
                _context3.Database.ExecuteSqlRaw("insert into ResponseQueries(userqueryid) values({0})", c.userqueryid);
                _context3.Database.ExecuteSqlRaw("insert into reviewtables(queryid) values({0})", c.userqueryid);
                return Json("sucess");
            }
            catch(Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();

                }
                return Json("Database Error");
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Query Submit Block Excuted" + " "  + timelog);
                    writer.Close();
                }

            }


        }
        public IActionResult response_student(string id) {
            List<responsegetdetail> b = new List<responsegetdetail>();
            try
            {
                var a = _context3.Register.Find(id);
                ViewData["registerdatas"] = a;
                ViewData["registerdatas2"] = a;


                b = _context3.responsegetdetails.FromSqlRaw("select u.userqueryid,u.query,u.submittedon,u.submittedby,r.review,p.priorityname,rq.responses,rq.responseon,s.statustype,rq.responseby,rq.modifiedon,rq.modifiedby  from UserQueries u inner join reviewtables r on u.userqueryid=r.queryid inner join ResponseQueries rq on u.userqueryid=rq.userqueryid inner join Priority p on u.priorityid=p.priorityid left join StatusTypes s on u.querystatusid=s.statusid  where u.usertypeid={0}", a.usertypeid).ToList();
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
                    writer.Write("Response Message Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }
            return View(b);
        }
        public IActionResult Responsereplaystu(string id)
        {
            Alldata b = new Alldata();
            try
            {
                string[] values = id.Split(',');
                int Id = Int32.Parse(values[0]);

                string emailnav = values[1];

                var a = _context3.Register.Find(emailnav);
                ViewData["registerdatas"] = a;


                b.Statustype = _context3.StatusTypes.ToList();
                b.Userqueries1 = _context3.userqueries1s.FromSqlRaw("select u.userqueryid,r.review from UserQueries u inner join reviewtables r on u.userqueryid=r.queryid  where u.userqueryid={0}", Id).FirstOrDefault();
                Userqueries userq = new Userqueries();
                userq = _context3.UserQueries.FromSqlRaw("select * from UserQueries where userqueryid={0}", Id).FirstOrDefault();
                b.Userqueries1.review = userq.query + " " + b.Userqueries1.review;
               
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
                    writer.Write("Response Message Form Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }
            return View(b);
        }

        public IActionResult student_responce_save(Alldata a)
        {
            try
            {
                DateTime time = DateTime.Now;
                string times = time.ToString();
                //  var querydet = _context3.ResponseQueries.Find(a.Responsequeries.userqueryid);
                var querydet = _context3.ResponseQueries.FromSqlRaw("select * from ResponseQueries where userqueryid={0}", a.Responsequeries.userqueryid).FirstOrDefault();
                string responses;
                string time1;
                string responseby;
                if (string.IsNullOrEmpty(querydet.responses))
                {
                    responses = a.Responsequeries.responses;
                }
                else
                {
                    responses = querydet.responses + ',' + a.Responsequeries.responses;
                }
                if (string.IsNullOrEmpty(querydet.responseon))
                {
                    time1 = times;
                }
                else
                {
                    time1 = querydet.responseon + ',' + times;
                }
                if (string.IsNullOrEmpty(querydet.responseby))
                {
                    responseby = a.Responsequeries.responseby;
                }
                else
                {
                    responseby = querydet.responseby + ',' + a.Responsequeries.responseby;
                }



                _context3.Database.ExecuteSqlRaw("update ResponseQueries set responses={0},responseon={1},responseby={2},statustypeid={3} where userqueryid={4}", responses, time1, responseby, a.Responsequeries.statustypeid, a.Responsequeries.userqueryid);

                _context3.Database.ExecuteSqlRaw("update UserQueries set querystatusid=r.statustypeid,modifiedon=r.responseon,modifiedby=r.responseby from UserQueries q left join ResponseQueries r on q.userqueryid=r.userqueryid");

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
                    writer.Write("Response Message Form Sumbit Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }

            return RedirectToAction("Student", "Home",new {id= a.Responsequeries.responseby });
            
            }
            public IActionResult response_employ(string id)
            {
            List<responsegetdetail> b = new List<responsegetdetail>();
            try
            {
                var a = _context3.Register.Find(id);
                ViewData["registerdatas"] = a;



                b = _context3.responsegetdetails.FromSqlRaw("select u.userqueryid,u.query,u.submittedon,u.submittedby,r.review,p.priorityname,rq.responses,rq.responseon,s.statustype,rq.responseby,rq.modifiedon,rq.modifiedby  from UserQueries u inner join reviewtables r on u.userqueryid=r.queryid inner join ResponseQueries rq on u.userqueryid=rq.userqueryid inner join Priority p on u.priorityid=p.priorityid left join StatusTypes s on u.querystatusid=s.statusid  where u.usertypeid={0}", a.usertypeid).ToList();

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
                    writer.Write("Response Message Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }
            return View(b);
            }
            public IActionResult Responsereplayemp(string id)
            {
            Alldata b = new Alldata();
            try
            {
                string[] values = id.Split(',');
                int Id = Int32.Parse(values[0]);

                string emailnav = values[1];
                var a = _context3.Register.Find(emailnav);
                ViewData["registerdatas"] = a;
              
                b.Statustype = _context3.StatusTypes.ToList();
                b.Userqueries1 = _context3.userqueries1s.FromSqlRaw("select u.userqueryid,r.review from UserQueries u inner join reviewtables r on u.userqueryid=r.queryid  where u.userqueryid={0}", Id).FirstOrDefault();
                Userqueries userq = new Userqueries();
                userq = _context3.UserQueries.FromSqlRaw("select * from UserQueries where userqueryid={0}", Id).FirstOrDefault();
                b.Userqueries1.review = userq.query + " " + b.Userqueries1.review;
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
                    writer.Write("Response Message Form Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }
            return View(b);
            

            }
        public IActionResult employ_responce_save(Alldata a)
        {
            try
            {
                DateTime time = DateTime.Now;
                string times = time.ToString();
                Responsequeries querydet = new Responsequeries();
                querydet = _context3.ResponseQueries.FromSqlRaw("select * from ResponseQueries where userqueryid={0}", a.Responsequeries.userqueryid).FirstOrDefault();
                string responses;
                string time1;
                string responseby;
                if (string.IsNullOrEmpty(querydet.responses))
                {
                    responses = a.Responsequeries.responses;
                }
                else
                {
                    responses = querydet.responses + ',' + a.Responsequeries.responses;
                }
                if (string.IsNullOrEmpty(querydet.responseon))
                {
                    time1 = times;
                }
                else
                {
                    time1 = querydet.responseon + ',' + times;
                }
                if (string.IsNullOrEmpty(querydet.responseby))
                {
                    responseby = a.Responsequeries.responseby;
                }
                else
                {
                    responseby = querydet.responseby + ',' + a.Responsequeries.responseby;
                }



                _context3.Database.ExecuteSqlRaw("update ResponseQueries set responses={0},responseon={1},responseby={2},statustypeid={3} where userqueryid={4}", responses, time1, responseby, a.Responsequeries.statustypeid, a.Responsequeries.userqueryid);



                _context3.Database.ExecuteSqlRaw("update UserQueries set querystatusid=r.statustypeid,modifiedon=r.responseon,modifiedby=r.responseby from UserQueries q left join ResponseQueries r on q.userqueryid=r.userqueryid");

                
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
                    writer.Write("Response Message Form Submit Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }



            return RedirectToAction("Employ", "Home", new {id= a.Responsequeries.responseby });
        }


        public JsonResult reportsaveemp(Userqueries b)
        {
            Alldata a = new Alldata();
            try
            {
                a.registration = _context3.Register.Find(b.submittedby);

                _context3.Database.ExecuteSqlRaw("insert into UserQueries(query,usertypeid,priorityid,submittedby) values({0},{1},{2},{3})", b.query, b.usertypeid, b.priorityid, b.submittedby);
                Userqueries c = _context3.UserQueries.FromSqlRaw("select top 1  * from UserQueries order by submittedon desc").FirstOrDefault();
                _context3.Database.ExecuteSqlRaw("insert into ResponseQueries(userqueryid) values({0})", c.userqueryid);
                _context3.Database.ExecuteSqlRaw("insert into reviewtables(queryid) values({0})", c.userqueryid);
                return Json("sucess");
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();

                }
                return Json("Database Error");
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Query Submit Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }

        }

        public JsonResult ResponseGet(int id)
        {
            try
            {
                Userqueries userq = new Userqueries();
                userq = _context3.UserQueries.FromSqlRaw("select * from UserQueries where userqueryid={0}", id).FirstOrDefault();
                if (userq.querystatusid == 204)
                {
                    return Json("Message Status is Closed");
                }
                else
                {

                    modalget a = _context3.modalgets.FromSqlRaw("select u.userqueryid,u.submittedby,r.responses from ResponseQueries r inner join UserQueries u on u.userqueryid=r.userqueryid where u.userqueryid={0}", id).FirstOrDefault();
                    return Json(a);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();

                }
                return Json("Database Error");
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Report Message Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }



        } 
        public JsonResult reviewpost(reviewtable a)
        {
            try
            {
                DateTime time = DateTime.Now;
                string times = time.ToString();
                var reviews = _context3.reviewtables.Find(a.queryid);

                string response;
                string review;
                string reviewer;
                string time1;


                if (string.IsNullOrEmpty(reviews.review))
                {
                    review = a.review;
                }
                else
                {
                    review = reviews.review + ',' + a.review;
                }
                if (string.IsNullOrEmpty(reviews.reviewer))
                {
                    reviewer = a.reviewer;
                }
                else
                {
                    reviewer = reviews.reviewer + ',' + a.reviewer;
                }
                if (string.IsNullOrEmpty(reviews.modifiedon))
                {
                    time1 = times;
                }
                else
                {
                    time1 = reviews.modifiedon + ',' + times;
                }
                _context3.Database.ExecuteSqlRaw("update reviewtables set response={0},review={1},reviewer={2},modifiedon={3} where queryid={4}", a.response, review, reviewer, time1, a.queryid);
                _context3.Database.ExecuteSqlRaw("update ResponseQueries set modifiedon=rt.modifiedon,modifiedby=rt.reviewer   from ResponseQueries r inner join reviewtables rt on r.userqueryid=rt.queryid");

                return Json("sucess");
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();

                }
                return Json("Database Error");
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Report Message Sent Block Excuted" + " " + timelog);
                    writer.Close();
                }

            }

        }

        public JsonResult FeedbackGet(int id)
        {
          reviewtable a= _context3.reviewtables.Find(id);

            return Json(a);
        }
        public JsonResult querychecklist(string id)
        {
            List<getdetail> data = new List<getdetail>();
            try
            {
             

                data = _context3.getdetails.FromSqlRaw("select u.userqueryid,u.query,ut.typename,s.statustype,p.priorityname,rq.responses,u.submittedon,u.submittedby,u.modifiedon,u.modifiedby from Userqueries u left join StatusTypes s on u.querystatusid=s.statusid inner join UserType ut on u.usertypeid=ut.typeid inner join Priority p on p.priorityid=u.priorityid inner join ResponseQueries rq on u.userqueryid=rq.userqueryid where u.submittedby={0} and datediff(ss,u.submittedon,getdate()) > 5 and u.modifiedby is  null", id).ToList();
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(ex.Message + "-" + timelog);
                    writer.Close();

                }
                return Json("Database Error");
            }
            finally
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write("Unresponse Message  Block Excuted" + " " + timelog);
                    writer.Close();
                }


            }

            return Json(data);
        }
        public JsonResult resentviewscript(int id)
        {

            modalget a = _context3.modalgets.FromSqlRaw("select u.userqueryid,u.submittedby,r.responses from ResponseQueries r inner join UserQueries u on u.userqueryid=r.userqueryid where u.userqueryid={0}", id).FirstOrDefault();
            return Json(a);
        }


    }
}
