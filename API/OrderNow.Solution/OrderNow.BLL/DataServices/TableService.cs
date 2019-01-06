using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class TableService : Service<Table>, ITableService
    {
        public TableService(IRepositoryAsync<Table> repository) : base(repository)
        {
            _repository = repository;
        }
        
        public PagedResultsDto GetAllTables(int page, int pageSize,int tenantId)
        {  

            var query = Queryable().OrderBy(x => x.TableId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();

            results.Data = Mapper.Map<List<Table>, List<TableDto>>(query.OrderBy(x => x.TableId).Skip((page - 1) * pageSize).Take(pageSize).ToList());

            return results;

        }
        public List<TableDto> GetAllTables(long branchId)
        {

            var query = Queryable().OrderBy(x => x.TableId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();

            var list = Mapper.Map<List<Table>, List<TableDto>>(query.Where(x=>x.BranchId==branchId).OrderBy(x => x.TableId).ToList());

            return list;

        }
    }
}