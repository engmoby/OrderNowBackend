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
    public class SideItemService:Service<SideItem>,ISideItemService
    {
        public SideItemService(IRepositoryAsync<SideItem> repository):base(repository)
        {
            
        }
    }
}
