using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace MultiButtonEx.Web
{
    public static class SubmitButtonExtension
    {
        public static MvcHtmlString MultiButtonEx(this HtmlHelper helper, object data, string actionName, string title)
        {
            var dataString = actionName;
            foreach (var property in GetProperties(data))
            {
                dataString += "_" + property.Key + ":" + property.Value;
            }
            
            return new MvcHtmlString(string.Format("<input type='submit' name='{0}' value='{1}' />", dataString, title));
        }

        private static Dictionary<string, object> GetProperties(object anonymousObject)
        {
            return anonymousObject.GetType().GetProperties()
                .Where(x => x.CanRead)
                .ToDictionary(x => x.Name, x => x.GetValue(anonymousObject, new object[0]));
        }
    }
}