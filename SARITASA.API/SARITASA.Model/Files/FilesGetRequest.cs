using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARITASA.Model.Files
{
    public class FilesGetRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LinkFile { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
    }
}
