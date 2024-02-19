using ETicaretApi.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            File.Delete($"{pathOrContainerName}\\{fileName}");
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            DirectoryInfo directory = new(pathOrContainerName);
            return directory.GetFiles().Select(x => x.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
            => File.Exists($"{pathOrContainerName}\\{fileName}");

        public async Task<List<(string fileName, string path)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            foreach (var item in files)
            {

                await CopyFileAsync($"{uploadPath}\\{item.Name}", item);
                datas.Add((item.Name, $"{uploadPath}\\{item.Name}"));
            }


            return datas;
        }


        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                //todo : log
                throw ex;
            }
        }
    }
}
