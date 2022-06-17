using Microsoft.Identity.Client.NativeInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWinConsoleWAM;
using System.Threading.Tasks;

namespace WAMTest
{
    [TestClass]
    public class WAMTest
    {

        //[TestInitialize]
        //public async Task Init()
        //{
        //    AuthResult = await WAMApp.WAMValidate();
        //    Assert.IsNotNull(AuthResult.Account.Id);
        //}

        [TestMethod]
        public async Task ValidateAcquireTokenSilentlyWithUserNamePasswordAsync()
        {
            var result = await WAMApp.ValidateSignInSilentlyAsync();
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public async Task ValidateAcquireTokenInteractivelyAsync()
        //{
        //    AuthResult result = await WAMApp.ValidateAcquireTokenInteractivelyAsync(AuthResult.Account).ConfigureAwait(false);

        //    Assert.IsNotNull(result.Account);
        //}

        //[TestMethod]
        //public async Task ValidateAcquireTokenSilentlyAsync()
        //{
        //    AuthResult result = await WAMApp.ValidateAcquireTokenSilentlyAsync(AuthResult.Account);

        //    Assert.IsNotNull(result.Account);
        //}

    }
}
