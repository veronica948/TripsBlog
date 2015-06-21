using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripsBlogProject;
using TripsBlogProject.Controllers;
using TripsBlogProject.Models;

namespace TripsBlogProjectTests
{
    [TestClass]
    public class CountriesControllerTests
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            CountriesController controller = new CountriesController();
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Posts()
        {
            // Arrange
            CountriesController controller = new CountriesController();

            // Act
            ViewResult result = controller.Posts(1) as ViewResult;
            Assert.IsNotNull(result);
            // Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            CountriesController controller = new CountriesController();
            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void HomeIndex()
        {
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
