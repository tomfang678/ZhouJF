using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfModule
{
    [DataContract]
    [ModelMap("BF_MODULE", "BfModule", "ID")]
    [KnownType(typeof (BfModuleInfo))]
    public class BfModuleInfo : BaseModel
    {
        [DataMember]
        [ColumnMap("ID", DbType.Int32)]
        public int ID { get; set; }

        [DataMember]
        [ColumnMap("CODE", DbType.String)]
        public string CODE { get; set; }

        [DataMember]
        [ColumnMap("NAME", DbType.String)]
        public string NAME { get; set; }

        [DataMember]
        [ColumnMap("PARENT_ID", DbType.Int32)]
        public int? PARENT_ID { get; set; }

        [DataMember]
        [ColumnMap("REMARK", DbType.String)]
        public string REMARK { get; set; }

        [DataMember]
        [ColumnMap("CREATE_USER", DbType.Int32)]
        public int CREATE_USER { get; set; }

        [DataMember]
        [ColumnMap("CREATE_DATE", DbType.DateTime)]
        public DateTime CREATE_DATE { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_USER", DbType.Int32)]
        public int LAST_MODIFIED_USER { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_TIME", DbType.DateTime)]
        public DateTime LAST_MODIFIED_TIME { get; set; }

        [DataMember]
        [ColumnMap("URL", DbType.String)]
        public string URL { get; set; }

        [DataMember]
        [ColumnMap("ORDER", DbType.Int32)]
        public int ORDER { get; set; }

        [DataMember]
        [ColumnMap("ICON", DbType.String)]
        public string ICON { get; set; }

        [DataMember]
        [ColumnMap("IsNeedControl", DbType.Boolean)]
        public bool IsNeedControl { get; set; }

        /// <summary>
        ///     子节点
        /// </summary>
        [DataMember]
        public List<BfModuleInfo> Items { get; set; }

        /// <summary>
        ///     普通模块操作
        /// </summary>
        [DataMember]
        public List<BfModuleOperationInfo> OperationItems { get; set; }

        /// <summary>
        ///     其他模块操作
        /// </summary>
        [DataMember]
        public List<BfModuleOtherOperationInfo> OtherOperationItems { get; set; }
    }
}