using AttnSoft.DevControls;
using System;
using System.Windows.Forms;
using System.Xml.Linq;
using TaskManager.BaseObjects;

namespace TaskManager
{
    public partial class UC_EXESqlSetting : XtraEditBase
    {
        private ImportSetting impSetting=new ImportSetting ();
        public UC_EXESqlSetting()
        {
            InitializeComponent();
        }
        public string Params
        {
            get
            {
                //XDocument xdoc=new XDocument ();
                XElement root = new XElement("EXESQL");
                XAttribute attrbute = new XAttribute("Connection", this.wtbtn_ConnectSQL.Text);
                root.Add(attrbute);

                attrbute = new XAttribute("SQL", this.rich_Sql.Text);
                root.Add(attrbute);
                //xdoc.Add(root);
                return root.ToString(SaveOptions.None);
            }
            set
            {
                try
                {
                    XElement root = XElement.Parse(value);
                    XAttribute attrbute = root.Attribute("Connection");
                    if (attrbute != null)
                    {
                        this.wtbtn_ConnectSQL.Text = attrbute.Value;
                    }
                    attrbute = root.Attribute("SQL");
                    if (attrbute != null)
                    {
                        this.rich_Sql.Text = attrbute.Value;
                    }

                }
                catch
                { }
            }
        }

        public override void New(object sender, EventArgs e)
        {
            base.isNew = true;
            //isNew = true;
            this.rich_Sql.Text=this.wtbtn_ConnectSQL.Text = string.Empty;
            impSetting.Clear();
            impSetting.TypeClass = EAITypeClass.AutoExeSql;
        }
        //public override bool Save(object sender, EventArgs e)
        //{
        //    impSetting.DataConnection = this.wtbtn_ConnectSQL.Text;
        //    impSetting.DataSource = this.rich_Sql.Text;
        //    if (isNew)
        //    {
        //        impSetting = impSetting.InsertGetEntity<ImportSetting>();
        //        isNew = false;
        //    }
        //    else
        //    {
        //        impSetting.SaveChanges();
        //    }
        //    return true;
        //}


        public override void Delete(object sender, EventArgs e)
        {
            base.Delete(sender, e);
            this.wtbtn_ConnectSQL.Text = this.rich_Sql.Text = string.Empty;
        }

        private void wtbtn_ConnectSQL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var connectServer = new FmSQLConnector();
            connectServer.DefaultDbase = string.Empty;
            if (connectServer.ShowDialog(FindForm()) == DialogResult.OK)
            {
                this.wtbtn_ConnectSQL.Text = connectServer.ConnectionString;
            }
        }
    }
}
