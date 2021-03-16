using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTranslate1.PageObject
{
    class HomePage
    {
        private readonly IWebDriver webDriver;

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
        

        public HomePage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }


        public void FillTextAreaInput(string word)
        {
            webDriver.FindElement(_textArea).SendKeys(word);
        }

        public string GetLangOfWordToBeTranslated()
        {
           WaitUntil.WaitElement(webDriver, _langOfWord);
           var actualLang = webDriver.FindElement(_langOfWord).Text;
           return actualLang;
        }

        public void ChooseEngLangToBeTranslated()
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(_engLangToBeTranslated)).Click().Perform();
        }

        public void ChooseUaLangOfTranslation()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            (js).ExecuteScript("arguments[0].click()", webDriver.FindElement(_uaLangOfTranslation));
        }

        public string GetTranslatedWord()
        {
            WaitUntil.WaitElement(webDriver, _translatedWord);
            var translation = webDriver.FindElement(_translatedWord).Text;
            return translation;
        }

        public void ChooseUaLangToBeTranslated()
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(_uaLangToBeTranslated)).Click().Perform();

        }

        public void ChooseEngLangOfTranslation()
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(_engLangOfTranslation)).Click().Perform();
        }

       public void ClickDefineLangButton()
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(_defineLangButton)).Click().Perform();
        }

        public void FillSerchLangInput(string language)
        {
            WaitUntil.WaitElement(webDriver, _searchLang);
            var searchLang = webDriver.FindElement(_searchLang);
            searchLang.SendKeys(language);
            searchLang.SendKeys(Keys.Enter);
        }

        public void ClickSwitchLangButton()
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(_switchLangButton)).Click().Perform();
        }

        public void ClickDocumentButton()
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(_documentButton)).Click().Perform();
        }

        public void ClickChooseDocumentButton()
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(_chooseDocumentButton)).Click().Perform();
        }

        public void ClickTranslateButton()
        {
            WaitUntil.WaitElement(webDriver, _translateButton);
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            (js).ExecuteScript("arguments[0].click()", webDriver.FindElement(_translateButton));
        }

        public string GetTranslatedText()
        {
            WaitUntil.WaitElement(webDriver, _body);
            var translation = webDriver.FindElement(_body).Text;
            return translation;
        }


    }
}
