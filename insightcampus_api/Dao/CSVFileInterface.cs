using System;
using System.Threading.Tasks;
using insightcampus_api.Data;
using ClosedXML.Excel;

namespace insightcampus_api.Dao
{
    public interface CSVFileInterface
    {
        void ReturnData(IXLWorksheet worksheet);
        //string[] GetColumnNames();

    }
}
