using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bangazon_TaskTracker;
using Bangazon_TaskTracker.Controllers;

namespace Bangazon_TaskTracker.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
