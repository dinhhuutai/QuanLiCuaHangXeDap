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
    public partial class frmDatHang : Form
    {
        public frmDatHang()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataTable data = null;
        int ktbtn = 0; //Biến kiểm tra đang thêm hay chỉnh sửa thông tin.

        private void frmDatHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachDatHang();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        //Tải danh sách lên
        private void LoadDanhSachDatHang()
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

            adapter = new SqlDataAdapter("select * from order_items", conn);
            data = new DataTable();
            adapter.Fill(data);
            gvDatHang.DataSource = data;
            LoadIdDonHang();
            LoadIdSanPham();
        }

        //Tải Id đơn hàng
        private void LoadIdDonHang()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from orders";
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            cboIdDonHang.Items.Clear();
            while (reader.Read())
            {
                cboIdDonHang.Items.Add(reader.GetString(0).Trim());
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

        //click vào thêm 
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
            cboIdDonHang.Text = "";
            cboIdSanPham.Text = "";
            nudSoLuong.Value = 0;
            txtBangGia.Text = "";
            txtGiamGia.Text = "";
        }

        //Click vao sua
        private void btnSua_Click(object sender, EventArgs e)
        {
            ktbtn = 2;
            AnHienCacMenu();
        }

        //Click vao Xoa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "") //Thông báo chưa nhập ID
            {
                MessageBox.Show("Chưa nhập ID.");
                return;
            }
            //Hoi nguoi dung cos muon xoa khong
            DialogResult dia = MessageBox.Show(
                "Có chắc chắn xóa.",
                "Hỏi xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dia == DialogResult.Yes)
            {
                //Mở kết nối 
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from order_items where item_id=" + txtId.Text;
                cmd.Connection = conn;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    LoadDanhSachDatHang();
                    MessageBox.Show("Xóa thành công.");
                    CacTextBoxRong();
                }
                else
                {
                    MessageBox.Show("ID không tồn tại.");
                }
            }
        }

        //Click vao luu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") //Thông báo chưa nhập ID
            {
                MessageBox.Show("Chưa nhập ID.");
                return;
            }
            if(cboIdDonHang.Text == "")//thôn báo chưa nhập ID đơn hàng
            {
                MessageBox.Show("Chưa nhập ID đơn hàng.");
                return;
            }
            if(cboIdSanPham.Text == "")//thông báo chưa nhạp ID sản phẩm
            {
                MessageBox.Show("Chưa nhập ID sản phẩm.");
                return;
            }

            //Mở kết nối 
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            if(ktbtn == 1)//them
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into order_items(order_id,item_id,product_id,quantity,list_price,discount) " +
                    "values (@order_id,@item_id,@product_id,@quantity,@list_price,@discount)";
                cmd.Connection = conn;

                cmd.Parameters.Add("@order_id", SqlDbType.Char).Value = cboIdDonHang.Text;
                cmd.Parameters.Add("@item_id", SqlDbType.Int).Value = txtId.Text;
                cmd.Parameters.Add("@product_id", SqlDbType.Char).Value = cboIdSanPham.Text;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = nudSoLuong.Text;
                cmd.Parameters.Add("@list_price", SqlDbType.Decimal).Value = txtBangGia.Text;
                cmd.Parameters.Add("@discount", SqlDbType.Decimal).Value = txtGiamGia.Text;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Thêm thành công.");
                    LoadDanhSachDatHang();
                    CacTextBoxRong();
                    txtId.Focus();
                }
            }
            else if(ktbtn == 2)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update order_items set order_id=@order_id,product_id=@product_id,quantity=@quantity,list_price=@list_price,discount=@discount " +
                    "where item_id=@item_id";
                cmd.Connection = conn;

                cmd.Parameters.Add("@order_id", SqlDbType.Char).Value = cboIdDonHang.Text;
                cmd.Parameters.Add("@item_id", SqlDbType.Int).Value = txtId.Text;
                cmd.Parameters.Add("@product_id", SqlDbType.Char).Value = cboIdSanPham.Text;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = nudSoLuong.Text;
                cmd.Parameters.Add("@list_price", SqlDbType.Decimal).Value = txtBangGia.Text;
                cmd.Parameters.Add("@discount", SqlDbType.Decimal).Value = txtGiamGia.Text;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Sửa thành công.");
                    LoadDanhSachDatHang();
                }
            }

            ktbtn = 0;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        //Click vao huy
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ktbtn = 0;
            CacTextBoxRong();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        //Tải thông tin lên các textbox khi bấm vào GV
        private void gvDatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = gvDatHang.CurrentCell.RowIndex;
            if (r < 0)
            {
                return;
            }
            cboIdDonHang.Text = gvDatHang.Rows[r].Cells[0].Value.ToString();
            txtId.Text = gvDatHang.Rows[r].Cells[1].Value.ToString();
            cboIdSanPham.Text = gvDatHang.Rows[r].Cells[2].Value.ToString();
            nudSoLuong.Text = gvDatHang.Rows[r].Cells[3].Value.ToString();
            txtBangGia.Text = gvDatHang.Rows[r].Cells[4].Value.ToString();
            txtGiamGia.Text = gvDatHang.Rows[r].Cells[5].Value.ToString();
        }

        //Kiểm tra ID có chưa 
        private void txtId_Leave(object sender, EventArgs e)
        {
            //Mở kết nối 
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from order_items where item_id=" + txtId.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                if (ktbtn == 1)// kiểm tra lúc thêm 
                {
                    if (reader.Read())//Nếu đọc được dữ liệu thông báo trùng Id.
                    {
                        MessageBox.Show("Lỗi: Trùng ID.");
                        txtId.Text = "";
                    }
                }
                else if (ktbtn == 2) // Kiểm tra lúc sửa 
                {
                    if (!reader.Read())//Nếu không đọc đc dữ liệu thông báo ID không tồn tại 
                    {
                        MessageBox.Show("ID không tồn tại.");
                        txtId.Text = "";
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {

            }
        }

        //Kiểm tra có ID đơn hàng không 
        private void cboIdDonHang_Leave(object sender, EventArgs e)
        {
            //Mở kết nối 
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            if(ktbtn == 1 || ktbtn == 2) //Kiểm tra lức thêm và sửa 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from orders where order_id=" + cboIdDonHang.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())//Nếu không đọc đc dữ liệu thông báo ID không tồn tại
                    {
                        MessageBox.Show("Không có ID đơn hàng này.");
                        cboIdDonHang.Text = "";
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

        //Tìm kiếm 
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(txtTimKiem.Text == "")//Thông báo chưa nhập ID.
            {
                MessageBox.Show("Chưa nhập ID.");
                return;
            }
            //Mở kết nối 
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from order_items where item_id=" + txtTimKiem.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                cboIdDonHang.Text = reader.GetString(0);
                txtId.Text = reader.GetInt32(1).ToString();
                cboIdSanPham.Text = reader.GetString(2);
                nudSoLuong.Value = reader.GetInt32(3);
                txtBangGia.Text = reader.GetDecimal(4).ToString();
                txtGiamGia.Text = reader.GetDecimal(5).ToString();
            }
            else
            {
                MessageBox.Show("ID không tồn tại.");
            }
        }
    }
}
