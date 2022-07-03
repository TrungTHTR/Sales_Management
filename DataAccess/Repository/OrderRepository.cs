using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessObject.BusinessObject;
using DataAccess.IRepository;

namespace DataAccess.Repository
{

    public class OrderRepository : IOrderRepository
    {
        private MemberManagementContext context = new MemberManagementContext();

        public void Delete(TblOrder od)
        {
            context.TblOrders.Remove(od);
            context.SaveChanges();
        }

        public List<TblOrder> GetList()
        {
            return context.TblOrders.ToList();
        }

        public TblOrder getRow(int odID)
        {
            TblOrder orders = context.TblOrders.Where(m => m.OrderId == odID).FirstOrDefault();
            return orders;
        }

        public TblOrder GetRowByID()
        {
            throw new NotImplementedException();
        }

        public void Insert(TblOrder od)
        {   
            if(od != null)
            {
                context.TblOrders.Add(od);
                context.SaveChanges();
            }
        }

        public void Update(TblOrder od)
        {
            int id = od.OrderId;
            TblOrder newOrder = getRow(id);
            if (newOrder != null)
            {
                newOrder.OrderId = od.OrderId;
                newOrder.MemberId = od.MemberId;
                newOrder.OrderDate = od.OrderDate;
                newOrder.RequiredDate = od.RequiredDate;
                newOrder.ShippedDate = od.ShippedDate;
                newOrder.Freight = od.Freight;
                context.SaveChanges();
            }
        }

        public List<TblOrder> GetListReport(DateTime startDate, DateTime endDate)
        {
            return context.TblOrders.Where( m => m.OrderDate >= startDate && m.OrderDate <= endDate).OrderByDescending(m => m.OrderDate).ToList();
        }
    }
}
