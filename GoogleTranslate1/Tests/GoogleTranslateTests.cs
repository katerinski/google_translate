
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using GoogleTranslate1.PageObject;

namespace GoogleTranslate1
{
    public partial class Tests
    {
        private IWebDriver webDriver;
        public static string url = "https://translate.google.com.ua/";

     

        private const string _wordInEng = "Hello!";

        private const string _wordInUa = "Привіт!";

        private const string _engLang = "англійська";

        private const string _expctedLang = "ВИЯВЛЕНО: АНГЛІЙСЬКА";

        private const string _engLangSl = "sl=en";

        private const string _engLangTl = "tl=en";

        private const string _uaLangTl = "tl=uk";

        private const string _uaLangSl = "sl=uk";


        [SetUp]
        public void Setup()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(url);
            WaitUntil.ShoudLocate(webDriver, url);
        }

        [Test]
        public void AutoDefinitionOfLanguageIsCorrect()
        {
            var homePage = new HomePage(webDriver);
            homePage.FillTextAreaInput(_wordInEng);
            Assert.AreEqual(_expctedLang, homePage.GetLangOfWordToBeTranslated(), "The language is wrong");
        }

        [Test]
        public void TranslationFomEngToUaLang()
        {
            var homePage = new HomePage(webDriver);
            homePage.ChooseEngLangToBeTranslated();
            homePage.FillTextAreaInput(_wordInEng);
            homePage.ChooseUaLangOfTranslation();
            Assert.AreEqual(_wordInUa, homePage.GetTranslatedWord(), "The translation is wrong");
        }

        [Test]
        public void TranslationFomUaToEngLang()
        {
            var homePage = new HomePage(webDriver);
            homePage.ChooseUaLangToBeTranslated();
            homePage.FillTextAreaInput(_wordInEng);
            //homePage.ChooseEngLangOfTranslation();
            Assert.AreEqual(_wordInEng, homePage.GetTranslatedWord(), "The translation is wrong");
        }

        [Test]
        public void ChoosigLanguageForTranslation()
        {
            var homePage = new HomePage(webDriver);
            homePage.ClickDefineLangButton();
            homePage.FillSerchLangInput(_engLang);
            Assert.IsTrue(webDriver.Url.Contains(_engLangSl));
        }

        [Test]
        public void SwitchingLanguagesForTranslation()
        {
            var homePage = new HomePage(webDriver);
            homePage.ChooseUaLangToBeTranslated();
            homePage.ChooseEngLangOfTranslation();
            Assert.IsTrue(webDriver.Url.Contains(_uaLangSl + "&" + _engLangTl));
            homePage.ClickSwitchLangButton();
            Assert.IsTrue(webDriver.Url.Contains(_engLangSl + "&" + _uaLangTl));
        }

        [Test]
        public void ChoosenDocumentForTranslation()
        {
            var homePage = new HomePage(webDriver);
            homePage.ClickDocumentButton();
            homePage.ClickChooseDocumentButton();
            var openFile = new OpenFile();
            openFile.OpenFileFromLocalMachine();
            homePage.ChooseUaLangOfTranslation();
            homePage.ClickTranslateButton();
            Assert.AreEqual(_wordInEng, homePage.GetTranslatedText(), "The translation is wrong");
        }


        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }
    }
}