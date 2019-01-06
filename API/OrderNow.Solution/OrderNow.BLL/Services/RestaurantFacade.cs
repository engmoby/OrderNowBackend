using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.BLL.Services.ManageStorage;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model; 
using Newtonsoft.Json;
using OrderNow.Common.CustomException;
using Repository.Pattern.UnitOfWork;

namespace OrderNow.BLL.Services
{
    public class RestaurantFacade : BaseFacade, IRestaurantFacade
    {
          private readonly IRestaurantService _restaurantService;
        private IRestaurantTranslationService _restaurantTranslationService;
        private IUserService _userService;
        private IRestaurantAdminService _restaurantAdminService;
        private IManageStorage _manageStorage;
        private IRestaurantWaiterService _restaurantWaiterService;
        private IAdminService _globalAdminService;
        private IPackageService _packageService; 
        private IFeedBackService _feedBackService;
        private IUserFacade _userFacade;

        public RestaurantFacade(  IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService,
            IUserService userService, IRestaurantAdminService restaurantAdminService
            , IManageStorage manageStorage,IRestaurantWaiterService restaurantWaiterService, IAdminService globalAdminService, IUnitOfWorkAsync unitOfWork, IPackageService packageService,  IFeedBackService feedBackService, IUserFacade userFacade) : base(unitOfWork)
        { 
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _userService = userService;
            _restaurantAdminService = restaurantAdminService;
            _manageStorage = manageStorage;
            _restaurantWaiterService = restaurantWaiterService;
            _globalAdminService = globalAdminService;
            _packageService = packageService; 
            _feedBackService = feedBackService;
            _userFacade = userFacade;
        }

        public RestaurantFacade( 
            IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService,
            IUserService userService, IRestaurantAdminService restaurantAdminService, IManageStorage manageStorage, IPackageService packageService,   IFeedBackService feedBackService, IUserFacade userFacade)
        { 
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _userService = userService;
            _restaurantAdminService = restaurantAdminService;
            _manageStorage = manageStorage;
            _packageService = packageService; 
            _feedBackService = feedBackService;
            _userFacade = userFacade;
        }

      
     
        public void AddRestaurant(RestaurantDTO restaurantDto, string path, long userId)
        {
            ValidateRestaurant(restaurantDto,userId);

           
            Restaurant restaurant = new Restaurant
            { 
                IsActive = false,
                RestaurantTranslations = new List<RestaurantTranslation>(),
                AdminId = userId, 

            };
            foreach (var restaurantName in restaurantDto.RestaurantNameDictionary)
            {
                restaurant.RestaurantTranslations.Add(new RestaurantTranslation
                {
                    Language = restaurantName.Key.ToLower(),
                    RestaurantName = restaurantName.Value,
                    RestaurantDescription = restaurantDto.RestaurantDescriptionDictionary[restaurantName.Key]
                });
            }
            restaurant.RestaurantAdmin = new RestaurantAdmin
            {
                UserName = restaurantDto.RestaurantAdminUserName,
                Password = PasswordHelper.Encrypt(restaurantDto.RestaurantAdminPassword),
                Role = Enums.RoleType.RestaurantAdmin,
                IsDeleted = false,
                IsActive = true
            }; 
            restaurant.BackgroundId = Strings.BackgroundId;
           

            var packages = _packageService.Query(x => x.AdminId == restaurant.AdminId).Include(x => x.Restaurants).Select().ToList();
            var package = packages.OrderBy(x => x.Start).FirstOrDefault();
            int count = 1;
            while (true)
            {
                if (package.MaxNumberOfRooms > package.Restaurants.Count(x => !x.IsDeleted))
                {
                    break;
                }
                //else
                //{
                //    consumedWaiters = consumedWaiters - package.MaxNumberOfWaiters;
                //}

                package = packages.OrderBy(x => x.Start).Skip(count).FirstOrDefault();
                count++;
            }
            //restaurant.PackageId = package.PackageId;

            //_roomService.Insert(room);
            //SaveChanges();

            //UpdateSubscription(adminId, package.PackageGuid, package.Rooms.Count(x => !x.IsDeleted));
            //_userService.Insert(restaurant.RestaurantAdmin);
            _restaurantAdminService.Insert(restaurant.RestaurantAdmin);
            _restaurantTranslationService.InsertRange(restaurant.RestaurantTranslations);
            _restaurantService.Insert(restaurant);

            restaurant.PackageId = package.PackageId;
            SaveChanges();
            restaurant.RestaurantAdmin.RestaurantId = restaurant.RestaurantId;
            _restaurantAdminService.Update(restaurant.RestaurantAdmin);
            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + restaurant.RestaurantId, restaurantDto.Image,
                restaurant.RestaurantId.ToString());

