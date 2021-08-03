namespace PetSanctuary.Services.Data.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        string UploadFile(IFormFile image, string rootPath);

        void DeleteFile(string rootPath, string imageName);
    }
}
