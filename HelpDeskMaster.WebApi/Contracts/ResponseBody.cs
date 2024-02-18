namespace HelpDeskMaster.WebApi.Contracts
{
    public class ResponseBody<T> where T : class
    {
        public ResponseBody(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}