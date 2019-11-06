using System;
using System.Reflection;


namespace Glut
{
    public static class GlutConstants
    {
        static GlutConstants()
        {
            GlutVersion = new AssemblyName(typeof(GlutConstants).Assembly.FullName).Version;
            FullApplicationName = $"{ApplicationName} - {GlutVersion}";
        }

        public static string ApplicationName = "Glut"; 
        public static string FullApplicationName { get; private set; }
        public static Version GlutVersion { get; private set; }

        public static string StartDateTime = "StartDateTime";
        public static string EndDateTime = "EndDateTime";
    }
}
