using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests_Golovachev;

public class SeleniumTestsForPractice
{
    [Test]
    public void Authorization()
    {
        var option = new ChromeOptions();
        option.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        
        var driver = new ChromeDriver(option);
        var url = "https://staff-testing.testkontur.ru";
        driver.Navigate().GoToUrl(url);

        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("bfire2066@gmail.com");
        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys("Bl0@dFire63");
        
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(5000);
        
        var currentUrl = driver.Url;
        Assert.That(currentUrl == url + "/news");
        
        driver.Quit();
    }
}