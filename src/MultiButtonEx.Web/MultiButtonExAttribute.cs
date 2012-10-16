using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace MultiButtonEx.Web
{
    /// <summary>
    /// Allows data to be passed through and bound to action parameters from an input name.
    /// For example, you can create a button called 'MyButton_key1:val1_key2:val2_key3:42' then create
    /// an action like:
    /// <code>
    /// [MultiButtonExAttribute("MyButton")]
    /// public ActionResult WhateverYouWantToCallTheAction(string key1, string key2, int key3)
    /// {
    ///     Assert.AreEqual("val1", key1);
    ///     Assert.AreEqual("val2", key2);
    ///     Assert.AreEqual(42, key3);
    /// }
    /// </code>
    /// Note that all the normal model binding applies to the parameters, so you can have parameters
    /// of the correct type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultiButtonExAttribute : ActionNameSelectorAttribute
    {
        public MultiButtonExAttribute(string matchFormKeyBeginningWith)
        {
            MatchFormKeyBeginningWith = EnsureEndsWithUnderscore(matchFormKeyBeginningWith);
        }

        public string MatchFormKeyBeginningWith { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            foreach (var key in controllerContext.HttpContext.Request.Params.Keys.Cast<string>())
            {
                if (key.StartsWith(MatchFormKeyBeginningWith))
                {
                    AddValuesToRouteDataForBinding(controllerContext, key);
                    return true;
                }
            }
            return false;
        }

        private void AddValuesToRouteDataForBinding(ControllerContext controllerContext, string key)
        {
            var keyWithoutPrefix = key.Replace(MatchFormKeyBeginningWith, "");
            var keyValuePairs = keyWithoutPrefix.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var keyValuePair in keyValuePairs)
            {
                var parts = keyValuePair.Split(':');
                controllerContext.RequestContext.RouteData.Values.Add(parts[0], parts[1]);
            }
        }

        private string EnsureEndsWithUnderscore(string matchFormKeyBeginningWith)
        {
            if (matchFormKeyBeginningWith.EndsWith("_"))
                return matchFormKeyBeginningWith;
            return matchFormKeyBeginningWith + "_";
        }
    }
}