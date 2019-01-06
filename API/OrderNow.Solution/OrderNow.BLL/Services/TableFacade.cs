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
    public class TableFacade : BaseFacade, ITableFacade
    {
        private readonly ITableService _TableService;

        public TableFacade(ITableService TableService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _TableService = TableService;
        }

        public TableFacade(ITableService TableService)
        {
            _TableService = TableService;
        }

        public TableDto GetTable(long TableId, int tenantId)
        {
            return Mapper.Map<TableDto>(_TableService.Query(x => x.TableId == TableId)
                .Select().FirstOrDefault());
        }
        public TableDto GetTable(long tableId)
        {
            return Mapper.Map<TableDto>(_TableService.Query(x => x.TableId == tableId)
                .Select().FirstOrDefault());
        }
        public TableDto CreateTable(TableDto tableDto, int userId, int tenantId)
        {
            if (GetTable(tableDto.TableId, tenantId) != null)
            {
                return EditTable(tableDto, userId, tenantId);
            }
            ValidateTable(tableDto, tenantId, tableDto.BranchId);
            var tableObj = Mapper.Map<Table>(tableDto);

            // TableObj.AreaId = TableDto.AreaId;
            tableObj.TableName = tableDto.TableName;
            tableObj.BranchId = tableDto.BranchId;
            _TableService.Insert(tableObj);
            SaveChanges();
            return tableDto;
        }

        public TableDto EditTable(TableDto tableDto, int userId, int tenantId)
        {
            var tableObj = _TableService.Query(x => x.TableId == tableDto.TableId)
                .Select().FirstOrDefault();
            if (tableObj == null) throw new NotFoundException(ErrorCodes.CountryNotFound);
            ValidateTable(tableDto, tenantId, tableObj.BranchId);

            tableObj.TableName = tableDto.TableName;
            _TableService.Update(tableObj);
            SaveChanges();
            return tableDto;

        }

        public PagedResultsDto GetAllTables(int page, int pageSize, int tenantId)
        {
            return _TableService.GetAllTables(page, pageSize, tenantId);
        }

        private void ValidateTable(TableDto tableDto, long tenantId, long areaId)
        {
            //foreach (var name in tableDto.TableName)
            //{
                if (tableDto.TableName.Length > 300)
                    throw new ValidationException(ErrorCodes.MenuNameExceedLength);

                if (_TableService.Query(x => x.TableName == tableDto.TableName && x.TableId != tableDto.TableId && x.BranchId == tableDto.BranchId).Select().Any())
                    throw new ValidationException(ErrorCodes.TableIsExist);
            //}
        }
    }
}
