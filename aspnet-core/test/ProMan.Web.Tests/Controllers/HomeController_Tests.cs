using System.Threading.Tasks;
using ProMan.Models.TokenAuth;
using ProMan.Web.Controllers;
using Shouldly;
using Xunit;

namespace ProMan.Web.Tests.Controllers
{
    public class HomeController_Tests: ProManWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}