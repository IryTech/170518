using System.Collections.Specialized;

namespace IryTech.AdmissionJankari.Components.Provider
{
  public static class ApplicationServices
    {
        #region Constants and Fields

        /// <summary>
        /// The lock object.
        /// </summary>
        private static readonly object TheLock = new object();

        /// <summary>
        /// The provider. Don't access this directly. Access it through the property accessor.
        /// </summary>
        private static ApplicationProvider  _provider=new DbApplicationProvider();

        /// <summary>
        /// The providers.
        /// </summary>
     
        #endregion

       /// <summary>
        /// Loads the settings from the provider and returns
        /// them in a StringDictionary for the applicationSetting class to use.
        /// </summary>
        /// <returns>A StringDictionary.</returns>
        public static StringDictionary LoadSettings()
        {
            return _provider.LoadSettings();
        }

        #region Methods

       
       

        #endregion
    }
}
