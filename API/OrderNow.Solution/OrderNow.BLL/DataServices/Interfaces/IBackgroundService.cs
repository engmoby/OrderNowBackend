using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IBackgroundService : IService<Background>
    {
        PagedResultsDto GetAllBackgrounds(int page, int pageSize, long userId); 
        PagedResultsDto GetActivatedBackgroundByUserId(long userId, int page, int pageSize);
    }
}
