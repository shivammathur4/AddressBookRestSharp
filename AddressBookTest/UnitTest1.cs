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
            client = new RestClient("http://localhost:5000/contacts");
        }

        private IRestResponse GetContactList()
        {
            RestRequest request = new RestRequest("/contacts/list", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        
        [TestMethod]
        public void ReadEntriesFromJsonServer()
        {
            IRestResponse response = GetContactList();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Contact> employeeList = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(4, employeeList.Count);
            foreach (Contact c in employeeList)
            {
                Console.WriteLine($"Id: {c.Id}\tFullName: {c.FirstName} {c.LastName}\tPhoneNo: {c.PhoneNumber}\tAddress: {c.Address}\tCity: {c.City}\tState: {c.State}\tZip: {c.Zip}\tEmail: {c.Email}");
            }
        }
    }
}
