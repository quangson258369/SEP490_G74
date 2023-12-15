using DataAccess.Entity;
using HcsBE.Dao.GenPassword;
using HcsBE.DTO;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.MemberDAO
{
    public class MemberDAO
    {
        private HcsContext context = new HcsContext();
        public MemberPagination GetListMember(int page = 1)
        {
            int pageSize = 3;
            var result = context.Users
           .SelectMany(u => u.Employees, (u, d) => new { User = u, Doctor = d })
           .SelectMany(ud => ud.Doctor.Contacts, (ud, c) => new { ud.User, ud.Doctor, Contact = c })
           .SelectMany(udc => udc.User.Roles, (udc, r) => new MemberDTO
           {
               MemberId = udc.Doctor.DoctorId,
               Name = udc.Contact.Name,
               Gmail= udc.User.Email,
               Phone = udc.Contact.Phone,
               RoleName = r.RoleName,
               Address = udc.Contact.Address
           })
           .ToList();
            var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalItemCount = result.Count();
            var viewModel = new MemberPagination
            {
                Members = pagedData,
                TotalItemCount = totalItemCount,
                PageNumber = pageSize
            };
            return viewModel;
        }
        public MemberDetailDTO GetMemberDetail(int id)
        {
            var result = context.Users
        .SelectMany(u => u.Employees, (u, d) => new { User = u, Doctor = d })
        .Where(x=>x.Doctor.DoctorId==id)
        .SelectMany(ud => ud.Doctor.Contacts, (ud, c) => new { ud.User, ud.Doctor, Contact = c })
        .SelectMany(udc => udc.User.Roles, (udc, r) => new MemberDetailDTO
        {
            MemberId = udc.Doctor.DoctorId, 
            Name = udc.Contact.Name,
            Gender = udc.Contact.Gender, 
            Gmail = udc.User.Email, 
            Phone = udc.Contact.Phone,
            RoleName = r.RoleName,
            Address = udc.Contact.Address,
            Dob = udc.Contact.Dob, 
            Status= (bool)udc.User.Status,
            ImageLink = udc.Contact.Img,
            UserId =udc.User.UserId,
            ServiceType=udc.User.Employees.FirstOrDefault().ServiceType.ServiceTypeName,
            
        })
        .FirstOrDefault();
            return result?? new MemberDetailDTO();
        }
        public List<RoleDTO> GetRoles()
        {
            var result =context.Roles.Select(role => new RoleDTO
            {
                RoleId =role.RoleId,
                RoleName= role.RoleName
            }).ToList();
            return result;
        }
        public string AddMember(MemberDetailDTO member)
        {
            var checkUserName = context.Users.Where(user => user.Email == member.Gmail);
            if (!checkUserName.Any())
            {
                PasswordGenerator passwordGenerator = new PasswordGenerator();
                string password = passwordGenerator.GenerateRandomPassword();
                var newUser = new User
                {
                    Email = member.Gmail,
                    Password = passwordGenerator.GetMD5Hash(password),
                    Status = true,
                };
                var newRole = context.Roles.SingleOrDefault(r => r.RoleId == int.Parse(member.RoleName));
                newUser.Roles.Add(newRole);
                context.Users.Add(newUser);
                context.SaveChanges();
                var maxUserId = context.Users.Max(u => u.UserId);
                var maxDoctorId = context.Employees.Max(u => u.DoctorId);

                var newDoctor = new Employee();
                if (int.Parse(member.RoleName) != 2)
                {
                    newDoctor = new Employee
                    {
                        DoctorId = maxDoctorId + 1,
                        UserId = maxUserId
                    };
                }
                else
                {
                    newDoctor = new Employee
                    {
                        DoctorId = maxDoctorId + 1,
                        ServiceTypeId = int.Parse(member.ServiceType),
                        UserId = maxUserId
                    };
                }


                var maxContactId = context.Contacts.Max(u => u.CId);
                var newContact = new Contact
                {
                    CId = maxContactId + 1,
                    Name = member.Name,
                    Gender = (bool)member.Gender,
                    Phone = member.Phone,
                    Dob = (DateTime)member.Dob,
                    Address = member.Address,
                    Img = member.ImageLink,
                    PatientId = null,
                    DoctorId = maxDoctorId + 1
                };


                context.Employees.Add(newDoctor);
                context.Contacts.Add(newContact);
                context.SaveChanges();
                return password;
            }
            else
            {
                return "exist";
            }
            
        }
        public bool EditMember(MemberDetailDTO member)
        {
            var memberToUpdate = context.Employees.SingleOrDefault(u => u.DoctorId == member.MemberId);
            if (memberToUpdate != null)
            {
                if (member.ServiceType!=null)
                {
                    memberToUpdate.ServiceTypeId = Int32.Parse(member.ServiceType);
                }               
            }
            var userIdToUpadte = memberToUpdate.UserId;
            var userToUpadte= context.Users.Include(u => u.Roles).SingleOrDefault(u => u.UserId == userIdToUpadte); ;
            if (userToUpadte != null)
            {
                //userToUpadte.Email = member.Gmail;
                //userToUpadte.Roles.FirstOrDefault().RoleId = Int32.Parse(member.RoleName);
            }
            var contactToUpdate = context.Contacts.FirstOrDefault(u => u.DoctorId == member.MemberId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = member.Name;
                contactToUpdate.Gender = (bool)member.Gender;
                contactToUpdate.Phone = member.Phone;
                contactToUpdate.Dob = (DateTime)member.Dob;
                contactToUpdate.Address = member.Address;
                contactToUpdate.Img = member.ImageLink;
            }
            context.SaveChanges();
            return true;
        }
        public List<Role> rolebyid(int id)
        {
            var user = context.Users
            .Include(u => u.Roles)
            .SingleOrDefault(u => u.UserId == id);
            var list= user.Roles.ToList();
            return list;
        }
        public int GetDoctorId(int userId)
        {
            var doctorId = context.Employees
                .Join(context.Users,
                employee => employee.UserId,
                user => user.UserId,
                (employee, user) => new { Employee = employee, User = user })
                .Where(e => e.User.UserId == userId)
                .Select(e => e.Employee.DoctorId)
                .FirstOrDefault();
            return doctorId;
        }
    }
}
