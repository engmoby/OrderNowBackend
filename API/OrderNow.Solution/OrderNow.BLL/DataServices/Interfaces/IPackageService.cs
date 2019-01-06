using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IPackageService : IService<Package>
    {
        int GetRoomsCountByAdminId(long AdminId);
        List<Package> GetAllPackagesByAdminId(long AdminId);
        int GetPackageById(long packageId);
    }
}
