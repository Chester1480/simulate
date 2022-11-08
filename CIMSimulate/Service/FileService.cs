

using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CIMSimulate.Service
{
    public class FileService
    {
       

        public FileService(IServiceProvider service)
        {
          
        }

        public async Task<dynamic> ReadAsync(string path)
        {

            bool isFileExists = File.Exists(path);
            if (!isFileExists) { return "file is not exist"; };
            var result = await System.IO.File.ReadAllTextAsync(path);
            dynamic dynamicObj = JsonConvert.DeserializeObject(result);
            return dynamicObj;
        }

    }
}
