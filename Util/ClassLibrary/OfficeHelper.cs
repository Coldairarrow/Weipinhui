using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Util
{
    public class OfficeHelper
    {
        /// <summary>
        /// 将数据表保存到Excel表格中
        /// </summary>
        /// <param name="addr">Excel表格存放地址（程序运行目录后面的部分）</param>
        /// <param name="dt">要输出的DataTable</param>
        public static void SaveToExcel(string addr, System.Data.DataTable dt)
        {
            //0.注意：
            // * Excel中形如Cells[x][y]的写法，前面的数字是列，后面的数字是行!
            // * Excel中的行、列都是从1开始的，而不是0
            //1.制作一个新的Excel文档实例
            
            Excel::Application xlsApp = new Excel::Application();
            xlsApp.Workbooks.Add(true);
            /* 示例输入：需要注意Excel里数组以1为起始（而不是0）
              * for (int i = 1; i < 10; i++)
              * {
              *   for (int j = 1; j < 10; j++)
              *   {
              *     xlsApp.Cells[i][j] = "-"; 
              *   }
              * }
              */
            //2.设置Excel分页卡标题
            xlsApp.ActiveSheet.Name = dt.TableName;
            //3.合并第一行的单元格
            string temp = "";
            if (dt.Columns.Count < 26)
            {
                temp = ((char)('A' + dt.Columns.Count)).ToString();
            }
            else if (dt.Columns.Count <= 26 + 26 * 26)
            {
                temp = ((char)('A' + (dt.Columns.Count - 26) / 26)).ToString()
                  + ((char)('A' + (dt.Columns.Count - 26) % 26)).ToString();
            }
            else throw new Exception("列数过多");
            Excel::Range range = xlsApp.get_Range("A1", temp + "1");
            range.ClearContents(); //清空要合并的区域
            range.MergeCells = true; //合并单元格
                                     //4.填写第一行：表名，对应DataTable的TableName
            xlsApp.Cells[1][1] = dt.TableName;
            xlsApp.Cells[1][1].Font.Name = "黑体";
            xlsApp.Cells[1][1].Font.Size = 25;
            xlsApp.Cells[1][1].Font.Bold = true;
            xlsApp.Cells[1][1].HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//居中
            xlsApp.Rows[1].RowHeight = 60; //第一行行高为60（单位：磅）
                                           //5.合并第二行单元格，用于书写表格生成日期
            range = xlsApp.get_Range("A2", temp + "2");
            range.ClearContents(); //清空要合并的区域
            range.MergeCells = true; //合并单元格
                                     //6.填写第二行：生成时间
            xlsApp.Cells[1][2] = "报表生成于：" + DateTime.Now.ToString();
            xlsApp.Cells[1][2].Font.Name = "宋体";
            xlsApp.Cells[1][2].Font.Size = 15;
            //xlsApp.Cells[1][2].HorizontalAlignment = 4;//右对齐
            xlsApp.Cells[1][2].HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//居中
            xlsApp.Rows[2].RowHeight = 30; //第一行行高为60（单位：磅）
                                           //7.填写各列的标题行
            xlsApp.Cells[1][3] = "序号";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                xlsApp.Cells[i + 2][3] = dt.Columns[i].ColumnName;
            }
            xlsApp.Rows[3].Font.Name = "宋体";
            xlsApp.Rows[3].Font.Size = 15;
            xlsApp.Rows[3].Font.Bold = true;
            xlsApp.Rows[3].HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//居中
                                                                               //设置颜色
            range = xlsApp.get_Range("A3", temp + "3");
            range.Interior.ColorIndex = 33;
            //8.填写DataTable中的数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xlsApp.Cells[1][i + 4] = i.ToString();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    xlsApp.Cells[j + 2][i + 4] = dt.Rows[i][j];
                }
            }
            range = xlsApp.get_Range("A4", temp + (dt.Rows.Count + 3).ToString());
            range.Interior.ColorIndex = 37;
            range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //9.描绘边框
            range = xlsApp.get_Range("A1", temp + (dt.Rows.Count + 3).ToString());
            range.Borders.LineStyle = 1;
            range.Borders.Weight = 3;
            //10.打开制作完毕的表格
            //xlsApp.Visible = true;
            //11.保存表格到根目录下指定名称的文件中
            xlsApp.ActiveWorkbook.SaveAs(addr);
            xlsApp.Quit();
            xlsApp = null;
            GC.Collect();
        }
    }
}
