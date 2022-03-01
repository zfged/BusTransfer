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

namespace Bus.Controllers
{
    public class RoutesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Routes
        public async Task<ActionResult> Index()
        {
            return View(await db.Routes.ToListAsync());
        }

        // GET: Routes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // GET: Routes/Create
      /*  public ActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NameDriver,NameRoute,RoadTypeRoute,DateRoute")] Route route)
        {

            if (await db.Routes.Where(p => p.RoadTypeRoute == route.RoadTypeRoute  && p.DateRoute == route.DateRoute && p.NameRoute == route.NameRoute).FirstOrDefaultAsync() != null)
            {
                ModelState.AddModelError("Дубликат", "Рейс на Этот день уже создан");
            }



            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }

            return View(route);
        }*/

        // GET: Routes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NameDriver,NameRoute,RoadTypeRoute,DateRoute")] Route route)
        {
          



            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); 
            }
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Route route = await db.Routes.FindAsync(id);
            var clients = await db.Clients.Where(p=> p.RouteId == id).ToListAsync();
            foreach (var item in clients)
            {
                item.RouteId = null;
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            db.Routes.Remove(route);
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
