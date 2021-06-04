//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookRestSharp;

namespace AddressBookTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:5000");
        }


        
        private IRestResponse GetContactList()
        {
            RestRequest request = new RestRequest("/contacts/list", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        
        [TestMethod]
        public void OnCallingPutAPI_ReturnContactObjects()
        {
            
            RestRequest request = new RestRequest("/contacts/7", Method.PUT);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("firstname", "Yashu");
            jsonObj.Add("lastname", "V");
            jsonObj.Add("phoneNo", "7858070934");
            jsonObj.Add("address", "FC Real Madrid");
            jsonObj.Add("city", "Madrid");
            jsonObj.Add("state", "Spain");
            jsonObj.Add("zip", "535678");
            jsonObj.Add("email", "yash7@gmail.com");
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

           
            IRestResponse response = client.Execute(request);

            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Contact contact = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual("Yashu", contact.FirstName);
            Assert.AreEqual("V", contact.LastName);
            Assert.AreEqual("535678", contact.Zip);
            Console.WriteLine(response.Content);
        }
    }
}
