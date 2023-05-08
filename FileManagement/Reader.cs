//Handles reading from a file
using System.Text;
using OfficeOpenXml;

namespace FileHandling{

    public class FileReader{

        public string ReadFile(string filepath){
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(filepath))){
                var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

                var sb = new StringBuilder(); //this is your data
                for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                {
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    sb.AppendLine(string.Join(",-=-,", row));
                }
                return sb.ToString();
            }
            
        }

        public string ReadFile(string filepath, int sheet_num){
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(filepath))){
                var myWorksheet = xlPackage.Workbook.Worksheets[sheet_num]; //select sheet here
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

                var sb = new StringBuilder(); //this is your data
                for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                {
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    sb.AppendLine(string.Join(",-=-,", row));
                }
                return sb.ToString();
            }
            
        }

        public string ReadFileByColumns(string filepath, int sheet_num){
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(filepath))){
                var myWorksheet = xlPackage.Workbook.Worksheets[sheet_num]; //select sheet here
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

                var sb = new StringBuilder(); //this is your data
                for (int column_num = 1; column_num <= totalColumns; column_num++) //select starting column here
                {
                    var column = myWorksheet.Cells[1, column_num, totalRows, column_num].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    sb.AppendLine(string.Join(",-=-,", column));
                }
                return sb.ToString();
            }
            
        }
    }
}

