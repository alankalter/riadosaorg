using RiadosaOrg.Models;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public BaseController()
        {

        }

        
        public CSSNumbers cssNumbers
        {
            get
            {
                if (Session["CSSNumbers"] == null)
                    Session["CSSNumbers"] = Helpers.GetCSSNumbers();
                return (CSSNumbers)Session["CSSNumbers"];
            }
        }

        public string bgImage
        {
            get
            {
                if (Session["BGImage"] == null)
                    Session["BGImage"] = Helpers.GetRandomBgImage();
                return (string)Session["BGImage"];
            }
        }       

    }
}