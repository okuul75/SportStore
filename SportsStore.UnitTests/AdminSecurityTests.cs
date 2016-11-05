using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_Wih_Valid_Credentials()
        {
            // przygotowanie - imitacja dostawcy uwierzytelniania
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "admin")).Returns(true);

            // przygotowanie - model widoku
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "admin"
            };

            // przygotwanie - kontroler
            AccountController target = new AccountController(mock.Object);

            // działanie
            ActionResult result = target.Login(model, "/MyUrl");

            // asercje
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // przygotowanie - imitacja dostawcy uwierzytelniania
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("nieprawidłowyUżytkownik", "nieprawidłoweHasło")).Returns(false);
 
            // przygotowanie - model widoku
            LoginViewModel model = new LoginViewModel
            {
                UserName = "nieprawidłowyUżytkownik",
                Password = "nieprawidłoweHasło"
            };

            // przygotwanie - kontroler
            AccountController target = new AccountController(mock.Object);

            // działanie
            ActionResult result = target.Login(model, "/MyUrl");

            // asercje
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
