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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        string strConn = "Data Source=DESKTOP-IOQTTPU; Initial Catalog=CSDL_BikeStores; Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataTable data = null;
        int ktBtn = 0;

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            //Load danh sách nhân viên lên DataGridView
            LoadDanhSachNhanVien();
            btnLuu.Visible = false;
            btnHuy.Visible = false;
        }

        private void LoadDanhSachNhanVien()
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
                adapter = new SqlDataAdapter("Select * from staffs", conn);
                data = new DataTable();
                adapter.Fill(data);
                gvNhanVien.DataSource = data;
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi dữ liệu.");
            }

            // Tải id_stores lên comboBox
            SqlCommand cmdStores = new SqlCommand();
            cmdStores.CommandType = CommandType.Text;
            cmdStores.CommandText = "Select * from stores";
            cmdStores.Connection = conn;

            SqlDataReader readerStores = cmdStores.ExecuteReader();
            cboIdStores.Items.Clear();
            while(readerStores.Read())
            {
                cboIdStores.Items.Add(readerStores.GetString(0));
            }
            readerStores.Close();
            // Tải id_manager lên comboBox
            TaiIdManagerLenCb();
        }

        //Hiện thông tin nhân viên vào textbox
        private void gvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = gvNhanVien.CurrentCell.RowIndex;
            if (r < 0)
            {
                return;
            }
            txtId.Text = gvNhanVien.Rows[r].Cells[0].Value.ToString();
            txtName.Text = gvNhanVien.Rows[r].Cells[2].Value.ToString() + " " + gvNhanVien.Rows[r].Cells[1].Value.ToString();
            txtEmail.Text = gvNhanVien.Rows[r].Cells[3].Value.ToString();
            txtPhone.Text = gvNhanVien.Rows[r].Cells[4].Value.ToString();
            txtTichCuc.Text = gvNhanVien.Rows[r].Cells[5].Value.ToString();
            cboIdStores.Text = gvNhanVien.Rows[r].Cells[6].Value.ToString();
            cboIdManager.Text = gvNhanVien.Rows[r].Cells[7].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
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
            //Mở kết nối
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string idstaffs = txtTimKiem.Text.Trim();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "Select * from staffs";
            command.Connection = conn;

            SqlDataReader reader = command.ExecuteReader();
            int r = 0;
            while (reader.Read()) //Tìm vị trí hàng nhân viên
            {
                if(idstaffs == reader.GetString(0))//Thoát khi thấy hàng nhân viên đó
                {
                    break;
                }
                r++;
            }
            reader.Close();
            //Truy vấn nhân viên có mã @id
            command.CommandText = "Select * from staffs where staff_id=@id";
            command.Parameters.Add("@id", SqlDbType.VarChar).Value = idstaffs;
            reader = command.ExecuteReader();
            string id;
            // Tải thông tin nhân viên lên textbox
            if (reader.Read())
            {
                id = reader.GetString(0);
                txtName.Text = reader.GetString(2) + " " + reader.GetString(1);
                txtEmail.Text = reader.GetString(3);
                txtPhone.Text = reader.GetString(4);
                txtTichCuc.Text = gvNhanVien.Rows[r].Cells[5].Value.ToString();
                cboIdStores.Text = reader.GetString(6);
                cboIdManager.Text = gvNhanVien.Rows[r].Cells[7].Value.ToString();
                reader.Close();
                txtId.Text = id;
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            // Tải id_manager lên comboBox
            TaiIdManagerLenCb();
        }

        private void TaiIdManagerLenCb()
        {
            SqlCommand cmdManager = new SqlCommand();
            cmdManager.CommandType = CommandType.Text;
            cmdManager.CommandText = "Select * from staffs";
            cmdManager.Connection = conn;

            SqlDataReader readerManager = cmdManager.ExecuteReader();
            cboIdManager.Items.Clear();
            while (readerManager.Read())
            {
                if (readerManager.GetString(0).CompareTo(txtId.Text) != 0)//khỏi thêm khi gặp mã của nhân viên đó
                {
                    //Thêm mã id vào comboBox quản lí
                    cboIdManager.Items.Add(readerManager.GetString(0));
                }
            }
            readerManager.Close();
        }

        //Thêm nhân viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            ktBtn = 1;
            AnHienCacBtn();
            CacTextBoxRong();
            txtId.Focus();
        }

        //Xóa nhân viên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")//Kiểm tra id có trống không
            {
                MessageBox.Show("ID không được trống.");
                return;
            }
            //Hỏi người dùng có muốn xóa
            DialogResult dr = MessageBox.Show(
            "Bạn có chắc chắn xóa nhân viên này?",
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
                    command.CommandText = "delete from staffs where staff_id=@id";
                    command.Connection = conn;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachNhanVien();
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

        //Chỉnh sửa thông tin nhân viên
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            ktBtn = 2;
            AnHienCacBtn();
        }

        //Kiểm tra email có hơp lệ
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if ((ktBtn == 1 || ktBtn == 2) && txtEmail.Text != "") //Kiểm tra có phải đang thêm hoặc chỉnh sửa không
            {
                //Kiểm tra email có @gmail.com ở cuối ko
                int checkGmail = txtEmail.Text.IndexOf("@gmail.com");//Kiểm tra email có trong chuỗi
                if (checkGmail == -1 || txtEmail.Text.Substring(checkGmail).Trim().Length > 10)
                {
                    MessageBox.Show("Email phải có @gmail.com ở sau.");
                    txtEmail.Text = "";
                }
                txtEmail.Text = txtEmail.Text.Trim();
            }
        }

        //Kiểm tra staff_id có trùng không
        private void txtId_Leave(object sender, EventArgs e)
        {
            if (ktBtn == 1) // Kiểm tra có phải đang thêm không
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    //Truy vấn SQL có 
                    cmd.CommandText = "select * from staffs where staff_id=" + txtId.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        MessageBox.Show("Lỗi: Trùng Id.");
                        txtId.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
            if(ktBtn == 2)// Khi thực hiện chỉnh sửa
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from staffs where staff_id=" + txtId.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    //Kiểm tra ID này có trong ID cửa hàng không
                    if (!reader.Read())
                    {
                        reader.Close();
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

            if (ktBtn == 1)//Thêm nhân viên
            {
                if(txtId.Text == "")//Kiểm tra id có trống không
                {
                    MessageBox.Show("ID không được trống.");
                    return;
                }
                if(txtName.Text == "")// Kiểm tra tên có trống không
                {
                    MessageBox.Show("Bạn chưa nhập tên.");
                    return;
                }
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into staffs(staff_id,first_name,last_name,email,phone,active,store_id,manager_id)" +
                    "values (@id,@fname,@lname,@email,@phone,@active,@store_id,@manager_id)";
                command.Connection = conn;
                //Tách họ&tên ra họ  và  tên đẹm
                string[] arr = txtName.Text.Split(' ');
                string lname = arr[0]; // họ
                string fname = "";
                for (int i = 1; i < arr.Length; i++)
                {
                    fname += (arr[i] + " ");
                }
                fname.Trim(); // tên

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;
                command.Parameters.Add("@fname", SqlDbType.VarChar).Value = fname;
                command.Parameters.Add("@lname", SqlDbType.VarChar).Value = lname;
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtPhone.Text;
                command.Parameters.Add("@active", SqlDbType.TinyInt).Value = txtTichCuc.Text;
                command.Parameters.Add("@store_id", SqlDbType.VarChar).Value = cboIdStores.Text;
                //Cho dữ liệu = null nếu comboBox = "".
                if (cboIdManager.Text.CompareTo("") == 0)
                {
                    command.Parameters.AddWithValue("@manager_id", DBNull.Value);
                }
                else
                {
                    command.Parameters.Add("@manager_id", SqlDbType.VarChar).Value = cboIdManager.Text;
                }

                int ret = command.ExecuteNonQuery();
                if (ret > 0)
                {
                    LoadDanhSachNhanVien();
                    MessageBox.Show("Thêm thành công.");
                }
            }
            else if(ktBtn == 2)//Chỉnh sửa thông tin nhân viên
            {
                if (txtId.Text == "")//Kiểm tra id có trống không
                {
                    MessageBox.Show("ID không được trống.");
                    return;
                }
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update staffs set first_name=@fname,last_name=@lname,email=@email," +
                        "phone=@phone,active=@active,store_id=@store_id,manager_id=@manager_id where staff_id=@id";
                    command.Connection = conn;
                    //Tách họtên
                    string[] arr = txtName.Text.Split(' ');
                    string lname = arr[0]; // họ
                    string fname = "";
                    for (int i = 1; i < arr.Length; i++)
                    {
                        fname += arr[i] + " ";
                    }
                    fname.Trim();//Tên

                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = txtId.Text;
                    command.Parameters.Add("@fname", SqlDbType.VarChar).Value = fname;
                    command.Parameters.Add("@lname", SqlDbType.VarChar).Value = lname;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtPhone.Text;
                    command.Parameters.Add("@active", SqlDbType.TinyInt).Value = txtTichCuc.Text;
                    command.Parameters.Add("@store_id", SqlDbType.VarChar).Value = cboIdStores.Text;
                    //Cho dữ liệu = null nếu comboBox = "".
                    if (cboIdManager.Text.CompareTo("") == 0)
                    {
                        command.Parameters.AddWithValue("@manager_id", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.Add("@manager_id", SqlDbType.VarChar).Value = cboIdManager.Text;
                    }

                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        LoadDanhSachNhanVien();
                        MessageBox.Show("Cập nhật thành công.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!");
                }
            }
            CacTextBoxRong();
            btnThem.Visible = true;
            btnCapNhat.Visible = true;
            btnXoa.Visible = true;
            btnLuu.Visible = false;
            btnHuy.Visible = false;
            ktBtn = 0;
        }

        //Ẩn hiện các button khi bấm thêm hoặc chỉnh sửa
        private void AnHienCacBtn()
        {
            if(ktBtn == 1)
            {
                btnThem.Visible = true;
                btnLuu.Visible = true;
                btnHuy.Visible = true;
                btnCapNhat.Visible = false;
                btnXoa.Visible = false;
            }
            else if(ktBtn == 2)
            {
                btnCapNhat.Visible = true;
                btnLuu.Visible = true;
                btnHuy.Visible = true;
                btnThem.Visible = false;
                btnXoa.Visible = false;
            }
        }

        //Hủy các hoạt động
        private void btnHuy_Click(object sender, EventArgs e)
        {
            CacTextBoxRong();
            btnThem.Visible = true;//Hiện btn thêm
            btnCapNhat.Visible = true;
            btnXoa.Visible = true;
            btnLuu.Visible = false;//Ẩn btn lưu
            btnHuy.Visible = false;
            ktBtn = 0;
        }

        //Cho các textbox rỗng 
        private void CacTextBoxRong()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtTichCuc.Text = "";
            cboIdStores.Text = "";
            cboIdManager.Text = "";
        }

        //Kiểm tra xem có ID này bên bảng Stores không
        private void cboIdStores_Leave(object sender, EventArgs e)
        {
            if(ktBtn == 1 || ktBtn == 2) //Kiểm tra khi thực hiện thêm hoặc chỉnh sửa thông tin nhân viên
            {
                //Mở kết nối
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    //Truy vấn đến sql xem mã id này có tồn tại không
                    cmd.CommandText = "select * from stores where store_id=" + cboIdStores.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read()) // Thông báo nếu mã Id không tồn tại
                    {
                        reader.Close();
                        MessageBox.Show("Id_store không tồn tại.");
                        cboIdStores.Text = "";
                    }
                    reader.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        //kiểm tra ID nhân viên có tồn tại không
        private void cboIdManager_Leave(object sender, EventArgs e)
        {
            if (ktBtn == 1 || ktBtn == 2) //Kiểm tra khi thực hiện thêm hoặc chỉnh sửa thông tin nhân viên
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
                    cmd.CommandText = "select * from staffs where staff_id=" + cboIdManager.Text + " and staff_id!=" + txtId.Text;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        MessageBox.Show("Id không tồn tại.");
                        cboIdManager.Text = "";
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
