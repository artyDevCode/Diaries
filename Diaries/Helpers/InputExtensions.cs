using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;
using System.Collections.Specialized;


namespace Diaries.Helpers
{
    public class aBooleanList
    {
        public bool ID { get; set; }
        public String Name { get; set; }
    }

    public class BooleanRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value is bool)
                return (bool)value;
            else
                return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "booleanrequired"
            };
        }
    }

    public static class InputExtensions
    {
        public static IHtmlString RichTextAreaFor(this HtmlHelper helper, string name="", string text ="", bool isread=false)
        {
            return new MvcHtmlString(string.Format("<textarea name='{0}' data-njcc-read='{3}' id='{1}'>{2}</textarea>", name, name, text, isread));
        }

        public static MvcHtmlString RadioButtonForSelectList<TModel, TProperty>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    IEnumerable<SelectListItem> listOfValues)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                // Create a radio button for each item in the list 
                foreach (SelectListItem item in listOfValues)
                {
                    // Generate an id to be given to the radio button field 
                    var id = string.Format("{0}_{1}", metaData.PropertyName, item.Value);

                    // Create and populate a radio button using the existing html helpers 
                    var label = htmlHelper.Label(id, HttpUtility.HtmlEncode(item.Text));
                    var radio = htmlHelper.RadioButtonFor(expression, item.Value, new { id = id }).ToHtmlString();

                    // Create the html string that will be returned to the client 
                    // e.g. <input data-val="true" data-val-required="You must select an option" id="TestRadio_1" name="TestRadio" type="radio" value="1" /><label for="TestRadio_1">Line1</label> 
                    sb.AppendFormat("<div class=\"RadioButton\">{0}  {1}</div>", radio, label);
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        public static IHtmlString UnValuedCheckBox(this HtmlHelper helper, string name, string value)
        {
            string html = @"<input type=""checkbox"" name=""{0}"" value=""{1}""/>";
            return new MvcHtmlString(string.Format(html, name, value));
        }

        public static IHtmlString InputAreaFor<T>(this HtmlHelper helper, T val, string name, string text)
        {
            string isDate = null;
            if (typeof(T) == typeof(DateTime))
                isDate = ((DateTime)(object)val).ToString("dd-MMM-yyyy");

            return new MvcHtmlString(string.Format("<input style='width:100%; border:none; background-color:transparent;' class='text-box single-line' id='{0}' name='{0}' type='{1}' value='{2}' />", name, text, isDate == null ? val.ToString() : isDate));
            //return new MvcHtmlString(string.Format("<input style='width:100%; border:none; background-color:transparent;' class='text-box single-line' id='{0}' name='{0}' type='{1}' readonly='readonly' value='{2}' />", name, text, isDate == null ? val.ToString() : isDate));

        }

        //public static IHtmlString ReadOnlyDisplay(this HtmlHelper helper, string target, string text)
            
        public static IHtmlString ColorPickAreaFor<T>(this HtmlHelper helper, T val, string name, string text)
        {
            string isDate = null;
            if (typeof(T) == typeof(DateTime))
                isDate = ((DateTime)(object)val).ToString("dd-MMM-yyyy");

            return new MvcHtmlString(string.Format("<input type='text' name='{1}' id='{1}' style='width:100%;' class='form-control' value='{0}' >", name, text));

            //return new MvcHtmlString(string.Format("<input style='width:100%; border:none; background-color:transparent;' class='form-control' id='{0}' name='{0}' type='{1}' value='{0}' />", name, text, isDate == null ? val.ToString() : isDate));
            //<input type="text" name=model. value="" class="form-control" />
            //return new MvcHtmlString(string.Format("<input style='width:100%; border:none; background-color:transparent;' class='text-box single-line' id='{0}' name='{0}' type='{1}' readonly='readonly' value='{2}' />", name, text, isDate == null ? val.ToString() : isDate));

        }

        public static IHtmlString ColorPickAreaReadOnlyFor<T>(this HtmlHelper helper, T val, string name, string text)
        {
            string isDate = null;
            if (typeof(T) == typeof(DateTime))
                isDate = ((DateTime)(object)val).ToString("dd-MMM-yyyy");

            return new MvcHtmlString(string.Format("<input type='text' name='{1}' id='{1}' style='width:100%;' class='form-control' readonly='readonly' value='{0}' >", name, text));
        }
    
    }
}