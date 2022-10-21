using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace RestSharpTest
{
    public class Employee
    {
        public int Id { get; set; }
        public string name { get; set; }    
        public int salary { get; set; } 
    }

    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }
        private IRestResponse getEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees", Method.GET);

            //Act
            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void OnCallingListReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(3, dataResponse.Count);

            foreach(Employee e in dataResponse)
            {
               System.Console.WriteLine("Id : " +e.Id+ " Name : " +e.name+ " Salary : " +e.salary);

            }
        }
    }
}