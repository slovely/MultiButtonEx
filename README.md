MultiButtonEx
=============

This project demonstrates a way to pass extra information about which submit button was pressed on a HTML form.
It's useful when a button needs to be repeated a number of times, in the same HTML form.  If you can avoid 
having the form wrapping the whole table, you can add a form per row and just include the
details in a hidden field.  In that case there's *NO NEED TO USE THIS CODE*!

However, if you need the form to wrap the whole table, this code might help.

It enables you to encode extra information in the 'name' attribute of the submit button, which will be used
intercepted server-side and treated as form-data which will be parsed by the normal MVC model binding.  It's based on
the custom 'ActionMethodSelectorAttribute' in this post: http://weblogs.asp.net/dfindley/archive/2009/05/31/asp-net-mvc-multiple-buttons-in-the-same-form.aspx

Tutorial
========

First, copy the MultiButtonExAttribute class into your application somewhere.  Then create the action method you want
the submit button to post to, and decorate it with the 'MultiButtonExAttribute' like this:

    [MultiButtonEx("PerformTheAction")]
    [HttpPost]
    public ActionResult MyAction(int param1, string param2, Guid etcEtc)
    {
        //TODO: whatever needs to be done
        //The parameters will be parsed for you by the ModelBinder and
        //param1, param2 and param3 will have the expected values.
    }
    
Then when you output your submit button you can encode the extra data (in this case param1, param2 and param3) in the
name attribute in the following manner:

    <input type="submit" name="PerformTheAction_param1:42_param2:hello_param3:AB141CC9-DEB9-4946-B79F-10AE6DBE14B3" />
    
The basic structure here is: {ActionName}\_{property}:{value}\_{property}:{value}
The property key/value pairs will be part of the ModelBinding part of the MVC request and all normal rules will apply.

Included in the sample app is a HtmlHelper method for creating these inputs, use it like:

    @Html.MultiButtonEx(new {id = item.Id, other = item.Other}, "PerformAction", "Click Me!")
    
See src/MultiButtonEx.Web/Controllers/HomeController.cs and src/MultiButtonEx.Web/Views/Home/Index.cshtml for a few
example usages.

Limitations
===========

 - The length of the name attribute is probably limited in the HTML spec
 - I haven't bothered with any way to escape the hard-coded ':' and '_' separators, so if the data you want to submit contains these it won't work.
 - You have to ensure that the name attribute you generate doesn't contain values that aren't allowed in a name attribute.
 - This isn't production code, you'll want to add error handling, etc, if you decide to use it.