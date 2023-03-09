using AutoMapper;
using System.Diagnostics;
using System.Security.Principal;
using UniversityManagementSystemPortal.Authorization;

namespace UniversityManagementSystemPortal.PictureManager
{

    public class PictureManager : IPictureManager
    {
        private readonly ILogger<PictureManager> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const int MaxFileSizeMB = 10;

        public PictureManager(ILogger<PictureManager> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<byte[]> GetPicture(string fileName)
        {
            try
            {
                var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");
                var filePath = Path.Combine(uploadsFolderPath, fileName.Trim());

                if (!File.Exists(filePath))
                {
                    _logger.LogError($"File {fileName} does not exist at path: {uploadsFolderPath}");
                    throw new FileNotFoundException($"File {fileName} does not exist.");
                }

                var pictureBytes = await File.ReadAllBytesAsync(filePath);
                return pictureBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting picture with filename {fileName}");
                throw;
            }
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

            var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
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

            var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (!string.IsNullOrEmpty(currentFilePath))
            {
                var currentFilePathFull = Path.Combine(_webHostEnvironment.ContentRootPath, currentFilePath);

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
            var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public void ClearCache(string fileName)
        {
            var cacheFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "cache");
            var cacheFilePath = Path.Combine(cacheFolderPath, fileName);

            if (File.Exists(cacheFilePath))
            {
                File.Delete(cacheFilePath);
            }
        }
}   }  


