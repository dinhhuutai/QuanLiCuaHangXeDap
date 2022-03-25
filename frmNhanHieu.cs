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
    public partial class frmNhanHieu : Form
    {
        public frmNhanHieu()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;

        //Lúc load lên
        private void frmNhanHieu_Load(object sender, EventArgs e)
        { 
            //Hiện trang chủ và ẩn các trang còn lại.
            picHome.Visible = true;
            gvNhanHieu.Visible = false;
            pnThem.Visible = false;
            pnXoa.Visible = false;
            pnChinhSua.Visible = false;
            LoadDanhSachNhanHieu();
        }

        //click btn danh sách
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            //Hiện trang danh sách và ẩn các trang còn lại
            gvNhanHieu.Visible = true;
            picHome.Visible = false;
            pnThem.Visible = false;
            pnXoa.Visible = false;
            pnChinhSua.Visible = false;
            LoadDanhSachNhanHieu();
        }

        //Tải danh sách lên
        private void LoadDanhSachNhanHieu()
        {
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlDataAdapter adapter = new SqlDataAdapter("select * from brands", conn);
            DataTable data = new DataTable();
            adapter.Fill(data);
            gvNhanHieu.DataSource = data;
        }

        //click btn thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            //Hiện trang thêm 
            pnThem.Visible = true;
            pnXoa.Visible = false;
            pnChinhSua.Visible = false;
            picHome.Visible = false;
            gvNhanHieu.Visible = false;
            txtIdThem.Focus();
        }

        //click btn Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Hiện trang Xóa
            picHome.Visible = false;
            gvNhanHieu.Visible = false;
            pnChinhSua.Visible = false;
            pnThem.Visible = true;
            pnXoa.Visible = true;
            TaiThongTinLenComboBoxXoaHoacChinhSua(1);
            txtIdXoa.Focus();
        }

        //Tải thông tin lên comboBox Xóa.
        private void TaiThongTinLenComboBoxXoaHoacChinhSua(int n)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from brands";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            if (n == 1)
            {
                cboChonXoa.Items.Clear();
            }
            else if (n == 2)
            {
                cboIdChinhSua.Items.Clear();
            }
            while (reader.Read())
            {
                if (n == 1)
                {
                    string tt = reader.GetString(0).Trim() + " - " + reader.GetString(1);
                    cboChonXoa.Items.Add(tt);
                }
                else if (n == 2)
                {
                    cboIdChinhSua.Items.Add(reader.GetString(0).Trim());
                }
            }
            reader.Close();
        }

        //click btn Chỉnh Sửa
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            //Hiện trang Chỉnh Sửa
            picHome.Visible = false;
            gvNhanHieu.Visible = false;
            pnThem.Visible = true;
            pnXoa.Visible = true;
            pnChinhSua.Visible = true;
            TaiThongTinLenComboBoxXoaHoacChinhSua(2);
        }

        //Đổi màu khi hover 
        private void btnDanhSach_MouseHover(object sender, EventArgs e)
        {
            btnDanhSach.ForeColor = Color.Black;
        }

        //Đổi màu khi di chuyển chuột ra ngoài
        private void btnDanhSach_MouseLeave(object sender, EventArgs e)
        {
            btnDanhSach.ForeColor = Color.White;
        }

        //Đổi màu khi hover vào btn thêm
        private void btnThem_MouseHover(object sender, EventArgs e)
        {
            btnThem.ForeColor = Color.Black;
        }

        private void btnThem_MouseLeave(object sender, EventArgs e)
        {
            btnThem.ForeColor = Color.White;
        }

        //Đổi màu khi hover vào btn xóa
        private void btnXoa_MouseHover(object sender, EventArgs e)
        {
            btnXoa.ForeColor = Color.Black;
        }

        private void btnXoa_MouseLeave(object sender, EventArgs e)
        {
            btnXoa.ForeColor = Color.White;
        }

        //Đổi màu khi hover vào btn chỉnh sửa
        private void btnChinhSua_MouseHover(object sender, EventArgs e)
        {
            btnChinhSua.ForeColor = Color.Black;
        }

        private void btnChinhSua_MouseLeave(object sender, EventArgs e)
        {
            btnChinhSua.ForeColor = Color.White;
        }

        //kiểm tra có trùng ID không để thêm.
        private void txtIdThem_Leave(object sender, EventArgs e)
        {
            //mở kết nối
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from brands where brand_id=" + txtIdThem.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                //Nếu có dữ liệu là trùng ID
                if (reader.Read())
                {
                    MessageBox.Show("Lỗi: Trùng ID.");
                    txtIdThem.Text = "";
                }
                reader.Close();
            }
            catch (Exception)
            {

            }
        }

        //Lưu thông tin khi đã nhập xong.
        private void btnLuuThem_Click(object sender, EventArgs e)
        {
            if(txtIdThem.Text == "" || txtNameThem.Text == "")//ID và tên không để trống khi thêm
            {
                MessageBox.Show("ID hoặc tên không được để trống.");
                return;
            }
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into brands(brand_id,brand_name) " +
                "values (@id,@name)";
            cmd.Connection = conn;
            cmd.Parameters.Add("@id", SqlDbType.Char).Value = txtIdThem.Text;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtNameThem.Text;

            int ret = cmd.ExecuteNonQuery();
            if(ret > 0)
            {
                MessageBox.Show("Thêm thành công.");
                txtIdThem.Text = "";
                txtNameThem.Text = "";
                txtIdThem.Focus();
            }
        }

        //Hủy hoạt động thêm.
        private void btnHuyThem_Click(object sender, EventArgs e)
        {
            txtIdThem.Text = "";
            txtNameThem.Text = "";
        }

        //kiểm tra có trùng ID không để xóa.
        private void txtIdXoa_Leave(object sender, EventArgs e)
        {
            KiemTraIdKhiXoaHoacChinhSua(1);
        }

        // Kiểm tra ID có trùng không để Xóa hoặc Chỉnh sửa.
        private void KiemTraIdKhiXoaHoacChinhSua(int n)
        {
            //mở kết nối
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                if (n == 1)
                {
                    cmd.CommandText = "select * from brands where brand_id=" + txtIdXoa.Text;
                }
                else if (n == 2)
                {
                    cmd.CommandText = "select * from brands where brand_id=" + cboIdChinhSua.Text;
                }
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                //Nếu có dữ liệu là trùng ID
                if (!reader.Read())
                {
                    MessageBox.Show("ID không tồn tại.");
                    if (n == 1)
                    {
                        txtIdXoa.Text = "";
                    }
                    else if (n == 2)
                    {
                        cboIdChinhSua.Text = "";
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {

            }
        }

        //hủy xóa.
        private void btnHuyXoa_Click(object sender, EventArgs e)
        {
            txtIdXoa.Text = "";
            cboChonXoa.Text = "";
        }

        private void btnXoaXoa_Click(object sender, EventArgs e)
        {
            //Kiểm tra đã nhập ID chưa.
            if(txtIdXoa.Text == "" && cboChonXoa.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn ID.");
                return;
            }

            DialogResult dia = MessageBox.Show(
                "Bạn có chắc chắn xóa.",
                "Hỏi Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dia == DialogResult.Yes)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                if (cboChonXoa.Text == "")
                {
                    cmd.CommandText = "delete from brands where brand_id=" + txtIdXoa.Text;
                }
                else if (txtIdXoa.Text == "")
                {
                    string[] arr = cboChonXoa.Text.Split('-');
                    string id = arr[0].Trim();
                    cmd.CommandText = "delete from brands where brand_id=" + id;
                }
                cmd.Connection = conn;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Xóa Thành Công.");
                    txtIdXoa.Text = "";
                    cboChonXoa.Text = "";
                    TaiThongTinLenComboBoxXoaHoacChinhSua(1);
                }
            }
        }

        //kiểm tra có trùng ID không để Xoa.
        private void cboChonXoa_Leave(object sender, EventArgs e)
        {
            //mở kết nối
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from brands where brand_id=" + cboChonXoa.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("ID không tồn tại.");
                    cboChonXoa.Text = "";
                }
                reader.Close();
            }
            catch (Exception)
            {

            }
        }

        //Cho cboChon bằng rỗng nếu chọn nhập thông tin.
        private void txtIdXoa_Click(object sender, EventArgs e)
        {
            cboChonXoa.Text = "";
        }

        //Cho txetbox ID bằng rỗng nếu chọn thông tin có sẵn.
        private void cboChonXoa_Click(object sender, EventArgs e)
        {
            txtIdXoa.Text = "";
        }

        //kiểm tra có trùng ID không để chinh sua.
        private void cboIdChinhSua_Leave(object sender, EventArgs e)
        {
            KiemTraIdKhiXoaHoacChinhSua(2);
        }

        //Lưu thông tin khi bấm chỉnh sửa
        private void btnCapNhatChinhSua_Click(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update brands set brand_name='" + txtNameChinhSua.Text + "' where brand_id='" + cboIdChinhSua.Text + "'";
            cmd.Connection = conn;

            int ret = cmd.ExecuteNonQuery();
            if(ret > 0)
            {
                MessageBox.Show("Chỉnh sửa thành công.");
                cboIdChinhSua.Text = "";
                txtNameChinhSua.Text = "";
            }
        }

        //Hủy hoạt động chỉnh sửa
        private void btnHuyChinhSua_Click(object sender, EventArgs e)
        {
            cboIdChinhSua.Text = "";
            txtNameChinhSua.Text = "";
        }
    }
}
