﻿namespace UniversityManagementSystemPortal.PictureManager
{
    public interface IPictureManager

    {
        Task<byte[]> GetPicture(string fileName);
        Task<string> Upload(IFormFile formFile);
        Task<string> Update(Guid id, IFormFile formFile, string currentFilePath = null);
        void Delete(string filePath);
        void ClearCache(string filePath);
    }
}