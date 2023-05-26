using SARITASA.DataAccess;
using SARITASA.Entity.Files;
using SARITASA.Model.Files;
using SARITASA.Repository;
using SARITASA.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SARITASA.Sevices.Implementations
{
    public class FilesServices : IFilesServices
    {
        private IFilesRepository _filesRepository;
        private ApplicationDbContext _appContext;
        public FilesServices(IFilesRepository filesRepository, ApplicationDbContext appContext)
        {
            _filesRepository = filesRepository;
            _appContext = appContext;
        }

        //Create New Item In File
        public async Task<FilesCreateReponse> CreateFile(FilesCreateRequest filesCreateRequest)
        {
            if (filesCreateRequest.ID == 0)
            {
                var files = new Files
                {
                    Name = filesCreateRequest.Name,
                    LinkFile = filesCreateRequest.LinkFile,
                    CreateBy = filesCreateRequest.CreateBy,
                    CreateDate = filesCreateRequest.CreateDate,
                    UpdatedBy = filesCreateRequest.UpdatedBy,
                    UpdateDate = filesCreateRequest.UpdateDate,
                };
                _filesRepository.Add(files);
                _filesRepository.SaveChanges();

                var fileRenponse = new FilesCreateReponse
                {
                    Name = files.Name,
                    LinkFile = files.LinkFile,
                    CreateBy = files.CreateBy,
                    CreateDate = files.CreateDate,
                    UpdatedBy = files.UpdatedBy,
                    UpdateDate = files.UpdateDate,
                };
                return await Task.FromResult(fileRenponse);

            }
            else
            {
                return new FilesCreateReponse();
            }
        }

        // Used to delete File ID
        public async Task<bool> DeleteFile(int id)
        {
            var file = _filesRepository.FindByCondition(c => c.ID == id).FirstOrDefault();
            if (file != null)
            {
                _filesRepository.Remove(file);
                _filesRepository.SaveChanges();
            }
            return await Task.FromResult(true);
        }

        // Get All Item In File
        public async Task<List<FilesGetReponse>> GetAll()
        {
            var listfile = _filesRepository.FindAll().Select(c => new FilesGetReponse
            {
                ID = c.ID,
                Name = c.Name,
                LinkFile = c.LinkFile,
                CreateBy = c.CreateBy,
                CreateDate = c.CreateDate,
                UpdatedBy = c.UpdatedBy,
                UpdateDate = c.UpdateDate,
                Status = c.Status,
            }).ToList();
            return await Task.FromResult(listfile);
        }

        //Get the Id in the File's Item
        public async Task<FilesGetReponse> GetById(int id)
        {
            var file = _filesRepository.FindByCondition(c => c.ID == id).Select(c => new FilesGetReponse
            {
                ID = c.ID,
                Name = c.Name,
                LinkFile = c.LinkFile,
                CreateBy = c.CreateBy,
                CreateDate = c.CreateDate,
                UpdatedBy = c.UpdatedBy,
                UpdateDate = c.UpdateDate,
                Status = c.Status,
            }).FirstOrDefault();
            return await Task.FromResult(file);

        }

        //Update Id By File
        public async Task<FilesUpdateReponse> UpdateFile(int Id, FilesUpdateRequest filesUpdateRequest)
        {
            if (Id > 0)
            {
                var file = _filesRepository.FindByCondition(c => c.ID == Id).FirstOrDefault();
                if (file != null)
                {
                    var files = new Files
                    {
                        ID = Id,
                        Name = filesUpdateRequest.Name,
                        LinkFile = filesUpdateRequest.LinkFile,
                        CreateBy = filesUpdateRequest.CreateBy,
                        CreateDate = filesUpdateRequest.CreateDate,
                        UpdatedBy = filesUpdateRequest.UpdatedBy,
                        UpdateDate = filesUpdateRequest.UpdateDate,
                        Status = filesUpdateRequest.Status,
                    };
                    _filesRepository.Update(files);
                    _filesRepository.SaveChanges();
                    var filesReponese = new FilesUpdateReponse
                    {
                        ID = Id,
                        Name = files.Name,
                        LinkFile = files.LinkFile,
                        CreateBy = files.CreateBy,
                        CreateDate = files.CreateDate,
                        UpdatedBy = files.UpdatedBy,
                        UpdateDate = files.UpdateDate,
                        Status = files.Status,
                    };
                    return await Task.FromResult(filesReponese);

                }
                return new FilesUpdateReponse();
            }
            else
            {
                return new FilesUpdateReponse();
            }
        }
    }
}
