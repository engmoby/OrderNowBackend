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
    public interface ICategoryService:IService<Category>
    {
       PagedResultsDto GetAllCategories(string language,  int page, int pageSize);
        PagedResultsDto GetAllCategoriesByTable(long tableId);

    }
}
