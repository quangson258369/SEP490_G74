using API.Common;
using API.Common.Entity;
using HcsBE.Dao.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.MedicalRecordDAO
{
    public class MedicalRecordDao
    {
        public List<MedicalRecordDaoOutputDto> MedicalRecordList()
        {
            var context = new ApplicationDbContext();
            var output = new List<MedicalRecordDaoOutputDto>();

            var query = from medicalrecord in context.MedicalRecords
                        from patient in context.Patients
                        from prescription in context.Prescriptions
                        from service in context.Services
                        from examinationResultId in context.ExaminationResultIds

                        where medicalrecord.Patient.PatientId == patient.PatientId
                        where examinationResultId.MedicalRecord.MedicalRecordId == medicalrecord.MedicalRecordId
                        where service.MedicalRecords.Any(x=>x.MedicalRecordId==medicalrecord.MedicalRecordId)
                        where prescription.MedicalRecordId == medicalrecord.MedicalRecordId

                        select new {medicalrecord};
            if (!query.Any())
            {
                return new List<MedicalRecordDaoOutputDto>(){
                    new MedicalRecordDaoOutputDto()
                {
                    ExceptionMessage = ConstantHcs.EmptyList,
                    ResultCd = ConstantHcs.Success
                }};
            }
            output.AddRange((IEnumerable<MedicalRecordDaoOutputDto>)query.ToList());

            return output;
        }

        public MedicalRecordDaoOutputDto GetMedicalRecord(int id)
        {
            var context = new ApplicationDbContext();
            var output = new MedicalRecordDaoOutputDto();

            var query = from medicalrecord in context.MedicalRecords
                        from patient in context.Patients
                        from prescription in context.Prescriptions
                        from service in context.Services
                        from examinationResultId in context.ExaminationResultIds

                        where medicalrecord.MedicalRecordId == id
                        where medicalrecord.Patient.PatientId == patient.PatientId
                        where examinationResultId.MedicalRecord.MedicalRecordId == medicalrecord.MedicalRecordId
                        where service.MedicalRecords.Any(x => x.MedicalRecordId == medicalrecord.MedicalRecordId)
                        where prescription.MedicalRecordId == medicalrecord.MedicalRecordId
                        select new { medicalrecord, patient , service,examinationResultId,prescription};

            if (!query.Any())
            {
                return output;
            }
            output.medicalRecordDto = query.First().medicalrecord;
            output.Service = query.First().service;
            output.Patient = query.First().patient;
            output.ExaminationResultDTO = query.First().examinationResultId;
            output.Prescription = query.First().prescription;
            return output;
        }


    }
}
