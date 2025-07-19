namespace login_app.DTOs
{
    public class ResponseModel<T>
    {
        public bool error { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
    }
}
