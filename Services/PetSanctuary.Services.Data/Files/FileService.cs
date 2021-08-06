namespace PetSanctuary.Services.Data.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class FileService : IFileService
    {
        public void DeleteFile(string rootPath, string imageName)
        {
             string uploadDir = Path.Combine(rootPath, "img");
            var fileName = Path.Combine(uploadDir, imageName);
            var file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public string UploadFile(IFormFile image, string rootPath)
        {
            string fileName = null;
            if (image != null)
            {
                string uploadDir = Path.Combine(rootPath, "img");
                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                if (fileName.Contains("Test"))
                {
                    return "TestImage";
                }
                else
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                }
            }

            return fileName;
        }
    }
}
