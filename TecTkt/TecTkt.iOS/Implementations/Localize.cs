[assembly: Xamarin.Forms.Dependency(typeof(TecTkt.iOS.Implementations.Localize))]

namespace TecTkt.iOS.Implementations
{
    using Foundation;
    using System.Globalization;
    using System.Threading;
    using TecTkt.Helpers;
    using TecTkt.Interfaces;
    using Xamarin.Forms.PlatformConfiguration;

    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if(NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = iOSToDotnetLanguage(pref);
            }
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch(CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new System.Globalization.CultureInfo("en");
                }
            }
            return ci;
        }
        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        string iOSToDotnetLanguage(string iOSLaguage)
        {
            var netLanguage = iOSLaguage;
            // certain languages need to be converted to CultureInfo equivalent
            switch (iOSLaguage)
            {
                case "ms-MY":    // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":    // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms";     // Closest supported
                    break;
                case "gsw-CH":   //"Schwiizertuutsh (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH";   // Closest supported
                    break;
            }
            return netLanguage;
        }
        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode;  // use the first part of the identifier
            switch (platCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT";  // fallback to Portuguese
                    break;
                case "gsw":
                    netLanguage = "de-CH";  // equivalent to German (Switzerland)
                    break;
            }
            return netLanguage;
        }

    }
}