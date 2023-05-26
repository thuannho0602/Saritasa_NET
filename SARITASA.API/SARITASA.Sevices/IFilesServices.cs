using SARITASA.Model.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARITASA.Sevices
{
    public interface IFilesServices
    {
        // Used for class inheritance FileServices
        Task<List<FilesGetReponse>> GetAll();
        Task<FilesGetReponse> GetById(int id);
        Task<FilesCreateReponse> CreateFile(FilesCreateRequest filesCreateRequest);
        Task<FilesUpdateReponse> UpdateFile(int Id, FilesUpdateRequest filesUpdateRequest);
        Task<bool> DeleteFile(int id);
    }
}
