using System.Web.Mvc;
using System.Web.Routing;
using DevelopWithTest.UI.WebApp.Controllers;
using BusinessLogic = DevelopWithTest.BusinessLogic;
using BusinessLogicContract = DevelopWithTest.BusinessLogic.Contract;
using DataAccess = DevelopWithTest.DataAccess;
using DataAccessContract = DevelopWithTest.DataAccess.Contract;

namespace DevelopWithTest.UI.WebApp.Helpers
{
    /// <summary>
    /// Creates controller instence. This functionality has been
    /// registered in Application_Start() in Global.ascx.
    /// </summary>
    public class CustomControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Fulfill all dependencies and create controller instence.
        /// </summary>
        /// <param name="requestContext">
        /// The context of the HTTP request, which includes the HTTP context and route data.
        /// </param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>The controller.</returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            switch (controllerName)
            {
                case "Employee":
                    IController controllerEmployee = GetEmployeeControllerInstence();
                    return controllerEmployee;

                default:
                    return null;
            }
        }

        private IController GetEmployeeControllerInstence()
        {
            DataAccessContract::IEmployee dalEmployee = new DataAccess::Employee();
            BusinessLogicContract::IEmployee bllEmployee = new BusinessLogic::Employee(dalEmployee);
            EmployeeController controllerEmployee = new EmployeeController(bllEmployee);

            return controllerEmployee;
        }
    }
}