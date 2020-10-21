using OpenQA.Selenium;

namespace AutoTests.Base
{
    public class BaseElements
    {
        public class ElementParams
        {
            public string xpath { get; set; }
            public string name { get; set; }

            public ElementParams(string xpath, string name)
            {
                this.xpath = xpath;
                this.name = name;
            }
        }

        public class Element
        {
            public IWebElement webElement { get; set; }
            public ElementParams parameters { get; set; }


            public Element(IWebElement webElement, ElementParams elParams)
            {
                this.webElement = webElement;
                this.parameters = elParams;
            }

        }
    }

}
