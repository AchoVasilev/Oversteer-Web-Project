#pragma checksum "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Cars\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12bc47d736d0c098700664b3e9c71f7f31031422"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cars_Details), @"mvc.1.0.view", @"/Views/Cars/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Models.Home;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Models.Cars;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Models.Companies;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12bc47d736d0c098700664b3e9c71f7f31031422", @"/Views/Cars/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3be4b71c8e9a1c4d628e32372118262554fd4d7", @"/Views/_ViewImports.cshtml")]
    public class Views_Cars_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarDetailsFormModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""pricing-header px-3 py-3 pt-md-5 pb-md-4 mx-auto text-center"">
    <h1 class=""display-4"">Pricing</h1>
    <p class=""lead"">Quickly build an effective pricing table for your potential customers with this Bootstrap example. It's built with default Bootstrap components and utilities with little customization.</p>
</div>

<div class=""container"">
    <div class=""card-deck mb-3 text-center"">
        <div class=""card mb-4 box-shadow"">
            <div class=""card-header"">
                <h4 class=""my-0 font-weight-normal"">Daily Price</h4>
            </div>
            <div class=""card-body"">
                <h1 class=""card-title pricing-card-title"">$");
#nullable restore
#line 15 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Cars\Details.cshtml"
                                                      Write(Model.DailyPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" <small class=""text-muted"">/ mo</small></h1>
                <ul class=""list-unstyled mt-3 mb-4"">
                    <li class=""list-group-item d-flex justify-content-between lh-condensed"">
                        <div>
                            <h6 class=""my-0"">Product name</h6>
                            <small class=""text-muted"">Brief description</small>
                        </div>
                        <span class=""text-muted"">$12</span>
                    </li>
                    <li class=""list-group-item d-flex justify-content-between lh-condensed"">
                        <div>
                            <h6 class=""my-0"">Second product</h6>
                            <small class=""text-muted"">Brief description</small>
                        </div>
                        <span class=""text-muted"">$8</span>
                    </li>
                    <li class=""list-group-item d-flex justify-content-between lh-condensed"">
                        <div>
                  ");
            WriteLiteral(@"          <h6 class=""my-0"">Third item</h6>
                            <small class=""text-muted"">Brief description</small>
                        </div>
                        <span class=""text-muted"">$5</span>
                    </li>
                </ul>
                <button type=""button"" class=""btn btn-lg btn-block btn-outline-primary"">Sign up for free</button>
            </div>
        </div>
        <div class=""card mb-4 box-shadow"">
            <div class=""card-header"">
                <h4 class=""my-0 font-weight-normal"">Pro</h4>
            </div>
            <div class=""card-body"">
                <h1 class=""card-title pricing-card-title"">$15 <small class=""text-muted"">/ mo</small></h1>
                <ul class=""list-unstyled mt-3 mb-4"">
                    <li>20 users included</li>
                    <li>10 GB of storage</li>
                    <li>Priority email support</li>
                    <li>Help center access</li>
                </ul>
                <button ");
            WriteLiteral(@"type=""button"" class=""btn btn-lg btn-block btn-primary"">Get started</button>
            </div>
        </div>
        <div class=""card mb-4 box-shadow"">
            <div class=""card-header"">
                <h4 class=""my-0 font-weight-normal"">Enterprise</h4>
            </div>
            <div class=""card-body"">
                <h1 class=""card-title pricing-card-title"">$29 <small class=""text-muted"">/ mo</small></h1>
                <ul class=""list-unstyled mt-3 mb-4"">
                    <li>30 users included</li>
                    <li>15 GB of storage</li>
                    <li>Phone and email support</li>
                    <li>Help center access</li>
                </ul>
                <button type=""button"" class=""btn btn-lg btn-block btn-primary"">Contact us</button>
            </div>
        </div>
    </div>
</div>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarDetailsFormModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
