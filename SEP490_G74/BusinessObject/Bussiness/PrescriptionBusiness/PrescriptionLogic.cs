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


        public bool AddPrescriptionInMedicalRC(int medicalRCid, int prescriptionId)
        {
            var status = dao.AddPrescriptionInMedicalRC(medicalRCid,prescriptionId);
            return status;
        }
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
        public List<PrescriptionInforDTO> GetListPresciptionInfors(int page, int idUser)
        {
            var prescriptionInforDTOs = dao.GetListPresciptionInfors(page,idUser);
            return prescriptionInforDTOs;
        }
        public List<PrescriptionInforDTO> GetListPresciptionInforsByIdPatient(int idUser,int idPatient)
        {
            var prescriptionInforDTOs = dao.GetListPresciptionInforsByIdPatient(idUser, idPatient);
            return prescriptionInforDTOs;
        }
        //GetPresciptionInforsByMedicalRC(int doctorIdInt = 0, int medicalRCid = 0)
        public PrescriptionInforDTO GetPresciptionInforsByMedicalRC(int idUser, int medicalRCid)
        {
            var prescriptionInforDTOs = dao.GetPresciptionInforsByMedicalRC(idUser, medicalRCid);
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
        public ContactPatientInPrescriptionDTO GetPatientContactByMedicalRCId(int medicalRCId)
        {
            var contactPatientInPrescriptionDTO = dao.GetPatientContactByMedicalRCId(medicalRCId);
            return contactPatientInPrescriptionDTO;
        }
        public List<PrescriptionDetailSuppliesDTO> GetSuppliesByPrescriptionId(int idPrescription)
        {
            var listSuppliesByPrescriptionId = dao.GetSuppliesByPrescriptionId(idPrescription);
            return listSuppliesByPrescriptionId;
        }
        public string GetDoctorName(int idDoctor)
        {
            var doctorName = dao.GetDoctorName(idDoctor);
            return doctorName;
        }

    }
}
