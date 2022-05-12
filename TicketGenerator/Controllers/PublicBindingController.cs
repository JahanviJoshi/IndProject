using Microsoft.AspNetCore.Mvc;
using TicketGenerator.Models;
using TicketGenerator.Services;

namespace TicketGenerator.Controllers
{
    public class PublicBindingController : Controller
    {
        private IService<Tickect, int> ticketserv;
        Tickect TC = new Tickect();
        public IActionResult Index()
        {
            return View();
        }


       
        
    }
}
