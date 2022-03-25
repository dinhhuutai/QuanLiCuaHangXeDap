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
    public partial class frmDangKi : Form
    {
        public frmDangKi()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            //Kiểm tra ô nhập lại mật khẩu có giống ô mật khẩu không
            if(txtMatKhau.Text.CompareTo(txtNhapLaiMatKhau.Text) != 0)
            {
                MessageBox.Show("Nhập lại mật khẩu không giống mật khẩu.",
                    "Lỗi mật khẩu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            //Mở kết nối
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                //Thêm dữ liệu vào SQL
                command.CommandText = "Insert into users(ID,Password) values (@id,@pw)";
                command.Connection = conn;

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtEmailSdt.Text;
                command.Parameters.Add("@pw", SqlDbType.VarChar).Value = txtMatKhau.Text;

                int ret = command.ExecuteNonQuery();
                //Thông báo đăng kí thành công
                if(ret > 0)
                {
                    MessageBox.Show("Đăng kí thành công.");
                    Close();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            
        }

        private void txtEmailSdt_Leave(object sender, EventArgs e)
        {
            bool hl = false;
            if(KiemTraLaSo() == true && txtEmailSdt.Text.Length == 10)//Kiểm tra có phải số điện thoại không
            {
                hl = true;
            }
            else if(txtEmailSdt.Text.IndexOf("@gmail.com") != -1 && //Nếu không phải số thì có phải gmail ko
                txtEmailSdt.Text.Substring(txtEmailSdt.Text.IndexOf("@gmail.com")).Trim().Length <= 10)// Và @gmail.com có nằm ở cuối không
            {
                hl = true;
            }

            //thông báo tên tài khoản không hợp lệ
            if(hl == false)
            {
                MessageBox.Show("Lỗi: Tên tài khoản không hợp lệ.");
                txtEmailSdt.Focus();
                return;
            }

            //Kiểm tra tên tài khoản đã tồn tại chưa
            if(conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            //Mở kết nối
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from users where ID=@id";
            cmd.Connection = conn;
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtEmailSdt.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                MessageBox.Show("Tên tài khoản đã tồn tại.");
                txtEmailSdt.Text = "";
            }
            reader.Close();
        }

        //Hàm kiểm tra chuỗi có toàn số không
        private bool KiemTraLaSo()
        {
            foreach (char c in txtEmailSdt.Text)
            {
                if(!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
