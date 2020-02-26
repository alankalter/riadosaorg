using System;
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

        [Authorize]
        [HttpPost]
        public string UpdatePost(Blog blog)
        {
            try
            {
                using (var data = new revocarr_RiadosaOrgEntities())
                {
                    var blogForUpdate = data.Blogs.Where(x => x.BlogId == blog.BlogId).FirstOrDefault();

                    if (blogForUpdate == null)
                        throw new Exception("Event not found");

                    blogForUpdate.Text = blog.Text;
                    blogForUpdate.Date = blog.Date;

                    data.SaveChanges();
                }

                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost]
        public string CreatePost(Blog blog)
        {
            try
            {
                using (var data = new revocarr_RiadosaOrgEntities())
                {
                    data.Blogs.Add(blog);
                    data.SaveChanges();
                }

                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost]
        public string DeletePost(Blog blog)
        {
            try
            {
                using (var data = new revocarr_RiadosaOrgEntities())
                {
                    var blogToDelete = data.Blogs.Where(x => x.BlogId == blog.BlogId).Single();
                    data.Blogs.Remove(blogToDelete);
                    data.SaveChanges();
                }

                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}