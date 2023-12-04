using API.Common;
using AutoMapper;
using DataAccess.Entity;
using HcsBE.Dao.ExaminationResultDAO;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.ExamResult
{
    public class ExamResultLogic
    {
        private ExamResultDao dao = new ExamResultDao();
        private IMapper mapper;

        public ExamResultLogic (IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ExaminationResultIdMRDTO> getList()
        {
            List<ExaminationResultId> list = dao.GetList();
            var output = mapper.Map<List<ExaminationResultIdMRDTO>>(list);
            if (list == null)
            {
                return new List<ExaminationResultIdMRDTO>()
                {
                    new ExaminationResultIdMRDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public ExaminationResultIdMRDTO GetExamination(int id)
        {
            ExaminationResultId er = dao.GetExamResult(id);
            if(er == null)
            {
                return new ExaminationResultIdMRDTO()
                {
                    ExceptionMessage = ConstantHcs.NotFound,
                    ResultCd = ConstantHcs.BussinessError
                };
            }
            return mapper.Map<ExaminationResultIdMRDTO>(er);
        }

        public bool Update(ExaminationResultIdMRDTO exam)
        {
            var e = mapper.Map<ExaminationResultId>(exam);
            var stt = dao.Update(e);
            return stt;
        }

        public bool Delete(int id)
        {
            return dao.DeleteMedicalRecord(id);
        }
        
        public bool Add(ExaminationResultIdMRDTO exam)
        {
            var e = mapper.Map<ExaminationResultId>(exam);
            var stt = dao.AddExamResult(e);
            return stt;
        }
    }
}
