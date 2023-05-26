using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SARITASA.Model.Files;
using SARITASA.Sevices;

namespace SARITASA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IFilesServices _filesServices;
        public FilesController(IFilesServices filesServices)
        {
            _filesServices = filesServices;
        }
        // method HTTP GetALL Files
        [HttpGet]
        public async Task<IEnumerable<FilesGetReponse>> GetAll()
        {
            return await _filesServices.GetAll();
        }

        //method HTTP GetById{Id} Files
        [HttpGet("{Id}")]
        public async Task<FilesGetReponse> GetById(int Id)
        {
            return await _filesServices.GetById(Id);
        }

        //method HTTP Potst Create File
        [HttpPost]
        public async Task<FilesCreateReponse> CreateFiles(FilesCreateRequest filesCreateRequest)
        {
            return await _filesServices.CreateFile(filesCreateRequest);
        }

        //method HTTP Put {ID}  UPdate file {Id}
        [HttpPut("{Id}")]
        public async Task<FilesUpdateReponse> UpdateFiles(int Id, FilesUpdateRequest filesUpdateRequest)
        {
            return await _filesServices.UpdateFile(Id, filesUpdateRequest);
        }

        //method HTTP Delete {ID}  Delete file
        [HttpDelete("{Id}")]
        public async Task<bool> DeleteFiles(int Id)
        {
            return await _filesServices.DeleteFile(Id);
        }
    }
}
