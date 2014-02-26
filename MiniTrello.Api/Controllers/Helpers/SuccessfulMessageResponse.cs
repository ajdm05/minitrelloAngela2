using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MiniTrello.Api.Controllers.Helpers
{
    public class SuccessfulMessageResponse : HttpResponseException
    {
        public SuccessfulMessageResponse(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public SuccessfulMessageResponse(HttpResponseMessage response) : base(response)
        {
        }

        public SuccessfulMessageResponse(string successfulMessage)
            : base(HttpStatusCode.OK)
        {

            this.Response.ReasonPhrase = successfulMessage;
        }
    }
}