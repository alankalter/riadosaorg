using Microsoft.VisualBasic.FileIO;
using RiadosaOrg.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult Index()
        {
            var blogs = GetBlogs().OrderByDescending(x => x.Date);

            return View(blogs);
        }

        public IEnumerable<Blog> GetBlogs()
        {
            var csvData = new DataTable("Blogs");
            var blogs = new List<Blog>();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(Server.MapPath("~/blogs.csv")))
                {
                    csvReader.SetDelimiters(new string[]
                    {
                        ","
                    });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null

                        //Skip rows that have any csv header information or blank rows in them
                        if (string.IsNullOrEmpty(fieldData[0]))
                        {
                            continue;
                        }
                        //for (int i = 0; i < fieldData.Length; i++)
                        //{
                        //    if (fieldData[i] == string.Empty)
                        //    {
                        //        fieldData[i] = string.Empty; //fieldData[i] = null
                        //    }
                            
                        //}
                        blogs.Add(new Blog
                        {
                            Text = fieldData[0],
                            Date = DateTime.Parse(fieldData[1]),
                        });
                        //csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blogs;
        }
    }
}