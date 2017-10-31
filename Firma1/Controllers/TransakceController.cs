using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Firma1.Controllers
{
    public class TransakceController : Controller
    {
        // GET: Transakce
        public ActionResult Index()
        {
            List<string> list = new List<string>();
            /* list.Add("PURSP001");
             list.Add("02");
             list.Add("01");
             list.Add("PURCHASING SYSTEM        PRICE AND SUPLIER MENU");
             list.Add("02");
             list.Add("18");
             list.Add(DateTime.Now.ToShortDateString());
             list.Add("02");
             list.Add("70");
             list.Add(DateTime.Now.ToLongTimeString());
             list.Add("03");
             list.Add("70");
             list.Add("JOB CODE =");
             list.Add("03");
             list.Add("01");
             */
            string cesta = Server.MapPath("~/temp/Transakce.txt");
            using (StreamReader sr = new StreamReader(cesta))
            {
                list.Clear();
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains("DFLD") && !s.Contains("CDATE") && !s.Contains("CTIME") && !s.Contains("NUMBER") && !s.Contains("MSG"))
                    {
                        Match match=Regex.Match(s,@"(?<=')[^']*(?=')");
                        if (match.Success)
                        {
                            string value = match.Groups[0].Value;
                            list.Add(value);
                        }

                        
                        
                    }

                   

                    if (s.Contains("CDATE"))
                    {
                        
                            list.Add(DateTime.Now.ToShortDateString());
                        
                    }

                    if (s.Contains("CTIME"))
                    {
                        list.Add(DateTime.Now.ToLongTimeString());
                    }

                    if (s.Contains("NUMBER"))
                    {

                        list.Add("?");

                    }

                    if (s.StartsWith("MSG"))
                    {
                        int index = s.IndexOf("POS=");
                        
                        string v1 = s.Substring(index + 5, 2);
                        string v2 = s.Substring(index + 8, 2);
                        list.Add("?");
                        list.Add(v1);
                        list.Add(v2);
                        list.Add("WHITE");
                        break;
                    }

                    if (s.Contains("POS="))
                    {
                        int index = s.IndexOf("POS=");
                        string v1 = s.Substring(index + 5, 2);
                        string v2 = s.Substring(index + 8, 2);
                        list.Add(v1);
                        list.Add(v2);
                    }
                   
                    if (s.Contains("EATTR=") && s.Contains("POS="))
                    {
                        int index = s.IndexOf("EATTR=");
                        string v1 = s.Substring(index + 8,7);
                        int index1 = v1.IndexOf(")");
                       v1 = v1.Substring(0, index1);
                        if (v1 == "TURQ")
                        {
                            v1 = "TURQUOISE";
                        }
                        if (v1 == "PINK")
                        {
                            v1 = "GREEN";
                        }
                        list.Add(v1);
                    }
                    else if(s.Contains("POS=") && !s.Contains("EATTR="))
                    {
                        list.Add("RED");
                    }

                    
                }

    

            }
            ViewBag.A = list;
            
            return View();
        }
    }
}