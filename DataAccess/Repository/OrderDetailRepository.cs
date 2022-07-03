using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessObject.BusinessObject;
using DataAccess.IRepository;
namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private MemberManagementContext context = new MemberManagementContext();

        public void Delete(TblOrderDetail od)
        {
            context.TblOrderDetails.Remove(od);
            context.SaveChanges();
        }

        public List<TblOrderDetail> GetList()
        {
            return context.TblOrderDetails.ToList();
        }

        public List<TblOrderDetail> getListByID(int orderID)
        {           
            return context.TblOrderDetails.Where(m => m.OrderId == orderID).ToList(); ;
        }

        public TblOrderDetail GetRowByID()
        {
            throw new NotImplementedException();
        }

        public void Insert(TblOrderDetail od)
        {
            if (od != null)
            {
                context.TblOrderDetails.Add(od);
                context.SaveChanges();
            }
        }

        public void Update(TblOrderDetail od)
        {
            throw new NotImplementedException();
        }

        TblOrderDetail IOrderDetailRepository.getRow(int oddID)
        {
            throw new NotImplementedException();
        }
    }
}
