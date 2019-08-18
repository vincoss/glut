using Glut.Data;
using Glut.Interface;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Glut.Services
{
    public class MemoryResultStore : IResultStore
    {
        public void Add(string projectName, int runId, IDictionary<string, string> attributes, ThreadResult result)
        {
            throw new NotImplementedException();
        }
    }
}
