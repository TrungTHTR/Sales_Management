﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject.BusinessObject;
using DataAccess.Repository;
namespace SalesWinApp
{
    public partial class frmProduct : Form
    {
        TblMember loginMember = null;
        string function = null;
        private ProductRepository productDao = new ProductRepository();
        public frmProduct(TblMember member)
        {
            InitializeComponent();
            loginMember = member;
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Tài khoản :" + loginMember.Email;
            loadProduct();
            tool(false);
            btnEdit.Enabled = false;

            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void loadProduct()
        {
            dgvProduct.DataSource = productDao.GetList();
        }

        private void tool(Boolean check)
        {
            txtCategoryID.Enabled = check;
            txtProductID.Enabled = check;
            txtProductName.Enabled = check;
            txtWeight.Enabled = check;
            txtPrice.Enabled = check;
            txtStock.Enabled = check;
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tool(true);
            function = "Add";
            btnSave.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            tool(true);
            function = "Edit";
            btnSave.Enabled = true;
            txtProductID.Enabled = false;
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowindex = e.RowIndex;
                if (rowindex < 0 || rowindex >= dgvProduct.Rows.Count)
                {
                    throw new Exception("Chưa chọn đúng thông tin");
                }

                txtProductID.Text = dgvProduct.Rows[rowindex].Cells["ProductId"].Value.ToString();
                txtCategoryID.Text = dgvProduct.Rows[rowindex].Cells["CategoryId"].Value.ToString();
                txtProductName.Text = dgvProduct.Rows[rowindex].Cells["ProductName"].Value.ToString();
                txtWeight.Text = dgvProduct.Rows[rowindex].Cells["Weight"].Value.ToString();
                txtPrice.Text = dgvProduct.Rows[rowindex].Cells["UnitPrice"].Value.ToString();
                txtStock.Text = dgvProduct.Rows[rowindex].Cells["UnitslnStock"].Value.ToString();

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TblProduct product = new TblProduct();
                if (productDao.getRow(int.Parse(txtProductID.Text)) != null){
                    throw new Exception("Duplicate ID");
                }
                if (int.Parse(txtProductID.Text) <= 0)
                {
                    throw new Exception("Product ID lon hon 0");
                }
                if (productDao.getRowCategory(int.Parse(txtCategoryID.Text)) != null){
                    throw new Exception("Duplicate CategoryID");
                }
                product.ProductId = int.Parse(txtProductID.Text.Trim());
                product.CategoryId = int.Parse(txtCategoryID.Text.Trim());
                product.ProductName = txtProductName.Text.Trim();
                product.Weight = txtWeight.Text.Trim();
                product.UnitPrice = decimal.Parse(txtPrice.Text.Trim());
                product.UnitslnStock = int.Parse(txtStock.Text.Trim());

                switch (function)
                {
                    case "Add":
                        {
                            productDao.Insert(product);
                            loadProduct();
                            MessageBox.Show("Thêm thành công.", "Thông báo");
                            break;
                        }
                    case "Edit":
                        {
                            productDao.Update(product);
                            loadProduct();
                            MessageBox.Show("Sửa thành công.", "Thông báo");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(txtProductID.Text.Trim());
            TblProduct product = productDao.getRow(productId);
            productDao.Delete(product);
            MessageBox.Show("Xóa thành công.", "Thông báo");
            dgvProduct.DataSource = productDao.GetList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
                int productId = int.Parse(txtProductID.Text.Trim());
                loadSearch(productId);
  
        }
        private void loadSearch(int productId)
        {
            dgvProduct.DataSource = productDao.getListSearch(productId);
        }
    }
}
