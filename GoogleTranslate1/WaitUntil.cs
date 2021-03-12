using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;

namespace GoogleTranslate1
{
    
        public static class WaitUntil
        {
            public static void ShoudLocate(IWebDriver webDriver, string location)
            {
                try
                {
                    new WebDriverWait(webDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.UrlContains(location));
                }
                catch(WebDriverTimeoutException ex)
                {
                    throw new NotFoundException($"Can't find location: {location}", ex);
                }
            }

            public static void WaitSomeInterval(int seconds = 2)
            {
                Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
            }

            public static void WaitElement(IWebDriver webDriver, By locator, int seconds = 20)
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(locator));
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(locator));
            }
        }
    
}