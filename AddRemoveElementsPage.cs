using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class AddRemoveElementsPage(ChromeDriver driver)
{
    private readonly ChromeDriver _driver = driver;

    private IWebElement AddButton => _driver.FindElement(By.CssSelector(".example > button"));
    private By ChildrenSelector => By.CssSelector("#elements > *");
    private IWebElement FirstChild => _driver.FindElement(By.CssSelector("#elements > *:first-child"));

    public void GoTo(string url)
    {
        _driver.Navigate().GoToUrl(url);
        WaitForPageLoad();
    }

    public void ClickAddButton(int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            AddButton.Click();
        }
    }

    public int CountChildren()
    {
        return _driver.FindElements(ChildrenSelector).Count;
    }

    public void ClickFirstChild()
    {
        FirstChild.Click();
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
