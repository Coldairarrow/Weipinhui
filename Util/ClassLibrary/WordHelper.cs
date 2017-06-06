//=====================================================================================
// All Rights Reserved , Copyright © Learun 2013
//=====================================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Office.Interop.Word;
using System.Data;
namespace Util
{
    /// <summary>
    /// Word帮助类
    /// </summary>
    public class WordHelper
    {
        #region 向Word指定的书签插入图片
        /// <summary>
        /// 向Word指定的书签插入图片
        /// </summary>
        /// <param name="wordPath">word文件绝对路径</param>
        /// <param name="imgPath">图片绝对路径</param>
        /// <param name="mark">书签名称</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public void WordReplacePicture(string wordPath, string imgPath, string mark, int width, int height)
        {
            object Nothing = System.Reflection.Missing.Value;
            //创建一个名为wordApp的组件对象
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            //word文档位置
            object filename = wordPath;

            //定义该插入图片是否为外部链接
            object linkToFile = true;

            //定义插入图片是否随word文档一起保存
            object saveWithDocument = true;

            //打开word文档
            Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            try
            {
                //标签
                object bookMark = mark;
                //图片
                string replacePic = imgPath;

                if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                {
                    //查找书签
                    doc.Bookmarks.get_Item(ref bookMark).Select();
                    //设置图片位置
                    app.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    //在书签的位置添加图片
                    Microsoft.Office.Interop.Word.InlineShape inlineShape = app.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    //设置图片大小
                    inlineShape.Width = width;
                    inlineShape.Height = height;

                    doc.Save();
                }
                //else
                //{
                //    //word文档中不存在该书签，关闭文档
                //    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                //}
            }
            catch
            {

            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();//关闭word文档
                }
                if (app != null)
                {
                    app.Quit();//退出word应用程序
                }
            }
        }
        #endregion

        #region Doc2Pdf
        /// <summary>
        /// 基于Office转换为PDF
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">转换后的目标文件夹路径</param>
        /// <returns>转换是否成功</returns>
        public static bool ConvertWithWord(string sourcePath, string targetPath)
        {
            bool result = false;
            Application application = new Application();
            Document document = null;
            try
            {
                application.Visible = false;
                document = application.Documents.Open(sourcePath);
                if (!Directory.Exists(Path.GetDirectoryName(targetPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                }
                document.ExportAsFixedFormat(targetPath, WdExportFormat.wdExportFormatPDF);
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }
            finally
            {
                document.Close();//关闭
                application.Quit();//退出word
            }
            return result;
        }
        /// <summary>
        /// 基于WPS转换为PDF
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">转换后的目标文件夹路径</param>
        /// <returns>转换是否成功</returns>
        public bool ConvertWithWPS(string sourcePath, string targetPath)
        {
            //WPS.ApplicationClass app = new WPS.ApplicationClass();
            //WPS.Document doc = null;
            //try
            //{
            //    doc = app.Documents.Open(sourcePath, true, true, false, null, null, false, "", null, 100, 0, true, true, 0, true);
            //    doc.ExportPdf(targetPath, "", "");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return false;
            //}
            //finally
            //{
            //    doc.Close();
            //}
            return true;
        }

        #endregion
    }
}
