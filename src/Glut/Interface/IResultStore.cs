using Glut.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Glut.Interface
{
    public interface IResultStore
    {
        void Add(string projectName, int runId, IDictionary<string, string> attributes, ThreadResult result);
    }
}
