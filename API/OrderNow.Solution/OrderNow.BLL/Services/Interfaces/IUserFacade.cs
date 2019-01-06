using OrderNow.BLL.DTOs;
using OrderNow.Common;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface IUserFacade
    {
        UserDto ValidateUser(string email, string password);

        UserDto GetUser(long userId);
        PagedResultsDto GetAllUsers(long adminId, int page, int pageSize,Enums.RoleType role);
      
        UserConsumed GetMaxAndConsumedUsers(long userId);
        void AddNewGlobalUser(AdminDto adminDto);
        void UpdateGlobalUser(AdminDto adminDto);
        void UpdateAdminPackage(AdminDto adminDto);
        void AddRestaurantWaiter(RestaurantWaiterDto restaurantWaiterDto, long restaurantAdminId);
        RestaurantWaiterDto GetRestaurantWaiter(long waiterId);
        PagedResultsDto GetAllRestaurantWaiters(long restaurantAdminId, int page, int pageSize, string language);
        void UpdateRestaurantWaiter(RestaurantWaiterDto restaurantWaiterDto);
        void DeleteRestaurantWaiter(long restaurantWaiterId);

        SupervisorDto GetSupervisor(long supervisorId);
        void AddSupervisor(SupervisorDto supervisorDto, long adminId);
        void UpdateSupervisor(SupervisorDto supervisorDto, long adminId);
        void ActivateSupervisor(long supervisorId, long adminId);
        void DeActivateSupervisor(long supervisorId, long adminId);
        void DeleteSupervisor(long supervisorId, long adminId);


        ClientDto AddClient(ClientDto clientDto);

    }
}
