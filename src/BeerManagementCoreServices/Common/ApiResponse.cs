namespace BeerManagement.Web.Common
{
    public class ApiAttributes<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}