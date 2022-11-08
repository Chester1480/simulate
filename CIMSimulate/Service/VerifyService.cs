using System;
using System.Reflection;

namespace CIMSimulate.Service
{
    public class VerifyService
    {


        public async Task<string> Verify(dynamic obj,dynamic verifyObj)
        {
            var propertyInfos = obj.GetType().GetProperties();
            //propertyInfo
            //foreach (var item in verifyObj)
            //{

            //}

            foreach (var propertyInfo in propertyInfos)
            {
              
            }

            return "0";
        }

    }
}
