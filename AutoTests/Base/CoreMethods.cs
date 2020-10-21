using AutoTests.Params;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using static AutoTests.Base.BaseElements;

namespace AutoTests.Base
{
    public abstract class CoreMethods
    {
        protected IWebDriver oDriver;
        protected int baseTimer;


        /// <summary>
        /// Core test scenario methods
        /// </summary>
        /// <param name="action"> All actions to do during test scenario </param>
        protected void TestScenarioMethod(Action action)
        {
            try
            {
                action();
                PrintImportantLog("success");
            }
            catch (Exception e)
            {
                PrintImportantLog("fail");
                TrySaveScreenshot();
                PrintLog("Current url: " + oDriver.Url);                
                PrintLog("");

                throw new Exception("exception: " + e);
            }

        }



        /// <summary>
        /// Return visible element
        /// </summary>
        /// <param name="elementParams"> instance of ElementParams class </param>
        protected Element ReturnVisibleElement(ElementParams elementParams)
        {
            return TryMoveToElement(TryToReturnVisibleElement(elementParams, baseTimer));
        }

        /// <summary>
        /// Return clickable element
        /// </summary>
        /// <param name="elementParams"> instance of ElementParams class </param>
        protected Element ReturnClickableElement(ElementParams elementParams)
        {
            Element element = TryMoveToElement(TryToReturnVisibleElement(elementParams, baseTimer));
            return WaitAndCheckElementIsClickable(element, baseTimer);

        }

        /// <summary>
        /// Wait for clickable element
        /// </summary>
        /// <param name="element"> instance of Element class </param>
        protected Element WaitForClickableElement(Element element)
        {
            return WaitAndCheckElementIsClickable(element, baseTimer);
        }

        /// <summary>
        /// Click on element methods
        /// </summary>
        /// <param name="element"> instance of Element class </param>
        protected void ClickOnElement(Element element)
        {
            TryToClickOnElementWithJs(element);
        }

        /// <summary>
        /// Fill text field methods
        /// </summary>
        /// <param name="element"> instance of Element class </param>
        /// <param name="text"> text to put into web element </param>
        protected void FillTextField(Element element, string text)
        {
            Element textField = WaitForClickableElement(element);
            TryToSendKeysToField(textField, text);
        }

        /// <summary>
        /// Select option from Scroll List
        /// </summary>
        /// <param name="element"> instance of Element class </param>
        /// <param name="text"> text to choose on scroll list </param>
        protected void SelectValueFromScrollList(Element element, string text)
        {
            TryToSelectValueFromScrollList(element, text);
        }

        /// <summary>
        /// Return text from element
        /// </summary>
        /// <param name="element"> instance of Element class </param>
        protected string ReturnTextFromField(Element element)
        {
            return TryReturnTextFromElement(element);
        }

        /// <summary>
        /// Check that text on element is the same like expected
        /// </summary>
        /// <param name="element"> instance of Element class </param>
        /// <param name="expectedText"> expected text to choose </param>
        protected void AssertThatFieldContainsText(Element element, string expectedText)
        {        
            Assert.IsTrue(ReturnTextFromField(element).Contains(expectedText), 
                element.parameters.name + " doesn't contains text '" + expectedText + 
                "' ; xpath: " + element.parameters.xpath);

            PrintLog(element.parameters.name + " contains expected text '" + expectedText + "'");
        }



        #region PrivateMethods

        private string TryReturnTextFromElement(Element element)
        {
            string text;
            try
            {
                text = element.webElement.GetAttribute("innerHTML");
            }
            catch (Exception e)
            {
                throw new Exception("Return text from " + element.parameters.name + " failed "
                                 + " xpath : " + element.parameters.xpath + " : " + e);
            }

            return text;

        }

        private void PrintLog(string message)
        {
            Console.WriteLine(message);
        }

        private void TryToClickOnElementWithJs(Element element)
        {
            DateTime startDate = DateTime.UtcNow;
            do
            {
                try
                {
                    ((IJavaScriptExecutor)oDriver).ExecuteScript("arguments[0].scrollIntoView(false);", element.webElement); ;
                    ((IJavaScriptExecutor)oDriver).ExecuteScript("arguments[0].click();", element.webElement);
                    PrintLog("Click the " + element.parameters.name);
                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                    if (startDate.AddSeconds(baseTimer) < DateTime.UtcNow)
                    {
                        Assert.Fail("Failed to click "
                        + element.parameters.name + " xpath : " + element.parameters.xpath);
                    }
                    continue;
                }
            } while (baseTimer > 0);
        }

        private Element TryMoveToElement(Element element)
        {
            try
            {
                Actions actions = new Actions(oDriver);
                actions.MoveToElement(element.webElement);
                actions.Perform();
            }
            catch (Exception e)
            {
                throw new Exception("Nie udalo sie przejsc do widoku elementu : " + element.parameters.name
                    + "; xpath : " + element.parameters.xpath + " : " + e);

            }
            return element;

        }

        private void TryToSendKeysToField(Element element, string text)
        {
            try
            {
                element.webElement.Clear();
                element.webElement.SendKeys(text);
            }
            catch (Exception e)
            {
                throw new Exception("Enter text '" + text + "' to "
                + element.parameters.name + " failed ; xpath : " + element.parameters.xpath + " : " + e);
            }

            PrintLog("Enter text '" + text + "' to " + element.parameters.name);
        }

        private void TryToSelectValueFromScrollList(Element element, string text)
        {
            try
            {
                SelectElement select = new SelectElement(element.webElement);
                select.SelectByText(text);
            }
            catch (Exception e)
            {
                throw new Exception("Select value '" + text + "' from "
                + element.parameters.name + " failed ; xpath : " + element.parameters.xpath + " : " + e);
            }

            PrintLog("Select value '" + text + "' from " + element.parameters.name);
        }

        private Element WaitAndCheckElementIsClickable(Element element, int timerSec)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(oDriver, TimeSpan.FromSeconds(timerSec));

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element.webElement));
                return element;

            }
            catch (Exception e)
            {
                throw new Exception("Element " + element.parameters.name + " did not become visible after "
                + timerSec.ToString() + " seconds; xpath : " + element.parameters.xpath + " : " + e);
            }
        }

        private Element TryToReturnVisibleElement(ElementParams elParams, int timerSec)
        {
            try
            {
                By locator = By.XPath(elParams.xpath);
                WebDriverWait wait = new WebDriverWait(oDriver, TimeSpan.FromSeconds(timerSec));

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                IWebElement webElement = wait.Until(d => d.FindElement(locator));

                return new Element(webElement, elParams);

            }
            catch (Exception e)
            {
                throw new Exception("Element " + elParams.name + " did not become clickable after "
                + timerSec.ToString() + " seconds; xpath : " + elParams.xpath + " : " + e);
            }

        }

        private void TrySaveScreenshot()
        {
            string dateStr = DateTime.Now.ToString().Replace(":", "-").Replace(" ", "_");
            string screenshotName = dateStr + ".png";
            string path = TestParams.screenshotPath;

            try
            {
                oDriver.TakeScreenshot().SaveAsFile(path + screenshotName, ScreenshotImageFormat.Png);
                PrintLog("Screenshot " + screenshotName + " save in localization " + path);
            }
            catch (Exception)
            {
                PrintLog("Screenshot " + screenshotName + " did not save in localization " + path);
            }
        }

        private void PrintImportantLog(string shortMessage)
        {
            PrintLog("");
            PrintLog(" ================= " + shortMessage.ToUpper() + " ==================== ");
            PrintLog("");
        }


    }
}
#endregion