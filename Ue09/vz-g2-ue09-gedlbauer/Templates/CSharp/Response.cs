using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CurrencyConverter.Blazor.Services
{
  public class Response
  {
    public HttpStatusCode StatusCode { get; private set; }

    public Response(HttpStatusCode statusCode) => this.StatusCode = statusCode;
  }

  public class Response<T> : Response
  {
    public T Result { get; private set; }

    public Response(T result, HttpStatusCode statusCode = HttpStatusCode.OK) : base(statusCode)
    {
      this.Result = result;
    }
  }
}
