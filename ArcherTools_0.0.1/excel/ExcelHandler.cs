using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.methods.strings;
using OfficeOpenXml;
using System.Collections.Concurrent;
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
                throw new FileNotFoundException($"The Excel file does not exist in the specified path: {filePath} ");
            }
            this._filePath = filePath;
            FileInfo fileInfo = new FileInfo(_filePath);
        }

        public ExcelHandler() { }

        internal Dictionary<string,Dictionary<string ,int>> SearchWorksheetFor<T>(string worksheetName, ConcurrentBag<T> itemsToSearch)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                Dictionary<string,Dictionary<string, int>> locationsOfItems = new Dictionary<string, Dictionary<string, int>>();
                if (WorksheetExists(worksheetName))
                {
                    Debug.WriteLine($"worksheet ({worksheetName}) to search for exists");
                    var workbook = package.Workbook;
                    if (workbook != null)
                    {
                        var worksheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == worksheetName);
                        if (worksheet != null)
                        {
                            int totalColumns = worksheet.Dimension?.Columns ?? 0;
                            int totalRows = worksheet.Dimension?.Rows ?? 0;
                            for (int col = 1; col <= totalColumns; col++)
                            {
                                for (int row = 1; row <= totalRows; row++)
                                {
                                    var cell = worksheet.Cells[row,col];
                                    /*Parallel.ForEach(itemsToSearch, item =>
                                    {
                                        Debug.WriteLine(StringMatching.CosineSimilarity(item?.ToString(), cell.Text));
                                        if (StringMatching.CosineSimilarity(item?.ToString(), cell.Text) > 70)
                                        {
                                            lock(locationsOfItems)
                                            {
                                                Debug.WriteLine("found item");
                                                locationsOfItems.Add(item.ToString(), new Dictionary<string, int> { { "row", row }, { "column", col } });
                                            }
                                        }
                                    });*/
                                    foreach (var item in itemsToSearch)
                                    {
                                        var matching = StringMatching.CosineSimilarity(item?.ToString(), cell.Text);
                                        if (matching > 0)
                                        {
                                            Debug.WriteLine(matching);
                                        }
                                        if (StringMatching.CosineSimilarity(item?.ToString(), cell.Text) > 40)
                                        {
                                            Debug.WriteLine($"found item: {item}");
                                            if (!locationsOfItems.TryAdd(item.ToString(), new Dictionary<string, int> { { "row", row }, { "column", col } }))
                                            {
                                                locationsOfItems.Remove(item.ToString());
                                                locationsOfItems.TryAdd(item.ToString(), new Dictionary<string, int> { { "row", row }, { "column", col } });
                                            }
                                            
                                        }
                                    }
                                }
                            }
                            return locationsOfItems;
                        }
                    }
                }
                return locationsOfItems;
            }
        }

        internal Dictionary<string, Dictionary<string, int>> SearchWorksheetFor(string worksheetName, ConcurrentDictionary<string, int> itemsToSearch)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                Dictionary<string, Dictionary<string, int>> locationsOfItems = new Dictionary<string, Dictionary<string, int>>();
                if (WorksheetExists(worksheetName))
                {
                    Debug.WriteLine($"worksheet ({worksheetName}) to search for exists");
                    var workbook = package.Workbook;
                    if (workbook != null)
                    {
                        var worksheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == worksheetName);
                        if (worksheet != null)
                        {
                            int totalColumns = worksheet.Dimension?.Columns ?? 0;
                            int totalRows = worksheet.Dimension?.Rows ?? 0;
                            for (int col = 1; col <= totalColumns; col++)
                            {
                                for (int row = 1; row <= totalRows; row++)
                                {
                                    var cell = worksheet.Cells[row, col];
                                    /*Parallel.ForEach(itemsToSearch, item =>
                                    {
                                        Debug.WriteLine(StringMatching.CosineSimilarity(item?.ToString(), cell.Text));
                                        if (StringMatching.CosineSimilarity(item?.ToString(), cell.Text) > 70)
                                        {
                                            lock(locationsOfItems)
                                            {
                                                Debug.WriteLine("found item");
                                                locationsOfItems.Add(item.ToString(), new Dictionary<string, int> { { "row", row }, { "column", col } });
                                            }
                                        }
                                    });*/
                                    foreach (var item in itemsToSearch)
                                    {
                                        var matching = StringMatching.CosineSimilarity(item.Key.ToString(), cell.Text);
                                        if (matching > 0)
                                        {
                                            Debug.WriteLine(matching);
                                        }
                                        if (StringMatching.CosineSimilarity(item.Key.ToString(), cell.Text) > item.Value)
                                        {
                                            Debug.WriteLine($"found item: {item}");
                                            if (!locationsOfItems.TryAdd(item.Key.ToString(), new Dictionary<string, int> { { "row", row }, { "column", col } }))
                                            {
                                                
                                            }

                                        }
                                    }
                                }
                            }
                            return locationsOfItems;
                        }
                    }
                }
                return locationsOfItems;
            }
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

        internal List<ExcelWorksheet> GetWorksheets()
        {
            using (var package = new ExcelPackage(new FileInfo(_filePath)))
            {                    
                return package.Workbook.Worksheets.ToList();
            }
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
                    worksheet.Cells[row, column].Calculate();
                    var cellValue = worksheet.Cells[row, column].Value;
                    if (worksheet.Cells[row, column].Merge)
                    {
                        var mergeId = worksheet.MergedCells[row, column];
                        if (worksheet.Cells[mergeId].First().Value != null)
                        {
                            worksheet.Cells[mergeId].First().Calculate();
                            cellValue = worksheet.Cells[mergeId].First().Value.ToString();
                        }

                    }

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
                if (excelWorksheet == null)
                {
                    throw new FileNotFoundException($"Worksheet \"{worksheetName}\" does not exist.\nInfo: worksheetName: {worksheetName}, row: {row}, column: {column}, value: {value}");
                }
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
                package.Save();
                package.Dispose();

            }
        }

        public int GetLastFilledRow(string worksheetName, int column, int startrow = 1)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (!this.WorksheetExists(worksheetName))
                {
                    throw new FileNotFoundException($"No worksheet with this name \"{worksheetName}\" was found to get this column.");
                }

                ExcelWorkbook excelWorkbook = package.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkbook.Worksheets[worksheetName];
                if (excelWorksheet == null)
                {
                    throw new InvalidOperationException($"Worksheet \"{worksheetName}\" has empty dimensions.");
                }
                ConcurrentBag<int> lastRows = new ConcurrentBag<int>();
                Parallel.For(startrow, excelWorksheet.Dimension.Rows, row =>
                {
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
                        if (!string.IsNullOrWhiteSpace(cellValue))
                        {
                          lastRows.Add(row);
                        }
                    }
                });
                var lastFilledRow = 0;
                if (lastRows.Any())
                {
                    lastFilledRow = lastRows.Max();
                }
                Debug.WriteLine($"This is the last filled row found: {lastFilledRow}");
                return lastFilledRow;
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
                Parallel.For(startrow, excelWorksheet.Dimension.Rows, row =>
                {
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
                        if (!string.IsNullOrWhiteSpace(cellValue))
                        {
                            lock (columnData)
                            {
                                columnData.Add(cellValue);
                            }
                        }
                    }
                });
                Debug.WriteLine(columnData.Count);
                return columnData;
            }
        }

        public void SetColumn<T>(string worksheetName, int column, List<T> values, int startrow = 1, bool nullAll = false, int nullUpToRow = 1)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                if (!this.WorksheetExists(worksheetName))
                {
                    throw new FileNotFoundException($"No worksheet with this name \"{worksheetName}\" was found to set this column.");
                }

                ExcelWorkbook excelWorkbook = package.Workbook;
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[worksheetName];
                if (excelWorksheet == null)
                {
                    throw new FileNotFoundException($"Worksheet \"{worksheetName}\" is null");
                }
                if (excelWorksheet.Dimension == null)
                {
                    throw new InvalidOperationException($"Worksheet \"{worksheetName}\" has empty dimensions.");
                }
                List<String> columnData = new List<String>();
                if (!nullAll)
                {

                    excelWorksheet.Cells[startrow, column].LoadFromCollection(values);
                    package.Save();

                }
                else
                {
                    for (int row = startrow; row < nullUpToRow + startrow; row++)
                    {
                        SetCell(worksheetName, row, column, " ");
                        Thread.Sleep(5);
                    }
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
