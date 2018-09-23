using NetDocs.UI.AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NetDocs.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_PostAuthorizeRequest(object sender, EventArgs eventArgs)
        {
            var user = HttpContext.Current.User;
            if (user.Identity.IsAuthenticated)
            {
                var accountNode = MvcSiteMapProvider.SiteMaps.Current.FindSiteMapNodeFromKey("MyAccount");
                if (accountNode != null)
                {
                    accountNode.Title = "Hello, " + user.Identity.Name;
                }
            }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e) 
        {

            //=========================================================
            //Pega o nome do que cookie que o usuário informou
            String _cultureCookieName = "Stefanini.QT.NetDocs.UI.Culture";
            //Captura o cookie
            HttpCookie _cultureCookieValue = Request.Cookies[_cultureCookieName];
            //Certifica-se que o cookie existe
            string _culture = string.Empty;

            //---
            //TROCA DE CULTURA
            if (!string.IsNullOrEmpty(Request["ddlLanguageSelector"]))
            {
                _culture = _culture = Request["ddlLanguageSelector"];

                //Cria um novo cookie, passando o nome no construtor
                HttpCookie _cookie = new HttpCookie("Stefanini.QT.NetDocs.UI.Culture");

                //Determina o valor o cookie
                _cookie.Value = _culture;

                //Configura o cookie para expirar 30 dias
                DateTime _date = DateTime.Now;
                TimeSpan _time = new TimeSpan(30, 0, 0, 0);

                _cookie.Expires = _date + _time;
                //Adiciona o cookie
                Response.Cookies.Add(_cookie);
            }
            else if (_cultureCookieValue != null)
            {
                //---
                //CULTURA EXISTENTE NO COOKIE
                _culture = _cultureCookieValue.Value;
            }
            else
            {
                //---
                //CULTURA DEFAULT DO BROWSER
                _culture = CultureInfo.InstalledUICulture.IetfLanguageTag;                
            }
            //=========================================================            

            _culture = (String.IsNullOrEmpty(_culture) ? "pt-BR" : _culture);            
            CultureInfo _cultureInfo = CultureInfo.GetCultureInfo(_culture);

            Thread.CurrentThread.CurrentUICulture = _cultureInfo;
            Thread.CurrentThread.CurrentCulture = _cultureInfo;            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMapping();
        }
    }
}
