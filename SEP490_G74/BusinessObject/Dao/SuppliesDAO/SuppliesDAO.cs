using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.SuppliesDAO
{
    public class SuppliesDAO
    {
        private HcsContext context = new HcsContext();
        public SuppliesPagination GetSuppliesList(int page=1)
        {
            int pageSize = 3;
            var result = context.Supplies
                .Join(
                    context.SuppliesTypes,
                    supplies => supplies.SuppliesTypeId,
                    suppliesType => suppliesType.SuppliesTypeId,
                    (supplies, suppliesType) => new SuppliesDTO
                    {
                        SId = supplies.SId,
                        SName = supplies.SName,
                        UnitInStock = supplies.UnitInStock,
                        suppliesType = suppliesType
                    })
                .ToList();
            var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalItemCount = result.Count();
            var viewModel = new SuppliesPagination
            {
                supplies = pagedData,
                TotalItemCount = totalItemCount,
                PageNumber = pageSize
            };
            return viewModel;
        }
        public SuppliesDetailDTO? GetSuppliesDetailList(int id)
        {
            var result = context.Supplies
        .Join(
            context.SuppliesTypes,
            supplies => supplies.SuppliesTypeId,
            suppliesType => suppliesType.SuppliesTypeId,
            (supplies, suppliesType) => new SuppliesDetailDTO
            {
                SId = supplies.SId,
                SName = supplies.SName,
                UnitInStock = supplies.UnitInStock,
                InputDay = supplies.Inputday,
                Exp = supplies.Exp,
                Price = supplies.Price,
                Distributor = supplies.Distributor,
                Uses = supplies.Uses,
                SuppliesType = suppliesType
            })
        .Where(x => x.SId == id) 
        .FirstOrDefault();

            return result;
        }
        public bool AddSupplies(SuppliesExcuteDTO supplies)
        {
            var newSupplies = new Supply
            {
                SName = supplies.SName,
                Uses = supplies.Uses,
                Exp = supplies.Exp,
                Distributor = supplies.Distributor,
                UnitInStock = (short)supplies.UnitInStock,
                Price = supplies.Price,
                SuppliesTypeId = supplies.SuppliesTypeID,
                Inputday = supplies.InputDay
            };
            context.Supplies.Add(newSupplies);
            context.SaveChanges();
            return true;
        }
        public bool UpdateSupplies(SuppliesExcuteDTO supplies)
        {
            var suppliesToUpdate = context.Supplies.Find(supplies.SId); 

            if (suppliesToUpdate != null)
            {
                suppliesToUpdate.SName = supplies.SName;
                suppliesToUpdate.Uses = supplies.Uses;
                suppliesToUpdate.Exp = supplies.Exp;
                suppliesToUpdate.Distributor = supplies.Distributor;
                suppliesToUpdate.UnitInStock = (short)supplies.UnitInStock;
                suppliesToUpdate.Price = supplies.Price;
                suppliesToUpdate.SuppliesTypeId = supplies.SuppliesTypeID;
                suppliesToUpdate.Inputday = supplies.InputDay;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<SuppliesTypeDTO> GetListSuppliesType()
        {
            var result = context.SuppliesTypes.Select(suppliesType => new SuppliesTypeDTO
            {
                suppliesTypeId= suppliesType.SuppliesTypeId,
                suppliesTypeName= suppliesType.SuppliesTypeName
            }).ToList();
            return result;
        }
        public List<SuppliesDTO> GetSuppliesListByType(int typeId)
        {
            var result = context.Supplies
                .Join(
                    context.SuppliesTypes,
                    supplies => supplies.SuppliesTypeId,
                    suppliesType => suppliesType.SuppliesTypeId,
                    (supplies, suppliesType) => new SuppliesDTO
                    {
                        SId = supplies.SId,
                        SName = supplies.SName,
                        UnitInStock = supplies.UnitInStock,
                        suppliesType = suppliesType
                    })
                .Where(sptype=>sptype.suppliesType.SuppliesTypeId==typeId).ToList();
            return result;
        }
    }
}
