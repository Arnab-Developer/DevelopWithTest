using System.Web.Mvc;
using DevelopWithTest.UI.WebApp.Helpers;

namespace DevelopWithTest.UI.WebApp
{
    /// <summary>
    /// Configuration of Custom Controller Factory.
    /// </summary>
    public class CustomControllerFactoryConfig
    {
        /// <summary>
        /// Register Custom Controller Factory.
        /// </summary>
        public static void RegisterCustomControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(typeof(CustomControllerFactory)); 
        }
    }
}