using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.OutSourceService
{
    public class OutSourceApi: IOutSourceApi
    {
        public string Create(string URL, object inputObject)
        {
            string resData;
            try
            {
                using (var client = new HttpClient())
                {
                    var serializedObject = JsonConvert.SerializeObject(inputObject);
                    var objectBody = new StringContent(serializedObject);
                    objectBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultApi = client.PostAsync(URL, objectBody).Result;
                    var resultContent = resultApi.Content.ReadAsStringAsync();
                    resData = resultContent.Result;
                }
                return resData;

            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public string Get(string URL, object inputObject)
        {
            string resData;
            try
            {
                using (var client = new HttpClient())
                {
                    var serializedObject = JsonConvert.SerializeObject(inputObject);
                    var objectBody = new StringContent(serializedObject);
                    objectBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultApi = client.PostAsync(URL, objectBody).Result;
                    var resultContent = resultApi.Content.ReadAsStringAsync();
                    resData = resultContent.Result;
                }
                return resData;

            }
            catch (Exception)
            {

                return null;
            }

        }
        public string Get(string URL)
        {
            string resData;
            try
            {
                using (var client = new HttpClient())
                {
                   
                    var resultApi = client.GetAsync(URL).Result;
                    var resultContent = resultApi.Content.ReadAsStringAsync();
                    resData = resultContent.Result;
                }
                return resData;

            }
            catch (Exception)
            {

                return null;
            }

        }

    }
}
