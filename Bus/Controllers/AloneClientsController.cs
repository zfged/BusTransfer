using Bus.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Text;
using System.ComponentModel;

namespace Bus.Controllers
{
    [Authorize(Roles = "Admin")]
    [RequireHttps]
    public class AloneClientsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AloneClients
        public async Task<ActionResult> Index()
        {




            List<Client> clientsList = new List<Client>();
            foreach (var item in await db.Clients.GroupBy(p => p.Phone).ToListAsync())
            {
                clientsList.Add(item.First());
            }




            return View(clientsList);
        }

        public async Task<ActionResult> Mailing(string smstext)
        {
            List<SMSSend> SMSList = new List<SMSSend>();
            if (smstext != "" && smstext != null)
            {
                SMSWorker sms = new SMSWorker();
                var auth = sms.Auth("busforest", "5163809a");

                foreach (var item in await db.Clients.GroupBy(p => p.Phone).ToListAsync())
                {
                    var send = sms.SendSMS("Msg", item.First().Phone, smstext, null);
                    if (send[0] == "Сообщения успешно отправлены")
                    {
                        SMSList.Add(new SMSSend() { Verify = true, NumberUser = item.First().Phone, FIO = item.First().FIO });
                    }
                    else
                    {
                        SMSList.Add(new SMSSend() { Verify = false, NumberUser = item.First().Phone, FIO = item.First().FIO });
                    }
                }
            }
            return PartialView(SMSList);
        }



        public async Task<ActionResult> Excel()
        {
            GridView gv = new GridView();


            List<Client> clientsList = new List<Client>();
            foreach (var item in await db.Clients.GroupBy(p => p.Phone).ToListAsync())
            {
                clientsList.Add(item.First());
            }




            gv.DataSource = clientsList;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Clients.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = Encoding.UTF8;
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
        }


    }
}