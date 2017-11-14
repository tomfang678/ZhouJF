// 需要引用的命名空间  using System.Runtime.Serialization;  

using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhAreaInfo
{
    public class RoadbasicinfoDto : PagedQueryParam
    {
        public RoadbasicinfoDto()
        {

        }

        private int _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _RoadCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadCode", System.Data.DbType.String)]
        public string RoadCode
        {
            get { return _RoadCode; }
            set { _RoadCode = value; }
        }
        private string _RoadName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadName", System.Data.DbType.String)]
        public string RoadName
        {
            get { return _RoadName; }
            set { _RoadName = value; }
        }
        private string _RoadLevel;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadLevel", System.Data.DbType.String)]
        public string RoadLevel
        {
            get { return _RoadLevel; }
            set { _RoadLevel = value; }
        }
        private string _RoadMaterial;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadMaterial", System.Data.DbType.String)]
        public string RoadMaterial
        {
            get { return _RoadMaterial; }
            set { _RoadMaterial = value; }
        }
        private int? _BridgeNumber;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeNumber", System.Data.DbType.Int32)]
        public int? BridgeNumber
        {
            get { return _BridgeNumber; }
            set { _BridgeNumber = value; }
        }
        private string _AreaCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AreaCode", System.Data.DbType.String)]
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }
    }
}
