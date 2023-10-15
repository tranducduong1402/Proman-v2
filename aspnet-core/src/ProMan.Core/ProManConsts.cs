using ProMan.Debugging;

namespace ProMan
{
    public class ProManConsts
    {
        public const string LocalizationSourceName = "ProMan";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "cd795da5b167492da67252cc6d0ca1a0";
    }
}
