using Akavache;
using Covid19Stats.Constant;
using Covid19Stats.Interface;
using Covid19Stats.Model;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Covid19Stats.Services.Data
{
    class WorldDataService : IWorldDataService
    {
        private readonly IRepository<CoronaStats> _coronaStatsRepository;
        public WorldDataService(IRepository<CoronaStats> coronaStatsRepository)
        {
            _coronaStatsRepository = coronaStatsRepository;
        }
        public async Task<CoronaStats> GetTotalStats()
        {
            return await BlobCache.LocalMachine.GetOrFetchObject<CoronaStats>("worldSummary",
           async () => await _coronaStatsRepository.GetAsync(Endpoint.getWorldSummary), DateTimeOffset.Now.AddHours(4));
        }
    }
}
