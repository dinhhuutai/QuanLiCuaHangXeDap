using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quanlicuahangxedap
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        //Kiểm tra đăng nhập
        public bool loginSuccess = false;

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            // Mới load thì chưa đăng nhập nên loginsuccess = false
            if (loginSuccess == false)
            {
                AnHienMenu(loginSuccess);
            }
        }

        //Ẩn hiện các menu khi đăng nhập hoặc đăng xuất
        public void AnHienMenu(bool loginSuccess)
        {
            //Chưa đăng nhập
            if (loginSuccess == false)
            {
                mnsQuanLiDanhMuc.Visible = false; //Ẩn mục quản lí danh mục
                mnsQuanLiDonHang.Visible = false; // Ẩn mục quản lí đơn hàng
                đăngXuấtToolStripMenuItem.Visible = false; //Ẩn mục đăng xuất

                đăngNhậpToolStripMenuItem.Visible = true; // Hiện mục đăng nhập
            }
            else // Đăng nhập thành công
            {
                mnsQuanLiDanhMuc.Visible = true; //Hiện mục quản lí danh mục
                mnsQuanLiDonHang.Visible = true;// Hiện mục quản lí đơn hàng
                đăngXuấtToolStripMenuItem.Visible = true; //Hiện mục đăng xuất

                đăngNhậpToolStripMenuItem.Visible = false; // Ẩn mục đăng nhập
            }
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmLg = new frmLogin();
            //Hiện form đăng nhập
            frmLg.ShowDialog();
            //Thông báo trạng thái đăng nhập từ form Login
            loginSuccess = frmLg.loginSuccess;
            AnHienMenu(loginSuccess);
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult drt = MessageBox.Show(
                "Bạn có chắc chắn thoát?",
                "Hỏi thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if(drt == DialogResult.Yes)
            {
                Close();
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Thông báo trạng thái đăng xuất
            loginSuccess = false;
            AnHienMenu(loginSuccess);
        }

        private void storesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiện trang cửa hàng
            frmCuaHang frmCH = new frmCuaHang();
            frmCH.MdiParent = this;
            frmCH.Show();
        }

        private void staffsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiện trang nhân viên
            frmNhanVien frmNV = new frmNhanVien();
            frmNV.MdiParent = this;
            frmNV.Show();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //hiện trang Khách hàng
            frmKhachHang frmKH = new frmKhachHang();
            frmKH.MdiParent = this;
            frmKH.Show();
        }

        private void brandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //hiện trang nhãn hiệu
            frmNhanHieu frmNH = new frmNhanHieu();
            frmNH.MdiParent = this;
            frmNH.Show();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiện trang thể loại
            frmTheLoai frmTL = new frmTheLoai();
            frmTL.MdiParent = this;
            frmTL.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiện trang sản phẩm
            frmSanPham frmSP = new frmSanPham();
            frmSP.MdiParent = this;
            frmSP.Show();
        }

        private void đơnĐặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiện trang đơn hàng
            frmDonDatHang frmDDH = new frmDonDatHang();
            frmDDH.MdiParent = this;
            frmDDH.Show();
        }

        private void orderItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiện trang đặt hàng
            frmDatHang frmDH = new frmDatHang();
            frmDH.MdiParent = this;
            frmDH.Show();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiện trang cổ phiếu
            frmCoPhieu frmCP = new frmCoPhieu();
            frmCP.MdiParent = this;
            frmCP.Show();
        }
    }
}
