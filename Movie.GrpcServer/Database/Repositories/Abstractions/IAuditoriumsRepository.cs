using Movie.GrpcServer.Database.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.GrpcServer.Database.Repositories.Abstractions
{
    public interface IAuditoriumsRepository
    {
        Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel);
    }
}