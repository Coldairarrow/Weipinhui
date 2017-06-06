using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class FileHelper
    {

        /// <summary>
        /// 从指定位置读取文件
        /// </summary>
        /// <param name="path">文件位置</param>
        /// <returns></returns>
        public static string ReadTxt(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string txtStr = sr.ReadToEnd();
                sr.Close();

                return txtStr;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                WriteLog(e.ToString());
            }

            return "";
        }

        /// <summary>
        /// 将字符串写到txt文件中，若文件不存在就新建，若存在则追加到后面
        /// </summary>
        /// <param name="html">字符串</param>
        /// <param name="path">文件路径</param>
        public static void WritTxt(string html, string path)
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write(html + "\r\n");
                streamWriter.Flush();
                streamWriter.Close();
                fileStream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                WriteLog(e.ToString());
            }
        }

        /// <summary>
        /// 创建目录，若目录已存在则不变
        /// </summary>
        /// <param name="path">目录位置</param>
        public static void CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch(Exception e)
            {
                WriteLog(e.ToString());
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 输出日志到指定文件
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="path">日志文件位置（默认为D:\测试\a.log）</param>
        public static void WriteLog(String msg,string path= @"D:\测试\a.log")
        {
            StreamWriter writer = null;
            try
            {
                writer = File.AppendText(path);
                writer.WriteLine("{0} {1}", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), msg);
                writer.Flush();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
