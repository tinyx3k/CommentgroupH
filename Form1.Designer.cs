namespace Commentgroup
{
    partial class Form1
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
            this.openAccs = new System.Windows.Forms.Button();
            this.openComments = new System.Windows.Forms.Button();
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.numberComment = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.delayfrom = new System.Windows.Forms.NumericUpDown();
            this.delayto = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Reload = new System.Windows.Forms.Button();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hisstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.link = new System.Windows.Forms.DataGridViewLinkColumn();
            this.openImage = new System.Windows.Forms.Button();
            this.cmimage = new System.Windows.Forms.CheckBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stop = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.An = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cookie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createListFriend = new System.Windows.Forms.DataGridViewButtonColumn();
            this.isdemnguoc = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayfrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // openAccs
            // 
            this.openAccs.Location = new System.Drawing.Point(51, 13);
            this.openAccs.Name = "openAccs";
            this.openAccs.Size = new System.Drawing.Size(128, 23);
            this.openAccs.TabIndex = 0;
            this.openAccs.Text = "Mở file tài khoản";
            this.openAccs.UseVisualStyleBackColor = true;
            this.openAccs.Click += new System.EventHandler(this.openAccs_Click);
            // 
            // openComments
            // 
            this.openComments.Location = new System.Drawing.Point(211, 12);
            this.openComments.Name = "openComments";
            this.openComments.Size = new System.Drawing.Size(124, 23);
            this.openComments.TabIndex = 1;
            this.openComments.Text = "Mở file comment";
            this.openComments.UseVisualStyleBackColor = true;
            this.openComments.Click += new System.EventHandler(this.openComments_Click);
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.AllowUserToAddRows = false;
            this.dgvAccounts.AllowUserToDeleteRows = false;
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.status,
            this.stop,
            this.An,
            this.action,
            this.cookie,
            this.createListFriend,
            this.isdemnguoc});
            this.dgvAccounts.Location = new System.Drawing.Point(12, 98);
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.Size = new System.Drawing.Size(888, 203);
            this.dgvAccounts.TabIndex = 2;
            this.dgvAccounts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccounts_CellContentClick);
            this.dgvAccounts.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccounts_CellContentDoubleClick);
            this.dgvAccounts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvAccounts_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Số comment trong 1 group:";
            // 
            // numberComment
            // 
            this.numberComment.Location = new System.Drawing.Point(211, 52);
            this.numberComment.Name = "numberComment";
            this.numberComment.Size = new System.Drawing.Size(38, 20);
            this.numberComment.TabIndex = 5;
            this.numberComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numberComment.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label2.Location = new System.Drawing.Point(502, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Thời gian delay từ:";
            // 
            // delayfrom
            // 
            this.delayfrom.Location = new System.Drawing.Point(602, 12);
            this.delayfrom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.delayfrom.Name = "delayfrom";
            this.delayfrom.Size = new System.Drawing.Size(52, 20);
            this.delayfrom.TabIndex = 7;
            this.delayfrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.delayfrom.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // delayto
            // 
            this.delayto.Location = new System.Drawing.Point(716, 12);
            this.delayto.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.delayto.Name = "delayto";
            this.delayto.Size = new System.Drawing.Size(45, 20);
            this.delayto.TabIndex = 8;
            this.delayto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.delayto.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label3.Location = new System.Drawing.Point(671, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "đến:";
            // 
            // Reload
            // 
            this.Reload.BackColor = System.Drawing.SystemColors.Control;
            this.Reload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Reload.Location = new System.Drawing.Point(273, 48);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(78, 23);
            this.Reload.TabIndex = 10;
            this.Reload.Text = "Reload";
            this.Reload.UseVisualStyleBackColor = false;
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            // 
            // dgvHistory
            // 
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stt,
            this.time,
            this.hisstatus,
            this.link});
            this.dgvHistory.Location = new System.Drawing.Point(12, 324);
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.Size = new System.Drawing.Size(888, 286);
            this.dgvHistory.TabIndex = 11;
            this.dgvHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistory_CellContentClick);
            // 
            // stt
            // 
            this.stt.DataPropertyName = "stt";
            this.stt.HeaderText = "stt";
            this.stt.Name = "stt";
            this.stt.ReadOnly = true;
            this.stt.Width = 50;
            // 
            // time
            // 
            this.time.DataPropertyName = "time";
            this.time.HeaderText = "time";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            this.time.Width = 150;
            // 
            // hisstatus
            // 
            this.hisstatus.DataPropertyName = "status";
            this.hisstatus.HeaderText = "Status";
            this.hisstatus.Name = "hisstatus";
            this.hisstatus.ReadOnly = true;
            this.hisstatus.Width = 250;
            // 
            // link
            // 
            this.link.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.link.DataPropertyName = "link";
            this.link.HeaderText = "link";
            this.link.Name = "link";
            this.link.ReadOnly = true;
            // 
            // openImage
            // 
            this.openImage.Location = new System.Drawing.Point(356, 12);
            this.openImage.Name = "openImage";
            this.openImage.Size = new System.Drawing.Size(117, 23);
            this.openImage.TabIndex = 12;
            this.openImage.Text = "Mở thư mục ảnh";
            this.openImage.UseVisualStyleBackColor = true;
            this.openImage.Click += new System.EventHandler(this.openImage_Click);
            // 
            // cmimage
            // 
            this.cmimage.AutoSize = true;
            this.cmimage.Checked = true;
            this.cmimage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmimage.Location = new System.Drawing.Point(372, 52);
            this.cmimage.Name = "cmimage";
            this.cmimage.Size = new System.Drawing.Size(118, 17);
            this.cmimage.TabIndex = 13;
            this.cmimage.Text = "Comment cùng ảnh";
            this.cmimage.UseVisualStyleBackColor = true;
            this.cmimage.CheckedChanged += new System.EventHandler(this.cmimage_CheckedChanged);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.Frozen = true;
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ToolTipText = "Tạm dừng";
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.Width = 325;
            // 
            // stop
            // 
            this.stop.DataPropertyName = "stop";
            this.stop.HeaderText = "stop";
            this.stop.Name = "stop";
            this.stop.Width = 60;
            // 
            // An
            // 
            this.An.DataPropertyName = "an";
            this.An.HeaderText = "An";
            this.An.Name = "An";
            this.An.Width = 60;
            // 
            // action
            // 
            this.action.DataPropertyName = "action";
            this.action.HeaderText = "action";
            this.action.Name = "action";
            // 
            // cookie
            // 
            this.cookie.DataPropertyName = "cookie";
            this.cookie.HeaderText = "cookie";
            this.cookie.Name = "cookie";
            this.cookie.ReadOnly = true;
            this.cookie.Visible = false;
            // 
            // createListFriend
            // 
            this.createListFriend.DataPropertyName = "createListFriend";
            this.createListFriend.HeaderText = "Tạo DS BB";
            this.createListFriend.Name = "createListFriend";
            // 
            // isdemnguoc
            // 
            this.isdemnguoc.DataPropertyName = "isdemnguoc";
            this.isdemnguoc.HeaderText = "isdemnguoc";
            this.isdemnguoc.Name = "isdemnguoc";
            this.isdemnguoc.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 618);
            this.Controls.Add(this.cmimage);
            this.Controls.Add(this.openImage);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.Reload);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.delayto);
            this.Controls.Add(this.delayfrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numberComment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAccounts);
            this.Controls.Add(this.openComments);
            this.Controls.Add(this.openAccs);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayfrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openAccs;
        private System.Windows.Forms.Button openComments;
        private System.Windows.Forms.DataGridView dgvAccounts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numberComment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown delayfrom;
        private System.Windows.Forms.NumericUpDown delayto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Reload;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button openImage;
        private System.Windows.Forms.CheckBox cmimage;
        private System.Windows.Forms.DataGridViewTextBoxColumn stt;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn hisstatus;
        private System.Windows.Forms.DataGridViewLinkColumn link;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewCheckBoxColumn stop;
        private System.Windows.Forms.DataGridViewCheckBoxColumn An;
        private System.Windows.Forms.DataGridViewButtonColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn cookie;
        private System.Windows.Forms.DataGridViewButtonColumn createListFriend;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isdemnguoc;
    }
}

