using Microsoft.Identity.Client.NativeInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWinConsoleWAM;
using System.Threading.Tasks;

namespace WAMTest
{
    [TestClass]
    public class WAMTest
    {
        AuthResult AuthResult = null;

        //[TestInitialize]
        //public async Task Init()
        //{
        //    AuthResult = await WAMApp.WAMValidate().ConfigureAwait(false);
        //    //Assert.IsNotNull(AuthResult.Account.Id);
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
        //    AuthResult result = await WAMApp.ValidateAcquireTokenSilentlyAsync(AuthResult.Account).ConfigureAwait(false);

        //    Assert.IsNotNull(result.Account);
        //}

    }
}
