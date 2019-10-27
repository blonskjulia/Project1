using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace Project1
{
    public class Tests
    {
        protected RemoteWebDriver driver;
        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.google.com/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            signInAccount();
            goToGmail();
            goToDraftFolder();
            createDraft();
            updateDraft();
            singOut();            
        }

        public void switchToMainWindow()
        {
            String mainWindowHandle = driver.CurrentWindowHandle; 
            driver.SwitchTo().Window(mainWindowHandle);
        }

        public void switchToMyWindow()
        {
            String myWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(myWindowHandle);
        }

        public void signInAccount()
        {
            driver.FindElement(By.Id("gb_70")).Click();
            driver.FindElement(By.Name("identifier")).SendKeys("juliatest2510@gmail.com");            
            driver.FindElement(By.XPath("//span[@class='RveJvd snByac']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement password = driver.FindElement(By.XPath("//*[@id='password']/div[1]/div/div[1]/input"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(password));
            password.SendKeys("qwerty2019");
            driver.FindElement(By.XPath("//span[@class='RveJvd snByac']")).Click();
        }

        public void goToGmail()
        {
            driver.FindElement(By.XPath("//a[contains(text(),'Gmail')]")).Click();
        }

        public void goToDraftFolder()
        {
            driver.FindElement(By.XPath("//div[@class='T-I J-J5-Ji T-I-KE L3']")).Click();
        }

        public void createDraft()
        {
            switchToMyWindow();
            IWebElement email = driver.FindElement(By.XPath("//textarea[@class='vO']"));
            email.SendKeys("qa@gmail.com");
            IWebElement text = driver.FindElement(By.ClassName("aoT"));
            string draftTopic = "New Message";
            text.SendKeys(draftTopic);
            driver.FindElement(By.XPath("//img[@class='Ha']")).Click();
            switchToMainWindow();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            try
            {
                IWebElement ell = driver.FindElement(By.XPath("//a[@href='https://mail.google.com/mail/u/0/?ogbl#drafts']"));
                ell.Click();
            }
            catch (Exception)
            {
                IWebElement ell = driver.FindElement(By.XPath("//a[@href='https://mail.google.com/mail/u/0/?ogbl#drafts']"));
                ell.Click();
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            validateTopicInDraft(draftTopic); 
            Console.WriteLine("The draft was created");
        }

        public void updateDraft()
        {
            var list = driver.FindElements(By.CssSelector("[jsmodel='nXDxbd']"));
            list[0].Click();
            switchToMyWindow();            
            IWebElement input = driver.FindElement(By.ClassName("aoT"));
            input.Click();
            input.SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            input.SendKeys(Keys.Delete);
            String newTopic = "Automation";
            input.SendKeys(newTopic);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.FindElement(By.XPath("//img[@class='Ha']")).Click();
            validateTopicInDraft(newTopic);            
            Console.WriteLine("The draft was updated");                        
        }

        private void validateTopicInDraft(string expectedToopic) 
        {
            var list = driver.FindElements(By.CssSelector("[jsmodel='nXDxbd']"));
            list[0].Click();
            switchToMyWindow();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            //getting actual draft header - expected to be equal to provided topic
            IWebElement draftHeader = driver.FindElement(By.XPath("//div[@class='aYF']"));
            String topic = draftHeader.Text;
            Console.WriteLine("ExpectedToopic: " + expectedToopic);
            Console.WriteLine("Topic: " + topic);
            Assert.AreEqual(expectedToopic, topic);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.FindElement(By.CssSelector("[aria-label='Save & close']")).Click();
            switchToMainWindow();
        }

        public void singOut()
        {
            switchToMainWindow();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.XPath("//span[@class='gb_xa gbii']"))).Click().Perform();
            switchToMyWindow();
            driver.FindElement(By.XPath("//a[@id='gb_71']")).Click();
        }

        [TearDown]
        public void TearDown() 
        {
            Thread.Sleep(3000);
            //driver.Quit();
        }

    }
}