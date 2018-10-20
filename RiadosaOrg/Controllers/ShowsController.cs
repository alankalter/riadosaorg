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
    public class ShowsController : BaseController
    {
        // GET: Shows
        public ActionResult Index()
        {
            var shows = GetShows().OrderByDescending(x => x.Date);
            return View(new Shows { Future = shows.Where(x=>x.Date > DateTime.Now), Past = shows.Where(x => x.Date < DateTime.Now)  });
        }

        public IEnumerable<Show> GetShows()
        {
            var csvData = new DataTable("Shows");
            var shows = new List<Show>();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(Server.MapPath("~/shows.csv")))
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
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == string.Empty)
                            {
                                fieldData[i] = string.Empty; //fieldData[i] = null
                            }
                            //Skip rows that have any csv header information or blank rows in them
                            if (fieldData[0].Contains("Disclaimer") || string.IsNullOrEmpty(fieldData[0]))
                            {
                                continue;
                            }
                        }
                        shows.Add(new Show {
                            Venue = fieldData[0],
                            Location = fieldData[1],
                            Date = DateTime.Parse(fieldData[2]),
                            Url = fieldData[3],
                            Info = fieldData[4],
                        });
                        //csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return shows;
        }
    }
}