using Microsoft.Identity.Client.NativeInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWinConsoleWAM;
using System.Threading.Tasks;

namespace WAMTest
{
    [TestClass]
    public class WAMTest
    {
        AuthResult authResult { get; set; }

        [TestInitialize]
        public async Task Init()
        {
            authResult = await WAMApp.WAMValidate();
            Assert.IsNotNull(authResult.Account.Id);
        }

        [TestMethod]
        public async Task ValidateAcquireTokenSilentlyWithUserNamePasswordAsync()
        {
            AuthResult result = await WAMApp.ValidateSignInSilentlyAsync();

            Assert.IsNotNull(result.Account);
        }

        [TestMethod]
        public async Task ValidateAcquireTokenInteractivelyAsync()
        {
            AuthResult result = await WAMApp.ValidateAcquireTokenInteractivelyAsync(authResult.Account).ConfigureAwait(false);

            Assert.IsNotNull(result.Account);
        }

        [TestMethod]
        public async Task ValidateAcquireTokenSilentlyAsync()
        {
            AuthResult result = await WAMApp.ValidateAcquireTokenSilentlyAsync(authResult.Account);

            Assert.IsNotNull(result.Account);
        }

    }
}
