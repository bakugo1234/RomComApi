namespace RomCom.Service.Data.Model.Contract
{
    public interface IServiceResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public dynamic ResultData { get; set; }
        public int StatusCode { get; set; }
        public Meta MetaData { get; set; }
    }

    public class Meta
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}

