using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bus.Models;
using Yandex.Geocoder;

namespace Bus.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        public async Task<ActionResult> Index()
        {
            var clients = db.Clients.Include(c => c.Route);
            return View(await clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
   /*     public ActionResult Create()
        {
            ViewBag.RouteId = new SelectList(db.Routes, "Id", "NameRoute");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,FIO,Phone,PlaceBus,RoadType,CoordsFrom,CoordsTo,RouteId")] Client client)
        {
            if(await db.Clients.Where(p=> p.RoadType == client.RoadType && p.PlaceBus == client.PlaceBus && p.Date == client.Date && p.RouteId == client.RouteId).FirstOrDefaultAsync() != null)
            {
                ModelState.AddModelError("Дубликат", "Это Место В Автобусе Уже занято");
            }

            var geocoderFrom = new YandexGeocoder
            {
                SearchQuery = client.CoordsFrom,
                Results = 1,

                LanguageCode = LanguageCode.ru_RU


            };

            var coordFromFirst = geocoderFrom.GetResults();

            if (geocoderFrom.GetResults().Count == 0)
            {
                ModelState.AddModelError("Локация", "Адрес Отправки Не Определился");
            }
            else
            {
                client.CoordsFromR = geocoderFrom.GetResults().First().Point.Latitude + "," + geocoderFrom.GetResults().First().Point.Longitude;
            }

            var geocoderTo = new YandexGeocoder
            {
                SearchQuery = client.CoordsTo,
                Results = 1,

                LanguageCode = LanguageCode.ru_RU
            };

            if (geocoderTo.GetResults().Count == 0)
            {
                ModelState.AddModelError("Локация", "Адрес Прибытия Не Определился");
            }
            else
            {
                client.CoordsToR = geocoderTo.GetResults().First().Point.Latitude + "," + geocoderTo.GetResults().First().Point.Longitude;
            }


            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RouteId = new SelectList(db.Routes, "Id", "NameRoute", client.RouteId);
            return View(client);
        }*/

        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }


            ViewBag.RouteId = new SelectList(db.Routes.Where(p => p.DateRoute == client.Date && p.RoadTypeRoute == client.RoadType ), "Id", "NameRoute", client.RouteId);
            return View(client);
        }
        
        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,FIO,Phone,PlaceBus,RoadType,CoordsFrom,CoordsTo,RouteId")] Client client)
        {
         


            if (client.RouteId == 0)
            {
                client.RouteId = null;
            }

            var geocoderFrom = new YandexGeocoder
            {
                SearchQuery = client.CoordsFrom,
                Results = 1,

                LanguageCode = LanguageCode.ru_RU


            };

            var coordFromFirst = geocoderFrom.GetResults();

            if (geocoderFrom.GetResults().Count == 0)
            {
                ModelState.AddModelError("Локация", "Адрес Отправки Не Определился");
            }
            else
            {
                client.CoordsFromR = geocoderFrom.GetResults().First().Point.Latitude + "," + geocoderFrom.GetResults().First().Point.Longitude;
            }

            var geocoderTo = new YandexGeocoder
            {
                SearchQuery = client.CoordsTo,
                Results = 1,

                LanguageCode = LanguageCode.ru_RU
            };

            if (geocoderTo.GetResults().Count == 0)
            {
                ModelState.AddModelError("Локация", "Адрес Прибытия Не Определился");
            }
            else
            {
                client.CoordsToR = geocoderTo.GetResults().First().Point.Latitude + "," + geocoderTo.GetResults().First().Point.Longitude;
            }

            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.RouteId = new SelectList(db.Routes.Where(p => p.DateRoute == client.Date && p.RoadTypeRoute == client.RoadType), "Id", "NameRoute", client.RouteId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home"); 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
