using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
namespace SecondApp
{

    class Program
    {

        IWebDriver driver = new ChromeDriver();



        static void Main(string[] args)
        {


        }
        public void product(IWebDriver driver)
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
        



        }
        public void Login(IWebDriver driver)
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
        }
        [SetUp]
        public void initialize()
        {


            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        }

        [Test] //TC 01
        public void Login_WithValidUser_ShouldGoToProducts()
        {
            //Make the browser Fullscreen
            driver.Manage().Window.Maximize();
            IWebElement Firstname = driver.FindElement(By.Id("user-name"));
            Firstname.SendKeys("standard_user");
            IWebElement Secondname = driver.FindElement(By.Name("password"));
            Secondname.SendKeys("secret_sauce");
            IWebElement Login = driver.FindElement(By.Id("login-button"));
            Login.Click();

            Thread.Sleep(2000);
            //Assert that user is redirected to products page
            Assert.That(driver.Url.Contains("https://www.saucedemo.com/inventory.html"));
          
        }


        [Test] //TC 02
        public void loginwithlockeduser()
        { 
         IWebElement username = driver.FindElement(By.Id("user-name"));
         username.SendKeys("locked_out_user");
         IWebElement Password = driver.FindElement(By.Name("password"));
         Password.SendKeys("secret_sauce");
         IWebElement Login = driver.FindElement(By.Id("login-button"));
         Login.Click();
         Thread.Sleep(2000);
         var error = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
         Assert.That(error.Contains("locked out"));
        }

        [Test] //TC 03 
        public void Invalidpasswordlogin()
        {
            IWebElement username = driver.FindElement(By.Id("user-name"));
            username.SendKeys("locked_out_user");
            IWebElement Password = driver.FindElement(By.Name("password"));
            Password.SendKeys("abcd123");
            IWebElement Login = driver.FindElement(By.Id("login-button"));
            Login.Click();
            Thread.Sleep(2000);
            var error = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
            Assert.That(error.Contains("do not match"));
        }
        [Test]  // TC04
        public void AddProductToCart_ShouldAppearInCart()
        {
            // create incognito driver just for this test because of chrome password warning pop-up
            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            using (var incognitoDriver = new ChromeDriver(options))
            {
                incognitoDriver.Manage().Window.Maximize();
                incognitoDriver.Navigate().GoToUrl("https://www.saucedemo.com/");

                // login
                incognitoDriver.FindElement(By.Id("user-name")).SendKeys("standard_user");
                incognitoDriver.FindElement(By.Id("password")).SendKeys("secret_sauce");
                incognitoDriver.FindElement(By.Id("login-button")).Click();

                // add product and check
                incognitoDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
                incognitoDriver.FindElement(By.ClassName("shopping_cart_link")).Click();

                var item = incognitoDriver.FindElement(By.ClassName("inventory_item_name")).Text;
                Assert.That(item, Does.Contain("Backpack"));
            }
        }
        [Test] //TC 05
        public void Removeproductfromcart()
        {
            Login(driver);
            product(driver);
            IWebElement cart1 = driver.FindElement(By.ClassName("shopping_cart_link"));
            cart1.Click();
            IWebElement remove = driver.FindElement(By.Id("remove-sauce-labs-backpack"));
            remove.Click();
            var item = driver.FindElements(By.ClassName("cart_item"));
            Assert.That(item.Count, Is.EqualTo(0));
        }
        [Test] //TC 06
        //Used Incognito Mode because of password warning
        public void checkoutprocesspisitive()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            using (var incognitoDriver = new ChromeDriver(options))

            {
                incognitoDriver.Manage().Window.Maximize();
                incognitoDriver.Navigate().GoToUrl("https://www.saucedemo.com/");
                incognitoDriver.FindElement(By.Id("user-name")).SendKeys("standard_user");
                incognitoDriver.FindElement(By.Id("password")).SendKeys("secret_sauce");
                incognitoDriver.FindElement(By.Id("login-button")).Click();
                incognitoDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
                IWebElement cart1 = incognitoDriver.FindElement(By.ClassName("shopping_cart_link"));
                cart1.Click();
                IWebElement checkout = incognitoDriver.FindElement(By.Id("checkout"));
                checkout.Click();
                IWebElement firstname = incognitoDriver.FindElement(By.Id("first-name"));
                firstname.SendKeys("Test");
                IWebElement lastname = incognitoDriver.FindElement(By.Id("last-name"));
                lastname.SendKeys("Test");
                IWebElement pcode = incognitoDriver.FindElement(By.Id("postal-code"));
                pcode.SendKeys("55200");
                IWebElement continue1 = incognitoDriver.FindElement(By.Id("continue"));
                continue1.Click();
                IWebElement continue2 = incognitoDriver.FindElement(By.Id("finish"));
                continue2.Click();
                var message = incognitoDriver.FindElement(By.ClassName("complete-header")).Text;
                Assert.That(message, Does.Contain("Thank you for your order!"));
            }
        }
            [Test] //TC 07
        public void checkoutprocessnegative()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            using (var incognitoDriver = new ChromeDriver(options))

            {
                incognitoDriver.Manage().Window.Maximize();
                incognitoDriver.Navigate().GoToUrl("https://www.saucedemo.com/");
                incognitoDriver.FindElement(By.Id("user-name")).SendKeys("standard_user");
                incognitoDriver.FindElement(By.Id("password")).SendKeys("secret_sauce");
                incognitoDriver.FindElement(By.Id("login-button")).Click();
                incognitoDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
                IWebElement cart1 = incognitoDriver.FindElement(By.ClassName("shopping_cart_link"));
                cart1.Click();
                IWebElement checkout = incognitoDriver.FindElement(By.Id("checkout"));
                checkout.Click();
                IWebElement fname = incognitoDriver.FindElement(By.Id("first-name"));
                fname.SendKeys("");
                IWebElement lastname = incognitoDriver.FindElement(By.Id("last-name"));
                lastname.SendKeys("Test");
                IWebElement pcode = incognitoDriver.FindElement(By.Id("postal-code"));
                pcode.SendKeys("55200");
                IWebElement continue1 = incognitoDriver.FindElement(By.Id("continue"));
                continue1.Click();

                var error = incognitoDriver.FindElement(By.XPath("//*[@id=\"checkout_info_container\"]/div/form/div[1]/div[4]")).Text;
                Assert.That(error, Does.Contain("Error: First Name is required"));
            }
        }

        [Test] //TC08   
        public void logout()
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");

            using (var incognitoDriver = new ChromeDriver(options))

            {
                incognitoDriver.Manage().Window.Maximize();
                incognitoDriver.Navigate().GoToUrl("https://www.saucedemo.com/");
                incognitoDriver.FindElement(By.Id("user-name")).SendKeys("standard_user");
                incognitoDriver.FindElement(By.Id("password")).SendKeys("secret_sauce");
                incognitoDriver.FindElement(By.Id("login-button")).Click();
                IWebElement tab = incognitoDriver.FindElement(By.Id("react-burger-menu-btn"));
                tab.Click();
                Thread.Sleep(100);
                IWebElement logout2 = incognitoDriver.FindElement(By.Id("logout_sidebar_link"));
                logout2.Click();
                string title = incognitoDriver.Title;
                Assert.That(title.Equals("Swag Labs"));
            }

        }



        [TearDown]
        public void QuitTest()

        {

            driver.Quit();


        }



    }
}