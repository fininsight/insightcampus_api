using System;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

namespace insightcampus_api.Dao
{
    public class CSVFileRepository : CSVFileInterface
    {

        public CSVFileRepository()
        {
        }

        public void ReturnData(IXLWorksheet worksheet)
        {
            worksheet.Range("A1:E1").Merge().Value = "제목";
            var columnNames = GetColumnNames();

            for (int index = 1; index <= 5; index++)
            {
                worksheet.Cell(2, index).Value = columnNames[index - 1];
                worksheet.Cell(3, index).Value = index;
            }

        }

        private string[] GetColumnNames()
        {
            return new[] {
                "내용1",
                "내용2",
                "내용3",
                "내용4",
                "내용5"
            };
        }
    }
}
