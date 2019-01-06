using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OrderNow.BLL.Services.ManageStorage
{
    public class ManageStorage : IManageStorage
    {
        public void UploadImage(string path, MemoryStream image, string id)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }

            Image img = Image.FromStream(image);
            var filePath = path + "\\" + id + ".png";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            var thumbPath = path + "\\" + id + "-thumbnail.png";
            if (File.Exists(thumbPath))
            {
                File.Delete(thumbPath);
            }

            var thumb = img.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            img.Save(filePath, ImageFormat.Png);
            thumb.Save(thumbPath, ImageFormat.Png);
        }
    }
}
