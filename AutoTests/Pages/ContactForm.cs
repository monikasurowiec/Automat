using AutoTests.Base;
using AutoTests.Params;
using OpenQA.Selenium;
using System.Collections.Generic;
using static AutoTests.Base.BaseElements;
using static AutoTests.Params.WebElementsParams;

namespace AutoTests.Pages
{
    class ContactForm:CoreMethods
    {
        public ContactForm(IWebDriver oDriver)
        {
            this.oDriver = oDriver;
            this.baseTimer = TestParams.timerInSec;
        }

        public enum RequestTopic
        {
            COOPERATION,
            RECLAMATION,
            SETTLEMENT,
            HELP_DESK
        }

        public class RequestData
        {
            public string name { get; set; }
            public string email { get; set; }
            public string text { get; set; }
            public RequestTopic topic { get; set; }

            public RequestData(string name, string email, RequestTopic requestTopic, string requestText)
            {
                this.name = name;
                this.email = email;
                this.topic = requestTopic;
                this.text = requestText;
            }

            public RequestData(string name, RequestTopic requestTopic, string requestText)
            {
                this.name = name;
                this.email = DataGenerators.ReturnRandomEmail();
                this.topic = requestTopic;
                this.text = requestText;
            }
        }

        #region webElements

        private static string FORM_CONTAINER_XPATH = "//h2[contains(text(),'Napisz Do Nas')]/ancestor::div[2]";

        private static ElementParams nameTextField = SetInputParams("Imię i Nazwisko", FORM_CONTAINER_XPATH);
        private static ElementParams emailTextField = SetInputParams("E-mail", FORM_CONTAINER_XPATH);
        private static ElementParams questionTextField = SetInputParams("Treść", FORM_CONTAINER_XPATH, "textarea");
        private static ElementParams recaptchaTextField = SetInputParams("Recaptcha", FORM_CONTAINER_XPATH);


        private static ElementParams topicScrollList = SetScrollListParams("Wybierz Temat", FORM_CONTAINER_XPATH);

        private static ElementParams checkboxWithClause = SetCheckboxParams("Wyrażam zgodę na przetwarzanie", FORM_CONTAINER_XPATH);

        private static ElementParams buttonSend = SetButtonParams("Wyślij", FORM_CONTAINER_XPATH);

        #endregion

        public void SendRequestFormWithoutRecaptcha(RequestData requestData)
        {
            TestScenarioMethod(() =>
            {
                FillRequestForm(requestData);
                SelectCheckboxAndSendForm();
                CheckValidationMessageForField(recaptchaTextField, "To pole jest wymagane.");
            });
        }

        #region PrivateMethods
        private void FillRequestForm(RequestData requestData)
        {
            FillTextField(ReturnClickableElement(nameTextField), requestData.name);
            FillTextField(ReturnClickableElement(emailTextField), requestData.email);

            SelectValueFromScrollList(ReturnClickableElement(topicScrollList), topicStr[requestData.topic]);

            FillTextField(ReturnClickableElement(questionTextField), requestData.text);

        }
        private void SelectCheckboxAndSendForm()
        {
            ClickOnElement(ReturnClickableElement(checkboxWithClause));
            ClickOnElement(ReturnClickableElement(buttonSend));
        }

        private void CheckValidationMessageForField(ElementParams fieldParams, string expectedText)
        {
            ElementParams messageParams = SetValidationMessageParams(fieldParams);
            Element messageElem = ReturnVisibleElement(messageParams);

            AssertThatFieldContainsText(messageElem, expectedText);
        }

        private Dictionary<RequestTopic, string> topicStr = new Dictionary<RequestTopic, string>
        {
            {RequestTopic.COOPERATION, "Współpraca" },
            {RequestTopic.RECLAMATION, "Reklamacja" },
            {RequestTopic.SETTLEMENT, "Rozliczenie" },
            {RequestTopic.HELP_DESK, "Help Desk" }

        };

        #endregion


    }
}
