using DataAccess.Entity;
using HcsBE.Dao.MemberDAO;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.Member
{
    public class MemberLogic
    {
        private MemberDAO memberDAO= new MemberDAO();
        public MemberPagination GetListMembers(int page)
        {
            MemberPagination listMember = memberDAO.GetListMember(page);
            return listMember;
        }
        public MemberDetailDTO GetMembersDetail(int id)
        {
            MemberDetailDTO memberDetail = memberDAO.GetMemberDetail(id);
            return memberDetail;
        }
        public List<RoleDTO> GetRoles()
        {
            List<RoleDTO> listRole = memberDAO.GetRoles();
            return listRole;
        }
        public string AddMember(MemberDetailDTO member)
        {
            string password = memberDAO.AddMember(member);
            return password;
        }
        public bool EditMember(MemberDetailDTO member)
        {
            bool rowAffected = memberDAO.EditMember(member);
            return rowAffected;
        }
        public List<Role> rolebyid(int id)
        {
            List<Role> list = memberDAO.rolebyid(id); 
            return list;
        }
    }
}
