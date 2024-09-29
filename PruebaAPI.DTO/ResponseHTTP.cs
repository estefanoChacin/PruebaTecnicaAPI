using System;


namespace PruebaAPI.DTO
{
    public class ResponseHTTP<T>
    {
        public bool status { get; set; }
        public T Content { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
