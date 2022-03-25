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
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;
        int ktBtn = 0; //Cờ kiểm tra thêm hoặc chỉnh sửa

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhachHang();
            btnLuu.Enabled = false;
            picLuu.Enabled = false;
            btnHuy.Enabled = false;
            picHuy.Enabled = false;
        }

        private void LoadDanhSachKhachHang()
        {
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            //Mở kết nối
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from customers", conn);
            DataTable data = new DataTable();
            adapter.Fill(data);
            gvKhachHang.DataSource = data;
        }

        //Tải thông tin khách hàng lên các textbox khi nhấn vào DataGirdView
        private void gvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = gvKhachHang.CurrentCell.RowIndex;
            if (r < 0)
            {
                return;
            }
            txtId.Text = gvKhachHang.Rows[r].Cells[0].Value.ToString();
            txtTen.Text = gvKhachHang.Rows[r].Cells[1].Value.ToString();
            txtHo.Text = gvKhachHang.Rows[r].Cells[2].Value.ToString();
            txtPhone.Text = gvKhachHang.Rows[r].Cells[3].Value.ToString();
            txtEmail.Text = gvKhachHang.Rows[r].Cells[4].Value.ToString();
            txtStreet.Text = gvKhachHang.Rows[r].Cells[5].Value.ToString();
            txtCity.Text = gvKhachHang.Rows[r].Cells[6].Value.ToString();
            txtState.Text = gvKhachHang.Rows[r].Cells[7].Value.ToString();
            txtCode.Text = gvKhachHang.Rows[r].Cells[8].Value.ToString();
        }

        //Tìm kiếm thông tin khách hàng bằng ID
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from customers where customer_id=" + txtTimKiem.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtId.Text = reader.GetString(0);
                    txtTen.Text = reader.GetString(1);
                    txtHo.Text = reader.GetString(2);
                    txtPhone.Text = reader.GetString(3);
                    txtEmail.Text = reader.GetString(4);
                    txtStreet.Text = reader.GetString(5);
                    txtCity.Text = reader.GetString(6);
                    txtState.Text = reader.GetString(7);
                    txtCode.Text = reader.GetString(8);
                }
                else
                {
                    MessageBox.Show("Không có mã ID này.");
                }
                reader.Close();
            }
            catch (Exception)
            {

            }
        }

        //Thêm khách hàng
        private void btnThem_Click(object sender, EventArgs e)
        {
            ktBtn = 1; //Thông báo cho chương trình biết là đang thêm
            AnHienCacTextBox();
            CacTextBoxRong(); // Cho các textbox rỗng khi bấm thêm
            txtId.Focus(); // Cho nháy chuột đến ô textbox ID
        }

        //Sửa thông tin khách hàng
        private void btnSua_Click(object sender, EventArgs e)
        {
            ktBtn = 2;//Thông báo cho chương trình biết là đang chỉnh sửa
            AnHienCacTextBox();
        }

        //Xóa Khách hàng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "")//Kiểm tra đã điền ID chưa.
            {
                MessageBox.Show("Bạn chưa nhập ID.");
                return;
            }

            DialogResult dr = MessageBox.Show(
                "Bạn có chắc chắn xóa khách hàng này?",
                "Trả lời",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from customers where customer_id=@id";
                    command.Connection = conn;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachKhachHang();
                        CacTextBoxRong();
                        MessageBox.Show("Xóa thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không có ID này.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!");
                }
            }
        }

        //Lưu khi thêm hoặc sửa thông tin khách hàng
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            if (ktBtn == 1) // Thêm
            {
                //Kiểm tra nhập thông tin ID chưa.
                if (txtId.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập ID.");
                    return;
                }
                if (txtTen.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên.");
                    return;
                }
                if (txtHo.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập họ.");
                    return;
                }
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into customers(customer_id,first_name,last_name,phone,email,street,city,state,zip_code)" +
                        "values (@id,@fname,@lname,@phone,@email,@street,@city,@state,@code)";
                    command.Connection = conn;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;
                    command.Parameters.Add("@fname", SqlDbType.VarChar).Value = txtTen.Text;
                    command.Parameters.Add("@lname", SqlDbType.VarChar).Value = txtHo.Text;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtPhone.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                    command.Parameters.Add("@street", SqlDbType.VarChar).Value = txtStreet.Text;
                    command.Parameters.Add("@city", SqlDbType.VarChar).Value = txtCity.Text;
                    command.Parameters.Add("@state", SqlDbType.VarChar).Value = txtState.Text;
                    command.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachKhachHang();
                        MessageBox.Show("Thêm thành công.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!");
                }
            }
            else if (ktBtn == 2)// Chỉnh sửa
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
                    command.CommandText = "update customers set first_name=@fname,last_name=@lname,phone=@phone,email=@email," +
                        "street=@street,city=@city,state=@state,zip_code=@code where customer_id=@id";
                    command.Connection = conn;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;
                    command.Parameters.Add("@fname", SqlDbType.VarChar).Value = txtTen.Text;
                    command.Parameters.Add("@lname", SqlDbType.VarChar).Value = txtHo.Text;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtPhone.Text;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                    command.Parameters.Add("@street", SqlDbType.VarChar).Value = txtStreet.Text;
                    command.Parameters.Add("@city", SqlDbType.VarChar).Value = txtCity.Text;
                    command.Parameters.Add("@state", SqlDbType.VarChar).Value = txtState.Text;
                    command.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachKhachHang();
                        MessageBox.Show("Cập nhật thành công.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!");
                }
            }
            CacTextBoxRong();//Cho cac text box rỗng khi bấm hủy
            //Hiện các btn Thêm, Sửa, Xóa
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            picThem.Enabled = true;
            picSua.Enabled = true;
            picXoa.Enabled = true;

            //Ẩn các btn Lưu, Hủy
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            picLuu.Enabled = false;
            picHuy.Enabled = false;
            ktBtn = 0; //Thông báo cho chương trình đang không làm gì
        }

        //Hủy trạng thái thêm hoặc chỉnh sửa thông tin khách hàng
        private void btnHuy_Click(object sender, EventArgs e)
        {
            CacTextBoxRong();//Cho cac text box rỗng khi bấm hủy
            //Hiện các btn Thêm, Sửa, Xóa
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            picThem.Enabled = true;
            picSua.Enabled = true;
            picXoa.Enabled = true;

            //Ẩn các btn Lưu, Hủy
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            picLuu.Enabled = false;
            picHuy.Enabled = false;
            ktBtn = 0; //Thông báo cho chương trình đang không làm gì
        }

        //Kiểm tra ID khách hàng 
        private void txtId_Leave(object sender, EventArgs e)
        {
            if(ktBtn == 1) // Khi bấm thêm Khách hàng thì kiểm tra xem ID đã có chưa
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from customers where customer_id=" + txtId.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Lỗi: Trùng ID.");
                        txtId.Text = "";
                    }
                    reader.Close();
                }
                catch(Exception)
                {

                }
            }
            else if(ktBtn == 2) // Khi bấm chỉnh sửa thông tin thì kiểm tra ID có tồn tại không
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
                    cmd.CommandText = "select * from customers where customer_id=" + txtId.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
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

        //Kiểm tra Email hợp lệ Không
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if ((ktBtn == 1 || ktBtn == 2) && txtEmail.Text != "")//Kiểm tra khi bấm thêm hoặc chỉnh sửa
            {
                int kq = txtEmail.Text.IndexOf("@gmail.com");
                if (kq == -1 || txtEmail.Text.Substring(kq).Trim().Length > 10)
                {
                    MessageBox.Show("Email phải có @gmail.com ở sau.");
                    txtEmail.Text = "";
                }
                txtEmail.Text.Trim();
            }
        }

        //Kiểm tra Số điện thoại
        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if((ktBtn == 1 || ktBtn == 2) && txtPhone.Text != "")//Kiểm tra khi bấm thêm hoặc sửa
            {
                if(!(KiemTraLaSo() == true && txtPhone.Text.Length == 10))//Số điẹn thoại phải là số và có 10 số
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

        //Ẩn hiện các textbox khi bấm các chức năng
        private void AnHienCacTextBox()
        {
            if(ktBtn == 1)//Nếu bấm thêm
            {
                //Hiện các btn Thêm, Lưu, Huy
                btnThem.Enabled = true;
                picThem.Enabled = true;
                btnLuu.Enabled = true;
                picLuu.Enabled = true;
                btnHuy.Enabled = true;
                picHuy.Enabled = true;

                //Ẩn các btn Sửa, Xoa
                btnSua.Enabled = false;
                picSua.Enabled = false;
                btnXoa.Enabled = false;
                picXoa.Enabled = false;
            }
            else if (ktBtn == 2)//Nếu bấm Chỉnh sửa
            {
                //Hiện các btn Sửa, Lưu, Huy
                btnSua.Enabled = true;
                picSua.Enabled = true;
                btnLuu.Enabled = true;
                picLuu.Enabled = true;
                btnHuy.Enabled = true;
                picHuy.Enabled = true;

                //Ẩn các btn Thêm, Xóa
                btnThem.Enabled = false;
                picThem.Enabled = false;
                btnXoa.Enabled = false;
                picXoa.Enabled = false;
            }
        }

        //Cho các textBox rỗng
        private void CacTextBoxRong()
        {
            txtId.Text = "";
            txtTen.Text = "";
            txtHo.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtStreet.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtCode.Text = "";
        }

        private void picThem_Click(object sender, EventArgs e)
        {
            btnThem.PerformClick();
        }

        private void picSua_Click(object sender, EventArgs e)
        {
            btnSua.PerformClick();
        }

        private void picLuu_Click(object sender, EventArgs e)
        {
            btnLuu.PerformClick();
        }

        private void picXoa_Click(object sender, EventArgs e)
        {
            btnXoa.PerformClick();
        }

        private void picHuy_Click(object sender, EventArgs e)
        {
            btnHuy.PerformClick();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            btnTimKiem.PerformClick();
        }
    }
}
