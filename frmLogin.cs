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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            //Ẩn mật khẩu
            txtMatKhau.UseSystemPasswordChar = true;
        }
        //Trạng thái đăng nhập
        public bool loginSuccess = false;

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;

        int i = 0; //Số lần đăng nhập sai
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
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
                //Truy vấn sql id và mk
                command.CommandText = "select * from users where ID='" + txtDangNhap.Text + "' and Password='" + txtMatKhau.Text + "'";
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();
                //Kiểm tra truy vấn có id và mật khẩu có trong SQL không
                if (reader.Read())
                {
                    // Thông báo đăng nhập thành công.
                    loginSuccess = true;
                    Close();
                }
                else
                {
                    //Kiểm tra số lần đăng nhập
                    i++;
                    //Nếu sai 3 lần thoát hệ thống
                    if (i == 3)
                    {
                        MessageBox.Show("Thoát hệ thống do đăng nhập sai 3 lần.");
                        Application.Exit();
                    }
                    MessageBox.Show("Tài khoản không tồn tại.");
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void ckbHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHienMatKhau.Checked)
            {
                //Hiện mk khi check ô checkbox
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                //Ẩn mk khi tắt ô checkbox
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            //Mở form đăng kí
            frmDangKi frmDK = new frmDangKi();
            frmDK.ShowDialog();
        }
    }
}
