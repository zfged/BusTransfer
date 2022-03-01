using Bus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bus.Controllers
{

    [RequireHttps]
    [Authorize(Roles = "Driver,Admin,Moderator")]
    public class MapsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Maps

        public async Task<ActionResult> Index(string coordsType,int id)
        {
            ViewBag.coordsType = Convert.ToBoolean(coordsType);

            var routes = await db.Routes.FindAsync(id);
            routes.Clients = db.Clients.Where(p => p.RouteId == id);
           

          
            return  View(routes.Clients);
        }
    }
}