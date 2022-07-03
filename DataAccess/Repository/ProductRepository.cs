﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.BusinessObject;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private MemberManagementContext context = new MemberManagementContext();

        public void Delete(TblProduct member)
        {
            context.TblProducts.Remove(member);
            context.SaveChanges();
        }

        public List<TblProduct> GetList()
        {
            return context.TblProducts.ToList();
        }

        public List<TblProduct> getListSearch(int productID)
        {
            return context.TblProducts.Where(m => m.ProductId == productID).ToList();
        }

        public TblProduct getRow(int productID)
        {
            TblProduct product = context.TblProducts.Where(m => m.ProductId == productID).FirstOrDefault();
            return product;
        }

        public TblProduct getRowCategory(int categoryID)
        {
            TblProduct product = context.TblProducts.Where(m => m.CategoryId == categoryID).FirstOrDefault();
            return product;
        }

        public void Insert(TblProduct product)
        {
            context.TblProducts.Add(product);
            context.SaveChanges();
        }

        public void Update(TblProduct product)
        {
            int id = product.ProductId;
            TblProduct newproduct = getRow(id);
            if (newproduct != null)
            {
                newproduct.ProductName = product.ProductName;
                newproduct.CategoryId = product.CategoryId;
                newproduct.Weight = product.Weight;
                newproduct.UnitPrice = product.UnitPrice;
                newproduct.UnitslnStock = product.UnitslnStock;
                context.SaveChanges();
            }

        }
    }
}
