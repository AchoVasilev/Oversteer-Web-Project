#pragma checksum "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Cars\Add.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cb702d04ab692c8e7defd3a4ca9824d3495a62eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cars_Add), @"mvc.1.0.view", @"/Views/Cars/Add.cshtml")]
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
using Oversteer.Web.Models.Rents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Areas.Company.Models.Companies;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Models.Countries;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Services.Countries;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Areas.Company.Models.Locations;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Areas.Company.Services.Companies;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cb702d04ab692c8e7defd3a4ca9824d3495a62eb", @"/Views/Cars/Add.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9fee75a7d88f09060e3e16578c8e8aed23aa34c", @"/Views/_ViewImports.cshtml")]
    public class Views_Cars_Add : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarFormModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_CarFormPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Cars\Add.cshtml"
  
    ViewBag.Title = "Add Car";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cb702d04ab692c8e7defd3a4ca9824d3495a62eb6031", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 7 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Cars\Add.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<div class=\"col-sm-12 offset-lg-2 col-lg-8 offset-xl-3 col-xl-6\">\r\n    <input class=\"btn btn-primary\" type=\"submit\" onclick=\"carForm.submit()\" value=\"Save\" />\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cb702d04ab692c8e7defd3a4ca9824d3495a62eb7898", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

    <script src=""https://code.jquery.com/jquery-3.6.0.js""
            integrity=""sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk=""
            crossorigin=""anonymous""></script>
    <script src=""https://code.jquery.com/jquery-migrate-3.3.2.js""
            integrity=""sha256-BDmtN+79VRrkfamzD16UnAoJP8zMitAz093tvZATdiE=""
            crossorigin=""anonymous""></script>

    <script type=""text/javascript"">
        $(document).ready(function () {
            var allOptions = $('#selectprod option')
            $('#selectcat').change(function () {
                $('#selectprod option').remove()
                var classN = $('#selectcat option:selected').prop('class');
                var opts = allOptions.filter('.' + classN);
                $.each(opts, function (i, j) {
                    $(j).appendTo('#selectprod');
                });
            });
        });
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarFormModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
