using SharedLibrary.Domains;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Communications
{
    public interface IMassTransitHelper
    {
        Task PrepareReport(ReportDTO report);
    }
}
