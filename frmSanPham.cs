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
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataTable data = null;
        int ktbtn = 0; //Biến kiểm tra đang thêm hay chỉnh sửa thông tin.

        //Load danh sách sản phẩm lên GV
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadDanhSachSanPham();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        //Tải Id nhãn hiệu lên comboBox
        private void LoadIdNhanHieu()
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
            cboIdNhanHieu.Items.Clear();
            while (reader.Read())
            {
                cboIdNhanHieu.Items.Add(reader.GetString(0).Trim());
            }
            reader.Close();
        }

        //Tải Id Thể loại lên comboBox
        private void LoadIdTheLoai()
        {
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
            cboIdTheLoai.Items.Clear();
            while (reader.Read())
            {
                cboIdTheLoai.Items.Add(reader.GetString(0).Trim());
            }
            reader.Close();
        }

        private void LoadDanhSachSanPham()
        {
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            //Mở kết nối.
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            adapter = new SqlDataAdapter("select * from products", conn);
            data = new DataTable();
            adapter.Fill(data);
            gvDanhSachSanPham.DataSource = data;
            //Tải Id nhãn hiệu với thể loại lên comboBox
            LoadIdNhanHieu();
            LoadIdTheLoai();
        }

        //tải thông tin lên các textbox khi bấm gỉdview 
        private void gvDanhSachSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = gvDanhSachSanPham.CurrentCell.RowIndex;
            if (r < 0)
            {
                return;
            }
            txtId.Text = gvDanhSachSanPham.Rows[r].Cells[0].Value.ToString().Trim();
            txtName.Text = gvDanhSachSanPham.Rows[r].Cells[1].Value.ToString().Trim();
            cboIdNhanHieu.Text = gvDanhSachSanPham.Rows[r].Cells[2].Value.ToString().Trim();
            cboIdTheLoai.Text = gvDanhSachSanPham.Rows[r].Cells[3].Value.ToString().Trim();
            cboNamSanXuat.Text = gvDanhSachSanPham.Rows[r].Cells[4].Value.ToString().Trim();
            txtBangGia.Text = gvDanhSachSanPham.Rows[r].Cells[5].Value.ToString().Trim();
        }

        //khi bấm thêm 
        private void btnThem_Click(object sender, EventArgs e)
        {
            ktbtn = 1;
            AnHienCacMenu();
            CacTextBoxRong();
            txtId.Focus();
        }

        //Ẩn hiện các menu
        private void AnHienCacMenu()
        {
            if(ktbtn == 1)//Thêm
            {
                //hiện các menu thêm, lưu, hủy
                btnThem.Enabled = true;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
                //ẩn các menu sửa, xóa
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            else if(ktbtn == 2)//Sửa
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
            txtName.Text = "";
            txtBangGia.Text = "";
            cboIdNhanHieu.Text = "";
            cboIdTheLoai.Text = "";
            cboNamSanXuat.Text = "";
        }

        //Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "")//Thong bao chua nhap ID
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
            if(dia == DialogResult.Yes)
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from products where product_id=" + txtId.Text;
                cmd.Connection = conn;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadDanhSachSanPham();
                    CacTextBoxRong();
                }
                else
                {
                    MessageBox.Show("ID không tồn tại.");
                }
            }
        }

        //Sửa 
        private void btnSua_Click(object sender, EventArgs e)
        {
            ktbtn = 2;
            AnHienCacMenu();
        }

        //Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Chưa nhập ID.");
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
                cmd.CommandText = "insert into products(product_id,product_name,brand_id,category_id,model_year,list_price) " +
                    "values (@id,@name,@idBrand,@idCategory,@year,@price)";
                cmd.Connection = conn;
                cmd.Parameters.Add("@id", SqlDbType.Char).Value = txtId.Text;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("@idBrand", SqlDbType.Char).Value = cboIdNhanHieu.Text;
                cmd.Parameters.Add("@idCategory", SqlDbType.Char).Value = cboIdTheLoai.Text;
                cmd.Parameters.Add("@year", SqlDbType.SmallInt).Value = cboNamSanXuat.Text;
                cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = txtBangGia.Text;

                int ret = cmd.ExecuteNonQuery();
                if(ret > 0)
                {
                    MessageBox.Show("Thêm thành công.");
                    LoadDanhSachSanPham();
                    CacTextBoxRong();
                    txtId.Focus();
                }
            }
            else if(ktbtn == 2)//Sua
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update products set product_name=@name,brand_id=@idBrand,category_id=@idCategory,model_year=@year,list_price=@price where product_id=@id";
                cmd.Connection = conn;
                cmd.Parameters.Add("@id", SqlDbType.Char).Value = txtId.Text;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("@idBrand", SqlDbType.Char).Value = cboIdNhanHieu.Text;
                cmd.Parameters.Add("@idCategory", SqlDbType.Char).Value = cboIdTheLoai.Text;
                cmd.Parameters.Add("@year", SqlDbType.SmallInt).Value = cboNamSanXuat.Text;
                cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = txtBangGia.Text;

                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    MessageBox.Show("Sửa thành công.");
                    LoadDanhSachSanPham();
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

        //Hủy 
        private void btnHuy_Click(object sender, EventArgs e)
        {
            CacTextBoxRong();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            ktbtn = 0;
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
                cmd.CommandText = "Select * from products where product_id=" + txtId.Text;
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

        //Tìm Thông tin sản phẩm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(txtTimKiem.Text == "")//thông báo chưa nhập ID 
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
            cmd.CommandText = "Select * from products where product_id=" + txtTimKiem.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtId.Text = reader.GetString(0).Trim();
                txtName.Text = reader.GetString(1).Trim();
                cboIdNhanHieu.Text = reader.GetString(2).Trim();
                cboIdTheLoai.Text = reader.GetString(3).Trim();
                cboNamSanXuat.Text = reader.GetInt16(4).ToString().Trim();
                txtBangGia.Text = reader.GetDecimal(5).ToString().Trim();
            }
            else
            {
                MessageBox.Show("ID không tồn tại.");
            }
            reader.Close();
        }

        //Kiểm tra Id nhãn hiệu có tồn tại không
        private void cboIdNhanHieu_Leave(object sender, EventArgs e)
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
                    cmd.CommandText = "select * from brands where brand_id=" + cboIdNhanHieu.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không có ID này.");
                        cboIdNhanHieu.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        //Kiểm tra Id thể loại có tồn tại không 
        private void cboIdTheLoai_Leave(object sender, EventArgs e)
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
                    cmd.CommandText = "select * from categories where category_id=" + cboIdTheLoai.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không có ID này.");
                        cboIdTheLoai.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
