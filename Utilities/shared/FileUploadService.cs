using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingUtilities.shared
{
    public static class FileUploadService
    {

        public static async Task<string?> UploadFileAsync(IFormFile? file, string folder = null!, string baseURL = null!)
        {

            if (file?.Length == 0 || file == null)
                return null;

            string wwwRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string _uploadDirectory = Path.Combine(wwwRoot, folder); 
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(_uploadDirectory, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string fileUrl = $"{baseURL}/{folder}/{uniqueFileName}";
            return fileUrl;
        }
    
    }
}
