using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Common.Application.SecurityUtil
{
   public static class  ImageValidator
    {
        //public static bool IsImage(this IFormFile? file)
        //{
        //    if (file == null) return false;
        //    try
        //    {
        //        var img = Image.FromStream(file.OpenReadStream());
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        public static bool IsImage(this IFormFile?  file)
        {
            try
            {
                // بررسی نال بودن فایل
                if (file == null || string.IsNullOrEmpty(file.FileName))
                {
                    return false;
                }

                // لیستی از پسوندهای معتبر برای تصاویر
                var validExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

                // گرفتن پسوند فایل
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                return validExtensions.Contains(fileExtension);
            }
            catch
            {
                return false;
            }
        }
    }
}
