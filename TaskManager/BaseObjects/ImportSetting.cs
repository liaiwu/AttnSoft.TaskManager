using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using AttnSoft.SqlData;

namespace TaskManager.BaseObjects
{

    public class ImportSetting : Entity
    {
        //public override IDBServer DbServer
        //{
        //    get
        //    {
        //        if (ServerFactory.DefaultServerType != ServerType.SqlServer)
        //        {
        //            return ServerFactory.GetServer();
        //        }
        //        return ServerFactory.GetServer(ServerType.MasterDB);
        //    }
        //    set
        //    {
        //        base.DbServer = value;
        //    }
        //}
        public ImportSetting()
        {
            _TableName = "ZZ_EAIImportSetting";
            _ColumnId = "ID";
        }


        #region 属性
        public int ID
        {
            get
            {
                return base.GetProperty("ID", 0);
            }
            set
            {
                base.SetValue("ID", value);
            }
        }
        /// <summary>
        /// 分类：EAI，EAI导入设置，EAIExtra，EAI扩展字段导入
        /// </summary>
        public string TypeClass
        {
            get
            {
                return base.GetProperty("TypeClass", "");
            }
            set
            {
                base.SetValue("TypeClass", value);
            }
        }
        public string DataConnection
        {
            get
            {
                return base.GetProperty("DataConnection", "");
            }
            set
            {
                base.SetValue("DataConnection", value);
            }
        }
        public string DataSource
        {
            get
            {
                return base.GetProperty("DataSource", "");
            }
            set
            {
                base.SetValue("DataSource", value);
            }
        }
        public string DataMember
        {
            get
            {
                return base.GetProperty("DataMember", "");
            }
            set
            {
                base.SetValue("DataMember", value);
            }
        }
        public string EAI_Account
        {
            get
            {
                return base.GetProperty("EAI_Account", "");
            }
            set
            {
                base.SetValue("EAI_Account", value);
            }
        }
        public string EAI_Object
        {
            get
            {
                return base.GetProperty("EAI_Object", "");
            }
            set
            {
                base.SetValue("EAI_Object", value);
            }
        }
        public bool ImportAfterIsDeleteFile
        {
            get
            {
                return base.GetProperty("ImportAfterIsDeleteFile", false);
            }
            set
            {
                base.SetValue("ImportAfterIsDeleteFile", value);
            }
        }
        public string ImportAferExeQuery
        {
            get
            {
                return base.GetProperty("ImportAferExeQuery", "");
            }
            set
            {
                base.SetValue("ImportAferExeQuery", value);
            }
        }
        public string XmlTemplateName
        {
            get
            {
                return base.GetProperty("XmlTemplateName", "");
            }
            set
            {
                base.SetValue("XmlTemplateName", value);
            }
        }
        public Guid TaskID
        {
            get
            {
                return base.GetProperty("TaskID", Guid.Empty);

            }
            set
            {
                base.SetValue("TaskID", value);
            }
        }
        public string ProcName
        {
            get
            {
                return base.GetProperty("ProcName", "add");
            }
            set
            {
                base.SetValue("ProcName", value);
            }
        }

        public string DestDataSource
        {
            get
            {
                return base.GetProperty("DestDataSource", "");
            }
            set
            {
                base.SetValue("DestDataSource", value);
            }
        }
        #endregion
    }

    public struct EAITypeClass
    {
        public static readonly string EAI = "EAI";
        public static readonly string EAIExtra = "EAIExtra";
        public static readonly string AutoExeSql = "AutoExeSql";
    }
}
