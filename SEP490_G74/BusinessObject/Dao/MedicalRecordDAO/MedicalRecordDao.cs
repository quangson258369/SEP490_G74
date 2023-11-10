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
                        from contact in context.Contacts
                        from user in context.Users
                        from examinationResultId in context.ExaminationResultIds
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
    }
}
