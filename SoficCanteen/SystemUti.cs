using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml.Linq;

namespace SoficCanteen
{
    public class SystemUti
    {
        #region WRITE LOG
        public static void WriteLogError(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString("g") + ": " + ex.Source + "; " + ex.Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // ignored
            }
        }
        public static void WriteLogError(string message)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Log_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": " + message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": " + message);
                }

            }
            //StreamWriter sw = null;
            //try
            //{
            //    sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
            //    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": " + message);
            //    sw.Flush();
            //    sw.Close();
            //}
            //catch (Exception ex)
            //{
            //    // ignored
            //    Console.Write(ex.ToString());
            //}
        }
        #endregion

        #region SEND MAIL
        public static void SendEmail(string body, string subject)
        {
            if (PingFunction("mail.thaco.com.vn", 1000, "MAIL SERVER SUCCESS"))
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("tranvannam3@thaco.com.vn");
                    message.To.Add(new MailAddress("tranvannam3@thaco.com.vn"));
                    message.Subject = subject;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;

                    string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt";
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(filePath);
                    message.Attachments.Add(attachment);

                    smtp.Port = 587;
                    smtp.Host = "mail.thaco.com.vn"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("tranvannam3@thaco.com.vn", "Th@cochulai2019");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    WriteLogError(ex.ToString());
                }
            }
        }
        #endregion

        #region PING FUNCTION
        public static bool PingFunction(string ipAddress, int timeOut, string logMessage)
        {
            bool result = false;
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(ipAddress, timeOut);
                if (reply.Status == IPStatus.Success)
                {
                    //Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);
                    //Console.WriteLine(reply.ToString());
                    //WriteLogError(logMessage);
                    result = true;
                }
            }
            catch
            {
                WriteLogError("NOT " + logMessage);
            }
            return result;
        }
        #endregion

        #region Download Image
        public static System.Drawing.Image DownloadImage(string imageUrl)
        {
            System.Drawing.Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;
                webRequest.ServicePoint.ConnectionLeaseTimeout = 5000;
                webRequest.ServicePoint.MaxIdleTime = 5000;

                using (System.Net.WebResponse webResponse = webRequest.GetResponse())
                {

                    using (System.IO.Stream stream = webResponse.GetResponseStream())
                    {
                        image = System.Drawing.Image.FromStream(stream);
                    }
                }

                webRequest.ServicePoint.CloseConnectionGroup(webRequest.ConnectionGroupName);
                webRequest = null;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message, ex);
                image = SoficCanteen.Properties.Resources.no_image_icon_23483;

            }
            return image;
        }
        #endregion

        #region Convert datatable to list
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        #endregion
        public static string AvatarUrl(string url)
        {
            string result = null;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "APIKEY THACO2017");
            httpWebRequest.Headers.Add("APIKEY", "THACO2017");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
        //Function consuming api
        public static string Consuming(string url)
        {
            string result = null;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "APIKEY THACO2017");
            httpWebRequest.Headers.Add("APIKEY", "THACO2017");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
        public static string[] ReadConfig()
        {
            string[] arr = null;
            try
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml("Config.xml");
                DataTable dt = new DataTable();
                dt = dataSet.Tables["Configuration"];
                arr = string.Join(",", dt.Rows[0].ItemArray).Split(',').ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return arr;
        }
        public static bool UpdateConfig(string loaithucdonId, string ngay, string khunggioanId, string makhunggioAn, string tenloaithucDon)
        {
            bool sKetQua = false;
            try
            {
                XDocument testXML = XDocument.Load("Config.xml");
                XElement config = testXML.Descendants("Configuration").Where(c => c.Attribute("id").Value.Equals("1")).FirstOrDefault();
                
                config.Element("LoaiThucDon_Id").Value = loaithucdonId;
                config.Element("Ngay").Value = ngay;
                config.Element("KhungGioAn_Id").Value = khunggioanId;
                config.Element("MaKhungGioAn").Value = makhunggioAn;
                config.Element("TenLoaiThucDon").Value = tenloaithucDon;

                testXML.Save("Config.xml");
                sKetQua = true;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
            return sKetQua;
        }
    }
}
