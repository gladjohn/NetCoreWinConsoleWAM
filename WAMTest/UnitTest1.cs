using Microsoft.Identity.Client.NativeInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWinConsoleWAM;
using System.Threading.Tasks;

namespace WAMTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task ValidateAcquireTokenSilentlyAsync()
        {
            AuthResult authResult = await WAMApp.WAMValidate();

            Assert.IsNotNull(authResult.Account.Id);

            AuthResult result = await WAMApp.ValidateAcquireTokenSilentlyAsync(authResult.Account);

            Assert.AreEqual(authResult.Account, result.Account);
        }

        [TestMethod]
        public async Task ValidateAcquireTokenInteractivelyAsync()
        {
            AuthResult authResult = await WAMApp.WAMValidate();

            Assert.IsNotNull(authResult.Account.Id);

            AuthResult result = await WAMApp.ValidateAcquireTokenInteractivelyAsync(authResult.Account);

            Assert.AreEqual(authResult.Account, result.Account);
        }
    }
}
