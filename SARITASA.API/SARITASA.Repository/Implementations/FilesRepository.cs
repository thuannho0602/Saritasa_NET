using SARITASA.DataAccess;
using SARITASA.Entity.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARITASA.Repository.Implementations
{
    public class FilesRepository:RepositoryBase<Files,ApplicationDbContext>,IFilesRepository
    {
        public FilesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
