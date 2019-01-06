using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface ICategoryFacade
    {
        void AddCategory(CategoryDTO categoryDto, string path);
        CategoryDTO GetCategory(long categoryId); 
        void ActivateCategory(long categoryId);
        void DeActivateCategory(long categoryId);
        void DeleteCategory(long categoryId);
        void UpdateCategory(CategoryDTO categoryDto, string path); 
    }
}
