using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests_Golovachev;

public class SeleniumTestsForPractice
{
    public ChromeDriver driver;
    [Test]
    public void AuthorizationTest()
    {
        var title = GetTitle(); 
        title.Should().Be("Новости");
    }

    [Test]
    public void PageMainMenuTest()
    {
        var community = driver
            .FindElements(By.CssSelector("[data-tid='Community']"))
            .First(el => el.Displayed);
        community.Click();

        var title = GetTitle();
        title.Should().Be("Сообщества");
    }

    [Test]
    public void CreateCommunityTest()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/news"));
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
        
        var createButton = driver.FindElement(By.ClassName("sc-juXuNZ"));
        createButton.Click();

        var communityName = driver.FindElement(By.ClassName("react-ui-seuwan"));
        var name = "Сообщество для теста";
        communityName.SendKeys(name);

        var createCommunity = driver
            .FindElements(By.ClassName("react-ui-button-caption"))
            .First(el => el.Text == "Создать");
        createCommunity.Click();

        var createdCommunityName = driver.FindElement(By.ClassName("sc-eCApnc")).Text;
        createdCommunityName.Should().Be(name);
    }

    [Test]
    public void EditProfileTest()
    {
        var avatar = driver.FindElement(By.CssSelector("[data-tid='Avatar']"));
        avatar.Click();
        
        var profileEdit = driver.FindElement(By.CssSelector("[data-tid='ProfileEdit']")); 
        profileEdit.Click();

        var whatAmIDoing = driver.FindElement(By
                .XPath("//*[@id=\"root\"]/section/section[2]/section[3]/div[1]/div[7]/label[2]"));
        var whatAmIDoingText = "Занимаюсь";
        whatAmIDoing.SendKeys(Keys.Control+"a");
        whatAmIDoing.SendKeys(Keys.Backspace);
        whatAmIDoing.SendKeys(whatAmIDoingText);
        
        var saveEdit = driver
            .FindElements(By.ClassName("sc-juXuNZ"))
            .First(el => el.Text == "Сохранить");
        saveEdit.Click();

        var visibleProfile = driver.FindElement(By.ClassName("sc-EhTUr")).Text;
        visibleProfile.Should().Be(whatAmIDoingText);
    }

    [Test]
    public void GoToMainPageTest()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/news"));
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");

        var siteLogo = driver.FindElement(By.ClassName("sc-hzUIXc"));
        siteLogo.Click();

        var title = GetTitle();
        title.Should().Be("Новости");
    }

    public string GetTitle() => driver.FindElement(By.CssSelector("[data-tid='Title']")).Text;
    
    public void Auth()
    {
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("xxxxx");
        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys("xxxxx");
        
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
    }

    public void SetOption()
    {
        var option = new ChromeOptions();
        option.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        
        driver = new ChromeDriver(option);
        var url = "https://staff-testing.testkontur.ru";
        driver.Navigate().GoToUrl(url);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }
    
    [TearDown]
    public void TearDown()
    {
        driver.Close();
        driver.Quit();
    }

    [SetUp]
    public void SetUp()
    {
        SetOption();
        Auth();
    }
}
