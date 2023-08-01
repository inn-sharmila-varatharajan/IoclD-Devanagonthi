using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoclDSqlWebApi1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Card",
                url: "",
                defaults: new { controller = "Home", action = "Card", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Login",
               url: "",
               defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "SVG",
              url: "",
              defaults: new { controller = "Home", action = "gas", id = UrlParameter.Optional }
          );
            routes.MapRoute(
  name: "Dashboard",
  url: "",
  defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
);
            routes.MapRoute(name: "hourly",
 url: "",
 defaults: new { controller = "Home", action = "hourly", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Daily",
url: "",
defaults: new { controller = "Home", action = "Daily", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Monthly",
url: "",
defaults: new { controller = "Home", action = "Monthly", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Settings",
url: "",
defaults: new { controller = "Home", action = "Settings", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Interrupt",
url: "",
defaults: new { controller = "Home", action = "Interrupt", id = UrlParameter.Optional }
);
            routes.MapRoute(
          name: "Truck",
          url: "",
          defaults: new { controller = "Home", action = "Truck", id = UrlParameter.Optional }
          );
            routes.MapRoute(
         name: "TruckOccupancy",
         url: "",
         defaults: new { controller = "Home", action = "TruckOccupancy", id = UrlParameter.Optional }
         );
            routes.MapRoute(
                    name: "Deviation",
                    url: "",
                    defaults: new { controller = "Home", action = "Deviation", id = UrlParameter.Optional }
                    );
			routes.MapRoute(
				   name: "TruckCount",
				   url: "",
				   defaults: new { controller = "Home", action = "TruckCount", id = UrlParameter.Optional }
				   );
			routes.MapRoute(
				   name: "TruckPage",
				   url: "",
				   defaults: new { controller = "Home", action = "TruckPage", id = UrlParameter.Optional }
				   );
			routes.MapRoute(
				   name: "SensorPage",
				   url: "",
				   defaults: new { controller = "Home", action = "SensorPage", id = UrlParameter.Optional }
				   );
			routes.MapRoute(
				   name: "Unregnfc",
				   url: "",
				   defaults: new { controller = "Home", action = "Unregnfc", id = UrlParameter.Optional }
				   );
			routes.MapRoute(
				   name: "Upload",
				   url: "",
				   defaults: new { controller = "Home", action = "Upload", id = UrlParameter.Optional }
				   );
			routes.MapRoute(
				   name: "Config",
				   url: "",
				   defaults: new { controller = "Home", action = "Config", id = UrlParameter.Optional }
				   );
			routes.MapRoute(
				   name: "Devicestatus",
				   url: "",
				   defaults: new { controller = "Home", action = "Devicestatus", id = UrlParameter.Optional }
				   );
		}
    }
}
