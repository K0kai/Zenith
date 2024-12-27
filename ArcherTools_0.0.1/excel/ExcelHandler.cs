using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace ArcherTools_0._0._1.excel
{
    internal class ExcelHandler : IDisposable
    {
        public string _filePath { get; set; }
        public ExcelPackage _package { get; set; }

        public ExcelHandler(string filePath) {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!File.Exists(filePath)) {
                throw new FileNotFoundException("The Excel file does not exist in the specified path.");
            }
            this._filePath = filePath;
            FileInfo fileInfo = new FileInfo(_filePath);
            _package = new ExcelPackage(fileInfo);
        }


        internal bool WorksheetExists(string worksheetName)
        {
            if (_package != null)
            {
                
                {
                    foreach (var sheet in _package.Workbook.Worksheets)
                    {
                        if (sheet.Name.Equals(worksheetName, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                   
                }
            }
            return false;
        }

        public string? ReadCell(string worksheetName, int row, int column)
        {
            if (_package == null)
            {
                throw new FileNotFoundException("The Excel package is empty.");
            }

            if (!this.WorksheetExists(worksheetName))
            {
                throw new ArgumentException($"The worksheet \"{worksheetName}\" does not exist within the excel.");
            }

            var worksheet = _package.Workbook.Worksheets[worksheetName];
            if (worksheet != null)
            {
                var cellValue = worksheet.Cells[row, column].Value;
                if (cellValue != null)
                {
                    return cellValue.ToString();
                }
            }
            return null;
        }
        public void Dispose()
        {
            _package?.Dispose();
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

