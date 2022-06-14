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

            Assert.IsNotNull( result.Account);
        }

        
    }
}
