using CRUDExample.Controllers;
using CRUDExample.Data;
using CRUDExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CRUDExample.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task Delete_DeletesProductAndRedirectsToIndex()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(new Product { Name = "Test Product", Price = 10 });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ProductsController(context);

                // Act
                var result = await controller.Delete(1);

                // Assert
                var product = await context.Products.FindAsync(1);
                Assert.Null(product);
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
        }
    }
}
