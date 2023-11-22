﻿using API.Common;
using AutoMapper;
using HcsBE.Bussiness.MedicalRecord;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.Dao.PatientDao;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.Patient
{
    public class PatientLogic
    {
        private PatientDAO dao = new PatientDAO();

        public List<PatientDTO> ListPatient()
        {
            // khoi tao mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PatientMapper());
            });
            var mapper = config.CreateMapper();
            List<DataAccess.Entity.Patient> list = dao.ListPatients();
            var output =  mapper.Map<List<PatientDTO>>(list);
            if (list == null)
            {
                return new List<PatientDTO>()
                {
                    new PatientDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public PatientDTO GetPatientGetId(int id)
        {
            // khoi tao mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PatientMapper());
            });
            var mapper = config.CreateMapper();
            DataAccess.Entity.Patient patient = dao.GetPatientById(id);
            if (patient == null)
            {
                return new PatientDTO()
                {
                    ExceptionMessage = ConstantHcs.NotFound,
                    ResultCd = ConstantHcs.BussinessError
                };
            }
            return mapper.Map<PatientDTO>(patient);
        }

        public bool Update(PatientModify entity)
        {
            // khoi tao mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PatientMapper());
            });
            var mapper = config.CreateMapper();
            var p = mapper.Map<DataAccess.Entity.Patient>(entity);
            var status = dao.UpdatePatient(p);
            return status;
        }
        public bool Add(PatientModify entity)
        {
            // khoi tao mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PatientMapper());
            });
            var mapper = config.CreateMapper();
            var p = mapper.Map<DataAccess.Entity.Patient>(entity);
            var status = dao.AddPatient(p);
            return status;
        }
        
        public bool Delete(int id)
        {
            var status = dao.DeletePatient(id);
            return status;
        }
    }
}
