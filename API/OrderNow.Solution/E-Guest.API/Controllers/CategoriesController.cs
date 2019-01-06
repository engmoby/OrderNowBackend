using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using AutoMapper;
using E_Guest.API.Infrastructure;
using E_Guest.API.Models;
using E_Guest.API.Providers;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;
using OrderNow.Common.CustomException;

namespace OrderNow.API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private ICategoryFacade _categoryFacade;
        private ICategoryTranslationService _categoryService;
        private ICategoryService _catService;
        private IitemFacade _itemFacade;
        private IItemService _itemService;
        private readonly IUserFacade _userFacade;

        public CategoriesController(ICategoryFacade categoryFacade, IitemFacade itemFacade, ICategoryTranslationService categoryService, IUserFacade userFacade, ICategoryService catService, IItemService itemService)
        {
            _categoryFacade = categoryFacade;
            _itemFacade = itemFacade;
            _categoryService = categoryService;
            _userFacade = userFacade;
            _catService = catService;
            _itemService = itemService;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories", Name = "AddCategory")]
        [HttpPost]
        public IHttpActionResult AddCategory()
        {

            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyCategoryImage);
            var httpPostedFile = HttpContext.Current.Request.Files[0];

            var categoryModel = new JavaScriptSerializer().Deserialize<CategoryModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null)
                throw new ValidationException(ErrorCodes.EmptyCategoryImage);

            if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                throw new ValidationException(ErrorCodes.ImageExceedSize);


            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var categoryDto = Mapper.Map<CategoryDTO>(categoryModel);
            var getResturantIdByUserId = _userFacade.GetUser(UserId);
            categoryDto.RestaurantId = getResturantIdByUserId.RestaurantId;
            //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
            categoryDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(categoryDto.Image);

            _categoryFacade.AddCategory(categoryDto, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}", Name = "GetCategory")]
        [HttpGet]
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult GetCategory(long categoryId)
        {
            var category = Mapper.Map<CategoryModel>(_categoryFacade.GetCategory(categoryId));
            category.ImageURL = Url.Link("CategoryImage", new { category.RestaurantId, category.CategoryId });

            return Ok(category);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}", Name = "DeleteCategory")]
        [HttpDelete]
        public IHttpActionResult DeleteCategory(long categoryId)
        {
            _categoryFacade.DeleteCategory(categoryId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/Activate", Name = "ActivateCategory")]
        [HttpGet]
        public IHttpActionResult ActivateCategory(long categoryId)
        {
            _categoryFacade.ActivateCategory(categoryId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/DeActivate", Name = "DeActivateCategory")]
        [HttpGet]
        public IHttpActionResult DeActivateCategory(long categoryId)
        {
            _categoryFacade.DeActivateCategory(categoryId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories", Name = "UpdateCategory")]
        [HttpPut]
        public IHttpActionResult UpdateCategory()
        {
            var categoryModel =
                new JavaScriptSerializer().Deserialize<CategoryModel>(HttpContext.Current.Request.Form.Get(0));
            var categoryDto = Mapper.Map<CategoryDTO>(categoryModel);
            if (categoryModel.IsImageChange)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);
                var httpPostedFile = HttpContext.Current.Request.Files[0];


                if (httpPostedFile == null)
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);

                //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
                var getResturantIdByUserId = _userFacade.GetUser(UserId);
                categoryDto.RestaurantId = getResturantIdByUserId.RestaurantId;
                categoryDto.Image = new MemoryStream();
                httpPostedFile.InputStream.CopyTo(categoryDto.Image);
            }
            _categoryFacade.UpdateCategory(categoryDto, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        } 
        [Route("api/Categories/LatestItems", Name = "GetLatestItemsForResturant")]
        [HttpGet]
        [ResponseType(typeof(List<ItemModel>))]
        public IHttpActionResult GetLatestItemsForResturant( int page = Page, int pagesize = PageSize)
        {
            var getuserResturant = _userFacade.GetUser(UserId);
            PagedResultsDto items;
            var itemList =  _itemService.GetlatestItemsByRestuantId(getuserResturant.RestaurantId, page, pagesize);
             
            var data = Mapper.Map<List<ItemModel>>(itemList.Data);
            foreach (var item in data)
            {
                item.ImageURL = Url.Link("ItemImage", new { item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID });
            }
            return PagedResponse("GetLatestItemsForResturant", page, pagesize, itemList.TotalCount, data);
        }



        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/Items", Name = "GetAllItemsForCategory")]
        [HttpGet]
        [ResponseType(typeof(List<ItemModel>))]
        public IHttpActionResult GetAllItemsForCategory(long categoryId, int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto items;
            items = UserRole == Enums.RoleType.RestaurantAdmin.ToString() ? _itemFacade.GetAllItemsByCategoryId(Language, categoryId, page, pagesize) : _itemFacade.GetActivatedItemsByCategoryId(Language, categoryId, page, pagesize);

            //var items = _itemFacade.GetAllItemsByCategoryId(Language, categoryId, page, pagesize);
            var data = Mapper.Map<List<ItemModel>>(items.Data);
            foreach (var item in data)
            {
                item.ImageURL = Url.Link("ItemImage", new { item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID });
            }
            return PagedResponse("GetAllItemsForCategory", page, pagesize, items.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/Items/Name", Name = "GetAllItemNamesForCategory")]
        [HttpGet]
        [ResponseType(typeof(List<ItemModel>))]
        public IHttpActionResult GetAllItemNamesForCategory(long categoryId)
        {
            var data = Mapper.Map<List<ItemNameModel>>(_itemFacade.GetAllItemNamesByCategoryId(Language, categoryId));
            return Ok(data);
        }


        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin, Enums.RoleType.User)]
        [Route("api/Categories/GetAllCategories", Name = "GetAllCategories")]
        [HttpGet]
        [ResponseType(typeof(List<CategoryModel>))]
        public IHttpActionResult GetAllCategories(int page = Page, int pagesize = PageSize)
        {
            var getResturantIdByUserId = _userFacade.GetUser(UserId);
            PagedResultsDto categories;
            categories = UserRole == Enums.RoleType.RestaurantAdmin.ToString() ?
                _categoryService.GetAllCategories(Language, getResturantIdByUserId.RestaurantId, page, pagesize) :
                _categoryService.GetActivatedCategories(Language, getResturantIdByUserId.RestaurantId, page, pagesize);
            var data = Mapper.Map<List<CategoryModel>>(categories.Data);
            foreach (var category in data)
            {
                category.ImageURL = Url.Link("CategoryImage", new { category.RestaurantId, category.CategoryId });
            }
            return PagedResponse("GetAllCategories", page, pagesize, categories.TotalCount, data);
        }
        //  [AuthorizeRoles(Enums.RoleType.RestaurantAdmin, Enums.RoleType.User)]
        [Route("api/Categories/GetAllCategoriesByTable/{tableId:long}", Name = "GetAllCategoriesByTable")]
        [HttpGet]
        [ResponseType(typeof(List<CategoryModel>))]
        public IHttpActionResult GetAllCategoriesByTable(long tableId)
        {
            var categories = _catService.GetAllCategoriesByTable(tableId);
            var data = Mapper.Map<List<CategoryModel>>(categories.Data);
            foreach (var category in data)
            {
                category.ImageURL = Url.Link("CategoryImage", new { category.RestaurantId, category.CategoryId });
                foreach (var item in category.Items)
                { 
                    item.ImageURL = Url.Link("ItemImage", new { item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID });

                }
            }
            return Ok(data);
        }
        //[Route("api/Categories/{restaurantId:long}/{categoryId:long}/image", Name = "CategoryImage")]
        //public HttpResponseMessage GetCategoryImage(long restaurantId, long categoryId, string type = "orignal")
        //{
        //    try
        //    {
        //        string filePath = type == "orignal"
        //            ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Restaurant-" + restaurantId + "Category-" + categoryId)
        //                .FirstOrDefault(x => Path.GetFileName(x).Contains(restaurantId.ToString()) && !Path.GetFileName(x).Contains("thumb"))
        //            : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Restaurant-" + restaurantId + "Category-" + categoryId)
        //                .FirstOrDefault(x => Path.GetFileName(x).Contains(restaurantId.ToString()) && Path.GetFileName(x).Contains("thumb"));


        //        HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);

        //        byte[] fileData = File.ReadAllBytes(filePath);

        //        Response.Content = new ByteArrayContent(fileData);
        //        Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        //        return Response;
        //    }
        //    catch (Exception e)
        //    {
        //        return new HttpResponseMessage();
        //    }
        //}
    }
}
