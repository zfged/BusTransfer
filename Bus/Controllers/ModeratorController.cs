using Bus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yandex.Geocoder;

namespace Bus.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [RequireHttps]
    public class ModeratorController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

        // GET: Roads/Create
        public ActionResult CreatePar()
        {
            return PartialView();
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: Roads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<ActionResult> CreatePar([Bind(Include = "Id,Date,FIO,Phone,PlaceBus,RoadType,CoordsFrom,CoordsTo,RouteId")] Client client)
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "invalid adress");
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "invalid adress");
            }
            else
            {
                client.CoordsToR = geocoderTo.GetResults().First().Point.Latitude + "," + geocoderTo.GetResults().First().Point.Longitude;
            }


            if (ModelState.IsValid)
            {
                db.Clients.Add(client);


                await db.SaveChangesAsync();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return Content("<h1 style='color:red;'>Ошибка Валидации</h1>");
        }


        public async Task<ActionResult> AddRoute([Bind(Include = "Id,NameDriver,NameRoute,RoadType,DateRoute")] Route route, string clientsField)
        {
            if (ModelState.IsValid)
            {

                db.Routes.Add(route);
                await db.SaveChangesAsync();

                if (clientsField != "")
                {
                    foreach (var item in clientsField.Split(','))
                    {
                        Client client = db.Clients.Find(Convert.ToInt32(item));
                        client.RouteId = route.Id;
                        db.Entry(client).State = EntityState.Modified;

                        await db.SaveChangesAsync();

                    }
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return Content("<h1 style='color:red;'>Ошибка Валидации</h1>");
        }






        public async Task<ActionResult> LoadRoutes(GetRoute getRoute)
        {

            DateTime dataBus = Convert.ToDateTime(getRoute.Date);
            int dd = ((TimeSpan)(dataBus - DateTime.Now)).Days;
            if (dd < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Вы не имеете права смотреть историю");
            }
            else
            {
                Response.Headers["Content-Type"] = "charset=utf-8";
                string freeClients = "<select class='selectpicker' multiple id='clientsSelect' name='clientsSelect'>";
                foreach (var item in db.Clients.Where(p => p.RouteId == null && p.Date == getRoute.Date && p.RoadType == getRoute.RoadType))
                {
                    freeClients += "<option value='" + item.Id + "'>" + item.FIO + "</option>";
                }

                freeClients += "</select>";

                //  Response.Headers["freeClients"] = freeClients;

                ViewBag.FreeClients = freeClients;



                string selectRoutes = "<select class='form-control valid' id='RouteId' name='RouteId'><option value='0' >Не Выбирать Пока</option>";




                var routes = await db.Routes.Where(p => p.DateRoute == getRoute.Date && p.RoadTypeRoute == getRoute.RoadType).ToListAsync();

                foreach (var item in routes)
                {
                    item.Clients = await db.Clients.Where(p => p.RouteId == item.Id).ToListAsync();
                    selectRoutes += "<option value='" + item.Id + "'>" + item.NameRoute + "</option>";
                }

                selectRoutes += "</select> <span class='field-validation-valid text-danger' data-valmsg-for='RouteId' data-valmsg-replace='true'></span>";

                // Response.Headers["selectRoutes"] = selectRoutes;

                ViewBag.SelectRoutes = selectRoutes;

                ViewBag.url = Request.Url.Host;
                return PartialView(routes);
            }
        }

        public async Task<ActionResult> LoadClients(GetRoute getRoute)
        {
            DateTime dataBus = Convert.ToDateTime(getRoute.Date);
            int dd = ((TimeSpan)(dataBus - DateTime.Now)).Days;
            if (dd < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Вы не имеете права смотреть историю");
            }
            else
            {
                return PartialView(await db.Clients.Where(p => p.RoadType == getRoute.RoadType && p.Date == getRoute.Date).Include(c => c.Route).ToListAsync());
            }
        }



        /* public ActionResult About()
         {
             ViewBag.Message = "Your application description page.";

             return View();
         }

         public ActionResult Contact()
         {
             ViewBag.Message = "Your contact page.";

             return View();
         }*/
    

}
}