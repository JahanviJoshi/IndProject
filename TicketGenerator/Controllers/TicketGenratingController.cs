using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketGenerator.Models;
using TicketGenerator.Services;

namespace TicketGenerator.Controllers
{
    public class TicketGenratingController : Controller
    {
        private IService<Tickect, int> ticketserv;
        private TickectCreatingDbContext ctx;
        public Tickect ticket;
        public Role role;
        public UserRole user;
        public TicketGenratingController(IService<Tickect, int> ticketserv, TickectCreatingDbContext ctx)
        {
            this.ticketserv = ticketserv;
            ticket = new Tickect();
            role = new Role();
            user = new UserRole();
            this.ctx = ctx;
        }
        public IActionResult Index(int id, int UserId)
        {
            //var Result = ticketserv.Get().Result;
            var res = new Tickect();
          
           var outcome=(ticketserv.Get().Result.Where(e => e.TicketNo == id).FirstOrDefault());
            //List<SelectListItem> items = new List<SelectListItem>();
            //SelectListItem item1 = new SelectListItem() { Text = "AssignRole", Value = "1", Selected = true };
            //SelectListItem item2 = new SelectListItem() { Text = "User", Value = "2", Selected = false };
            //SelectListItem item3 = new SelectListItem() { Text = "Supporter", Value = "3", Selected = false };
            //SelectListItem item4 = new SelectListItem() { Text = "Engineer", Value = "4", Selected = false };

            //items.Add(item1);
            //items.Add(item2);
            //items.Add(item3);
            //items.Add(item4);

            // ViewBag.AssignRole = items;
            //ViewBag.TicketNo = outcome.TicketNo;
            //outcome.DateAndTime = DateTime.Now;
            //ViewBag.DataTime = outcome.DateAndTime;
            //ViewBag.Status = outcome.Status;
            //ViewBag.Title = outcome.Title;
            //ViewBag.Message = outcome.Message;
            var tickno = HttpContext.Session.GetInt32("Ticketno");
            var title = HttpContext.Session.GetString("Title");
            return View("Create");
        }


        

        public IActionResult Create(string UserId)
        {

           
            var res = new Tickect();
            return View(res);

        }

        [HttpPost]
        public IActionResult Create(Tickect tickect , int id)
        {
            var Userid = HttpContext.Session.GetInt32("UserId");
            var Roleid = HttpContext.Session.GetInt32("RoleId");  
            //var Emailid = HttpContext.Session.GetString("EmailId");
            if (ModelState.IsValid)
            {
               
                if (Roleid == null)
                {
                    tickect.DateAndTime = DateTime.Now;
                    tickect.AssignTo = "None";
                    tickect.Uid = 7;
                    tickect.Status = 0;
                    var res = ticketserv.CreateAsync(tickect).Result;
                    //ViewBag.Roleid = user.RoleId;
                    HttpContext.Session.SetInt32("Ticketno", res.TicketNo);
                    HttpContext.Session.SetString("Title", res.Title);
                   // HttpContext.Session.SetString("Message", res.Message);
                    //  return RedirectToAction("Index", "TicketGenrating", new { id = res.TicketNo });




                }
                //  return RedirectToAction("Index", "TicketGenrating");
                return RedirectToAction("Create", "TicketGenrating");

                //var outcome = (ticketserv.Get().Result.Where(e => e.TicketNo == id).FirstOrDefault());



                //ViewBag.TicketNo = outcome.TicketNo;
                //outcome.DateAndTime = DateTime.Now;
                //ViewBag.DataTime = outcome.DateAndTime;
                //ViewBag.Status = outcome.Status;
                //ViewBag.Title = outcome.Title;
                //ViewBag.Message = outcome.Message;

                

            }
            else
            {
                ViewData["Message"] = " Wrong Data!!!!";

                //Stay on the same page
                return View(tickect);
            }
        }

        public PartialViewResult All()
        {
            var tickects = ctx.Tickects.ToList();
            return PartialView("BindingPublicPartial", tickects);
        }

        public PartialViewResult Public(int uid)
        {
            
            var role = ctx.UserRoles.ToList().Where(x => x.Uid == uid).Select(x => x.RoleId).FirstOrDefault();
            if (role == 1 && role ==2)
            {
                var tickects = ctx.Tickects.ToList().Where(x => x.Uid == uid);
                return PartialView("BindingPublicPartial", tickects);
            }
            return PartialView();


        }

        public PartialViewResult Internal(int uid)
        {
            var role = ctx.UserRoles.ToList().Where(x => x.Uid == uid).Select(x => x.RoleId).FirstOrDefault();
            if (role == 1 && role==3 && role==4)
            {
                var tickects = ctx.Tickects.ToList().Where(x => x.Uid == uid);
                return PartialView("BindingPublicPartial", tickects);
            }
            return PartialView();
        }
    }
}
