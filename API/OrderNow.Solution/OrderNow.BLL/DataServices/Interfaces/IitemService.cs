using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IItemService : IService<Item>
    {
        PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize);
        List<Item> GetAllItemsByRestuantId(long restuantId);
        PagedResultsDto GetlatestItemsByRestuantId(long restuantId, int page, int pageSize);
    }
}
