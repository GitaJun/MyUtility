using System;
using ClosedXML.Excel;

namespace JunUtility.ExcelUtility
{
    /// <summary>
    /// Excel处理器
    /// </summary>
    public class ExcelHandler
    {
        private readonly string excelPath;
        /// <summary>
        /// 创建Excel处理器，文件类型不符合会抛出异常
        /// </summary>
        /// <param name="excelPath">Excel文件绝对路径</param>
        public ExcelHandler(string excelPath)
        {
            using (SupportedFileSuffix supportedSuffix = new(".xlsx", "xlsm", "xltx", "xltm"))
            {
                if (!supportedSuffix.CheckFileSuffix(excelPath))
                    throw new Exception($"请选择Excel类型文件({supportedSuffix})");

                this.excelPath = excelPath;
            }
        }

        /// <summary>
        /// 从Excel表中获取数据
        /// </summary>
        /// <param name="sheet">工作表页数（从1开始）</param>
        /// <param name="handle">对工作表的读写操作</param>
        public void Handle(int sheet, Action<IXLWorksheet> handle)
        {
            using (XLWorkbook workbook = new(this.excelPath))
            {
                IXLWorksheet worksheet = workbook.Worksheet(sheet);  //某个sheet
                handle(worksheet);
            }
        }
    }
}
