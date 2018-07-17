using System.Collections.Specialized;
using System.Configuration.Provider;

namespace IryTech.AdmissionJankari.Components.Provider
{
    public abstract class ApplicationProvider : ProviderBase
    {
        /// <summary>
        /// Loads the settings from the provider.
        /// </summary>
        /// <returns>A StringDictionary.</returns>
        public abstract StringDictionary LoadSettings();
    }
}
