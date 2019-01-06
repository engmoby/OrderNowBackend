using System.IO;

namespace OrderNow.BLL.Services.ManageStorage
{
    public interface IManageStorage
    {
        void UploadImage(string path, MemoryStream image, string id);
    }
}
