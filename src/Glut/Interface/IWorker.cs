using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Glut.Interface
{
    public interface IWorker
    {
        ValueTask Run(HttpRequestMessage request, ThreadResult result, CancellationToken cancellationToken);
    }
}
