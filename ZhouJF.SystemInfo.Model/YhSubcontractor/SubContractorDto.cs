// 需要引用的命名空间  using System.Runtime.Serialization;  

using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.Subcontractor
{
    public class SubContractorDto : PagedQueryParam
    {
        public SubContractorDto()
        {

        }

        private string _SubContractorCode;

        public string SubContractorCode
        {
            get { return _SubContractorCode; }
            set { _SubContractorCode = value; }
        }
        private string _SubContractorCrop;
        public string SubContractorCrop
        {
            get { return _SubContractorCrop; }
            set { _SubContractorCrop = value; }
        }
        private string _SubContractorBoss;
        public string SubContractorBoss
        {
            get { return _SubContractorBoss; }
            set { _SubContractorBoss = value; }
        }
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

    }
}
