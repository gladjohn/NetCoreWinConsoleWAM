using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWinConsoleWAM
{

    public class WAMApp
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        private const string CorrelationId = "fc8fc0e8-c631-4be3-8857-f21f4ea669f3";

        static async Task Main(string[] args)
        {
            AuthResult wamValidate = await WAMValidate().ConfigureAwait(false);
            await ValidateAcquireTokenSilentlyAsync(wamValidate.Account);
            await ValidateAcquireTokenInteractivelyAsync(wamValidate.Account);
            await ValidateReadAccountByIdAsync(wamValidate.Account.Id);

            Console.ReadLine();

        }

        public static async Task<AuthResult> WAMValidate()
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("SignInAsync api.");

            try
            {
                using (var core = new Core())
                using (var authParams = GetCommonAuthParameters(false))
                {
                    IntPtr hWnd = GetConsoleWindow();
                    
                    using (AuthResult result = await core.SignInAsync(hWnd, authParams, CorrelationId, default))
                    {
                        PrintResults(result);

                        if (result.IsSuccess)
                        {
                            return result;
                        }
                        else 
                        {
                            return null;
                        }
                    }
                }
            }
            catch (MsalRuntimeException ex)
            {
                return null;
            }
        }

        public static AuthParameters GetCommonAuthParameters(
            bool isMsaPassthrough)
        {
            const string clientId = "04f0c124-f2bc-4f59-8241-bf6df9866bbd";
            const string authority = "https://login.microsoftonline.com/organizations";
            const string scopes = "https://management.core.windows.net//.default";
            const string redirectUri = "http://localhost";

            //MSA-PT Auth Params
            const string nativeInteropMsalRequestType = "msal_request_type";
            const string consumersPassthroughRequest = "consumer_passthrough";

        var authParams = new AuthParameters(clientId, authority);

            //scopes
            authParams.RequestedScopes = scopes;

            //WAM redirect URi does not need to be configured by the user
            //this is used internally by the interop to fallback to the browser 
            authParams.RedirectUri = redirectUri;

            //MSA-PT
            if (isMsaPassthrough)
                authParams.Properties[nativeInteropMsalRequestType] = consumersPassthroughRequest;


            return authParams;
        }
        private static void PrintResults(AuthResult result)
        {
            if (result.IsSuccess)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine($"Account ID : {result.Account.Id}");
                Console.WriteLine($"Client Info : {result.Account.ClientInfo}");
                Console.WriteLine($"Access Token : {result.AccessToken}");
                Console.WriteLine($"Expires On : {result.ExpiresOn}");
                Console.WriteLine($"ID Token : {result.RawIdToken}");
                Console.WriteLine("---------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine($"Error: {result.Error}");
                throw new MsalRuntimeException(result.Error);
            }
        }

        private static async Task ValidateReadAccountByIdAsync(string accountId)
        {
            Console.WriteLine("ReadAccountByIdAsync api.");
            Console.WriteLine("---------------------------------------------------------------");

            using (var core = new Core())
            using (var authParams = GetCommonAuthParameters(false))
            {
                using (Account acc = await core.ReadAccountByIdAsync(accountId, CorrelationId))
                {
                    if (acc == null)
                    {
                        Console.WriteLine($"Account (id: {accountId}) is not found");
                    }
                    else
                    {
                        Console.WriteLine($"Account Id: {acc.Id}");
                        Console.WriteLine($"Account Client Info: {acc.ClientInfo}");
                    }
                }
            }
        }


        public static async Task<AuthResult> ValidateAcquireTokenInteractivelyAsync(Account account)
        {
            Console.WriteLine("AcquireTokenInteractivelyAsync api.");

            using (var core = new Core())
            using (var authParams = GetCommonAuthParameters(false))
            {
                using (AuthResult result = await core.AcquireTokenInteractivelyAsync(GetConsoleWindow(), authParams, CorrelationId, account))
                {
                    PrintResults(result);

                    if (result.IsSuccess)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {result.Error}");
                        throw new MsalRuntimeException(result.Error);
                    }
                }
            }
        }

        public static async Task<AuthResult> ValidateAcquireTokenSilentlyAsync(Account account)
        {
            Console.WriteLine("AcquireTokenSilentlyAsync api.");

            using (var core = new Core())
            using (var authParams = GetCommonAuthParameters(false))
            {
                using (AuthResult result = await core.AcquireTokenSilentlyAsync(authParams, CorrelationId, account))
                {
                    PrintResults(result);

                    if (result.IsSuccess)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {result.Error}");
                        throw new MsalRuntimeException(result.Error);
                    }
                }
            }
        }
    }
}