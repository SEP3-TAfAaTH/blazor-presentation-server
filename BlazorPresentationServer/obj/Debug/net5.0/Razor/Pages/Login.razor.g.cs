#pragma checksum "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\Pages\Login.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8a4325224bae510e261978b9c2da1e1ce7d6284"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorPresentationServer.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using BlazorPresentationServer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\_Imports.razor"
using BlazorPresentationServer.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Login")]
    public partial class Login : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>Login</h3>\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "form-group");
            __builder.AddMarkupContent(3, "<label>User name:</label> ");
            __builder.OpenElement(4, "input");
            __builder.AddAttribute(5, "type", "text");
            __builder.AddAttribute(6, "placeholder", "user name");
            __builder.AddAttribute(7, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 5 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\Pages\Login.razor"
                                                                                      username

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(8, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => username = __value, username));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", "form-group");
            __builder.AddMarkupContent(12, "<label>Password</label> ");
            __builder.OpenElement(13, "input");
            __builder.AddAttribute(14, "type", "password");
            __builder.AddAttribute(15, "placeholder", "password");
            __builder.AddAttribute(16, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 8 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\Pages\Login.razor"
                                                                                       password

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => password = __value, password));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n\r\n");
            __builder.OpenElement(19, "div");
            __builder.AddAttribute(20, "style", "color:red");
            __builder.AddContent(21, 
#nullable restore
#line 11 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\Pages\Login.razor"
                        errorMessage

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddContent(22, " ");
            __builder.OpenElement(23, "a");
            __builder.AddAttribute(24, "href", "");
            __builder.AddAttribute(25, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 11 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\Pages\Login.razor"
                                                                PerformLogin

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(26, " Login ");
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n");
            __builder.OpenElement(28, "div");
            __builder.AddAttribute(29, "class", "form-group");
            __builder.AddMarkupContent(30, "<label>Is this your first time?</label> ");
            __builder.OpenElement(31, "a");
            __builder.AddAttribute(32, "href", "Register");
            __builder.AddAttribute(33, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 13 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\Pages\Login.razor"
                                                                         PerformLogin

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(34, " Register ");
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 15 "C:\Users\lucas\Documents\RiderProjects\SEP3\BlazorPresentationServer\BlazorPresentationServer\Pages\Login.razor"
       
    private string username;
    private string password;
    private string errorMessage;

    public async Task PerformLogin()
    {
        errorMessage = "";
        try
        {
            //((CustomAuthenticationStateProvider) AuthenticationStateProvider).ValidateLogin(username, password);
            username = "";
            password = "";
            //NavigationManager.NavigateTo("/");
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
        }
    }

    public async Task PerformLogout()
    {
        errorMessage = "";
        username = "";
        password = "";
        try
        {
            //((CustomAuthenticationStateProvider) AuthenticationStateProvider).Logout();
            //NavigationManager.NavigateTo("/");
        }
        catch (Exception e)
        {
        }
    }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
