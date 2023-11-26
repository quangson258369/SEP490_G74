using DataAccess.Entity;
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
           .SelectMany(u => u.Doctors, (u, d) => new { User = u, Doctor = d })
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
        .SelectMany(u => u.Doctors, (u, d) => new { User = u, Doctor = d })
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
            DoctorSpecialize = udc.Doctor.DoctorSpecialize, 
            Dob = udc.Contact.Dob, 
            ImageLink = udc.Contact.Img,
            UserId =udc.User.UserId
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
        public bool AddMember(MemberDetailDTO member)
        {
            var newUser = new User
            {
                Email = member.Gmail,
                Password = "123456"
            };
            var maxUserId = context.Users.Max(u => u.UserId);
            var maxDoctorId = context.Doctors.Max(u => u.DoctorId);
            var newDoctor = new Doctor
            {
                DoctorId= maxDoctorId + 1,
                DoctorSpecialize = member.DoctorSpecialize,
                ServiceTypeId = 1,
                UserId = maxUserId
            };
            var maxContactId = context.Contacts.Max(u => u.CId);
            var newContact = new Contact
            {
                CId = maxContactId + 1,
                Name = member.Name,
                Gender = (bool)member.Gender,
                Phone=member.Phone,
                Dob= (DateTime)member.Dob,
                Address=member.Address,
                Img=member.ImageLink,
                PatientId=null,
                DoctorId= maxDoctorId+1
            };
            var newRole = context.Roles.SingleOrDefault(r => r.RoleId == int.Parse(member.RoleName));
            newUser.Roles.Add(newRole);
            context.Users.Add(newUser);
            context.Doctors.Add(newDoctor);
            context.Contacts.Add(newContact);

            context.SaveChanges();
            return true;
        }
        public bool EditMember(MemberDetailDTO member)
        {
            var memberToUpdate = context.Doctors.SingleOrDefault(u => u.DoctorId == member.MemberId);
            if (memberToUpdate != null)
            {
                memberToUpdate.DoctorSpecialize = member.DoctorSpecialize;
            }
            var userIdToUpadte = memberToUpdate.UserId;
            var userToUpadte= context.Users.Include(u => u.Roles).SingleOrDefault(u => u.UserId == userIdToUpadte); ;
            if (userToUpadte != null)
            {
                userToUpadte.Email = member.Gmail;
                //
                //var newRole = context.Roles.SingleOrDefault(r => r.RoleId == int.Parse(member.RoleName));
                //userToUpadte.Roles.Add(newRole);
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
    }
}
