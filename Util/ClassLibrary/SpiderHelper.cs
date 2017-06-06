using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO.Compression;
namespace Util
{
    public class SpiderHelper
    {
        /// <summary>
        /// 从URL下载图片并保存
        /// </summary>
        /// <param name="url">图片链接地址</param>
        /// <param name="path">图片保存地址</param>
        static public void GetImage(string url, string path)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.ServicePoint.Expect100Continue = false;
                req.Method = "GET";
                req.KeepAlive = true;

                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                req.ContentType = "image/jpg";
                Stream stream = null;
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                Image.FromStream(stream).Save(path);
                // 释放资源
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 从URL获取html文档
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            string htmlCode;
            for(int i=0;i<3;i++)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                    webRequest.Timeout = 10000;
                    webRequest.Method = "GET";
                    webRequest.UserAgent = "Mozilla/4.0";
                    webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");

                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                    //获取目标网站的编码格式
                    string contentype = webResponse.Headers["Content-Type"];
                    Regex regex = new Regex("charset\\s*=\\s*[\\W]?\\s*([\\w-]+)", RegexOptions.IgnoreCase);
                    //如果使用了GZip则先解压
                    if (webResponse.ContentEncoding.ToLower() == "gzip")
                    {
                        Stream streamReceive = webResponse.GetResponseStream();
                        MemoryStream ms = new MemoryStream();
                        streamReceive.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        var zipStream = new GZipStream(ms, CompressionMode.Decompress);
                        //匹配编码格式
                        if (regex.IsMatch(contentype))
                        {
                            Encoding ending = Encoding.GetEncoding(regex.Match(contentype).Groups[1].Value.Trim());
                            using (StreamReader sr = new StreamReader(zipStream, ending))
                            {
                                htmlCode = sr.ReadToEnd();
                            }
                        }
                        else//匹配不到则自动转换成utf-8
                        {
                            StreamReader sr = new StreamReader(zipStream, Encoding.GetEncoding("utf-8"));

                            htmlCode = sr.ReadToEnd();
                            string subStr = htmlCode.Substring(0, 2000);
                            string pattern = "charset=(.*?)\"";
                            Encoding encoding;
                            foreach (Match match in Regex.Matches(subStr, pattern))
                            {
                                if (match.Groups[1].ToString().ToLower() == "utf-8")
                                    break;
                                else
                                {
                                    encoding = Encoding.GetEncoding(match.Groups[1].ToString().ToLower());
                                    ms.Seek(0, SeekOrigin.Begin);//设置流的初始位置
                                    var zipStream2 = new GZipStream(ms, CompressionMode.Decompress);
                                    StreamReader sr2 = new StreamReader(zipStream2, encoding);
                                    htmlCode = sr2.ReadToEnd();
                                }
                            }
                        }
                    }
                    else
                    {
                        using (Stream streamReceive = webResponse.GetResponseStream())
                        {
                            using (StreamReader sr = new StreamReader(streamReceive, Encoding.Default))
                            {
                                htmlCode = sr.ReadToEnd();
                            }
                        }
                    }
                    return htmlCode;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("重试中....................");
                }
            }

            return "";
        }
    }
}
