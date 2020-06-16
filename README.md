# Mail Engine
Create mail template using Razor syntax

## The problem
A common solution of email template is storing the email template into database, and filling the template with token like mail merge.

It works for simple email template but what if we need to add business logic into the email such as show / hide a section base on the input data?

## The solution

MailEngine bases on RazorEngine to create email template. The template is actually cshtml file (can customize to store that cshtml content into database if required) and take full advantage of Razor to generate the email content:
- View Start
- View Import
- Layout
- web.config for view (namespace import)
- Inline code

The solution is simple redirecting the Razor output to a string variable instead of Response object, and the rest is using smtp client to send that mail message.

## Note
- Razor engine was built to served web request, hence for background process / multi-threading where there is no 'request', we need to create a 'mock' request