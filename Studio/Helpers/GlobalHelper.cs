using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;
using System.Net;
using System.Management;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.Mvc;
using Studio.Services;

namespace Studio
{
    public class TestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var url = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToLower();
            //WriteInLog(url);
            var list = new List<string>();
            list.Add("henhaoji.com.cn");
            list.Add("xn--hstt5p.com");
            list.Add("www.xn--hstt5p.com");
            list.Add("m.henhaoji.com.cn");

            if (list.Contains(url))
            {
                string newurl = "http://www.henhaoji.com.cn" + System.Web.HttpContext.Current.Request.RawUrl;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.StatusCode = 301;
                System.Web.HttpContext.Current.Response.Status = "301 Moved Permanently";
                System.Web.HttpContext.Current.Response.AddHeader("Location", newurl);
            }

            var rawUrl = System.Web.HttpContext.Current.Request.RawUrl.ToLower();
            if (rawUrl.Equals("/company/index.html") || rawUrl.Equals("/company.html") || rawUrl.Equals("/company"))
            {
                string newurl = "http://www.henhaoji.com.cn";
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.StatusCode = 301;
                System.Web.HttpContext.Current.Response.Status = "301 Moved Permanently";
                System.Web.HttpContext.Current.Response.AddHeader("Location", newurl);
            }
        }
    }

    public class ImageHelper
    {
        public ImageHelper()
        {
        }

        public static class ImageSaveType
        {
            /// <summary>
            /// 指定高宽裁减（不变形） 
            /// </summary>
            public const string Cut = "CUT";
            /// <summary>
            /// 指定高，宽按比例 
            /// </summary>
            public const string Height = "H";
            /// <summary>
            /// 指定宽，高按比例 
            /// </summary>
            public const string Width = "W";
            /// <summary>
            /// 指定高宽缩放（可能变形） 
            /// </summary>
            public const string WidthHeight = "HW";
            /// <summary>
            /// 原图
            /// </summary>
            public const string Original = "None";
        }
    }

    public class GlobalHelper
    {
        public GlobalHelper()
        {
        }

        public string GetUrl(string url)
        {
            var final = "";
            try
            {
                if (url == "/")
                {
                    final = "/Html/Company/index.html";
                }
                else
                {
                    var contenturl = url;

                    if (contenturl.IndexOf('?') > 0)
                    {
                        var fin = contenturl.Split('?');

                        final = "/Html" + fin[0].Split('.')[0];

                        var file = fin[1].Split('&');

                        var list = file.OrderBy(m => file[0]).ToList();

                        foreach (var item in list)
                        {
                            final += "_" + item.Split('=')[1];
                        }

                        final += ".html";
                    }
                    else
                    {
                        final = "/Html" + url;
                    }
                }
            }
            catch (Exception e)
            {
            }

            return final;
        }

        public static void WriteInLog(string msg)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath("/bin/log.txt"));

                using (FileStream fs = fileInfo.OpenWrite())
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("=================================================");
                    sw.WriteLine("时间:" + DateTime.Now + "\r\n");
                    sw.WriteLine(msg + "\r\n");
                    sw.WriteLine("=================================================");
                    sw.Flush();
                    sw.Close();

                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public static int UserID
        {
            get
            {
                int userID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                return userID;
            }
        }

        public static string UserName()
        {
            using (SiteDataContext db = new SiteDataContext())
            {
                var user = db.UserProfiles.Find(UserID);

                return user.UserName;
            }
        }

        public static string GetWebClientIp()
        {
            string userIP = "未获取用户IP";

            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                string CustomerIP = "";

                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch { }

            return userIP;
        }

        //// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="containsPage">要输出到的page对象</param>
        /// <param name="validateNum">验证码</param>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 5; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.DimGray), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.DarkGreen, Color.DarkBlue, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.BurlyWood), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        ////获取本机的IP  
        //public string getLocalIP()
        //{
        //    string strHostName = Dns.GetHostName(); //得到本机的主机名  
        //    IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP  
        //    string strAddr = ipEntry.AddressList[0].ToString();
        //    return (strAddr);
        //}
        ////获取本机的MAC  
        //public string getLocalMac()
        //{
        //    string mac = null;
        //    //[DllImport("System.Management.dll")]  
        //    ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection queryCollection = query.Get();
        //    foreach (ManagementObject mo in queryCollection)
        //    {
        //        if (mo["IPEnabled"].ToString() == "True")
        //            mac = mo["MacAddress"].ToString();
        //    }
        //    return (mac);
        //}

        ////获取远程主机IP  
        //public string[] getRemoteIP(string RemoteHostName)
        //{
        //    IPHostEntry ipEntry = Dns.GetHostByName(RemoteHostName);
        //    IPAddress[] IpAddr = ipEntry.AddressList;
        //    string[] strAddr = new string[IpAddr.Length];
        //    for (int i = 0; i < strAddr.Length; i++)
        //    {
        //        strAddr[i] = IpAddr[i].ToString();
        //    }
        //    return (strAddr);
        //}
        ////获取远程主机MAC  
        //public string getRemoteMac(string localIP, string remoteIP)
        //{
        //    Int32 ldest = inet_addr(remoteIP); //目的ip   
        //    Int32 lhost = inet_addr(localIP); //本地ip   

        //    try
        //    {
        //        Int64 macinfo = new Int64();
        //        Int32 len = 6;
        //        int res = SendARP(ldest, 0, ref macinfo, ref len);
        //        string macnum = Convert.ToString(macinfo, 16);//16进制的Mac地址数据  
        //        string[] macArr = new string[macnum.Length];
        //        string macaddr = "";
        //        for (int i = 0; i < (macnum.Length / 2); i++)
        //        {
        //            macArr[i] = macnum[macnum.Length - 2 * (i + 1)].ToString() + macnum[macnum.Length - 2 * i - 1].ToString();//将Mac地址顺序转置  
        //        }
        //        for (int j = 0; j < 6; j++)
        //        {
        //            if (j == 0)
        //            {
        //                macaddr = macArr[0].ToString();
        //            }
        //            else
        //            {
        //                macaddr = macaddr + ":" + macArr[j].ToString();
        //            }
        //        }
        //        return macaddr;
        //    }
        //    catch (Exception err)
        //    {
        //        Console.WriteLine("Error:{0}", err.Message);
        //    }
        //    return "无法获得MAC地址";
        //}
    }
}