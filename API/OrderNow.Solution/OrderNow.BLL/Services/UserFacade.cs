using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;
using OrderNow.Common.CustomException;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace OrderNow.BLL.Services
{
    public class UserFacade:BaseFacade,IUserFacade
    {
        private IUserService _UserService;
        private IClientService _clientService; 
        private ISupervisorService _supervisorService;
        private ISupervisorFeatureService _supervisorFeatureService;
        private IAdminService _adminService;
        private IPackageService _packageService;
        private IRestaurantService _restaurantService;
        private IRestaurantWaiterService _restaurantWaiterService; 
        public UserFacade(IUnitOfWorkAsync unitOFWork , IUserService userService,   IAdminService adminService, IPackageService packageService, IRestaurantService restaurantService, IRestaurantWaiterService restaurantWaiterService, ISupervisorService supervisorService, ISupervisorFeatureService supervisorFeatureService, IClientService clientService) : base(unitOFWork)
        {
            _UserService = userService; 
            _adminService = adminService;
            _packageService = packageService;
            _restaurantService = restaurantService;
            _restaurantWaiterService = restaurantWaiterService;
            _supervisorService = supervisorService;
            _supervisorFeatureService = supervisorFeatureService;
            _clientService = clientService;
        }

        public UserDto ValidateUser(string email, string password)
        {
            string encryptedPassword = PasswordHelper.Encrypt(password);
            var user = Mapper.Map<UserDto>(_UserService.ValidateUser(email, encryptedPassword)) ?? Mapper.Map<UserDto>(_UserService.CheckUserIsDeleted(email, encryptedPassword));
            //var user = Mapper.Map<UserDto>(_UserService.ValidateUser(email, encryptedPassword));
            if (user == null) throw new ValidationException(ErrorCodes.UserNotFound);

            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            //if (!user.IsActive)
            //    throw new ValidationException(ErrorCodes.UserDeactivated);
            if (user.Role == Enums.RoleType.User)
            {
                //var room = _roomService.Find(user.UserId);
                //if (DateTime.Now.Date > room.Package.End.Date) throw new ValidationException(ErrorCodes.PackageExpired);
                //if (DateTime.Now.Date < room.Package.Start.Date) throw new ValidationException(ErrorCodes.PackageNotActivated);
            }
            return user;

        }
        public UserDto GetUser(long UserId)
        {
            return Mapper.Map<UserDto>(_UserService.Find(UserId));
        }

        public PagedResultsDto GetAllUsers(long adminId, int page, int pageSize,Enums.RoleType role)
        {
            var user = _UserService.Find(adminId);
            if(user == null) throw new ValidationException(ErrorCodes.UserNotFound);
            switch (role)
            {
                case Enums.RoleType.Supervisor:
                    return GetAllSupervisor(user.RestaurantId, page, pageSize);
            }
            switch (role)
            {
                case Enums.RoleType.User:
                    return GetAllSupervisor(user.RestaurantId, page, pageSize);
            }
            return null;
        }

        #region Manage Supervisor
        private PagedResultsDto GetAllSupervisor(long restaurantId, int page, int pageSize)
        {
            var supervisorCount = _supervisorService.Query(x => !x.IsDeleted && x.RestaurantId == restaurantId).Select().Count();
            var supervisors = Mapper.Map<List<SupervisorDto>>(_supervisorService.GetAllSupervisors(restaurantId, page, pageSize));
            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = supervisorCount,
                Data = supervisors
            };
            return results;
        }

        public SupervisorDto GetSupervisor(long supervisorId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            if (supervisor.IsDeleted) throw new NotFoundException(ErrorCodes.UserNotFound);
            return Mapper.Map<SupervisorDto>(supervisor);

        }
        public void AddSupervisor(SupervisorDto supervisorDto, long adminId)
        {
            ValidateSupervisor(supervisorDto, adminId);
            var user = _UserService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            var restaurant = _restaurantService.GetRestaurantByAdminId(adminId);
            Supervisor supervisor = Mapper.Map<Supervisor>(supervisorDto);
          //  supervisor.AdminId = adminId;
            supervisor.Password = PasswordHelper.Encrypt(supervisorDto.Password);
            supervisor.Role = Enums.RoleType.Supervisor;
            supervisor.RestaurantId = restaurant.RestaurantId;
            supervisor.IsActive = true;

            //foreach (var feature in supervisorDto.Restaurant)
            //{
            //    supervisor.SupervisorFeatures.Add(new SupervisorFeature
            //    {
            //        RestaurantId = feature.RestaurantId
            //    });
            //}

            //_supervisorFeatureService.InsertRange(supervisor.SupervisorFeatures);
            _supervisorService.Insert(supervisor);
            SaveChanges();

        }
        public void UpdateSupervisor(SupervisorDto supervisorDto, long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorDto.UserId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            ValidateSupervisor(supervisorDto, adminId);
            supervisor.UserName = supervisorDto.UserName;
            supervisor.Name = supervisorDto.Name;
            supervisor.Password = PasswordHelper.Encrypt(supervisorDto.Password);

            //SupervisorFeature[] features = new SupervisorFeature[supervisor.SupervisorFeatures.Count];
            //supervisor.SupervisorFeatures.CopyTo(features, 0);
            //_supervisorFeatureService.DeleteRange(features.ToList());

            //foreach (var feature in supervisorDto.Restaurant)
            //{
            //    supervisor.SupervisorFeatures.Add(new SupervisorFeature
            //    {
            //        RestaurantId = feature.RestaurantTypeId
            //    });
            //}
            _supervisorService.Update(supervisor);
            SaveChanges();
        }
        public void ActivateSupervisor(long supervisorId, long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            supervisor.IsActive = true;
            _supervisorService.Update(supervisor);
            SaveChanges();
        }

        public void DeActivateSupervisor(long supervisorId, long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            supervisor.IsActive = false;
            _supervisorService.Update(supervisor);
            SaveChanges();
        }
        public void DeleteSupervisor(long supervisorId, long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            supervisor.IsDeleted = true;
            supervisor.IsActive = false;
            _supervisorService.Update(supervisor);
            SaveChanges();
        }


        private void ValidateSupervisor(SupervisorDto supervisorDto, long adminId)
        {
            if (string.IsNullOrEmpty(supervisorDto.UserName)) throw new ValidationException(ErrorCodes.EmptyUserName);
            if (supervisorDto.UserName.Length > 100) throw new ValidationException(ErrorCodes.NameExceedLength);
            if (string.IsNullOrEmpty(supervisorDto.Password)) throw new ValidationException(ErrorCodes.EmptyPassword);
            if (supervisorDto.Password.Length < 8 || supervisorDto.Password.Length > 25) throw new ValidationException(ErrorCodes.PasswordLengthNotMatched);

            if (_supervisorService.CheckUserNameDuplicated(supervisorDto.UserName, supervisorDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
        }

        #endregion

        #region integration with subscription module
        public void AddNewGlobalUser(AdminDto adminDto)
        {
            var admin = _adminService.GetAdminByAccountId(adminDto.UserAccountId);
            if (admin == null)
            {
                Admin newAdmin = new Admin();
                newAdmin.UserName = adminDto.UserName;
                newAdmin.UserAccountId = adminDto.UserAccountId;
                newAdmin.Role = Enums.RoleType.Admin;
                newAdmin.Password = adminDto.Password;
                newAdmin.IsActive = adminDto.IsActive;
              //  newAdmin.FeaturesBackgroundId = Strings.FeaturesBackgroundId;
                //newAdmin.ProductId = adminDto.ProductId;
                _adminService.Insert(newAdmin);
                admin = newAdmin;
            }
            else
            {
                admin.UserName = adminDto.UserName;
                admin.Password = adminDto.Password;
                admin.IsActive = adminDto.IsActive;
                //admin.ProductId = adminDto.ProductId;

                _adminService.Update(admin);
            }
            SaveChanges();
            var package = admin.Packages.FirstOrDefault(x => x.PackageGuid == adminDto.PackageGuid);
            if (package == null)
            {
                admin.Packages.Add(new Package
                {
                    End = adminDto.End,
                    Start = adminDto.Start,
                    MaxNumberOfRooms = adminDto.Limit,
                    PackageGuid = adminDto.PackageGuid,
                    AdminId = admin.UserId
                });
                _packageService.InsertRange(admin.Packages);
            }
            else
            {
                package.End = adminDto.End;
                package.Start = adminDto.Start;
                _packageService.Update(package);
            }

            _adminService.Update(admin);
            SaveChanges();
        }

        public void UpdateGlobalUser(AdminDto adminDto)
        {
            var admin = _adminService.GetAdminByAccountId(adminDto.UserAccountId);
            admin.UserName = adminDto.UserName;
            admin.Password = adminDto.Password;
            admin.IsActive = adminDto.IsActive;

            _adminService.Update(admin);
            SaveChanges();
        }

        public void UpdateAdminPackage(AdminDto adminDto)
        {
            var admin = _adminService.GetAdminByAccountId(adminDto.UserAccountId);
            var package = admin.Packages.FirstOrDefault(x => x.PackageGuid == adminDto.PackageGuid);
            if (package == null)
            {
                admin.Packages.Add(new Package
                {
                    End = adminDto.End,
                    Start = adminDto.Start,
                    MaxNumberOfRooms= adminDto.Limit,
                    PackageGuid = adminDto.PackageGuid,
                    AdminId = admin.UserId
                });
                _packageService.InsertRange(admin.Packages);
            }
            else
            {
                package.End = adminDto.End;
                package.Start = adminDto.Start;
                _packageService.Update(package);
            }
            //admin.ProductId = adminDto.ProductId;

            _adminService.Update(admin);
            SaveChanges();
        }


        public UserConsumed GetMaxAndConsumedUsers(long userId)
        {
            var maxNum = _packageService.GetRoomsCountByAdminId(userId);

            var consumedUsers = _adminService.Find(userId).Restaurants.Count(x => !x.IsDeleted);

            UserConsumed MaxCon = new UserConsumed();
            MaxCon.MaxNumUsers = maxNum;
            MaxCon.ConsumedUsers = consumedUsers;


            return MaxCon;
        }



        #endregion

        #region Restaurant waiter
        public void AddRestaurantWaiter(RestaurantWaiterDto restaurantWaiterDto, long restaurantAdminId)
        {
            ValidateRestaurantWaiter(restaurantWaiterDto, 0);
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            //var consumedWaiters = restaurant.GlobalAdmin.Restaurants.Where(x => !x.IsDeleted).Select(x => x.WaitersLimit).Sum();
            // var consumedWaiters = _packageService.Query(x => x.GlobalAdminId == restaurant.GlobalAdminId).Select(x => x.Waiters.Count(w=>!w.IsDeleted)).Sum();
            //Package package;

            //var packages = _packageService.Query(x => x.AdminId == restaurant.GlobalAdminId).Include(x => x.Waiters).Select().ToList();
            //package = packages.OrderBy(x => x.Start).FirstOrDefault();
            //int count = 1;
            //while (true)
            //{
            //    if (package.MaxNumberOfWaiters > package.Waiters.Count(x => !x.IsDeleted))
            //    {
            //        break;
            //    }
            //    //else
            //    //{
            //    //    consumedWaiters = consumedWaiters - package.MaxNumberOfWaiters;
            //    //}

            //    package = packages.OrderBy(x => x.Start).Skip(count).FirstOrDefault();
            //    count++;
            //}
            ///var packages = restaurant.GlobalAdmin.Packages;
            RestaurantWaiter restaurantWaiter = Mapper.Map<RestaurantWaiter>(restaurantWaiterDto);
            restaurantWaiter.RestaurantId = restaurant.RestaurantId;
            restaurantWaiter.Password = PasswordHelper.Encrypt(restaurantWaiterDto.Password);
            restaurantWaiter.Role = Enums.RoleType.Waiter;
            restaurantWaiter.IsActive = true;
            //restaurantWaiter.PackageId = package.PackageId;
            _restaurantWaiterService.Insert(restaurantWaiter);
            SaveChanges();
            //UpdateSubscription(restaurant.GlobalAdminId, package.PackageGuid, package.Waiters.Count(x => !x.IsDeleted));
        }

        public RestaurantWaiterDto GetRestaurantWaiter(long waiterId)
        {
            var waiter = _restaurantWaiterService.Find(waiterId);
            if (waiter == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            return Mapper.Map<RestaurantWaiterDto>(waiter);
        }

        public PagedResultsDto GetAllRestaurantWaiters(long restaurantAdminId, int page, int pageSize, string language)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            return _restaurantWaiterService.GetAllRestaurantWaiters(restaurant.RestaurantId, page, pageSize, language);
        }
        public void UpdateRestaurantWaiter(RestaurantWaiterDto restaurantWaiterDto)
        {
            var restaurantWaiter = _restaurantWaiterService.Find(restaurantWaiterDto.UserId);
            if (restaurantWaiter == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            ValidateRestaurantWaiter(restaurantWaiterDto, restaurantWaiter.RestaurantId);
            restaurantWaiter.Name = restaurantWaiterDto.Name;
            restaurantWaiter.UserName = restaurantWaiterDto.UserName;
            restaurantWaiter.Password = PasswordHelper.Encrypt(restaurantWaiterDto.Password);
           // restaurantWaiter.TableId = restaurantWaiterDto.TableId;
            _restaurantWaiterService.Update(restaurantWaiter);
            SaveChanges();
        }
        private void ValidateRestaurantWaiter(RestaurantWaiterDto restaurantWaiterDto, long restaurantId)
        {
            if (string.IsNullOrEmpty(restaurantWaiterDto.Name)) throw new ValidationException(ErrorCodes.EmptyRestaurantWaiterUserName);
            if (restaurantWaiterDto.Name.Length > 100) throw new ValidationException(ErrorCodes.RestaurantWaiterNameExceedLength);
            if (string.IsNullOrEmpty(restaurantWaiterDto.UserName)) throw new ValidationException(ErrorCodes.EmptyRestaurantWaiterUserName);
            if (restaurantWaiterDto.UserName.Length > 100) throw new ValidationException(ErrorCodes.RestaurantWaiterNameExceedLength);
            if (string.IsNullOrEmpty(restaurantWaiterDto.Password)) throw new ValidationException(ErrorCodes.EmptyRestaurantAdminPassword);
            if (restaurantWaiterDto.Password.Length < 8 || restaurantWaiterDto.Password.Length > 25) throw new ValidationException(ErrorCodes.RestaurantAdminPasswordLengthNotMatched);
            if (_restaurantWaiterService.CheckUserNameDuplicated(restaurantWaiterDto.UserName, restaurantId)) throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
            if (_UserService.CheckUserNameDuplicated(restaurantWaiterDto.UserName,restaurantWaiterDto.UserId)) throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
        }
        public void DeleteRestaurantWaiter(long restaurantWaiterId)
        {
            var restaurantWaiter = _restaurantWaiterService.Find(restaurantWaiterId);
            if (restaurantWaiter == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            restaurantWaiter.IsDeleted = true;
            _restaurantWaiterService.Update(restaurantWaiter);
            //var package = _packageService.Query(x => x.PackageId == restaurantWaiter.PackageId).Include(x => x.Waiters)
            //    .Select().FirstOrDefault();
            SaveChanges();
            //UpdateSubscription(package.GlobalAdminId, package.PackageGuid, package.Waiters.Count(x => !x.IsDeleted));
        }


        #endregion

        #region Manage Client

        public ClientDto AddClient(ClientDto clientDto)
        {
              ValidateClient(clientDto);

            Client clientMap = Mapper.Map<Client>(clientDto);
            clientMap.Password = PasswordHelper.Encrypt(clientDto.Password);
            clientMap.Phone =  clientDto.Phone;
            clientMap.ClientName =  clientDto.ClientName;

            clientMap.Role = Enums.RoleType.User;
            clientMap.IsActive = true;

             _clientService.Insert(clientMap);
            clientDto.UserId = clientMap.UserId;
            SaveChanges();
            return clientDto;
        }
        private void ValidateClient(ClientDto clientDto)
        {
            if (string.IsNullOrEmpty(clientDto.UserName)) throw new ValidationException(ErrorCodes.EmptyUserName);
            if (clientDto.UserName.Length > 100) throw new ValidationException(ErrorCodes.NameExceedLength);
            if (string.IsNullOrEmpty(clientDto.Password)) throw new ValidationException(ErrorCodes.EmptyPassword);
            if (clientDto.Password.Length < 8 || clientDto.Password.Length > 25) throw new ValidationException(ErrorCodes.PasswordLengthNotMatched);

            if (_clientService.CheckClientUserNameDuplicated(clientDto.UserName, clientDto.UserId,clientDto.Phone)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
        }
        #endregion
    }
}
