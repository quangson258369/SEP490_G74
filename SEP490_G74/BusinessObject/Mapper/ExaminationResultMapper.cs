using AutoMapper;
using DataAccess.Entity;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.Dao.ServiceDao;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Mapper
{
    public class ExaminationResultMapper:Profile
    {
        private HcsContext context = new HcsContext();
        private ServiceDAO serviceDAO = new ServiceDAO();
        private MedicalRecordDao dao =  new MedicalRecordDao();
        public ExaminationResultMapper()
        {
            CreateMap<ExaminationResultId,ExaminationResultIdMRDTO>();
            CreateMap<ExaminationResultIdMRDTO, ExaminationResultId>();
            CreateMap<ServiceMedicalRecord, ServiceMRDTO>()
                .ForMember(x => x.PatientContact, x => x.MapFrom(x => GetPatientContact(x.MedicalRecordId)))
                .ForMember(x => x.DoctorContact, x => x.MapFrom(x => GetDoctorContact(x.DoctorId)))
                .ForMember(x => x.Service, x => x.MapFrom(x => serviceDAO.GetService(x.ServiceId)))
                .ForMember(x=>x.MedicalRecord, x=>x.MapFrom(x=> dao.GetMedicalRecord(x.MedicalRecordId)))
                ;
            CreateMap<ServiceMRDTO, ServiceMedicalRecord>();
        }
        private Contact GetPatientContact(int medicalRecordId)
        {
            MedicalRecord record = dao.GetMedicalRecord(medicalRecordId);
            int pid = record.PatientId;
            return context.Contacts.SingleOrDefault(s => s.PatientId == pid);
        }
        private Contact GetDoctorContact(int? doctorId)
        {
            return context.Contacts.SingleOrDefault(s => s.DoctorId == doctorId);
        }
    }
}
