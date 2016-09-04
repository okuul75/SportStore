using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Linq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanGeneratePageLink()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Strona1"">1</a>" + @"<a class=""btn btn-default btn-promary selected"" href=""Strona2"">2</a>" + @"<a class=""btn btn-default"" href=""Strona3"">3</a>"
                            , result.ToString());
        }

        [TestMethod]
        public void CanPaginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1"},
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" }
            });

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1"},
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" }
            });

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            PagingInfo pageinfo = result.PagingInfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage, 3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);
        }
    }
}
