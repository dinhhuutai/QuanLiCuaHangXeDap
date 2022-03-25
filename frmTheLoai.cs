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
    public partial class frmTheLoai : Form
    {
        public frmTheLoai()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;

        //Đổi màu khi hover btn Danh sách
        private void btnDanhSach_MouseHover(object sender, EventArgs e)
        {
            btnDanhSach.ForeColor = Color.Black;
        }

        private void btnDanhSach_MouseLeave(object sender, EventArgs e)
        {
            btnDanhSach.ForeColor = Color.White;
        }

        //Đổi màu khi hover btn Thêm
        private void btnThem_MouseHover(object sender, EventArgs e)
        {
            btnThem.ForeColor = Color.Black;
        }

        private void btnThem_MouseLeave(object sender, EventArgs e)
        {
            btnThem.ForeColor = Color.White;
        }

        //Đổi màu khi hover btn Xóa
        private void btnXoa_MouseHover(object sender, EventArgs e)
        {
            btnXoa.ForeColor = Color.Black;
        }

        private void btnXoa_MouseLeave(object sender, EventArgs e)
        {
            btnXoa.ForeColor = Color.White;
        }

        //Đổi màu khi hover Chỉnh Sửa
        private void btnChinhSua_MouseHover(object sender, EventArgs e)
        {
            btnChinhSua.ForeColor = Color.Black;
        }

        private void btnChinhSua_MouseLeave(object sender, EventArgs e)
        {
            btnChinhSua.ForeColor = Color.White;
        }

        //Load form lên
        private void frmTheLoai_Load(object sender, EventArgs e)
        {
            //Kết nối SQL
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            //Mở kết nối
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            //Ẩn hiện các trang
            picHome.Visible = true;
            gvTheLoai.Visible = false;
            pnThem.Visible = false;
            pnXoa.Visible = false;
            pnChinhSua.Visible = false;
        }

        //Click vào btn danh sách
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            //Ẩn hiện các trang
            gvTheLoai.Visible = true;
            picHome.Visible = false;
            pnThem.Visible = false;
            pnXoa.Visible = false;
            pnChinhSua.Visible = false;

            //Load danh sách các thể loại
            LoadDanhSachTheLoai();
        }

        //Tải dữ liệu
        private void LoadDanhSachTheLoai()
        {
            //Mở kết nối
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from categories", conn);
            DataTable data = new DataTable();
            adapter.Fill(data);
            gvTheLoai.DataSource = data;
        }

        //Click vào btn Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            //Ẩn hiện các trang
            pnThem.Visible = true;
            gvTheLoai.Visible = false;
            picHome.Visible = false;
            pnXoa.Visible = false;
            pnChinhSua.Visible = false;
            txtIdThem.Focus();
        }

        //Kiểm tra ID Thêm có trùng không
        private void txtIdThem_Leave(object sender, EventArgs e)
        {
            KiemTraID(1, 0, 0);
        }

        private void KiemTraID(int v1, int v2, int v3)
        {
            // v1 = 1: Thêm,  v1 = 2: Xóa hoặc Sửa
            // v2 = 1: Xóa, v2 = 2: Sửa
            // Ở trang xóa v3 = 1: dùng cho textbox, v3 = 2 dùng cho comboBox 
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                if (v1 == 1) //Them
                {
                    cmd.CommandText = "select * from categories where category_id=" + txtIdThem.Text;
                }
                else if (v1 == 2)//Xoa hoac Sua
                {
                    if (v2 == 1)// Xoa
                    {
                        if (v3 == 1)// ở textbox
                        {
                            cmd.CommandText = "select * from categories where category_id=" + txtIdXoa.Text;
                        }
                        else if (v3 == 2)//ở comboBox
                        {
                            cmd.CommandText = "select * from categories where category_id=" + cboChonXoa.Text;
                        }
                    }
                    else if (v2 == 2)//Sua
                    {
                        cmd.CommandText = "select * from categories where category_id=" + cboIdChinhSua.Text;
                    }
                }
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();
                if (v1 == 1)//Them
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("Lỗi: Trùng ID.");
                        txtIdThem.Text = "";
                    }
                }
                else if (v1 == 2)//Xoa hoac Sua
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("ID không tồn tại.");
                        txtIdXoa.Text = "";
                        cboChonXoa.Text = "";
                        cboIdChinhSua.Text = "";
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {

            }
        }

        //Khi bấm Lưu ở trang Thêm 
        private void btnLuuThem_Click(object sender, EventArgs e)
        {
            //Kiểm tra đã nhập ID với Name chưa 
            if(txtIdThem.Text == "" || txtNameThem.Text == "")
            {
                MessageBox.Show("Tên hoặc ID không để trống.");
            }
            //Mở kết nối
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into categories(category_id,category_name) " +
                "values ('" + txtIdThem.Text + "','" + txtNameThem.Text + "')";
            cmd.Connection = conn;

            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                MessageBox.Show("Thêm thành công.");
                txtIdThem.Text = "";
                txtNameThem.Text = "";
                txtIdThem.Focus();
            }
        }

        //Khi bấm hủy ở trang Thêm 
        private void btnHuythem_Click(object sender, EventArgs e)
        {
            //Cho cac TextBox bằng rỗng.
            txtIdThem.Text = "";
            txtNameThem.Text = "";
        }

        //Click vào btn Xóa 
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Tải danh sách lên comboBox Xóa
            TaiDataLenComboBox(1);

            //Ẩn hiện các trang
            pnXoa.Visible = true;
            pnThem.Visible = true;
            gvTheLoai.Visible = false;
            picHome.Visible = false;
            pnChinhSua.Visible = false;
            txtIdXoa.Focus();
        }

        private void TaiDataLenComboBox(int v)
        {
            // v = 1: Xóa,  v = 2: Sửa
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from categories";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            if (v == 1)// ComboBox ben Xoa
            {
                cboChonXoa.Items.Clear();
                while(reader.Read())
                {
                    cboChonXoa.Items.Add(reader.GetString(0).Trim());
                }
            }
            else if(v == 2)
            {
                cboIdChinhSua.Items.Clear();
                while (reader.Read())
                {
                    cboIdChinhSua.Items.Add(reader.GetString(0).Trim());
                }
            }
            reader.Close();
        }

        //Kiểm tra ID Xóa có trùng không ở textboxID Xóa
        private void txtIdXoa_Leave(object sender, EventArgs e)
        {
            KiemTraID(2, 1, 1);
        }

        //Kiểm tra ID Xóa có trùng không ở comboBox ID Xóa
        private void cboChonXoa_Leave(object sender, EventArgs e)
        {
            KiemTraID(2, 1, 2);
        }

        //Click vào textbox ID xóa 
        private void txtIdXoa_Click(object sender, EventArgs e)
        {
            cboChonXoa.Text = "";
        }

        //Click vào comboBox ID xóa
        private void cboChonXoa_Click(object sender, EventArgs e)
        {
            txtIdXoa.Text = "";
        }

        //Khi bấm xóa ở trang Xóa
        private void btnXoaXoa_Click(object sender, EventArgs e)
        {
            //Kiểm tra đã nhập ID chưa 
            if (txtIdXoa.Text == "" && cboChonXoa.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ID.");
            }
            //Hỏi người dùng có chắc chắn xóa không 
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
                    cmd.CommandText = "delete from categories where category_id=" + txtIdXoa.Text;
                }
                if (txtIdXoa.Text == "")
                {
                    cmd.CommandText = "delete from categories where category_id=" + cboChonXoa.Text;
                }
                cmd.Connection = conn;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    txtIdXoa.Text = "";
                    cboChonXoa.Text = "";
                    TaiDataLenComboBox(1);
                }
            }
        }

        //Khi bấm hủy ở trang Xóa 
        private void btnHuyXoa_Click(object sender, EventArgs e)
        {
            txtIdXoa.Text = "";
            cboChonXoa.Text = "";
        }

        //Click vào btn chỉnh sửa
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            //Tải danh sách lên comboBox sửa
            TaiDataLenComboBox(2);

            //Ẩn hiện các trang
            pnThem.Visible = true;
            pnXoa.Visible = true;
            pnChinhSua.Visible = true;
            gvTheLoai.Visible = false;
            picHome.Visible = false;
        }

        //Kiểm tra ID có trùng không ở comboBox ID chỉnh sửa
        private void cboIdChinhSua_Leave(object sender, EventArgs e)
        {
            KiemTraID(2, 2, 0);
        }

        //Khi bấm Cập nhật ở trang chỉnh sửa 
        private void btnCapNhatChinhSua_Click(object sender, EventArgs e)
        {
            //Kiểm tra đã nhập ID chưa 
            if (cboIdChinhSua.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ID.");
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update categories set category_name='" + txtNameChinhSua.Text +
                "' where category_id=" + cboIdChinhSua.Text;
            cmd.Connection = conn;

            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                MessageBox.Show("Chỉnh sửa thành công.");
                cboIdChinhSua.Text = "";
                txtNameChinhSua.Text = "";
                TaiDataLenComboBox(2);
            }
        }

        //Khi bấm hủy ở trang chỉnh sửa
        private void btnHuyChinhSua_Click(object sender, EventArgs e)
        {
            cboIdChinhSua.Text = "";
            txtNameChinhSua.Text = "";
        }
    }
}
