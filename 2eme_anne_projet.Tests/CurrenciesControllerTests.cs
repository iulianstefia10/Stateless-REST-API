using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Dacă ai folosit alte namespace-uri în Controller/Model, actualizează aceste două rânduri:
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
            // Se execută înaintea fiecărui test
            controller = new CurrenciesController();
        }

        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Currency>), "Not an ActionResult");
            Assert.IsNull(result.Result, "Error is not null");
            Assert.IsInstanceOfType(result.Value, typeof(Currency), "Not a Currency");
            Assert.AreEqual(new Currency(1, "Dollar", 1.08), (Currency?)result.Value, "Not identical currencies");
        }

        [TestMethod]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var result = controller.GetById(50);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Currency>), "Not an ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Not a NotFoundResult");
            Assert.IsNull(result.Value, "Value is not null");
        }
    }
}
