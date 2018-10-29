using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using RiadosaOrg.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiadosaOrg.Providers
{
    public class DataProvider
    {
        public IEnumerable<Show> GetShows(string path)
        {
            var csvData = new DataTable("Shows");
            var shows = new List<Show>();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path))
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
                        shows.Add(new Show
                        {
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

        public IEnumerable<Blog> GetBlogs(string path)
        {
            var csvData = new DataTable("Blogs");
            var blogs = new List<Blog>();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path))
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

        public bool WriteToCsv(string path, object record)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, append: true))
                {
                    var csv = new CsvWriter(writer);
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }


            }
            catch(Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}