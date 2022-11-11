using Quartz;

namespace CIMSimulate.Service.ScheduleService
{
    //job可以繼續往下加
    public class Jobs : IJob
    {
        public Jobs(IServiceProvider service)
        {
            //_dealerService = service.GetService<DealerService>();
        }
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
