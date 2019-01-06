using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using E_Guest.API.Infrastructure;
using E_Guest.API.Models;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;

namespace E_Guest.API.Controllers
{
    public class TableController : BaseApiController
    { 
        private readonly ITableFacade _TableFacade;
        public TableController(ITableFacade TableFacade)
        {
            _TableFacade = TableFacade; 
        }

        [Route("api/Tables/GetAllTables", Name = "GetAllTables")]
        [HttpGet]
        public IHttpActionResult GetAllTables(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto TableObj = _TableFacade.GetAllTables(page, pagesize, TenantId);
            var data = Mapper.Map<List<TableModel>>(TableObj.Data);
            return PagedResponse("GetAllAreas", page, pagesize, TableObj.TotalCount, data );
        }


        [Route("api/Tables", Name = "CreateTable")]
        [HttpPost]
        public IHttpActionResult CreateTable([FromBody] TableModel TableModel)
        {
            var reurnTable = _TableFacade.CreateTable(Mapper.Map<TableDto>(TableModel), UserId, TenantId);

            return Ok(reurnTable);
        }


        [Route("api/Tables/EditTable", Name = "EditTable")]
        [HttpPost]
        public IHttpActionResult EditTable([FromBody] TableModel TableModel)
        {
            var reurnTable = _TableFacade.EditTable(Mapper.Map<TableDto>(TableModel), UserId, TenantId);

            return Ok(reurnTable);
        }


        [Route("api/Tables/GetTableById", Name = "GetTableById")]
        [HttpGet]
        public IHttpActionResult GetTableById(long TableId)
        {
            var reurnTable = _TableFacade.GetTable(TableId, TenantId);
            return Ok(reurnTable);
        }
    }

}