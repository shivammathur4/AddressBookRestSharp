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
        public void OnCallingPostAPIForAContactListWithMultipleContacts_ReturnContactObject()
        {
            List<Contact> contactList = new List<Contact>();
            contactList.Add(new Contact { FirstName = "Ramya", LastName = "U", PhoneNo = "9577456345", Address = "Feroz Shah Kotla", City = "New Delhi", State = "New Delhi", Zip = "547677", Email = "vs@gmail.com" });
            contactList.Add(new Contact { FirstName = "Ankitha", LastName = "H", PhoneNo = "9756723456", Address = "Chinnaswamy", City = "Bangalore", State = "Karnataka", Zip = "435627", Email = "yc@gmail.com" });
            contactList.Add(new Contact { FirstName = "Yashu", LastName = "V", PhoneNo = "9954564345", Address = "Mohali", City = "Mohali", State = "Punjab", Zip = "113425", Email = "klr@gmail.com" });

            foreach (var v in contactList)
            {
                RestRequest request = new RestRequest("/contacts/list", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("firstname", v.FirstName);
                jsonObj.Add("lastname", v.LastName);
                jsonObj.Add("phoneNo", v.PhoneNo);
                jsonObj.Add("address", v.Address);
                jsonObj.Add("city", v.City);
                jsonObj.Add("state", v.State);
                jsonObj.Add("zip", v.Zip);
                jsonObj.Add("email", v.Email);

                request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Contact contact = JsonConvert.DeserializeObject<Contact>(response.Content);
                Assert.AreEqual(v.FirstName, contact.FirstName);
                Assert.AreEqual(v.LastName, contact.LastName);
                Assert.AreEqual(v.PhoneNo, contact.PhoneNo);
                Console.WriteLine(response.Content);
            }
        }
    }
}
