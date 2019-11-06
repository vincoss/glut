using System;
using System.Collections.Generic;
using System.Text;

namespace Glut.Interface
{
    public interface IEnvironment
    {
        DateTime SystemDateTimeUtc { get; }

        string UserName { get; }
    }
}
