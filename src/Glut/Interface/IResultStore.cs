using Glut.Data;
using System.Linq;
using System.Threading.Tasks;


namespace Glut.Interface
{
    public interface IResultStore
    {
        void Add(ThreadResult result);
    }
}
