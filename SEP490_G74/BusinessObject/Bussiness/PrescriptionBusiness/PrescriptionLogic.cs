using AutoMapper;
using DataAccess.Entity;
using HcsBE.Dao.PrecriptionDAO;
using HcsBE.Dao.ServiceDao;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.PrescriptionBusiness
{
    public class PrescriptionLogic
    {
        private PrecriptionDAO dao = new PrecriptionDAO();



        public int AddPrescription(PrescriptionDTO prescription)
        {
            var id = dao.AddPrecription(prescription);
            return id;
        }
        public bool AddSuppliesInPrescription(SuppliesPrescriptionDTO suppliesPrescription)
        {
            var status = dao.AddSuppliesInPrescription(suppliesPrescription);
            return status;
        }
        public List<PrescriptionInforDTO> GetListPresciptionInfors(int page,int idDoctor)
        {
            var prescriptionInforDTOs = dao.GetListPresciptionInfors(page,idDoctor);
            return prescriptionInforDTOs;
        }
        public List<PrescriptionInforDTO> GetListPresciptionInforsByIdPatient(int page,int idPatient)
        {
            var prescriptionInforDTOs = dao.GetListPresciptionInforsByIdPatient(page,idPatient);
            return prescriptionInforDTOs;
        }
        public int GetCountListPresciptionInfor()
        {
            var count = dao.GetCountListPresciptionInfors();
            return count;
        }
        public ContactPatientInPrescriptionDTO GetPatientContactByPrescriptionId(int idPrescription)
        {
            var contactPatientInPrescriptionDTO = dao.GetPatientContactByPrescriptionId(idPrescription);
            return contactPatientInPrescriptionDTO;
        }
        public List<PrescriptionDetailSuppliesDTO> GetSuppliesByPrescriptionId(int idPrescription)
        {
            var listSuppliesByPrescriptionId = dao.GetSuppliesByPrescriptionId(idPrescription);
            return listSuppliesByPrescriptionId;
        }
    }
}
