using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Service.Contract
{
    public interface IFileStorageService
    {
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> EditFile(string containerName, IFormFile file, string fileRoute);
        Task<string> SaveFile(string containerName, IFormFile file);
    }
}
