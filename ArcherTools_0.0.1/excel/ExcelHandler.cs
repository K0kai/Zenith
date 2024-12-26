using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace ArcherTools_0._0._1.excel
{
    internal class ExcelHandler
    {

        internal bool WorksheetExists(string filePath, string worksheetName)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    if (sheet.Name.Equals(worksheetName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }

        }

    }
}
        
        /*internal DataTable ReadExcel(string filePath, string worksheetName)
        {
            DataTable dataTable = new DataTable();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                if (WorksheetExists(filePath, worksheetName))
                {
                    
                }
                }
            }

        */
        
