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
    public partial class frmCoPhieu : Form
    {
        public frmCoPhieu()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataTable data = null;
        int ktbtn = 0; //Biến kiểm tra đang thêm hay chỉnh sửa thông tin.

        private void frmCoPhieu_Load(object sender, EventArgs e)
        {
            LoadDanhSachCoPhieu();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            LoadIdCuaHang();
            LoadIdSanPham();
        }

        //Tải danh sách cổ phiếu 
        private void LoadDanhSachCoPhieu()
        {
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from stocks", conn);
            data = new DataTable();
            adapter.Fill(data);
            gvCoPhieu.DataSource = data;
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

        //Tải Id sản phẩm 
        private void LoadIdSanPham()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from products";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            cboIdSanPham.Items.Clear();
            while (reader.Read())
            {
                cboIdSanPham.Items.Add(reader.GetString(0).Trim());
            }
            reader.Close();
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
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        //Kiểm tra có ID sản phẩm không 
        private void cboIdSanPham_Leave(object sender, EventArgs e)
        {
            //Mở kết nối 
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            if (ktbtn == 1 || ktbtn == 2) //Kiểm tra lức thêm và sửa 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from products where product_id=" + cboIdSanPham.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())//Nếu không đọc đc dữ liệu thông báo ID không tồn tại
                    {
                        MessageBox.Show("Không có ID sản phẩm này.");
                        cboIdSanPham.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        //Tải thông tin cổ phiếu lên các textbox khi bấm GV
        private void gvCoPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = gvCoPhieu.CurrentCell.RowIndex;
            if (r < 0)
            {
                return;
            }
            cboIdCuaHang.Text = gvCoPhieu.Rows[r].Cells[0].Value.ToString();
            cboIdSanPham.Text = gvCoPhieu.Rows[r].Cells[1].Value.ToString();
            nudSoLuong.Text = gvCoPhieu.Rows[r].Cells[2].Value.ToString();
        }

        //ẩn hiện các button 
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
            cboIdCuaHang.Text = "";
            cboIdSanPham.Text = "";
            nudSoLuong.Value = 0;
        }

        //click them
        private void btnThem_Click(object sender, EventArgs e)
        {
            ktbtn = 1;
            CacTextBoxRong();
            AnHienCacMenu();
        }

        //click sua
        private void btnSua_Click(object sender, EventArgs e)
        {
            ktbtn = 2;
            AnHienCacMenu();
        }

        //click xoa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(cboIdCuaHang.Text == "")//Thông báo chưa nhập ID cửa hàng
            {
                MessageBox.Show("Chưa chọn ID cửa hàng.");
                return;
            }
            if(cboIdSanPham.Text == "")//Thông báo chưa nhập ID sản phẩm
            {
                MessageBox.Show("Chưa chọn ID sản phẩm.");
                return;
            }
            //Hỏi người dùng có chắc chắn xóa không 
            DialogResult dia = MessageBox.Show(
                "Có chắc chắn xóa.",
                "Hỏi xóa",
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
                cmd.CommandText = "delete from stocks where store_id=" + cboIdCuaHang.Text +
                    " and product_id=" + cboIdSanPham.Text;
                cmd.Connection = conn;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadDanhSachCoPhieu();
                    CacTextBoxRong();
                }
                else
                {
                    MessageBox.Show("ID không tồn tại.");
                }
            }

        }

        //click luu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboIdCuaHang.Text == "")//Thông báo chưa nhập ID cửa hàng
            {
                MessageBox.Show("Chưa chọn ID cửa hàng.");
                return;
            }
            if (cboIdSanPham.Text == "")//Thông báo chưa nhập ID sản phẩm
            {
                MessageBox.Show("Chưa chọn ID sản phẩm.");
                return;
            }

            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            if(ktbtn == 1)//Them
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from stocks where store_id=" + cboIdCuaHang.Text +
                    " and product_id=" + cboIdSanPham.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int sl = reader.GetInt32(2) + (int)nudSoLuong.Value;
                    reader.Close();
                    SqlCommand cmdCu = new SqlCommand();
                    cmdCu.CommandType = CommandType.Text;
                    cmdCu.CommandText = "update stocks set quantity=@quantity where store_id=@store_id and product_id=@product_id";
                    cmdCu.Connection = conn;

                    cmdCu.Parameters.Add("@store_id", SqlDbType.VarChar).Value = cboIdCuaHang.Text;
                    cmdCu.Parameters.Add("@product_id", SqlDbType.Char).Value = cboIdSanPham.Text;
                    cmdCu.Parameters.Add("@quantity", SqlDbType.Int).Value = sl;

                    int ret = cmdCu.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        MessageBox.Show("Thêm thành công.");
                        CacTextBoxRong();
                        LoadDanhSachCoPhieu();
                    }

                }
                else
                {
                    reader.Close();
                    SqlCommand cmdMoi = new SqlCommand();
                    cmdMoi.CommandType = CommandType.Text;
                    cmdMoi.CommandText = "insert into stocks(store_id,product_id,quantity) " +
                        "values (@store_id,@product_id,@quantity)";
                    cmdMoi.Connection = conn;

                    cmdMoi.Parameters.Add("@store_id", SqlDbType.VarChar).Value = cboIdCuaHang.Text;
                    cmdMoi.Parameters.Add("@product_id", SqlDbType.Char).Value = cboIdSanPham.Text;
                    cmdMoi.Parameters.Add("@quantity", SqlDbType.Int).Value = nudSoLuong.Value;

                    int ret = cmdMoi.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        MessageBox.Show("Thêm thành công.");
                        CacTextBoxRong();
                        LoadDanhSachCoPhieu();
                    }
                }
            }
            else if(ktbtn == 2)// Xoa
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update stocks set quantity=@quantity where store_id=@store_id and product_id=@product_id";
                cmd.Connection = conn;

                cmd.Parameters.Add("@store_id", SqlDbType.VarChar).Value = cboIdCuaHang.Text;
                cmd.Parameters.Add("@product_id", SqlDbType.Char).Value = cboIdSanPham.Text;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = nudSoLuong.Value;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Chỉnh sửa thành công.");
                    CacTextBoxRong();
                    LoadDanhSachCoPhieu();
                }
            }


            ktbtn = 0;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = true;
            CacTextBoxRong();
        }

        //click huy
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ktbtn = 0;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = true;
            CacTextBoxRong();
        }
    }
}
