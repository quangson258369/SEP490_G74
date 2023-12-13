using DataAccess.Entity;
using HcsBE.Dao.SuppliesDAO;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.Supplies
{
    public class SuppliesLogic
    {
        private SuppliesDAO supliesDAO=new SuppliesDAO();
        public SuppliesPagination GetListSupplies(int page)
        {
            SuppliesPagination suppliesList = supliesDAO.GetSuppliesList(page);
            return suppliesList;
        }
        public SuppliesDetailDTO GetSuppliesDetail(int id)
        {
            SuppliesDetailDTO  suppliesList = supliesDAO.GetSuppliesDetailList(id);
            return suppliesList;
        }
        public bool EditSupplies(SuppliesExcuteDTO supplies)
        {
            supliesDAO.UpdateSupplies(supplies);
            return true;
        }
        public bool AddSupplies(SuppliesExcuteDTO supplies)
        {
            supliesDAO.AddSupplies(supplies);
            return true;
        }
        public List<SuppliesTypeDTO> GetListSuppliesTypes()
        {
            List<SuppliesTypeDTO> listSuppliesType = supliesDAO.GetListSuppliesType();
            return listSuppliesType;
        }
        public List<SuppliesDTO> GetSuppliesListByType(int typeId)
        {
            List<SuppliesDTO> listSuppliesListByType = supliesDAO.GetSuppliesListByType(typeId);
            return listSuppliesListByType;
        }
    }
}
