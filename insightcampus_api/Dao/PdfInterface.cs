using System;
using System.Threading.Tasks;
using insightcampus_api.Model;

namespace insightcampus_api.Dao
{
    public interface PdfInterface
    {
        Task<IncamAddfareModel> Select(int addfare_seq);
        Task<ClassStudentModel> SelectStudent(int class_seq, int order_user_seq);
    }
}
