
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using AutoItX3Lib;

namespace GoogleTranslate1
{
    public partial class Tests
    {
        private IWebDriver driver;

        private readonly By _textArea = By.XPath("//div/textarea[@aria-label='Текст оригіналу']");

        private readonly By _defineLangButton = By.XPath("//div[@class='ccvoYb']//span[contains(text(),'Визначити мову')]");

        private readonly By _searchLang = By.XPath("//div[@class='hRFt4b']//div[@class='OoYv6d']//input[@class='KskmCc']");

        private readonly By _switchLangButton = By.XPath("//div[@class='aCQag']//i[@class='material-icons-extended VfPpkd-kBDsod']");

        private readonly By _langOfWord = By.XPath("//button[@id='c1']/span[@class='VfPpkd-N5Lhkf']");

        private readonly By _uaLangOfTranslation = By.XPath("//div[@class='akczyd'][2]//span[contains(text(),'українська')]");

        private readonly By _engLangOfTranslation = By.XPath("//div[@class='akczyd'][2]//span[contains(text(),'англійська')]");

        private readonly By _uaLangToBeTranslated = By.XPath("//div[@class='akczyd']//span[contains(text(),'українська')]");

        private readonly By _engLangToBeTranslated = By.XPath("//div[@class='akczyd']//span[contains(text(),'англійська')]");

        private readonly By _translatedWord = By.XPath("//span[@class='VIiyi']/span/span");

        private readonly By _documentButton = By.XPath("//button//span[contains(text(),'Документи')]");

        private readonly By _chooseDocumentButton = By.XPath("//label[contains(text(),'Вибрати на комп’ютері')]");

        private readonly By _translateButton = By.XPath("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ nCP5yc AjY5Oe DuMIQc']//span[@class='VfPpkd-vQzf8d']");

        private readonly By _body = By.XPath("//body");

        private const string _engTranslation = "Hello!";

        private const string _engLang = "англійська";

        private const string _expctedLang = "ВИЯВЛЕНО: АНГЛІЙСЬКА";

        private const string _uaTranslation = "Привіт!";

        private const string _engLangSl = "sl=en";

        private const string _engLangTl = "tl=en";

        private const string _uaLangTl = "tl=uk";

        private const string _uaLangSl = "sl=uk";

        private const string filePath = @"C:\Users\DM\Desktop\Testing\Hello.txt";


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://translate.google.com.ua/");
            WaitUntil.ShoudLocate(driver, "https://translate.google.com.ua/");
            
        }

        [Test]
        public void AutoDefinitionOfLanguageIsCorrect()
        {
            var textArea = driver.FindElement(_textArea);
            textArea.SendKeys(_engTranslation);
            WaitUntil.WaitElement(driver, _langOfWord);
            var actualLang = driver.FindElement(_langOfWord).Text;
            Assert.AreEqual(_expctedLang, actualLang, "The language is wrong");
        }

        [Test]
        public void TranslationFomEngToUaLang()
        {
            IWebElement langToBeTranslated = driver.FindElement(_engLangToBeTranslated);
            Actions actions = new Actions(driver);
            actions.MoveToElement(langToBeTranslated).Click().Perform();
            var textArea = driver.FindElement(_textArea);
            textArea.SendKeys(_engTranslation);
            IWebElement langOfTranslation = driver.FindElement(_uaLangOfTranslation);
            actions.MoveToElement(langOfTranslation).Click().Perform();
            WaitUntil.WaitElement(driver, _translatedWord);
            var uaActTranslation = driver.FindElement(_translatedWord).Text;
            Assert.AreEqual(_uaTranslation, uaActTranslation, "The translation is wrong");
        }

        [Test]
        public void TranslationFomUaToEngLang()
        {
            IWebElement langToBeTranslated = driver.FindElement(_uaLangToBeTranslated);
            Actions actions = new Actions(driver);
            actions.MoveToElement(langToBeTranslated).Click().Perform();
            var textArea = driver.FindElement(_textArea);
            textArea.SendKeys(_engTranslation);
            IWebElement langOfTranslation = driver.FindElement(_engLangOfTranslation);
            actions.MoveToElement(langOfTranslation).Click().Perform();
            WaitUntil.WaitElement(driver, _translatedWord);
            var engActTranslation = driver.FindElement(_translatedWord).Text;
            Assert.AreEqual(_engTranslation, engActTranslation, "The translation is wrong");
        }

        [Test]
        public void ChoosigLanguageForTranslation()
        {
            IWebElement defineLang = driver.FindElement(_defineLangButton);
            Actions actions = new Actions(driver);
            actions.MoveToElement(defineLang).Click().Perform();
            WaitUntil.WaitElement(driver, _searchLang);
            var searchLang = driver.FindElement(_searchLang);
            searchLang.SendKeys(_engLang);
            searchLang.SendKeys(Keys.Enter);
            Assert.IsTrue(driver.Url.Contains(_engLangSl));
        }

        [Test]
        public void SwitchingLanguagesForTranslation()
        {
            IWebElement langToBeTranslated = driver.FindElement(_uaLangToBeTranslated);
            Actions actions = new Actions(driver);
            actions.MoveToElement(langToBeTranslated).Click().Perform();
            IWebElement langOfTranslation = driver.FindElement(_engLangOfTranslation);
            actions.MoveToElement(langOfTranslation).Click().Perform();
            Assert.IsTrue(driver.Url.Contains(_uaLangSl + "&" + _engLangTl));
            IWebElement switchLangBtn = driver.FindElement(_switchLangButton);
            actions.MoveToElement(switchLangBtn).Click().Perform();
            Assert.IsTrue(driver.Url.Contains(_engLangSl + "&" + _uaLangTl));
        }

        [Test]
        public void ChoosenDocumentForTranslation()
        {
            IWebElement docBtn = driver.FindElement(_documentButton);
            Actions actions = new Actions(driver);
            actions.MoveToElement(docBtn).Click().Perform();
            IWebElement chooseDocBtn = driver.FindElement(_chooseDocumentButton);
            actions.MoveToElement(chooseDocBtn).Click().Perform();
            AutoItX3 autoIt = new AutoItX3();
            autoIt.WinActivate("Open");
            WaitUntil.WaitSomeInterval();
            autoIt.Send(filePath);
            WaitUntil.WaitSomeInterval();
            autoIt.Send("{ENTER}");
            IWebElement langOfTranslation = driver.FindElement(_uaLangOfTranslation);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            (js).ExecuteScript("arguments[0].click()", langOfTranslation);
            WaitUntil.WaitElement(driver, _translateButton);
            IWebElement translateBtn = driver.FindElement(_translateButton);
            (js).ExecuteScript("arguments[0].click()", translateBtn);
            WaitUntil.WaitElement(driver, _body);
            var translation = driver.FindElement(_body).Text;
            Assert.AreEqual(_engTranslation, translation, "The translation is wrong");
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}