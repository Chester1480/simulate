using CIMSimulate.Service.UtilS;
using Dapper;

namespace CIMSimulate.Service.SimulateService
{
    public class JQservice
    {
        private DBService _dBService;
        public JQservice(IServiceProvider service)
        {
            _dBService = service.GetService<DBService>()!;
        }
        public async Task<dynamic> GetLastWorkOrder()
        {
            string sql = @"SELECT * FROM work_order where enable = 1 AND task_id = 0 order by create_time LIMIT 1;";
            DynamicParameters parameters = new DynamicParameters();
            var result = await _dBService.MysqlQueryFirstOrDefaultAsync(sql, parameters);
            return result;
        }
    }
}
