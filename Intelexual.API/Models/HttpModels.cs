using System;
using System.Net;

namespace Intelexual.API.Models
{
	public class HttpModels
	{
        public class HttpCoreResponse<Type>
        {
            public int status { get; set; } = (int)HttpStatusCode.OK;
            public string? message { get; set; }
            public Type? results { get; set; }
        }
    }
}

