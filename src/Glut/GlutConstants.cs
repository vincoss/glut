using System;
using System.Reflection;


namespace Glut
{
    public static class GlutConstants
    {
        public static string ApplicationName = "Glut";
        public static string FullApplicationName = $"{ApplicationName} - {Version}"; // TODO: does not work
        public static Version Version = new AssemblyName(typeof(GlutConstants).Assembly.FullName).Version;

    }
}