            UpdateSubscription(restaurantDto.UserAccountId, package.PackageGuid, package.Restaurants.Count);


        }

        public RestaurantDTO GetRestaurant(long restaurantId, string language)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new NotFoundException(ErrorCodes.RestaurantDeleted);
            //var restaurantAdmin = _restaurantAdminService.Find(restaurant.RestaurantAdminId);
            //if(restaurantAdmin == null) throw new NotFoundException(ErrorCodes.RestaurantAdminNotFound);

            //var restaurantdto = Mapper.Map<Restaurant, RestaurantDTO>(restaurant, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.RestaurantTranslations = src.RestaurantTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            src.RestaurantType.RestaurantTypeTranslations = src.RestaurantType.RestaurantTypeTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //        }
            //    );
            //});

            //restaurantdto.RestaurantAdminPassword = restaurantAdmin.UserName;
            //restaurantdto.RestaurantAdminPassword = restaurantAdmin.Password;
            var restaurantDto= Mapper.Map<RestaurantDTO>(restaurant);
            //restaurantDto.ConsumedWaiters = _restaurantWaiterService
            //    .GetAllRestaurantWaiters(restaurant.RestaurantId, 1, 0, Strings.DefaultLanguage).TotalCount;
            return restaurantDto;
        }

        private void ValidateRestaurant(RestaurantDTO restaurantDto,  long userId)
        {
            foreach (var restaurantName in restaurantDto.RestaurantNameDictionary)
            {
                if (string.IsNullOrEmpty(restaurantName.Value))
                    throw new ValidationException(ErrorCodes.EmptyRestaurantName);
                if (restaurantName.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.RestaurantNameExceedLength);
                if (_restaurantTranslationService.CheckRestaurantNameExist(restaurantName.Value, restaurantName.Key,restaurantDto.RestaurantId, userId))
                    throw new ValidationException(ErrorCodes.RestaurantNameAlreadyExist);
                if (Strings.SupportedLanguages.All(x => x.ToLower() != restaurantName.Key.ToLower()))
                    throw new ValidationException(ErrorCodes.UnSupportedLanguage);
            }
            foreach (var restaurantDescription in restaurantDto.RestaurantDescriptionDictionary)
            {
                if (string.IsNullOrEmpty(restaurantDescription.Value))
                    throw new ValidationException(ErrorCodes.EmptyRestaurantDescription);
                if (Strings.SupportedLanguages.All(x => x.ToLower() != restaurantDescription.Key.ToLower()))
                    throw new ValidationException(ErrorCodes.UnSupportedLanguage);
            }
            if (string.IsNullOrEmpty(restaurantDto.RestaurantAdminUserName))
                throw new ValidationException(ErrorCodes.EmptyRestaurantAdminUserName);
            if (string.IsNullOrEmpty(restaurantDto.RestaurantAdminPassword))
                throw new ValidationException(ErrorCodes.EmptyRestaurantAdminPassword);
            if (restaurantDto.RestaurantAdminPassword.Length < 8 ||
                restaurantDto.RestaurantAdminPassword.Length > 25)
                throw new ValidationException(ErrorCodes.RestaurantAdminPasswordLengthNotMatched);
            if (_restaurantAdminService.CheckUserNameDuplicated(restaurantDto.RestaurantAdminUserName,
                restaurantDto.RestaurantId))
                throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
            //if (_userService.CheckUserNameDuplicated(restaurantDto.RestaurantAdminUserName))
            //    throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
        }

        public PagedResultsDto GetAllRestaurant(string language, int page, int pageSize, long userId)
        {
            var allRestaurant = _restaurantTranslationService.GetAllRestaurant(language, page, pageSize,userId);
            //foreach (var restaurant in (List<RestaurantDTO>)allRestaurant.Data)
            //{
            //    restaurant.ConsumedWaiters = _restaurantWaiterService
            //        .GetAllRestaurantWaiters(restaurant.RestaurantId, 1, 0, Strings.DefaultLanguage).TotalCount;
            //}
            return allRestaurant;
        }
       
        public void ActivateRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (!restaurant.IsReady) throw new ValidationException(ErrorCodes.RestaurantIsNotReady);
            //if (!_menuService.Query(x => x.RestaurantId == restaurant.RestaurantId && x.IsActive).Select().Any())
            //    throw new ValidationException(ErrorCodes.RestaurantMenuDoesNotActivated);
             
            restaurant.IsActive = true;
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public void DeActivateRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            restaurant.IsActive = false;
          
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public void DeleteRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            restaurant.IsDeleted = true;
            restaurant.RestaurantAdmin.IsDeleted = true;
            var waiters = _restaurantWaiterService.GetAlRestaurantWaitersByRestaurantId(restaurant.RestaurantId);
            waiters.ForEach(x => { x.IsDeleted = true; _restaurantWaiterService.Update(x); });
 
            _restaurantService.Update(restaurant);
            

            SaveChanges();
        }

        public void UpdateRestaurant(RestaurantDTO restaurantDto, string path, long userId)
        {
            Restaurant restaurant = _restaurantService.Find(restaurantDto.RestaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            ValidateRestaurant(restaurantDto,userId);
             
            restaurant.RestaurantAdmin.UserName = restaurantDto.RestaurantAdminUserName;
            restaurant.RestaurantAdmin.Password = PasswordHelper.Encrypt(restaurantDto.RestaurantAdminPassword);
            //restaurant.WaitersLimit = restaurantDto.WaitersLimit;

            foreach (var restaurantName in restaurantDto.RestaurantNameDictionary)
            {
                var restaurantTranslation =
                    restaurant.RestaurantTranslations.FirstOrDefault(x => x.Language.ToLower() == restaurantName.Key.ToLower());
                if (restaurantTranslation == null)
                {
                    restaurant.RestaurantTranslations.Add(new RestaurantTranslation
                    {
                        Language = restaurantName.Key.ToLower(),
                        RestaurantName = restaurantName.Value,
                        RestaurantDescription = restaurantDto.RestaurantDescriptionDictionary[restaurantName.Key]
                    });
                }
                else
                {
                    restaurantTranslation.RestaurantName = restaurantName.Value;
                    restaurantTranslation.RestaurantDescription = restaurantDto.RestaurantDescriptionDictionary[restaurantName.Key];
                }
            }

            _restaurantService.Update(restaurant);
            SaveChanges();
            if (restaurantDto.IsLogoChange)
                _manageStorage.UploadImage(path + "\\" + "Restaurant-" + restaurant.RestaurantId, restaurantDto.Image,
                    restaurant.RestaurantId.ToString());
        }

        public RestaurantDTO CheckRestaurantReady(long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            RestaurantDTO restaurantDto = new RestaurantDTO();
            restaurantDto.IsReady = restaurant.IsReady;
            return restaurantDto;
        }

        public void PublishRestaurant(long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            //if (!_menuService.Query(x => x.RestaurantId == restaurant.RestaurantId && x.IsActive).Select().Any())
            //    throw new ValidationException(ErrorCodes.RestaurantMenuDoesNotActivated); 
            restaurant.IsReady = true;
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public ResturantInfoDto GetGlobalRestaurantInfo(long restaurantId, string role,string language)
        {
            Restaurant restaurant;
            //if (role == Enums.RoleType.RestaurantAdmin.ToString())
            //{
            //    restaurant = _restaurantService.GetRestaurantByAdminId(userId);
            //    if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            //    if (restaurant.IsDeleted) throw new NotFoundException(ErrorCodes.RestaurantDeleted);
            //}
            //else
            //{
            //    var waiter = _restaurantWaiterService.Find(userId);
            //    restaurant = _restaurantService.Find(waiter.RestaurantId);
            //    if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            //    if (restaurant.IsDeleted) throw new NotFoundException(ErrorCodes.RestaurantDeleted);

            //}
            restaurant = _restaurantService.Find(restaurantId);
            var restaurantdto = new ResturantInfoDto
            {
                ResturentId = restaurant.RestaurantId,
                BackgroundId = restaurant.BackgroundId,
                RestaurantName = restaurant.RestaurantTranslations.FirstOrDefault(x=>x.Language.ToLower() == language.ToLower()).RestaurantName,
                Rate = _feedBackService.GetRestaurantRate(restaurantId)

            };
            return restaurantdto;
        }

        private void UpdateSubscription(Guid admin, Guid packageGuid, int consumed)
        {
            string url = ConfigurationManager.AppSettings["subscriptionURL"];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/Users/EditUserConsumer");
            //request.Headers.Add("X-Auth-Token:" + token);
            request.ContentType = "application/json";
            request.Method = "POST";
            var serializer = JsonConvert.SerializeObject(new
            {
                userConsumer = consumed,
                userAccountId = admin,
                backageGuid = packageGuid
            });
            //request.ContentLength = serializer.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = serializer;

                streamWriter.Write(json);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var infoResponse = readStream.ReadToEnd();

                response.Close();
                receiveStream.Close();
                readStream.Close();
            }
        }

    }
}
