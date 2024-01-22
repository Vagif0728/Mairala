﻿using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mairala.Utilities.Extension
{
    public static class FileValidator
    {
        public static bool FileType(this IFormFile file , string type="image/")
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }

        public static bool FileSize( this IFormFile file ,int limitKb)
        {
            if (file.Length <= limitKb * 1024)
            {
                return true;
            }
            return false;

        }

        public static async Task<string> CreateFileAsync(this IFormFile file,string root,params string[] folders)
        {
            string fileName = Guid.NewGuid().ToString()+ file.FileName;
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path=Path.Combine(path, folders[i]);

            }
            path= Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        public static async void DeleteFileAsync( this string fileName,string root,params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path= Path.Combine(path, fileName);

            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
