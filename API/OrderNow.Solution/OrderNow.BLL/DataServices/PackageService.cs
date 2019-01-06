using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class PackageService : Service<Package>, IPackageService
    {
        public PackageService(IRepositoryAsync<Package> repository) : base(repository)
        {

        }
        public int GetRoomsCountByAdminId(long AdminId)
        {
            return _repository.Query(x => x.AdminId == AdminId).Select(x => x.MaxNumberOfRooms).Sum();
        }

        public List<Package> GetAllPackagesByAdminId(long adminId)
        {
            return _repository.Query(x => x.AdminId == adminId).Include(x => x.Admin).Select().ToList();
        }
        public int GetPackageById(long packageId)
        {
            return _repository.Query(x => x.PackageId == packageId).Select(x => x.MaxNumberOfRooms).Sum();
        }
    }

}
