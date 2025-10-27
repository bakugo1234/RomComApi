using RomCom.Service.Data.Model.Contract;
using RomCom.Common.ServiceInstallers.Attributes;

namespace RomCom.Service.Data.Model.Common
{
    [TransientService]
    public class ServiceResult : IServiceResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public dynamic ResultData { get; set; }
        public int StatusCode { get; set; }
        public Meta MetaData { get; set; }
    }
}

