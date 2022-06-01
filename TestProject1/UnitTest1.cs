using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NetCoreWinConsoleWAM;
using Microsoft.Identity.Client.NativeInterop;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [TestMethod]
        public async Task TestMethod1()
        {

            AuthResult authResult = await WAMApp.WAMValidate();

            AuthResult result = await WAMApp.ValidateAcquireTokenSilentlyAsync(authResult.Account);

            Assert.AreEqual(authResult.Account, result.Account);

        }


    }
}
