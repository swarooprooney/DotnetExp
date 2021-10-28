﻿namespace Tweetbook.Contracts.V1.Response
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public Response()
        {

        }

        public Response(T response)
        {
            Data = response;
        }
    }
}
