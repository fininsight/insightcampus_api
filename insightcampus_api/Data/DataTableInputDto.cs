using System;
using System.Collections;

namespace insightcampus_api.Data
{
    public class DataTableInputDto
    {
        public int pageNumber { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int size { get; set; }
        public IEnumerable data { get; set; }

  
        public string codegroup_id { get; set; }
        public int class_seq { get; set; }
    }
}
