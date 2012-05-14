extra-info-submit-helper
========================

This project demonstrates a way to pass extra information about which submit button was pressed on a HTML form.
It's useful when a button needs to be repeated a number of times, in the same HTML form.  If you can avoid 
having the form wrapping the whole table, you can add a form per row and just include the
details in a hidden field.  In that case there's *NO NEED TO USE THIS CODE*!

However, if you need the form to wrap the whole table, this code might help.

It enables you to encode extra information in the 'name' attribute of the submit button, which will be used
intercepted server-side and treated as form-data which will be parsed by the normal MVC model binding.

Tutorial
========
    // TODO