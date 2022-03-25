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
    public partial class frmCuaHang : Form
    {
        public frmCuaHang()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;
        int ktBtn = 0; //Cờ kiểm tra thêm hoặc chỉnh sửa

        private void frmCuaHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachCuaHang();
            btnLuu.Visible = false;
            btnHuy.Visible = false;
        }

        private void LoadDanhSachCuaHang()
        {
            if (conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            // Mở kết nối
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            try
            {
                //Tải dữ liệu lên
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from stores", conn);
                DataTable data = new DataTable();
                adapter.Fill(data);
                gvCuaHang.DataSource = data;
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi dữ liệu.");
            }
        }

        //Hiện thông tin cửa hàng vào textbox
        private void gvCuaHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = gvCuaHang.CurrentCell.RowIndex;
            if (r < 0)
            {
                return;
            }
            txtId.Text = gvCuaHang.Rows[r].Cells[0].Value.ToString();
            txtName.Text = gvCuaHang.Rows[r].Cells[1].Value.ToString();
            txtPhone.Text = gvCuaHang.Rows[r].Cells[2].Value.ToString();
            txtEmail.Text = gvCuaHang.Rows[r].Cells[3].Value.ToString();
            txtStreet.Text = gvCuaHang.Rows[r].Cells[4].Value.ToString();
            txtCity.Text = gvCuaHang.Rows[r].Cells[5].Value.ToString();
            txtState.Text = gvCuaHang.Rows[r].Cells[6].Value.ToString();
            txtZipCode.Text = gvCuaHang.Rows[r].Cells[7].Value.ToString();
        }

        //Thêm cửa hàng
        private void btnThem_Click(object sender, EventArgs e)
        {
            ktBtn = 1;
            AnHienCacBtn();
            CacTextBoxRong();
            txtId.Focus();
        }

        //Xóa cửa hàng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Kiểm tra nhập thông tin ID chưa.
            if (txtId.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ID.");
                return;
            }
            DialogResult dr = MessageBox.Show(
                "Bạn có chắc chắn xóa cửa hàng này?",
                "Trả lời",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (conn == null)
                {
                    conn = new SqlConnection(strConn);
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from stores where store_id=@id";
                    command.Connection = conn;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachCuaHang();
                        CacTextBoxRong();
                        MessageBox.Show("Xóa thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không có ID này.");
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Lỗi!");
                }
            }
        }

        //chỉnh sửa thông tin cửa hàng
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            ktBtn = 2;
            AnHienCacBtn();
        }

        //Tìm kiếm thông tin cửa hàng bằng id
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ID.");
                return;
            }

            if (conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string idstores = txtTimKiem.Text.Trim();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Select * from stores where store_id=@id";
                command.Connection = conn;
                command.Parameters.Add("@id", SqlDbType.VarChar).Value = idstores;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtId.Text = reader.GetString(0);
                    txtName.Text = reader.GetString(1);
                    txtPhone.Text = reader.GetString(2);
                    txtEmail.Text = reader.GetString(3);
                    txtStreet.Text = reader.GetString(4);
                    txtCity.Text = reader.GetString(5);
                    txtState.Text = reader.GetString(6);
                    txtZipCode.Text = reader.GetString(7);
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Không có ID này!");
                }
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi!");
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if ((ktBtn == 1 || ktBtn == 2) && txtEmail.Text != "") // Kiểm tra có phải đang thêm 
            {
                //Kiểm tra email có @gmail.com ở cuối ko
                int checkGmail = txtEmail.Text.IndexOf("@gmail.com");
                if (checkGmail == -1 || txtEmail.Text.Substring(checkGmail).Trim().Length > 10)
                {
                    MessageBox.Show("Email phải có @gmail.com ở sau.");
                    txtEmail.Text = "";
                }
                txtEmail.Text = txtEmail.Text.Trim();
            }
        }

        //Kiểm tra trùng Id
        private void txtId_Leave(object sender, EventArgs e)
        {
            if (ktBtn == 1) // Khi thực hiện thêm
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
                    cmd.CommandText = "select * from stores where store_id=" + txtId.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Lỗi: Trùng Id.");
                        txtId.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
            if(ktBtn == 2) // Khi thực hiện chỉnh sửa
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
                    cmd.CommandText = "select * from stores where store_id=" + txtId.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    //Kiểm tra ID này có trong ID cửa hàng không
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không có ID này.");
                        txtId.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        //Kiểm tra Số điện thoại
        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if ((ktBtn == 1 || ktBtn == 2) && txtPhone.Text != "")//Kiểm tra khi bấm thêm hoặc sửa
            {
                if (!(KiemTraLaSo() == true && txtPhone.Text.Length == 10))//Số điẹn thoại phải là số và có 10 số
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.");
                    txtPhone.Text = "";
                }
            }
        }

        //Hàm kiểm tra chuỗi có toàn số không
        private bool KiemTraLaSo()
        {
            foreach (char c in txtPhone.Text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void AnHienCacBtn()
        {
            if(ktBtn == 1)
            {
                btnThem.Visible = true;
                btnLuu.Visible = true;
                btnHuy.Visible = true;
                btnCapNhat.Visible = false; //Ẩn btn cập nhât khi bấm btn thêm
                btnXoa.Visible = false;
            }
            else if(ktBtn == 2)
            {
                btnCapNhat.Visible = true;
                btnLuu.Visible = true;
                btnHuy.Visible = true;
                btnThem.Visible = false; //Ẩn btn thêm khi bấm btn cập nhật
                btnXoa.Visible = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (ktBtn == 1) // Thêm
            {
                //Kiểm tra nhập thông tin ID chưa.
                if(txtId.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập ID.");
                    return;
                }
                if(txtName.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên.");
                    return;
                }
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into stores(store_id,store_name,phone,email,street,city,state,zip_code)" +
                        "values (@id,@name,@phone,@email,@street,@city,@state,@code)";
                    command.Connection = conn;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtPhone.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                    command.Parameters.Add("@street", SqlDbType.VarChar).Value = txtStreet.Text;
                    command.Parameters.Add("@city", SqlDbType.VarChar).Value = txtCity.Text;
                    command.Parameters.Add("@state", SqlDbType.VarChar).Value = txtState.Text;
                    command.Parameters.Add("@code", SqlDbType.VarChar).Value = txtZipCode.Text;

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachCuaHang();
                        MessageBox.Show("Thêm thành công.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!");
                }
            }
            else if(ktBtn == 2)// Chỉnh sửa
            {
                //Kiểm tra nhập thông tin ID chưa.
                if (txtId.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập ID.");
                    return;
                }
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update stores set store_name=@name,phone=@phone,email=@email," +
                        "street=@street,city=@city,state=@state,zip_code=@code where store_id=@id";
                    command.Connection = conn;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtPhone.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                    command.Parameters.Add("@street", SqlDbType.VarChar).Value = txtStreet.Text;
                    command.Parameters.Add("@city", SqlDbType.VarChar).Value = txtCity.Text;
                    command.Parameters.Add("@state", SqlDbType.VarChar).Value = txtState.Text;
                    command.Parameters.Add("@code", SqlDbType.VarChar).Value = txtZipCode.Text;

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachCuaHang();
                        MessageBox.Show("Cập nhật thành công.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!");
                }
            }
            CacTextBoxRong();
            //Hiện lại các button
            btnThem.Visible = true;
            btnCapNhat.Visible = true;
            btnXoa.Visible = true;
            btnLuu.Visible = false;
            btnHuy.Visible = false;
            ktBtn = 0; //Trạng thái thêm, chỉnh sửa hoặc ko làm gì
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            CacTextBoxRong();
            btnThem.Visible = true;
            btnCapNhat.Visible = true;
            btnXoa.Visible = true;
            btnLuu.Visible = false;
            btnHuy.Visible = false;
            ktBtn = 0; //Trạng thái thêm, chỉnh sửa hoặc ko làm gì
        }

        //Cho các textbox bằng rỗng
        private void CacTextBoxRong()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtStreet.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZipCode.Text = "";
        }
    }
}
