using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessObject.BusinessObject;
using DataAccess.IRepository;
namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private MemberManagementContext context = new MemberManagementContext();

        public void Delete(TblMember member)
        {
            context.TblMembers.Remove(member);
            context.SaveChanges();

        }

        public List<TblMember> GetList()
        {
            return context.TblMembers.ToList();
        }

        public TblMember getRow(string tendangnhap)
        {
            TblMember member = context.TblMembers.Where(m => m.Email == tendangnhap).FirstOrDefault();
            return member;
        }

        public TblMember GetRowByID(int memberID)
        {
            return context.TblMembers.Where(m => m.MemberId == memberID).FirstOrDefault();
        }

        public void Insert(TblMember member)
        {
            context.TblMembers.Add(member);
            context.SaveChanges();
        }

        public void Update(TblMember member)
        {
            int memberId = member.MemberId;
            TblMember newmember = GetRowByID(memberId);
            if(newmember != null)
            {
                newmember.MemberId = member.MemberId;
                newmember.Password = member.Password;
                newmember.Email = member.Email;
                newmember.CompanyName = member.CompanyName;
                newmember.City = member.City;
                newmember.Country = member.Country;

                context.SaveChanges();
            }
        }
        
    }
}
