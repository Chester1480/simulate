using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace CIMSimulate.Service
{
    public class SeleniumService
    {
        private IWebDriver driver;
        public SeleniumService()
        {

        }
        public void OpenBroser(string path,string url, String browser = "chrome")
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    this.driver = new ChromeDriver(path);
                    driver = new ChromeDriver();
                    return;
                case "firefox":
                    this.driver = new FirefoxDriver(path);
                    driver = new FirefoxDriver();
                    return;
                default:
                    this.driver = new ChromeDriver(path);
                    driver = new ChromeDriver();
                    return;
            }
        }
    }
}
