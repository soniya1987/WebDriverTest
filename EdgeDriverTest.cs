using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace WebDriverTest
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(options);
            _driver.Url = "https://www.amazon.com";
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("nav-link-accountList")).Click();
            Thread.Sleep(2000);
            IWebElement emailElement = _driver.FindElement(By.Id("ap_email"));
            emailElement.Clear();
            Debug.WriteLine(Environment.GetEnvironmentVariable("X-Amazon-UserName"));
            Debug.WriteLine(Environment.GetEnvironmentVariable("X-Amazon-SecretKey"));
            emailElement.SendKeys(Environment.GetEnvironmentVariable("X-Amazon-UserName"));
            _driver.FindElement(By.Id("continue")).Click();
            IWebElement passwordElement = _driver.FindElement(By.Id("ap_password"));
            passwordElement.Clear();
            passwordElement.SendKeys(Environment.GetEnvironmentVariable("X-Amazon-SecretKey"));
            _driver.FindElement(By.Id("signInSubmit")).Click();
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            Thread.Sleep(5000);
            _driver.FindElement(By.Id("nav-orders")).Click();
            Thread.Sleep(2000);
            IWebElement orderElement = _driver.FindElement(By.Id("a-page"));
            ReadOnlyCollection<IWebElement> ordersElement = orderElement.FindElements(By.ClassName("order-card"));
            foreach (var item in ordersElement)
            {
                IWebElement orderIdElement = item.FindElement(By.ClassName("yohtmlc-order-id"));
                Debug.WriteLine(orderIdElement.Text);
            }
            Assert.AreEqual("Your Orders", _driver.Title);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
