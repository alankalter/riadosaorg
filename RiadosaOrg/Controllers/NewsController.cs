using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult Index()
        {
            List<Blog> blogs;
            using (var data = new revocarr_RiadosaOrgEntities())
            {
                blogs = data.Blogs.Select(x => x).OrderByDescending(x => x.Date).ToList();
            }
            return View(blogs);
        }        
    }
}