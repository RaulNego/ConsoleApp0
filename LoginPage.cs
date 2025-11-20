using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class LoginPage(ChromeDriver driver)
{
    private readonly ChromeDriver _driver = driver;

    private IWebElement UsernameBox => _driver.FindElement(By.Id("username"));
    private IWebElement PasswordBox => _driver.FindElement(By.Id("password"));
    private IWebElement SubmitButton => _driver.FindElement(By.CssSelector("button[type='submit']"));

    public void GoTo(String url)
    {
        _driver.Navigate().GoToUrl(url);
        WaitForPageLoad();
    }

    public void Login(string username, string password)
    {
        UsernameBox.SendKeys(username);
        PasswordBox.SendKeys(password);
        SubmitButton.Click();
        WaitForPageLoad();
    }

    public bool IsSecurePage(string secureUrl)
    {
        return _driver.Url == secureUrl;
    }

    public bool IsErrorMessageDisplayed(IWebDriver driver)
    {
        var errorElements = driver.FindElements(By.CssSelector("div#flash.flash.error"));
        return errorElements.Count > 0 && errorElements[0].Displayed;
    }

    private void WaitForPageLoad(int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        wait.Until(d =>
            ((IJavaScriptExecutor)d)
            .ExecuteScript("return document.readyState")
            .Equals("complete"));
    }
}
