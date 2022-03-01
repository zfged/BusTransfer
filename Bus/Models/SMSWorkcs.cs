using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Reflection;

namespace Bus.Models
{
    /// <summary>
    ///     Êëàññ äëÿ ðàáîòû ñ ñåðâèñîì
    /// </summary>
    public class SMSWorker : Service
    {
        // êëþ÷ ñåññèè äëÿ õðàíåíèÿ èíñòàíñà îáúåêòà
        private const string SessionKey = "SMSWorker_Instance";

        class SMSProxy
        {
            public string Host;
            public int Port;
        }

        // êîíñòðóêòîð
        public SMSWorker()
        {
            this.CookieContainer = new CookieContainer();

            // Åñëè èñïîëüçóåòñÿ ïðîêñè
            if (ConfigurationManager.AppSettings["UseProxy"] == "true")
            {
                // Âû÷èòûâàåì íàñòðîéêè ïðîêñè èç Web.Config - à
                SMSProxy pr = new SMSProxy();
                pr.Host = ConfigurationManager.AppSettings["ProxyHost"];
                pr.Port = Convert.ToInt32(ConfigurationManager.AppSettings["ProxyPort"]);

                WebProxy proxy = new WebProxy(pr.Host, pr.Port);
                this.Proxy = proxy;
            }
        }

        public static SMSWorker GetInstance()
        {
            if (HttpContext.Current == null)
                return new SMSWorker();

            // îïðåäåëÿåì, ñóùåñòâóåò ëè îáúåêò â ñåññèè
            SMSWorker worker = HttpContext.Current.Session[SessionKey] as SMSWorker;
            if (worker == null)
            {
                // ñîçäàåì îáúåêò
                worker = new SMSWorker();
                // ïîìåùàåì â ñåññèþ
                HttpContext.Current.Session[SessionKey] = worker;
            }
            return worker;
        }

        /// <summary>
        ///    Îáíóëÿåì îáúåêò â ñåññèè (åñëè íóæíî)
        /// </summary>
        public void CloseSession()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session[SessionKey] = null;
        }
    }
}
