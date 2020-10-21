using AutoTests.Base;
using AutoTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using static AutoTests.Pages.ContactForm;

namespace AutoTests.TestCases
{
    class ContactFormTC
    {
        private IWebDriver oDriver;
        public const string startUrl= "https://bluepartner.eu/pl/kontakt/";

        [Test]
        [Description("Send contact form without Recaptcha and check validation message in Recaptcha field")]
        public void SendContactFormWithoutRecaptcha()
        {
            RequestData requestData = new RequestData(
                "CloudServices Test", RequestTopic.SETTLEMENT, "automat test CloudServices");

            ContactForm contactForm = new ContactForm(oDriver);
            contactForm.SendRequestFormWithoutRecaptcha(requestData);
           
        }


        [TearDown]
        public void TearDownTest()
        {
            InitializeClass.Close();

        }


        [SetUp]
        public void SetupTest()
        {
            InitializeClass.InitChromeBrowser(startUrl);
            this.oDriver = InitializeClass.getDriver;

        }
    }
}
