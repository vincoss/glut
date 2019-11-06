using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Glut.Interface
{
    public interface IRequestMessageProvider
    {
        IEnumerable<HttpRequestMessage> Get();
    }
}
