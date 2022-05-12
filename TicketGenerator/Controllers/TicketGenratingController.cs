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
        
        public Tickect ticket;
        public Role role;
        public UserRole user;
        public TicketGenratingController(IService<Tickect, int> ticketserv)
        {
            this.ticketserv = ticketserv;
            ticket = new Tickect();
            role = new Role();
            user = new UserRole();
        }
        public IActionResult Index(int id, int UserId)
        {
            var Result = ticketserv.Get().Result;
            //return View(Result);

            

               var res = new Tickect();
          
            var outcome=(ticketserv.Get().Result.Where(e => e.TicketNo == id).FirstOrDefault());
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem() { Text = "AssignRole", Value = "1", Selected = true };
            SelectListItem item2 = new SelectListItem() { Text = "User", Value = "2", Selected = false };
            SelectListItem item3 = new SelectListItem() { Text = "Supporter", Value = "3", Selected = false };
            SelectListItem item4 = new SelectListItem() { Text = "Engineer", Value = "4", Selected = false };

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);

            ViewBag.AssignRole = items;
           

            ViewBag.TicketNo = outcome.TicketNo;

                outcome.DateAndTime = DateTime.Now;
                ViewBag.DataTime = outcome.DateAndTime;
                ViewBag.Status = outcome.Status;
                ViewBag.Title = outcome.Title;
                ViewBag.Message=outcome.Message;
                
                
          
            return View(res);

            //return View(tickect);

        }


        

        public IActionResult Create(string UserId)
        {

           
            var res = new Tickect();
            return View(res);

        }

        [HttpPost]
        public IActionResult Create(Tickect tickect)
        {
            var Userid = HttpContext.Session.GetInt32("UserId");
            var Roleid = HttpContext.Session.GetInt32("RoleId");
            //var Emailid = HttpContext.Session.GetString("EmailId");
            if (ModelState.IsValid)
            {
               
                if (Roleid != null)
                {
                    tickect.DateAndTime = DateTime.Now;
                    tickect.AssignTo = "None";
                    tickect.Uid = Userid;
                    tickect.Status = 0;
                    var res = ticketserv.CreateAsync(tickect).Result;
                    ViewBag.Roleid = user.RoleId;
                    HttpContext.Session.SetInt32("Ticketno", res.TicketNo);

                 //  return RedirectToAction("Index", "TicketGenrating", new { id = res.TicketNo });



                }
                return RedirectToAction("Index", "TicketGenrating");


            }
            else
            {
                ViewData["Message"] = " Wrong Data!!!!";

                //Stay on the same page
                return View(tickect);
            }
        }
    }
}
