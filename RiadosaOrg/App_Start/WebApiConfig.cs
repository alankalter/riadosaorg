using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using RiadosaOrg.Models;

namespace RiadosaOrg
{
    public static class WebApiConfig
    {
        public static string BackgroundImage;
        public static CSSNumbers CSSNumbers;

        public static void Register(HttpConfiguration config)
        {
            CSSNumbers = Helpers.GetCSSNumbers(); 
            BackgroundImage = Helpers.GetRandomBgImage();

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
