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
    public partial class frmDonDatHang : Form
    {
        public frmDonDatHang()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataTable data = null;
        int ktbtn = 0; //Biến kiểm tra đang thêm hay chỉnh sửa thông tin.

        //Load danh sách đơn hàng lên GV
        private void frmDonDatHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachDonHang();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void LoadDanhSachDonHang()
        {
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            //mở kết nối 
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            adapter = new SqlDataAdapter("select * from orders", conn);
            data = new DataTable();
            adapter.Fill(data);
            gvDonDatHang.DataSource = data;
            LoadIdCuaHang();
            LoadIdKhachHang();
            LoadIdNhanVien();
        }

        //Tải ID khách hàng lên combobox
        private void LoadIdKhachHang()
        {
            //Mo ket noi
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //Truy vấn đên bảng khách hàng
            cmd.CommandText = "Select * from customers";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            //Xóa danh sách trước khi tải lại
            cboIdKhachHang.Items.Clear();
            while (reader.Read())
            {
                cboIdKhachHang.Items.Add(reader.GetString(0).Trim());
            }
            reader.Close();
        }

        //Tải ID cửa hàng lên combobox
        private void LoadIdCuaHang()
        {
            //Mo ket noi
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //Truy vấn đên bảng khách hàng
            cmd.CommandText = "Select * from stores";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            //Xóa danh sách trước khi tải lại
            cboIdCuaHang.Items.Clear();
            while (reader.Read())
            {
                cboIdCuaHang.Items.Add(reader.GetString(0).Trim());
            }
            reader.Close();
        }

        //Tải ID Nhân viên lên combobox
        private void LoadIdNhanVien()
        {
            //Mo ket noi
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //Truy vấn đên bảng khách hàng
            cmd.CommandText = "Select * from staffs";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            //Xóa danh sách trước khi tải lại
            cboIdNhanVien.Items.Clear();
            while (reader.Read())
            {
                cboIdNhanVien.Items.Add(reader.GetString(0).Trim());
            }
            reader.Close();
        }

        //Click vào thêm 
        private void btnThem_Click(object sender, EventArgs e)
        {
            ktbtn = 1;
            AnHienCacMenu();
            CacTextBoxRong();
            txtId.Focus();
        }

        private void AnHienCacMenu()
        {
            if (ktbtn == 1)//Thêm
            {
                //hiện các menu thêm, lưu, hủy
                btnThem.Enabled = true;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
                //ẩn các menu sửa, xóa
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            else if (ktbtn == 2)//Sửa
            {
                //hiện các menu sửa, lưu, hủy
                btnSua.Enabled = true;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
                //ẩn các menu thêm, xóa
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        //Cho cac textbox = rỗng.
        private void CacTextBoxRong()
        {
            txtId.Text = "";
            txtTinhTrangDatHang.Text = "";
            dtpNgayDatHang.Value = DateTime.Now;
            dtpNgayVanChuyen.Value = DateTime.Now;
            dtpNgayYeuCau.Value = DateTime.Now;
            cboIdKhachHang.Text = "";
            cboIdNhanVien.Text = "";
            cboIdCuaHang.Text = "";
        }

        //Click vào Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            ktbtn = 2;
            AnHienCacMenu();
        }

        //Click vào hủy 
        private void btnHuy_Click(object sender, EventArgs e)
        {
            CacTextBoxRong();
            //hiện các menu thêm, sửa, xóa
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            //ẩn các menu lưu, hủy 
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            ktbtn = 0;
            //tải lại các id ở comboBox 
            LoadIdCuaHang();
            LoadIdNhanVien();
        }

        //Click vào Xóa 
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "")//Thông báo khi chưa nhập ID 
            {
                MessageBox.Show("Chưa nhập ID.");
                return;
            }
            //Hỏi người dùng có chắc chắn xóa không 
            DialogResult dia = MessageBox.Show(
                "Có chắc chắn xóa.",
                "Hỏi xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if(dia == DialogResult.Yes)
            {
                //Mở kết nối 
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from orders where order_id=" + txtId.Text;
                cmd.Connection = conn;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadDanhSachDonHang();
                    CacTextBoxRong();
                }
                else
                {
                    MessageBox.Show("ID không tồn tại.");
                }
            }
        }

        //Click vào Lưu 
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")//Thông báo chưa nhập ID đơn hàng 
            {
                MessageBox.Show("Chưa nhập ID.");
                return;
            }
            if(cboIdKhachHang.Text == "")//Thông báo chưa nhập ID khách hàng 
            {
                MessageBox.Show("Chưa nhập ID khách hàng.");
                return;
            }
            if(cboIdCuaHang.Text == "")//Thông báo chưa nhập ID cửa hàng 
            {
                MessageBox.Show("Chưa nhập ID cửa hàng.");
                return;
            }
            if(cboIdNhanVien.Text == "")//Thông báo chưa nhập ID nhân viên  
            {
                MessageBox.Show("Chưa nhập ID nhân viên.");
                return;
            }
            //Kiểm tra id cửa hàng với id nhân viên có trùng nhau không
            if (!KiemTraIdCuaHangVaIdNhanVien())
            {
                return;
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            if(ktbtn == 1)//Them
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into orders(order_id,customer_id,order_status,order_date,required_date,shipped_date,store_id,staff_id) " +
                    "values (@id,@customer_id,@order_status,@order_date,@required_date,@shipped_date,@store_id,@staff_id)";
                cmd.Connection = conn;

                cmd.Parameters.Add("@id", SqlDbType.Char).Value = txtId.Text;
                cmd.Parameters.Add("@customer_id", SqlDbType.VarChar).Value = cboIdKhachHang.Text;
                cmd.Parameters.Add("@order_status", SqlDbType.VarChar).Value = txtTinhTrangDatHang.Text;
                cmd.Parameters.Add("@order_date", SqlDbType.Date).Value = dtpNgayDatHang.Value;
                cmd.Parameters.Add("@required_date", SqlDbType.Date).Value = dtpNgayYeuCau.Value;
                cmd.Parameters.Add("@shipped_date", SqlDbType.Date).Value = dtpNgayVanChuyen.Value;
                cmd.Parameters.Add("@store_id", SqlDbType.VarChar).Value = cboIdCuaHang.Text;
                cmd.Parameters.Add("@staff_id", SqlDbType.VarChar).Value = cboIdNhanVien.Text;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Thêm thành công.");
                    LoadDanhSachDonHang();
                    CacTextBoxRong();
                    txtId.Focus();
                }
            }
            else if(ktbtn == 2)//Sửa 
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update orders set customer_id=@customer_id,order_status=@order_status,order_date=@order_date,required_date=@required_date,shipped_date=@shipped_date,store_id=@store_id,staff_id=@staff_id " +
                    "where order_id=@id";
                cmd.Connection = conn;

                cmd.Parameters.Add("@id", SqlDbType.Char).Value = txtId.Text;
                cmd.Parameters.Add("@customer_id", SqlDbType.VarChar).Value = cboIdKhachHang.Text;
                cmd.Parameters.Add("@order_status", SqlDbType.VarChar).Value = txtTinhTrangDatHang.Text;
                cmd.Parameters.Add("@order_date", SqlDbType.Date).Value = dtpNgayDatHang.Value;
                cmd.Parameters.Add("@required_date", SqlDbType.Date).Value = dtpNgayYeuCau.Value;
                cmd.Parameters.Add("@shipped_date", SqlDbType.Date).Value = dtpNgayVanChuyen.Value;
                cmd.Parameters.Add("@store_id", SqlDbType.VarChar).Value = cboIdCuaHang.Text;
                cmd.Parameters.Add("@staff_id", SqlDbType.VarChar).Value = cboIdNhanVien.Text;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Sửa thành công.");
                    LoadDanhSachDonHang();
                    CacTextBoxRong();
                }
            }

            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            ktbtn = 0;
        }

        //tải thông tin lên các textbox khi bấm girdview 
        private void gvDonDatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = gvDonDatHang.CurrentCell.RowIndex;
            if (r < 0)
            {
                return;
            }
            txtId.Text = gvDonDatHang.Rows[r].Cells[0].Value.ToString().Trim();
            cboIdKhachHang.Text = gvDonDatHang.Rows[r].Cells[1].Value.ToString();
            txtTinhTrangDatHang.Text = gvDonDatHang.Rows[r].Cells[2].Value.ToString();
            dtpNgayDatHang.Text = gvDonDatHang.Rows[r].Cells[3].Value.ToString();
            dtpNgayYeuCau.Text = gvDonDatHang.Rows[r].Cells[4].Value.ToString();
            dtpNgayVanChuyen.Text = gvDonDatHang.Rows[r].Cells[5].Value.ToString();
            cboIdCuaHang.Text = gvDonDatHang.Rows[r].Cells[6].Value.ToString();
            cboIdNhanVien.Text = gvDonDatHang.Rows[r].Cells[7].Value.ToString();
        }

        //Tìm Thông tin đơn hàng 
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(txtTimKiem.Text == "")
            {
                MessageBox.Show("Chưa nhập ID.");
                return;
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from orders where order_id=" + txtTimKiem.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtId.Text = reader.GetString(0).Trim();
                cboIdKhachHang.Text = reader.GetString(1);
                txtTinhTrangDatHang.Text = reader.GetString(2);
                dtpNgayDatHang.Text = reader.GetDateTime(3).ToString();
                dtpNgayYeuCau.Text = reader.GetDateTime(4).ToString();
                dtpNgayVanChuyen.Text = reader.GetDateTime(5).ToString();
                cboIdCuaHang.Text = reader.GetString(6);
                cboIdNhanVien.Text = reader.GetString(7);
            }
            else
            {
                MessageBox.Show("ID Không tồn tại.");
            }
            reader.Close();
        }

        //Kiểm tra ID hợp lệ chưa 
        private void txtId_Leave(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from orders where order_id=" + txtId.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                if (ktbtn == 1) // Them
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("Lỗi: Trùng ID.");
                        txtId.Text = "";
                    }
                }
                else if (ktbtn == 2) // Xoa
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("ID không tồn tại.");
                        txtId.Text = "";
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        //Kiểm tra ID khách hàng có không
        private void cboIdKhachHang_Leave(object sender, EventArgs e)
        {
            if (ktbtn == 1 || ktbtn == 2)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from customers where customer_id=" + cboIdKhachHang.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không có ID khách hàng này.");
                        cboIdKhachHang.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        //Kiểm tra ID cửa hàng có không
        private void cboIdCuaHang_Leave(object sender, EventArgs e)
        {
            if (ktbtn == 1 || ktbtn == 2)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from stores where store_id=" + cboIdCuaHang.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không có ID cửa hàng này.");
                        cboIdCuaHang.Text = "";
                        LoadIdNhanVien();
                    }
                    reader.Close();
                    TaiIdNhanVienDaChonIdCuaHang();
                }
                catch (Exception)
                {
                    LoadIdNhanVien();
                }
            }
        }

        //kiểm tra ID cửa hàng và Id nhân viên có hợp lệ không 
        private bool KiemTraIdCuaHangVaIdNhanVien()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            bool kt = false;
            if (cboIdNhanVien.Text != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from staffs where store_id=" + cboIdCuaHang.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == cboIdNhanVien.Text)
                    {
                        kt = true;
                    }
                }
                reader.Close();
                if(kt == false)
                {
                    MessageBox.Show("Không có nhân viên " + cboIdNhanVien.Text + " trong cửa hàng " + cboIdCuaHang.Text);
                    cboIdCuaHang.Text = "";
                    cboIdNhanVien.Text = "";
                    LoadIdNhanVien();
                    LoadIdCuaHang();
                }
            }
            return kt;
        }

        //Tải id nhân viên lên khi đã chon id cửa hàng
        private void TaiIdNhanVienDaChonIdCuaHang()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from staffs where store_id=" + cboIdCuaHang.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                cboIdNhanVien.Items.Clear();
                while (reader.Read())
                {
                    cboIdNhanVien.Items.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (Exception)
            {

            }
        }

        //Kiểm tra ID nhân viên có không
        private void cboIdNhanVien_Leave(object sender, EventArgs e)
        {
            if (ktbtn == 1 || ktbtn == 2)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from staffs where staff_id=" + cboIdNhanVien.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không có ID nhân viên này.");
                        cboIdNhanVien.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        //tự điền ID cửa hàng khi đã nhập id nhân viên
        private void cboIdNhanVien_TextChanged(object sender, EventArgs e)
        {
            if (ktbtn == 1 || ktbtn == 2)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from staffs where staff_id=" + cboIdNhanVien.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cboIdCuaHang.Text = reader.GetString(6);
                    }
                    else
                    {
                        cboIdCuaHang.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    cboIdCuaHang.Text = "";
                }
            }
        }
    }
}
