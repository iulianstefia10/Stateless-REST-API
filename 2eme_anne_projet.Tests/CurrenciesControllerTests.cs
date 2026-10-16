using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter.Models;
using CurrencyConverter.Controllers;

namespace _2eme_anne_projet.Tests
{
    [TestClass]
    public class CurrenciesControllerTests
    {
        private CurrenciesController controller = null!;

        [TestInitialize]
        public void InitializationDesTests()
        {
            controller = new CurrenciesController();
        }

        
        [TestMethod]
        public void GetAll_ReturnsRightItems()
        {
            
            var result = controller.GetAll();

            
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Currency>), "Not an IEnumerable<Currency>");
            List<Currency> expectedCurrencies = new List<Currency>
            {
                new Currency(1, "Dollar", 1.08),
                new Currency(2, "Swiss Franc", 1.07),
                new Currency(3, "Yen", 120)
            };
            CollectionAssert.AreEqual(expectedCurrencies, result.ToList(), "Currencies lists are not identical");
        }

        
        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            
            var result = controller.GetById(1);

            
            Assert.IsInstanceOfType(result, typeof(ActionResult<Currency>), "Not an ActionResult");
            Assert.IsNull(result.Result, "Error is not null");
            Assert.IsInstanceOfType(result.Value, typeof(Currency), "Not a Currency");
            Assert.AreEqual(new Currency(1, "Dollar", 1.08), (Currency?)result.Value, "Not identical currencies");
        }

        [TestMethod]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            
            var result = controller.GetById(50);

            
            Assert.IsInstanceOfType(result, typeof(ActionResult<Currency>), "Not an ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Not a NotFoundResult");
            Assert.IsNull(result.Value, "Value is not null");
        }

        
        [TestMethod]
        public void Post_ValidObjectPassed_ReturnsObject()
        {
           
            Currency newCurrency = new Currency(4, "Ruble", 1510.5);

            
            var result = controller.Post(newCurrency);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Currency>), "Not an ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Not a CreatedAtRouteResult");
            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            Assert.AreEqual(StatusCodes.Status201Created, routeResult.StatusCode, "Not 201 Created");
            Assert.AreEqual(newCurrency, (Currency?)routeResult.Value, "Currencies not identical");
        }

       
        /*
        [TestMethod]
        public void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            Currency invalidCurrency = new Currency(4, null, 4.0);

            // Act
            var result = controller.Post(invalidCurrency);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Not a BadRequest");
        }
        */

        
        [TestMethod]
        public void Put_InvalidUpdate_ReturnsBadRequest()
        {
            
            Currency updatedCurrency = new Currency(2, "Swiss Franc", 1.8);

          
            var result = controller.Put(1, updatedCurrency);

            
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Not a BadRequestResult");
        }

        [TestMethod]
        public void Put_InvalidUpdate_ReturnsNotFound()
        {
            
            Currency updatedCurrency = new Currency(50, "Swiss Franc", 1.8);

            
            var result = controller.Put(50, updatedCurrency);

            
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Not a NotFoundResult");
        }

        [TestMethod]
        public void Put_ValidUpdate_ReturnsNoContent()
        {
            
            Currency updatedCurrency = new Currency(2, "Swiss Franc", 1.8);

           
            var result = controller.Put(2, updatedCurrency);

            
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Not a NoContentResult");
        }

       
        [TestMethod]
        public void Delete_NotOk_ReturnsNotFound()
        {
            
            var result = controller.Delete(50);

            
            Assert.IsInstanceOfType(result, typeof(ActionResult<Currency>), "Not an ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Not a NotFoundResult");
            Assert.IsNull(result.Value, "Value is not null");
        }

        [TestMethod]
        public void Delete_Ok_ReturnsRightItem()
        {
           
            var result = controller.Delete(2);

           
            Assert.IsInstanceOfType(result, typeof(ActionResult<Currency>), "Not an ActionResult");
            Assert.IsNull(result.Result, "Error is not null");
            Assert.IsInstanceOfType(result.Value, typeof(Currency), "Not a Currency");
            Assert.AreEqual(new Currency(2, "Swiss Franc", 1.07), (Currency?)result.Value, "Not identical currencies");
        }

        [TestCleanup]
        public void CleaningTests()
        {
            
        }
    }
}