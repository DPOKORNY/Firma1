using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Firma1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string nazev = "";
            int pocet1 = 1;
            int pocet2 = 1;
            int vyber1=0;
            int vyber2=0;
            bool stop = true;
            List<string> tabulka = new List<string>();
            using (StreamReader sr = new StreamReader(Server.MapPath("~/temp/PURTDELI.inc")))
            {

                string s;
               
                    while (stop)
                    {
                    s = sr.ReadLine();

                    if (s.Contains("STRUCTURE"))
                    {
                        Match match = Regex.Match(s, @"(?<=\()(.*?)(?=\))");
                        if (match.Success)
                        {
                            string value = match.Groups[0].Value;
                            nazev = value;
                        }
                        
                    }

                        if (s.TrimStart().StartsWith("("))
                        {

                            vyber1 = pocet1;
                        }

                        if (s.EndsWith(";"))
                        {

                            vyber2 = pocet1;
                            stop = false;
                            
                        }
                        pocet1 += 1;
                    }
                
            }
            using (StreamReader sr = new StreamReader(Server.MapPath("~/temp/PURTDELI.inc")))
            {

                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (pocet2 >= vyber1)
                    {
                        if (pocet2 <= vyber2)
                        {
                            tabulka.Add(s);
                        }
                    }
                    pocet2 += 1;
                }
            }
            
            string crtable = "CREATE TABLE "+nazev;
            foreach (string s in tabulka) {
                crtable += s;
                }            
           

            using(SqlConnection connection=new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Transakce1;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(crtable, connection);
                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }
            }
            ViewBag.Tabulka = tabulka;
            return View();
        }

        public ActionResult ONas()
        {
            return View();
        }

        public ActionResult Kontakt()
        {
            return View();
        }

        public ActionResult WebPages()
        {
            return View();
        }

        public ActionResult WebApp()
        {
            return View();
        }
    }
}