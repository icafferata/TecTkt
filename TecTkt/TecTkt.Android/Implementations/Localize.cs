[assembly: Xamarin.Forms.Dependency(typeof(TecTkt.Droid.Implementations.Localize))]

namespace TecTkt.Droid.Implementations
{
    using System.Globalization;
    using System.Threading;
    using TecTkt.Helpers;
    using TecTkt.Interfaces;

    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));
            // this gets called a lot - try/catch can be expensive so consider caching or something
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch(CultureNotFoundException e1)
            {
                // iOS locale not valid .NET culture (eg. "en-ES" English in Spain)
                // falback to first character, in this case "en"
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch(CultureNotFoundException e2)
                {
                    // iOS language not valid .NET culture, falling back to English
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
        string AndroidToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            // certain languages needs to be converted to Cultureinfo equivalent
            switch (androidLanguage)
            {
                case "ms-BN":     // Malaysian (Brunei)" not supported .NET culture
                case "ms-MY":     // Malaysian (Malaysia)" not supported .NET culture  
                case "ms-SG":     // Malaysian (Singapoure)" not supported .NET culture  
                    netLanguage = "ms"; // closest support
                    break;
                case "in-ID":     // Indonesian (Indonesian)" not supported .NET culture  
                    netLanguage = "id-ID"; // closest support
                    break;
                case "gsw-CH":     // "Schwiizertuutsh (Swiss German)" not supported .NET culture 
                    netLanguage = "de-CH"; // closest support
                    break;
            }
            return netLanguage;
        }
        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (2 chars usually)
            switch (platCulture.LanguageCode)
            {
                case "gsw":                 // "Schwiizertuutsh (Swiss German)" not supported .NET culture 
                    netLanguage = "de-CH";  // closest support
                    break;
            }
            return netLanguage;
        }
    }
}