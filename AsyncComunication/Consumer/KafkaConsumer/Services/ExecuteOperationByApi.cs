using System;
using System.ComponentModel;
using System.Net.Http;
namespace KafkaConsumer.Services
{
    public class ExecuteOperationByApi : MyAbstractBase
    {
        protected override int ExecuteOperation(int val1, int val2)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://operationapi.com");

            var myReques = new
            {
                Val1 = val1,
                Val2 = val2
            };
            var response = client.PostAsync(new Uri("http://operationapi.com"), new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(myReques))).Result;

            return 1;
        }
    }
}