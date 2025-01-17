using OfficeOpenXml;
using System.Diagnostics;

namespace ArcherTools_0._0._1.excel
{
    internal class ExcelHandler
    {
        public string _filePath { get; set; }

        public ExcelHandler(string filePath)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The Excel file does not exist in the specified path.");
            }
            this._filePath = filePath;
            FileInfo fileInfo = new FileInfo(_filePath);
        }


        internal bool WorksheetExists(string worksheetName)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (package != null)
                {

                    {
                        foreach (var sheet in package.Workbook.Worksheets)
                        {
                            if (sheet.Name.Equals(worksheetName, StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }
                        }

                    }
                }
            }
            return false;
        }

        public string? GetCell(string worksheetName, int row, int column)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (package == null)
                {
                    throw new FileNotFoundException("The Excel package is empty.");
                }

                if (!this.WorksheetExists(worksheetName))
                {
                    throw new ArgumentException($"The worksheet \"{worksheetName}\" does not exist within the excel.");
                }

                var worksheet = package.Workbook.Worksheets[worksheetName];
                if (worksheet != null)
                {
                    var cellValue = worksheet.Cells[row, column].Value;
                    if (cellValue != null)
                    {
                        return cellValue.ToString();
                    }
                }
            }
            return null;
        }

        public void SetCell<T>(string worksheetName, int row, int column, T value)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (package == null)
                {
                    throw new FileNotFoundException("Cannot write to an empty package.");
                }
                if (package.Workbook == null)
                {
                    throw new FileNotFoundException("No workbook was found to write to.");
                }
                if (!this.WorksheetExists(worksheetName))
                {
                    throw new FileNotFoundException("No worksheet with this name was found to write to.");
                }
                ExcelWorkbook excelWorkbook = package.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkbook.Worksheets[worksheetName];
                if (excelWorksheet.Dimension == null)
                {
                    throw new InvalidOperationException($"Worksheet \"{worksheetName}\" has empty dimensions.");
                }
                var cell = excelWorksheet.Cells[row, column];
                if (cell.Merge)
                {
                    var mergeId = excelWorksheet.MergedCells[row, column];                   
                    var mergedCell = excelWorksheet.Cells[mergeId];
                    mergedCell.Value = value;
                }
                else
                {
                    cell.Value = value;
                }
                package.SaveAsync();
            }
        }

        public List<String> GetColumn(string worksheetName, int column, int startrow = 1)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (!this.WorksheetExists(worksheetName))
                {
                    throw new FileNotFoundException($"No worksheet with this name \"{worksheetName}\" was found to get this column.");
                }

                ExcelWorkbook excelWorkbook = package.Workbook;
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[worksheetName];
                if (excelWorksheet.Dimension == null)
                {
                    throw new InvalidOperationException($"Worksheet \"{worksheetName}\" has empty dimensions.");
                }
                List<String> columnData = new List<String>();
                for (int row = startrow ; row <= excelWorksheet.Dimension.Rows; row++)
                {

                    var cell = excelWorksheet.Cells[row, column];
                    string? cellValue = null;
                    if (cell.Value != null)
                    cellValue = cell.Value.ToString();
                    if (cell.Merge)
                    {
                        var mergeId = excelWorksheet.MergedCells[row, column];
                        if (excelWorksheet.Cells[mergeId].First().Value != null)
                        {
                            cellValue = excelWorksheet.Cells[mergeId].First().Value.ToString();
                        }
                    }
                    Debug.WriteLine(row);
                    if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        columnData.Add(cellValue);
                    }
                }
                return columnData;
            }
        }

        public void SetColumn<T>(string worksheetName, int column, List<T> values, int startrow = 1)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (!this.WorksheetExists(worksheetName))
                {
                    throw new FileNotFoundException($"No worksheet with this name \"{worksheetName}\" was found to set this column.");
                }

                ExcelWorkbook excelWorkbook = package.Workbook;
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[worksheetName];
                if (excelWorksheet.Dimension == null)
                {
                    throw new InvalidOperationException($"Worksheet \"{worksheetName}\" has empty dimensions.");
                }
                List<String> columnData = new List<String>();
                for (int row = startrow; row <= values.Count; row++)
                {
                    var value = values[row - startrow];
                    Debug.WriteLine($"{row}\n{column}");
                    SetCell(worksheetName, row, column, value);
                }
            }
        }

        public List<String> GetRow(string worksheetName, int row)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (!this.WorksheetExists(worksheetName))
                {
                    throw new FileNotFoundException($"No worksheet with this name \"{worksheetName}\" was found to get this row.");
                }

                ExcelWorkbook excelWorkbook = package.Workbook;
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[worksheetName];
                if (excelWorksheet.Dimension == null)
                {
                    throw new InvalidOperationException($"Worksheet \"{worksheetName}\" has empty dimensions.");
                }
                List<String> rowData = new List<String>();
                for (int column = 1; column <= excelWorksheet.Dimension.Columns; column++)
                {
                    var cellValue = excelWorksheet.Cells[row, column].Text;
                    if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        rowData.Add(cellValue);
                    }
                }
                return rowData;
            }
        }

        public static List<string> GetWorksheetNames(string excelPath)
        {
            List<String> nameList = new List<String>();
            if (File.Exists(excelPath))
            {
                try
                {                    
                    using (ExcelPackage package = new ExcelPackage(excelPath))
                    {
                        ExcelWorkbook workbook = package.Workbook;
                        ExcelWorksheets worksheets = workbook.Worksheets;
                        foreach (var sheet in worksheets)
                        {
                            nameList.Add(sheet.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error getting worksheet names.\n Error message: {ex.Message}");
                }

            }
            return nameList;

        }

        public void SaveExcel(ExcelPackage package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("Package cannot be null");
            }
            try
            {
                package.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error while saving the excel file");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                package.Dispose();
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
