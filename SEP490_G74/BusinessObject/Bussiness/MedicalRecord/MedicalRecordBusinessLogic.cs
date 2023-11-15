using API.Common;
using AutoMapper;
using HcsBE.Bussiness.Login;
using HcsBE.Dao.MedicalRecordDAO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.MedicalRecord
{
    public class MedicalRecordBusinessLogic
    {
        private MedicalRecordDao dao = new MedicalRecordDao();
        public List<MedicalRecordDaoOutputDto> GetListMedicalRecord()
        {
            // khoi tao mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MedicalRCMapper());
            });
            var mapper = config.CreateMapper();

            List<API.Common.Entity.MedicalRecord> list = dao.MedicalRecordList();
            var output = mapper.Map<List<MedicalRecordDaoOutputDto>>(list);
            return output;
        }

        public MedicalRecordDaoOutputDto GetMedicalRecord(int id) {
            // khoi tao mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MedicalRCMapper());
            });
            var mapper = config.CreateMapper();

            var mr = dao.GetMedicalRecord(id);
            if (mr == null) return null;
            return mapper.Map<MedicalRecordDaoOutputDto>(mr);
        }
    }
}
