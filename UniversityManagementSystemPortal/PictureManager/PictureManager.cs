using System.Diagnostics;
using UniversityManagementSystemPortal.Authorization;

namespace UniversityManagementSystemPortal.PictureManager
{
  
        public class PictureManager : IPictureManager
        {
            private readonly IWebHostEnvironment _webHostEnvironment;
            private const int MaxFileSizeMB = 10;

            public PictureManager(IWebHostEnvironment webHostEnvironment)
            {
                _webHostEnvironment = webHostEnvironment;
            }

        public async Task<string> Upload(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                throw new AppException("File is empty or not provided.");
            }

            if (formFile.Length > MaxFileSizeMB * 1024 * 1024)
            {
                throw new AppException($"File size exceeds the maximum allowed size of {MaxFileSizeMB} MB.");
            }

            var webRootPath = _webHostEnvironment.WebRootPath;
            var contentRootPath = _webHostEnvironment.ContentRootPath;
            var uploadsFolderPath = Path.Combine(webRootPath ?? contentRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            if (string.IsNullOrEmpty(webRootPath) && string.IsNullOrEmpty(contentRootPath))
            {
                throw new AppException("Web root path and content root path are not set correctly.");
            }

            var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            fileName = $"{fileName}_{DateTime.UtcNow.Ticks}{Path.GetExtension(formFile.FileName)}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return fileName;
        }




        public async Task<string> Update(Guid id, IFormFile formFile, string currentFilePath = null)
        {
            if (formFile == null || formFile.Length == 0)
            {
                throw new AppException("File is empty or not provided.");
            }

            if (formFile.Length > MaxFileSizeMB * 1024 * 1024)
            {
                throw new AppException($"File size exceeds the maximum allowed size of {MaxFileSizeMB} MB.");
            }

            var webRootPath = _webHostEnvironment.WebRootPath;
            var uploadsFolderPath = Path.Combine(webRootPath, "uploads");
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (!string.IsNullOrEmpty(currentFilePath))
            {
                var currentFilePathFull = Path.Combine(webRootPath, currentFilePath);

                if (File.Exists(currentFilePathFull))
                {
                    File.Delete(currentFilePathFull);
                }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return fileName;
        }

        public void Delete(string fileName)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var uploadsFolderPath = Path.Combine(webRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void ClearCache(string fileName)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var cacheFolderPath = Path.Combine(webRootPath, "cache");
            var cacheFilePath = Path.Combine(cacheFolderPath, fileName);

            if (File.Exists(cacheFilePath))
            {
                File.Delete(cacheFilePath);
            }
        }

    }
}


