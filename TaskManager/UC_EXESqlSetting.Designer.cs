namespace TaskManager
{
    partial class UC_EXESqlSetting
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.rich_Sql = new System.Windows.Forms.RichTextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.wtbtn_ConnectSQL = new DevExpress.XtraEditors.ButtonEdit();
            this.btn_Ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wtbtn_ConnectSQL.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "执行的SQ 或存储过程";
            // 
            // rich_Sql
            // 
            this.rich_Sql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rich_Sql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rich_Sql.Location = new System.Drawing.Point(24, 114);
            this.rich_Sql.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rich_Sql.Name = "rich_Sql";
            this.rich_Sql.Size = new System.Drawing.Size(946, 307);
            this.rich_Sql.TabIndex = 4;
            this.rich_Sql.Text = "";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(770, 438);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(160, 58);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // wtbtn_ConnectSQL
            // 
            this.wtbtn_ConnectSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wtbtn_ConnectSQL.Location = new System.Drawing.Point(24, 17);
            this.wtbtn_ConnectSQL.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.wtbtn_ConnectSQL.Name = "wtbtn_ConnectSQL";
            this.wtbtn_ConnectSQL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.wtbtn_ConnectSQL.Size = new System.Drawing.Size(952, 44);
            this.wtbtn_ConnectSQL.TabIndex = 2;
            this.wtbtn_ConnectSQL.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.wtbtn_ConnectSQL_ButtonClick);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(552, 438);
            this.btn_Ok.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(160, 58);
            this.btn_Ok.TabIndex = 5;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // UC_EXESqlSetting
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Font = new System.Drawing.Font("宋体", 10F);
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.rich_Sql);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.wtbtn_ConnectSQL);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "UC_EXESqlSetting";
            this.Size = new System.Drawing.Size(1018, 521);
            ((System.ComponentModel.ISupportInitialize)(this.wtbtn_ConnectSQL.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit wtbtn_ConnectSQL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rich_Sql;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
    }
}
