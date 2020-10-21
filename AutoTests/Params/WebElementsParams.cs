using static AutoTests.Base.BaseElements;

namespace AutoTests.Params
{
    public static class WebElementsParams
    {

        /// <summary>
        /// Set button with label element params
        /// </summary>
        /// <param name="label"> label on button text </param>
        /// <param name="containerXpath"> xpath to the container in which the element is </param>
        /// <param name="htmlType"> html element type (default button) </param>
        public static ElementParams SetButtonParams(string label, string containerXpath, string htmlType = "button")
        {
            string elementXpath = "//" + htmlType + "[contains(text(),'" + label + "')]";
            string name = "button with label " + label;

            return new ElementParams(containerXpath + elementXpath, name);
        }


        /// <summary>
        /// Set input with label element params
        /// </summary>
        /// <param name="label"> input element text label </param>
        /// <param name="containerXpath"> xpath to the container in which the element is </param>
        /// <param name="htmlType"> html element type (default input) </param>
        public static ElementParams SetInputParams(string label, string containerXpath, string htmlType = "input")
        {
            if(htmlType=="input")
            {
                htmlType = "input[@type='text']";
            }

            string elementXpath = "//label[contains(text(),'" + label + "')]/following-sibling::div//" + htmlType;
            string name = "text field with label " + label;

            return new ElementParams(containerXpath + elementXpath, name);

        }

        /// <summary>
        /// Set select element with label params
        /// </summary>
        /// <param name="label"> select element text label </param>
        /// <param name="containerXpath"> xpath to the container in which the element is </param>
        public static ElementParams SetScrollListParams(string label, string containerXpath)
        {
            string elementXpath = "//label[contains(text(),'" + label + "')]/following-sibling::div/select";
            string name = "select field with label " + label;

            return new ElementParams(containerXpath + elementXpath, name);
        }

        /// <summary>
        /// Set checkbox element params
        /// </summary>
        /// <param name="partialText"> partial text near checkbox </param>
        /// <param name="containerXpath"> xpath to the container in which the element is </param>
        public static ElementParams SetCheckboxParams(string partialText, string containerXpath)
        {
            string elementXpath = "//label[contains(text(),'" + partialText + "')]";
            string name = "checkbox with text : ..." + partialText + " ...";

            return new ElementParams(containerXpath + elementXpath, name);
        }

        /// <summary>
        /// Set validation message element params
        /// </summary>
        /// <param name="validateElementParams"> parameters of the element that the validation applies to</param>
        public static ElementParams SetValidationMessageParams(ElementParams validateElementParams)
        {
            string messageXpath = "//ancestor::div[2]//span[contains(@class,'invalid')]";

            string xpath = validateElementParams.xpath + messageXpath;
            string name = "validation message for " + validateElementParams.name;

            return new ElementParams(xpath, name);
        }
        

        

    }
}
