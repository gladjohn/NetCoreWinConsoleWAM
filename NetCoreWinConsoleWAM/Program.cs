using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NetCoreWinConsoleWAM
{
    public class WAMApp
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        public static string CorrelationId = "b0435a5c-6d97-41d6-9372-812e7fac3c10";
        public static string VSApplicationId = "26a7ee05-5602-4d76-a7ba-eae8b7b67941";
        public const string MicrosoftCommonAuthority = "https://login.microsoftonline.com/common";
        public const string Scopes = "profile";
        public const string RedirectUri = "about:blank";

        public static void Main(string[] args)
        {
            IntPtr hWnd = GetConsoleWindow();
            Task wamValidate = WAMValidate(hWnd);
            wamValidate.Wait();
            wamValidate.Dispose();
            wamValidate = null;

            try
            {
                Core.VerifyHandleLeaksForTest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Console.ReadLine();

        }

        /// <summary>
        /// Validate Interop WAM
        /// </summary>
        /// <returns></returns>
        public static async Task<AuthResult> WAMValidate(IntPtr hWnd)
        {
            try
            {
                using (var core = new Core())
                using (var authParams = new AuthParameters(VSApplicationId, MicrosoftCommonAuthority))
                {
                    authParams.RequestedScopes = Scopes;
                    authParams.RedirectUri = RedirectUri;
                    
                    using (AuthResult authResult = await core.SignInAsync(hWnd, authParams, CorrelationId))
                    {
                        //GetRuntimeAuthResult(authResult);

                        //if (authResult.IsSuccess)
                        //{
                        //    await ReadAccountAsync(core, CorrelationId, authResult.Account.Id);
                        //    await AcquireTokenSilentlyAsync(core, CorrelationId, authResult.Account, authParams);
                        //}
                        return authResult;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public static string GetRuntimeAuthResult(AuthResult authResult)
        {
            if (authResult.IsSuccess)
            {
                Console.WriteLine($"Account Id: {authResult.IdToken}");
                Console.WriteLine($"Account Id: {authResult.Account.Id}");
                Console.WriteLine($"Account Client Info: {authResult.Account.ClientInfo}");
                Console.WriteLine($"Access Token: {authResult.AccessToken}");
                Console.WriteLine($"Expires On: {authResult.ExpiresOn}");
                Console.WriteLine($"Raw Id Token: {authResult.RawIdToken}");

                return $"Account Id: {authResult.IdToken}";
            }
            else
            {
                Console.WriteLine($"Error: {authResult.Error}");
                throw new MsalRuntimeException(authResult.Error);
            }
        }

        public static async Task ReadAccountAsync(Core core, string correlationId, string accountId)
        {
            Console.WriteLine();
            Console.WriteLine("Verifying ReadAccountById api.");
            using (Account result = await core.ReadAccountByIdAsync(accountId, correlationId))
            {
                if (result == null)
                {
                    Console.WriteLine($"Account id: {accountId} is not found");
                }
                else
                {
                    Console.WriteLine($"Account Id: {result.Id}");
                    Console.WriteLine($"Account Client Info: {result.ClientInfo}");
                }
            }
        }

        public static async Task AcquireTokenSilentlyAsync(Core core, string correlationId, Account account, AuthParameters authParams)
        {
            Console.WriteLine();
            Console.WriteLine("Checking AcquireTokenSilently api.");
            using (AuthResult authResult = await core.AcquireTokenSilentlyAsync(authParams, correlationId, account))
            {
                GetRuntimeAuthResult(authResult);
            }
        }
    }
}
