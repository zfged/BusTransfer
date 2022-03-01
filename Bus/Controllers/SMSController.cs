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
    public class SMSController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SMS
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LastSMS()
        {
            SMSWorker sms = new SMSWorker();
            var auth = sms.Auth("busforest", "5163809a");

            return Content("Осталось СМС: " + sms.GetCreditBalance());
        }
      
            
        
    }
}