using OpenQA.Selenium.Chrome;


if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run positive|negative|functional");
    return;
}
string command = args[0].ToLower();

switch (command)
{
    case "positive":
        Console.WriteLine("Running positive test case.");
        RunPositiveLoginTest();
        break;

    case "negative":
        Console.WriteLine("Running negative test case.");
        RunNegativeLoginTest();
        break;

    case "functional":
        Console.WriteLine("Running functional test case.");
        RunFunctionalTest();
        break;

    default:
        Console.WriteLine("Unknown command.");
        break;
}

void RunPositiveLoginTest()
{
    var options = new ChromeOptions();
    options.AddArgument("--start-maximized");
    options.AddArgument("--headless");
    using ChromeDriver driver = new(options);
    string loginUrl = "https://the-internet.herokuapp.com/login";
    string secureUrl = "https://the-internet.herokuapp.com/secure";


    var loginPage = new LoginPage(driver);
    loginPage.GoTo(loginUrl);
    loginPage.Login("tomsmith", "SuperSecretPassword!");

    if (loginPage.IsSecurePage(secureUrl))
    {
        Console.WriteLine("Secure session obtained.");
    }
    else
    {
        Console.WriteLine("Failed secure access.");
    }

    driver.Quit();
}

void RunNegativeLoginTest()
{
    var options = new ChromeOptions();
    options.AddArgument("--start-maximized");
    options.AddArgument("--headless");
    using ChromeDriver driver = new(options);
    string loginUrl = "https://the-internet.herokuapp.com/login";
    string secureUrl = "https://the-internet.herokuapp.com/secure";

    var loginPage = new LoginPage(driver);
    loginPage.GoTo(loginUrl);
    loginPage.Login("bottomsmith", "wrongpassword");

    if (!loginPage.IsSecurePage(secureUrl) && loginPage.IsErrorMessageDisplayed(driver))
    {
        Console.WriteLine("Login successfully failed.");
    }
    else
    {
        Console.WriteLine("Login fialed unexpectedly.");
    }

    driver.Quit();
}

void RunFunctionalTest()
{
    var options = new ChromeOptions();
    options.AddArgument("--start-maximized");
    options.AddArgument("--headless");
    using ChromeDriver driver = new(options);
    string functionalUrl = "https://the-internet.herokuapp.com/add_remove_elements/";


    var functionalPage = new AddRemoveElementsPage(driver);
    functionalPage.GoTo(functionalUrl);

    functionalPage.ClickAddButton(2);

    int childrenNumber = functionalPage.CountChildren();
    Console.WriteLine(childrenNumber == 2
        ? $"Correctly found children: {childrenNumber}"
        : $"Incorrectly found children: {childrenNumber}");

    functionalPage.ClickFirstChild();

    childrenNumber = functionalPage.CountChildren();
    Console.WriteLine(childrenNumber == 1
        ? $"Correctly found children the 2nd time: {childrenNumber}"
        : $"Incorrectly found children the 2nd time: {childrenNumber}");

    driver.Quit();
}
