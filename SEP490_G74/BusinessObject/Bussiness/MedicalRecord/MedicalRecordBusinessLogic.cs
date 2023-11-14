using API.Common;
using HcsBE.Bussiness.Login;
using HcsBE.Dao.MedicalRecordDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.MedicalRecord
{
    public class MedicalRecordBusinessLogic
    {
        public List<MedicalRecordDaoOutputDto> GetListMedicalRecord()
        {
            MedicalRecordDao dao = new MedicalRecordDao();
            var listMR = dao.MedicalRecordList();
            if (listMR == null)
            {
                return new List<MedicalRecordDaoOutputDto>();
            }
            return listMR;
        }


    }
}
