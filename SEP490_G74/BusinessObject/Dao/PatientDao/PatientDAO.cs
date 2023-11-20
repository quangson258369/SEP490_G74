using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.PatientDao
{
    public class PatientDAO
    {
        private HcsContext context = new HcsContext();

        public List<Patient> ListPatients()
        {
            /*var query = context.Set<Patient>().AsQueryable();
            // Disable auto-includes temporarily
            query = query.IgnoreAutoIncludes();
            // Include only necessary entities*/

            var query = context.Patients.ToList();
            return query;
        }
    }
}
