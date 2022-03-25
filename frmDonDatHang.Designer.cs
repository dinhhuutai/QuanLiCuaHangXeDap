
namespace Quanlicuahangxedap
{
    partial class frmDonDatHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboIdNhanVien = new System.Windows.Forms.ComboBox();
            this.cboIdCuaHang = new System.Windows.Forms.ComboBox();
            this.cboIdKhachHang = new System.Windows.Forms.ComboBox();
            this.dtpNgayVanChuyen = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayYeuCau = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayDatHang = new System.Windows.Forms.DateTimePicker();
            this.txtTinhTrangDatHang = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gvDonDatHang = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonDatHang)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.btnLuu);
            this.panel1.Controls.Add(this.btnSua);
            this.panel1.Controls.Add(this.btnXoa);
            this.panel1.Controls.Add(this.btnThem);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 694);
            this.panel1.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Image = global::Quanlicuahangxedap.Properties.Resources.icons8_cancel_24;
            this.btnHuy.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHuy.Location = new System.Drawing.Point(0, 571);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(234, 99);
            this.btnHuy.TabIndex = 10;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Image = global::Quanlicuahangxedap.Properties.Resources.icons8_save_24;
            this.btnLuu.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLuu.Location = new System.Drawing.Point(0, 466);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(234, 105);
            this.btnLuu.TabIndex = 9;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnSua
            // 
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Image = global::Quanlicuahangxedap.Properties.Resources.icons8_edit_24;
            this.btnSua.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSua.Location = new System.Drawing.Point(0, 361);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(234, 105);
            this.btnSua.TabIndex = 8;
            this.btnSua.Text = "Sửa";
            this.btnSua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Image = global::Quanlicuahangxedap.Properties.Resources.icons8_delete_24;
            this.btnXoa.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXoa.Location = new System.Drawing.Point(0, 256);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(234, 105);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Image = global::Quanlicuahangxedap.Properties.Resources.icons8_plus_24;
            this.btnThem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThem.Location = new System.Drawing.Point(0, 151);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(234, 105);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm";
            this.btnThem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::Quanlicuahangxedap.Properties.Resources.pngtree_cycling_race_vector_logo_design_bicycle_shop_logo_design_template_image_322963;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 151);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.groupBox1.Controls.Add(this.txtTimKiem);
            this.groupBox1.Controls.Add(this.btnTimKiem);
            this.groupBox1.Controls.Add(this.cboIdNhanVien);
            this.groupBox1.Controls.Add(this.cboIdCuaHang);
            this.groupBox1.Controls.Add(this.cboIdKhachHang);
            this.groupBox1.Controls.Add(this.dtpNgayVanChuyen);
            this.groupBox1.Controls.Add(this.dtpNgayYeuCau);
            this.groupBox1.Controls.Add(this.dtpNgayDatHang);
            this.groupBox1.Controls.Add(this.txtTinhTrangDatHang);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(238, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(971, 367);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chi tiết:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(270, 308);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(332, 35);
            this.txtTimKiem.TabIndex = 16;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.ForeColor = System.Drawing.Color.Black;
            this.btnTimKiem.Image = global::Quanlicuahangxedap.Properties.Resources.icons8_search_241;
            this.btnTimKiem.Location = new System.Drawing.Point(608, 304);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(162, 43);
            this.btnTimKiem.TabIndex = 15;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboIdNhanVien
            // 
            this.cboIdNhanVien.DropDownHeight = 150;
            this.cboIdNhanVien.FormattingEnabled = true;
            this.cboIdNhanVien.IntegralHeight = false;
            this.cboIdNhanVien.Location = new System.Drawing.Point(637, 238);
            this.cboIdNhanVien.Name = "cboIdNhanVien";
            this.cboIdNhanVien.Size = new System.Drawing.Size(311, 37);
            this.cboIdNhanVien.TabIndex = 14;
            this.cboIdNhanVien.TextChanged += new System.EventHandler(this.cboIdNhanVien_TextChanged);
            this.cboIdNhanVien.Leave += new System.EventHandler(this.cboIdNhanVien_Leave);
            // 
            // cboIdCuaHang
            // 
            this.cboIdCuaHang.DropDownHeight = 150;
            this.cboIdCuaHang.FormattingEnabled = true;
            this.cboIdCuaHang.IntegralHeight = false;
            this.cboIdCuaHang.Location = new System.Drawing.Point(637, 166);
            this.cboIdCuaHang.Name = "cboIdCuaHang";
            this.cboIdCuaHang.Size = new System.Drawing.Size(311, 37);
            this.cboIdCuaHang.TabIndex = 13;
            this.cboIdCuaHang.Leave += new System.EventHandler(this.cboIdCuaHang_Leave);
            // 
            // cboIdKhachHang
            // 
            this.cboIdKhachHang.DropDownHeight = 150;
            this.cboIdKhachHang.FormattingEnabled = true;
            this.cboIdKhachHang.IntegralHeight = false;
            this.cboIdKhachHang.Location = new System.Drawing.Point(660, 101);
            this.cboIdKhachHang.Name = "cboIdKhachHang";
            this.cboIdKhachHang.Size = new System.Drawing.Size(288, 37);
            this.cboIdKhachHang.TabIndex = 12;
            this.cboIdKhachHang.Leave += new System.EventHandler(this.cboIdKhachHang_Leave);
            // 
            // dtpNgayVanChuyen
            // 
            this.dtpNgayVanChuyen.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayVanChuyen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayVanChuyen.Location = new System.Drawing.Point(230, 240);
            this.dtpNgayVanChuyen.Name = "dtpNgayVanChuyen";
            this.dtpNgayVanChuyen.Size = new System.Drawing.Size(192, 35);
            this.dtpNgayVanChuyen.TabIndex = 11;
            // 
            // dtpNgayYeuCau
            // 
            this.dtpNgayYeuCau.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayYeuCau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayYeuCau.Location = new System.Drawing.Point(202, 168);
            this.dtpNgayYeuCau.Name = "dtpNgayYeuCau";
            this.dtpNgayYeuCau.Size = new System.Drawing.Size(220, 35);
            this.dtpNgayYeuCau.TabIndex = 10;
            // 
            // dtpNgayDatHang
            // 
            this.dtpNgayDatHang.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayDatHang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayDatHang.Location = new System.Drawing.Point(203, 98);
            this.dtpNgayDatHang.Name = "dtpNgayDatHang";
            this.dtpNgayDatHang.Size = new System.Drawing.Size(220, 35);
            this.dtpNgayDatHang.TabIndex = 9;
            // 
            // txtTinhTrangDatHang
            // 
            this.txtTinhTrangDatHang.Location = new System.Drawing.Point(704, 38);
            this.txtTinhTrangDatHang.Name = "txtTinhTrangDatHang";
            this.txtTinhTrangDatHang.Size = new System.Drawing.Size(244, 35);
            this.txtTinhTrangDatHang.TabIndex = 8;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(72, 38);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(351, 35);
            this.txtId.TabIndex = 7;
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(473, 246);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 29);
            this.label8.TabIndex = 3;
            this.label8.Text = "ID_Nhân viên:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(473, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 29);
            this.label7.TabIndex = 6;
            this.label7.Text = "ID_Cửa hàng:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(473, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 29);
            this.label6.TabIndex = 5;
            this.label6.Text = "ID_Khách hàng:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(473, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tình trạng đặt hàng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ngày vận chuyển:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ngày yêu cầu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ngày đặt hàng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // gvDonDatHang
            // 
            this.gvDonDatHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDonDatHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvDonDatHang.Location = new System.Drawing.Point(238, 367);
            this.gvDonDatHang.Name = "gvDonDatHang";
            this.gvDonDatHang.RowHeadersWidth = 62;
            this.gvDonDatHang.RowTemplate.Height = 28;
            this.gvDonDatHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvDonDatHang.Size = new System.Drawing.Size(971, 327);
            this.gvDonDatHang.TabIndex = 2;
            this.gvDonDatHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDonDatHang_CellClick);
            // 
            // frmDonDatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 694);
            this.Controls.Add(this.gvDonDatHang);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "frmDonDatHang";
            this.Text = "Danh Sách Đơn Đặt Hàng";
            this.Load += new System.EventHandler(this.frmDonDatHang_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonDatHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gvDonDatHang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cboIdNhanVien;
        private System.Windows.Forms.ComboBox cboIdCuaHang;
        private System.Windows.Forms.ComboBox cboIdKhachHang;
        private System.Windows.Forms.DateTimePicker dtpNgayVanChuyen;
        private System.Windows.Forms.DateTimePicker dtpNgayYeuCau;
        private System.Windows.Forms.DateTimePicker dtpNgayDatHang;
        private System.Windows.Forms.TextBox txtTinhTrangDatHang;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
    }
}