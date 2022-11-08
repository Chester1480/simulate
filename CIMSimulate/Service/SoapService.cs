﻿using System.Xml.Serialization;

namespace CIMSimulate.Service
{
    public class SoapService
    {
        public SoapService(IServiceProvider service)
        {

        }

        

        public void ObjectToXml(dynamic parameters)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(parameters.GetType());
            x.Serialize(Console.Out, parameters);
            string xml = @"
                <?xml version=""1.0"" encoding=""IBM437""?>
                <clsPerson xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <FirstName>Jeff</FirstName>
                    <MI>A</MI>
                    <LastName>Price</LastName>
                </clsPerson>
            ";
        }

    }
}
