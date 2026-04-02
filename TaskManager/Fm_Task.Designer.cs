namespace TaskManager
{
    partial class Fm_Task
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
            this.txt_CronExpressionString = new DevExpress.XtraEditors.ButtonEdit();
            this.txt_TaskParam = new DevExpress.XtraEditors.ButtonEdit();
            this.txt_Remark = new System.Windows.Forms.RichTextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Class = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Assembly = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_CronRemark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_taskname = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();

            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_CronExpressionString);
            this.panel1.Controls.Add(this.txt_TaskParam);
            this.panel1.Controls.Add(this.txt_Remark);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_Class);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txt_Assembly);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_CronRemark);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_taskname);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 262);
            this.panel1.TabIndex = 0;
            // 
            // txt_CronExpressionString
            // 
            this.txt_CronExpressionString.Location = new System.Drawing.Point(133, 39);
            this.txt_CronExpressionString.Name = "txt_CronExpressionString";
            this.txt_CronExpressionString.Size = new System.Drawing.Size(200, 22);
            this.txt_CronExpressionString.TabIndex = 20;
            this.txt_CronExpressionString.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txt_CronExpressionString_ButtonClick);
            // 
            // txt_TaskParam
            // 
            this.txt_TaskParam.Location = new System.Drawing.Point(462, 11);
            this.txt_TaskParam.Name = "txt_TaskParam";
            this.txt_TaskParam.Size = new System.Drawing.Size(199, 22);
            this.txt_TaskParam.TabIndex = 19;
            this.txt_TaskParam.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txt_TaskParam_ButtonClick);
            // 
            // txt_Remark
            // 
            this.txt_Remark.Location = new System.Drawing.Point(136, 127);
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(525, 129);
            this.txt_Remark.TabIndex = 18;
            this.txt_Remark.Text = "";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(277, 99);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(53, 18);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "停止";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(136, 99);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 18);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.Text = "运行";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 14);
            this.label8.TabIndex = 14;
            this.label8.Text = "任务状态";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 12;
            this.label7.Text = "备注";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(361, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "类全名";
            // 
            // txt_Class
            // 
            this.txt_Class.Location = new System.Drawing.Point(461, 67);
            this.txt_Class.Name = "txt_Class";
            this.txt_Class.Size = new System.Drawing.Size(200, 23);
            this.txt_Class.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "程序集名称";
            // 
            // txt_Assembly
            // 
            this.txt_Assembly.Location = new System.Drawing.Point(133, 67);
            this.txt_Assembly.Name = "txt_Assembly";
            this.txt_Assembly.Size = new System.Drawing.Size(200, 23);
            this.txt_Assembly.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(361, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "表达式说明";
            // 
            // txt_CronRemark
            // 
            this.txt_CronRemark.Location = new System.Drawing.Point(461, 37);
            this.txt_CronRemark.Name = "txt_CronRemark";
            this.txt_CronRemark.Size = new System.Drawing.Size(200, 23);
            this.txt_CronRemark.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "周期(Cron)表达式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "任务参数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "任务名称";
            // 
            // txt_taskname
            // 
            this.txt_taskname.Location = new System.Drawing.Point(133, 10);
            this.txt_taskname.Name = "txt_taskname";
            this.txt_taskname.Size = new System.Drawing.Size(200, 23);
            this.txt_taskname.TabIndex = 0;
            // 
            // panel2
            // 

            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Controls.Add(this.btn_Ok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 262);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(678, 52);
            this.panel2.TabIndex = 1;

            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(568, 12);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(80, 28);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "返回";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(462, 12);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(80, 28);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // Fm_Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 314);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fm_Task";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "任务调度管理";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Class;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Assembly;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_CronRemark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_taskname;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox txt_Remark;
        private DevExpress.XtraEditors.ButtonEdit txt_TaskParam;
        private DevExpress.XtraEditors.ButtonEdit txt_CronExpressionString;
    }
}