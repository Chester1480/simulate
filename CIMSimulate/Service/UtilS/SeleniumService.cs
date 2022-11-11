using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace CIMSimulate.Service.UtilS
{
    public class SeleniumService
    {
        private IWebDriver driver;
        public SeleniumService()
        {

        }
        public void OpenBroser(string path, string url, string browser = "chrome")
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver(path);
                    driver = new ChromeDriver();
                    return;
                case "firefox":
                    driver = new FirefoxDriver(path);
                    driver = new FirefoxDriver();
                    return;
                default:
                    driver = new ChromeDriver(path);
                    driver = new ChromeDriver();
                    return;
            }
        }
    }
}
