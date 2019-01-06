using System.Collections.Generic;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ISupervisorFeatureService:IService<SupervisorFeature>
    {
        void DeleteRange(List<SupervisorFeature> features);
    }
}
