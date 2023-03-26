using Serilog;
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
            var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolderPath, fileName.Trim());

            if (!File.Exists(filePath))
            {
                _logger.LogError("File {FileName} does not exist at path: {FilePath}", fileName, filePath);
                throw new FileNotFoundException($"File {fileName} does not exist.");
            }

            var pictureBytes = await File.ReadAllBytesAsync(filePath);
            return pictureBytes;
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

            var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

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

            _logger.LogInformation("Uploaded file {FileName} to path: {FilePath}", fileName, filePath);

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

            var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (!string.IsNullOrEmpty(currentFilePath))
            {
                var currentFilePathFull = Path.Combine(_webHostEnvironment.WebRootPath, currentFilePath);

                if (File.Exists(currentFilePathFull))
                {
                    File.Delete(currentFilePathFull);
                    _logger.LogInformation("Deleted file {CurrentFilePath}", currentFilePathFull);
                }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            _logger.LogInformation("Updated file {FileName} to path: {FilePath}", fileName, filePath);

            return fileName;
        }

        public void Delete(string fileName)
        {
            var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.LogInformation("Deleted file {FilePath}", filePath);
            }
        }
        public void ClearCache(string fileName)
        {
            var cacheFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "cache");
            var cacheFilePath = Path.Combine(cacheFolderPath, fileName);

            if (File.Exists(cacheFilePath))
            {
                File.Delete(cacheFilePath);
            }
        }
    }

}


