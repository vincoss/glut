using Glut.Interface;
using System;


namespace Glut.Services
{
    public class EnvironmentService : IEnvironment
    {
        public DateTime SytemDateTimeUtc
        {
            get { return DateTime.UtcNow; }
        }

        public string UserName
        {
            get { return Environment.UserName; }
        }
    }
}
