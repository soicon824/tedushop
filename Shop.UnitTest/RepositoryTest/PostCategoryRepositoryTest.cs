using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;

namespace Shop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IProductCategoryRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new ProductCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void ProductCategoryRepository_Create()
        {
            ProductCategory productCategory = new ProductCategory();
            productCategory.Name = "Test product category";
            productCategory.Alias = "Alias";
            productCategory.Description = "Description";
            productCategory.Status = true;

            var result = objRepository.Add(productCategory);
            unitOfWork.Commit();

            Assert.AreEqual(result.Name, productCategory.Name);
        }
        [TestMethod]
        public void ProductCategoryRepository_GetAll()
        {
            var list = objRepository.GetAll().ToList();
            Assert.AreEqual(1, list.Count);
        }
    }
}
