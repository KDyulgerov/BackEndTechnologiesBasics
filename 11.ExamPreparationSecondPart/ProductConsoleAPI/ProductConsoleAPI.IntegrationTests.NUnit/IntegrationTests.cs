using Microsoft.EntityFrameworkCore;
using ProductConsoleAPI.Business;
using ProductConsoleAPI.Business.Contracts;
using ProductConsoleAPI.Data.Models;
using ProductConsoleAPI.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace ProductConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestProductsDbContext dbContext;
        private IProductsManager productsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestProductsDbContext();
            this.productsManager = new ProductsManager(new ProductsRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }

        [Test]
        public async Task AddProductAsync_ShouldAddNewProduct()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            var dbProduct = await this.dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == newProduct.ProductCode);

            Assert.NotNull(dbProduct);
            Assert.That(dbProduct.ProductName, Is.EqualTo(newProduct.ProductName));
            Assert.That(dbProduct.Description, Is.EqualTo(newProduct.Description));
            Assert.That(dbProduct.Price, Is.EqualTo(newProduct.Price));
            Assert.That(dbProduct.Quantity, Is.EqualTo(newProduct.Quantity));
            Assert.That(dbProduct.OriginCountry, Is.EqualTo(newProduct.OriginCountry));
            Assert.That(dbProduct.ProductCode, Is.EqualTo(newProduct.ProductCode));
        }

        [Test]
        public async Task AddProductAsync_TryToAddProductWithInvalidCredentials_ShouldThrowException()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = -1m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var exception = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.AddAsync(newProduct));
            var actual = await dbContext.Products.FirstOrDefaultAsync(c => c.ProductCode == newProduct.ProductCode);

            Assert.IsNull(actual);
            Assert.That(exception?.Message, Is.EqualTo("Invalid product!"));

        }

        [Test]
        public async Task DeleteProductAsync_WithValidProductCode_ShouldRemoveProductFromDb()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            await productsManager.DeleteAsync(newProduct.ProductCode);

            var productInDb = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductCode == newProduct.ProductCode);

            Assert.That(productInDb, Is.Null);
        }

        [TestCase("  ")]
        [TestCase(null)]
        public async Task DeleteProductAsync_TryToDeleteWithNullOrWhiteSpaceProductCode_ShouldThrowException(string nullOrWhiteSpace)
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await productsManager.DeleteAsync(nullOrWhiteSpace));

            var productInDb = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductCode == newProduct.ProductCode);

            Assert.IsNotNull(productInDb);
            Assert.That(exception?.Message, Is.EqualTo("Product code cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenProductsExist_ShouldReturnAllProducts()
        {
            var firstProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var secondProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct Second",
                ProductCode = "AB12B",
                Price = 1.25m,
                Quantity = 200,
                Description = "Anything for description Second"
            };

            await productsManager.AddAsync(firstProduct);
            await productsManager.AddAsync(secondProduct);

            var actual = await productsManager.GetAllAsync();

            var firstProductInDb = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductCode == firstProduct.ProductCode);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(firstProduct?.OriginCountry, Is.EqualTo(firstProductInDb?.OriginCountry));
            Assert.That(firstProduct?.ProductName, Is.EqualTo(firstProductInDb?.ProductName));
            Assert.That(firstProduct?.ProductCode, Is.EqualTo(firstProductInDb?.ProductCode));
            Assert.That(firstProduct?.Price, Is.EqualTo(firstProductInDb?.Price));
            Assert.That(firstProduct?.Quantity, Is.EqualTo(firstProductInDb?.Quantity));
            Assert.That(firstProduct?.Description, Is.EqualTo(firstProductInDb?.Description));
        }

        [Test]
        public async Task GetAllAsync_WhenNoProductsExist_ShouldThrowKeyNotFoundException()
        {
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await productsManager.GetAllAsync());

            Assert.That(exception?.Message, Is.EqualTo("No product found."));
        }

        [Test]
        public async Task SearchByOriginCountry_WithExistingOriginCountry_ShouldReturnMatchingProducts()
        {
            var firstProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var secondProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct Second",
                ProductCode = "AB12B",
                Price = 1.25m,
                Quantity = 200,
                Description = "Anything for description Second"
            };

            await productsManager.AddAsync(firstProduct);
            await productsManager.AddAsync(secondProduct);

            var actual = await productsManager.SearchByOriginCountry(firstProduct.OriginCountry);

            var firstProductInDb = await dbContext.Products.FirstOrDefaultAsync(x => x.OriginCountry == firstProduct.OriginCountry);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(firstProduct?.OriginCountry, Is.EqualTo(firstProductInDb?.OriginCountry));
            Assert.That(firstProduct?.ProductName, Is.EqualTo(firstProductInDb?.ProductName));
            Assert.That(firstProduct?.ProductCode, Is.EqualTo(firstProductInDb?.ProductCode));
            Assert.That(firstProduct?.Price, Is.EqualTo(firstProductInDb?.Price));
            Assert.That(firstProduct?.Quantity, Is.EqualTo(firstProductInDb?.Quantity));
            Assert.That(firstProduct?.Description, Is.EqualTo(firstProductInDb?.Description));
        }

        [Test]
        public async Task SearchByOriginCountryAsync_WithNonExistingOriginCountry_ShouldThrowKeyNotFoundException()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };
            var nonExistingCounrty = "Italy";

            await productsManager.AddAsync(newProduct);

            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await productsManager.SearchByOriginCountry(nonExistingCounrty));

            Assert.That(exception?.Message, Is.EqualTo("No product found with the given first name."));
        }

        [TestCase("   ")]
        [TestCase(null)]
        public async Task SearchByOriginCountryAsync_WithNonExistingOriginCountry_ShouldThrowKeyNotFoundException(string nullOrWhiteSpace)
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await productsManager.SearchByOriginCountry(nullOrWhiteSpace));

            Assert.That(exception?.Message, Is.EqualTo("Country name cannot be empty."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidProductCode_ShouldReturnProduct()
        {
            var firstProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var secondProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct Second",
                ProductCode = "AB12B",
                Price = 1.25m,
                Quantity = 200,
                Description = "Anything for description Second"
            };

            await productsManager.AddAsync(firstProduct);
            await productsManager.AddAsync(secondProduct);

            var actual = await productsManager.GetSpecificAsync(firstProduct.ProductCode);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual?.OriginCountry, Is.EqualTo(firstProduct?.OriginCountry));
            Assert.That(actual?.ProductName, Is.EqualTo(firstProduct?.ProductName));
            Assert.That(actual?.ProductCode, Is.EqualTo(firstProduct?.ProductCode));
            Assert.That(actual?.Price, Is.EqualTo(firstProduct?.Price));
            Assert.That(actual?.Quantity, Is.EqualTo(firstProduct?.Quantity));
            Assert.That(actual?.Description, Is.EqualTo(firstProduct?.Description));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidProductCode_ShouldThrowKeyNotFoundException()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };
            var nonExistingProductCode = "AB12X";

            await productsManager.AddAsync(newProduct);

            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await productsManager.GetSpecificAsync(nonExistingProductCode));

            Assert.That(exception?.Message, Is.EqualTo($"No product found with product code: {nonExistingProductCode}"));
        }

        [TestCase("   ")]
        [TestCase(null)]
        public async Task GetSpecificAsync_WithNullOrWhiteSpaceProductCode_ShouldThrowKeyNotFoundException(string nullOrWhiteSpace)
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await productsManager.GetSpecificAsync(nullOrWhiteSpace));

            Assert.That(exception?.Message, Is.EqualTo("Product code cannot be empty."));
        }

        [Test]
        public async Task UpdateAsync_WithValidProduct_ShouldUpdateProduct()
        {
            var currentProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(currentProduct);

            var updatedProduct = new Product()
            {
                OriginCountry = "Bulgaria Updated",
                ProductName = "TestProduct Update",
                ProductCode = "AB12X",
                Price = 2.30m,
                Quantity = 200,
                Description = "Anything for description Updated"
            };

            await productsManager.UpdateAsync(updatedProduct);

            var productInDb = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == updatedProduct.Id);

            Assert.That(productInDb, Is.Not.Null);
            Assert.That(productInDb.OriginCountry, Is.EqualTo(updatedProduct.OriginCountry));
            Assert.That(productInDb.ProductName, Is.EqualTo(updatedProduct.ProductName));
            Assert.That(productInDb.ProductCode, Is.EqualTo(updatedProduct.ProductCode));
            Assert.That(productInDb.Price, Is.EqualTo(updatedProduct.Price));
            Assert.That(productInDb.Quantity, Is.EqualTo(updatedProduct.Quantity));
            Assert.That(productInDb.Description, Is.EqualTo(updatedProduct.Description));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidProduct_ShouldThrowValidationException()
        {
            var currentProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(currentProduct);

            var invalidProduct = new Product()
            {
                OriginCountry = "",
                ProductName = "",
                ProductCode = "",
                Price = -2.302222222m,
                Quantity = -200,
                Description = ""
            };

            var exception = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.UpdateAsync(invalidProduct));

            Assert.That(exception?.Message, Is.EqualTo("Invalid prduct!"));
        }
    }
}
