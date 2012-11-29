using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using WeatherLogger;
using System.Data;
using System.Data.SqlClient;

namespace WeatherLogger
{
    
    class DownloadXML
    {
        private readonly string _url;

        public DownloadXML(string url)
        {
            _url = url;
        }


        public string DownloadWeatherXML()
        {
            try
            {
                string xml = new WebClient().DownloadString(_url);
                string modifiedxml = xml.Replace("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?> \r\n", "");
                return modifiedxml;
            }
            catch (Exception e)
            {
                LogErrors LogObj = new LogErrors();
                LogObj.InsertErrors(e.Message, e.Source, e.StackTrace, e.TargetSite.ToString());
                return null;
            }
        }
    }


    class InsertIntoDb
    {
        // Get the connection string from App.Config
        // Dont forget to the add the reference System.ConfigurationManager in the project
        private string _connstr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

        
        
        // This method calls a stored proc to insert the xml into a staging temp table 
        public void InsertData(string XmlStrObj)
        {
            
                using (SqlConnection conn = new SqlConnection(_connstr))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "CheckAndInsertData";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@raw_xml", SqlDbType.Xml);
                        cmd.Parameters["@raw_xml"].Value = XmlStrObj;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogErrors LogObj = new LogErrors();
                        LogObj.InsertErrors(e.Message, e.Source, e.StackTrace, e.TargetSite.ToString());
                    }
                }
            
        }
    }
        class Program
        {
            static void Main(string[] args)
            {

                Console.WriteLine("Executing the WeatherLogger Task....");

                // Set the xml url
                string url = "http://w1.weather.gov/xml/current_obs/KEWR.xml";
                string XmlStr;
                
                // Create the DownloadXML object
                DownloadXML XmlObj = new DownloadXML(url);
                
               
                XmlStr = XmlObj.DownloadWeatherXML();

                
                if (XmlStr == null)
                {
                    Environment.Exit(0);  
                }
                else
                {
                    InsertIntoDb DbObj = new InsertIntoDb();
                    DbObj.InsertData(XmlStr);
                }

                Console.WriteLine("Done");
                //Console.Read();
            }
        }
    }

