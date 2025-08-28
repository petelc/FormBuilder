using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FormBuilderAPI.Constants;
using FormBuilderAPI.DTO;
using FormBuilderAPI.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiIntegrationTests.AuthEndpoints;

[TestClass]
public class AuthenticateEndpointTest
{
    [TestMethod]
    [DataRow("", AuthorizationConstants.DEFAULT_PASSWORD, true)]
    public async Task ReturnsExpectedResultGivenCredentials(string testUserName, string testPassword,
        bool expectedResult)
    {
        var request = new LoginDTO()
        {
            UserName = testUserName,
            Password = testPassword
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await ProgramTest.NewClient.PostAsync("/api/auth/login", jsonContent);
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<LoginDTO>();
        
        //Assert.AreEqual(expectedResult, model!.);
    }
}