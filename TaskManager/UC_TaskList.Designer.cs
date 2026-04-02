namespace TaskManager
{
    partial class UC_TaskList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_TaskList));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlstripbtn_New = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstripbtn_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstripbtn_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstripbtn_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.tlstripbtn_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlstripbtn_EXENow = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stateimageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlbtn_New = new System.Windows.Forms.ToolStripButton();
            this.tlbtn_Edit = new System.Windows.Forms.ToolStripButton();
            this.tlbtn_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbtn_Run = new System.Windows.Forms.ToolStripButton();
            this.tlbtn_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbtn_Reload = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 27);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(953, 270);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.StateImageList = this.stateimageList;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "任务名称";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "运行频率";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "最近运行时间";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "下次运行时间";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "创建时间";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "备注";
            this.columnHeader6.Width = 50;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstripbtn_New,
            this.tlstripbtn_Edit,
            this.tlstripbtn_Delete,
            this.toolStripSeparator2,
            this.tlstripbtn_Run,
            this.tlstripbtn_Stop,
            this.toolStripSeparator4,
            this.tlstripbtn_EXENow});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 170);
            // 
            // tlstripbtn_New
            // 
            this.tlstripbtn_New.Image = ((System.Drawing.Image)(resources.GetObject("tlstripbtn_New.Image")));
            this.tlstripbtn_New.Name = "tlstripbtn_New";
            this.tlstripbtn_New.Size = new System.Drawing.Size(152, 22);
            this.tlstripbtn_New.Text = "新增";
            this.tlstripbtn_New.Click += new System.EventHandler(this.tlbtn_New_Click);
            // 
            // tlstripbtn_Edit
            // 
            this.tlstripbtn_Edit.Image = global::TaskManager.Properties.Resources.list_more;
            this.tlstripbtn_Edit.Name = "tlstripbtn_Edit";
            this.tlstripbtn_Edit.Size = new System.Drawing.Size(152, 22);
            this.tlstripbtn_Edit.Text = "修改";
            this.tlstripbtn_Edit.Click += new System.EventHandler(this.tlbtn_Edit_Click);
            // 
            // tlstripbtn_Delete
            // 
            this.tlstripbtn_Delete.Image = global::TaskManager.Properties.Resources.Delete;
            this.tlstripbtn_Delete.Name = "tlstripbtn_Delete";
            this.tlstripbtn_Delete.Size = new System.Drawing.Size(152, 22);
            this.tlstripbtn_Delete.Text = "删除";
            this.tlstripbtn_Delete.Click += new System.EventHandler(this.tlbtn_Delete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // tlstripbtn_Run
            // 
            this.tlstripbtn_Run.Image = global::TaskManager.Properties.Resources.Run;
            this.tlstripbtn_Run.Name = "tlstripbtn_Run";
            this.tlstripbtn_Run.Size = new System.Drawing.Size(152, 22);
            this.tlstripbtn_Run.Text = "启动";
            this.tlstripbtn_Run.Click += new System.EventHandler(this.tlbtn_Run_Click);
            // 
            // tlstripbtn_Stop
            // 
            this.tlstripbtn_Stop.Image = global::TaskManager.Properties.Resources.Stop;
            this.tlstripbtn_Stop.Name = "tlstripbtn_Stop";
            this.tlstripbtn_Stop.Size = new System.Drawing.Size(152, 22);
            this.tlstripbtn_Stop.Text = "停止";
            this.tlstripbtn_Stop.Click += new System.EventHandler(this.tlbtn_Stop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // tlstripbtn_EXENow
            // 
            this.tlstripbtn_EXENow.Image = global::TaskManager.Properties.Resources.Event;
            this.tlstripbtn_EXENow.Name = "tlstripbtn_EXENow";
            this.tlstripbtn_EXENow.Size = new System.Drawing.Size(152, 22);
            this.tlstripbtn_EXENow.Text = "立即执行";
            this.tlstripbtn_EXENow.Click += new System.EventHandler(this.tlstripbtn_EXENow_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Run.png");
            this.imageList1.Images.SetKeyName(1, "Stop.png");
            // 
            // stateimageList
            // 
            this.stateimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("stateimageList.ImageStream")));
            this.stateimageList.TransparentColor = System.Drawing.Color.Transparent;
            this.stateimageList.Images.SetKeyName(0, "clock.png");
            this.stateimageList.Images.SetKeyName(1, "Exeting.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbtn_New,
            this.tlbtn_Edit,
            this.tlbtn_Delete,
            this.toolStripSeparator1,
            this.tlbtn_Run,
            this.tlbtn_Stop,
            this.toolStripSeparator3,
            this.tlbtn_Reload});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(953, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlbtn_New
            // 
            this.tlbtn_New.Image = ((System.Drawing.Image)(resources.GetObject("tlbtn_New.Image")));
            this.tlbtn_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtn_New.Name = "tlbtn_New";
            this.tlbtn_New.Size = new System.Drawing.Size(57, 24);
            this.tlbtn_New.Text = "新增";
            this.tlbtn_New.Click += new System.EventHandler(this.tlbtn_New_Click);
            // 
            // tlbtn_Edit
            // 
            this.tlbtn_Edit.Image = global::TaskManager.Properties.Resources.list_more;
            this.tlbtn_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtn_Edit.Name = "tlbtn_Edit";
            this.tlbtn_Edit.Size = new System.Drawing.Size(57, 24);
            this.tlbtn_Edit.Text = "修改";
            this.tlbtn_Edit.Click += new System.EventHandler(this.tlbtn_Edit_Click);
            // 
            // tlbtn_Delete
            // 
            this.tlbtn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlbtn_Delete.Image")));
            this.tlbtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtn_Delete.Name = "tlbtn_Delete";
            this.tlbtn_Delete.Size = new System.Drawing.Size(57, 24);
            this.tlbtn_Delete.Text = "删除";
            this.tlbtn_Delete.Click += new System.EventHandler(this.tlbtn_Delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tlbtn_Run
            // 
            this.tlbtn_Run.Image = global::TaskManager.Properties.Resources.Run;
            this.tlbtn_Run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtn_Run.Name = "tlbtn_Run";
            this.tlbtn_Run.Size = new System.Drawing.Size(57, 24);
            this.tlbtn_Run.Text = "启动";
            this.tlbtn_Run.Click += new System.EventHandler(this.tlbtn_Run_Click);
            // 
            // tlbtn_Stop
            // 
            this.tlbtn_Stop.Image = global::TaskManager.Properties.Resources.Stop;
            this.tlbtn_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtn_Stop.Name = "tlbtn_Stop";
            this.tlbtn_Stop.Size = new System.Drawing.Size(57, 24);
            this.tlbtn_Stop.Text = "停止";
            this.tlbtn_Stop.Click += new System.EventHandler(this.tlbtn_Stop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tlbtn_Reload
            // 
            this.tlbtn_Reload.Image = global::TaskManager.Properties.Resources.reload;
            this.tlbtn_Reload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtn_Reload.Name = "tlbtn_Reload";
            this.tlbtn_Reload.Size = new System.Drawing.Size(57, 24);
            this.tlbtn_Reload.Text = "刷新";
            this.tlbtn_Reload.Click += new System.EventHandler(this.tlbtn_Reload_Click);
            // 
            // UC_TaskList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Name = "UC_TaskList";
            this.Size = new System.Drawing.Size(953, 297);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlbtn_New;
        private System.Windows.Forms.ToolStripButton tlbtn_Edit;
        private System.Windows.Forms.ToolStripButton tlbtn_Delete;
        private System.Windows.Forms.ImageList stateimageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlbtn_Run;
        private System.Windows.Forms.ToolStripButton tlbtn_Stop;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tlstripbtn_New;
        private System.Windows.Forms.ToolStripMenuItem tlstripbtn_Edit;
        private System.Windows.Forms.ToolStripMenuItem tlstripbtn_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tlstripbtn_Run;
        private System.Windows.Forms.ToolStripMenuItem tlstripbtn_Stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tlbtn_Reload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tlstripbtn_EXENow;
    }
}
