#pragma checksum "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d3ee04e196b9aa40d31d86ca36cf55f5f2c35d3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
using Oversteer.Web.Models.Companies;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\_ViewImports.cshtml"
using Oversteer.Web.Models.Countries;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d3ee04e196b9aa40d31d86ca36cf55f5f2c35d3", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"78dd4dc4d292c9c6a92bd9a5ae7169bff9d27f89", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IndexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Cars", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-lg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "All", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary py-2 ml-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("trip-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Bulgaria's car renting platform";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
 if (!Model.Cars.Any())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"jumbotron\">\r\n        <h1 class=\"display-4\">Welcome to Oversteer!</h1>\r\n        <p class=\"lead\">There are no available cars at the moment, so why don\'t you add one?\'</p>\r\n        <hr class=\"my-4\">\r\n        <p class=\"lead\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d3ee04e196b9aa40d31d86ca36cf55f5f2c35d38255", async() => {
                WriteLiteral("Add Car");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </p>\r\n    </div>\r\n");
#nullable restore
#line 16 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"mb-4\"></div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d3ee04e196b9aa40d31d86ca36cf55f5f2c35d310067", async() => {
                WriteLiteral("\r\n    <div class=\"row align-items-center mb-4\">\r\n        <div class=\"col-md-6\">\r\n            <h3 class=\"m-0\">Begin your trip here</h3>\r\n        </div>\r\n        <div class=\"col-md-6 text-md-right\">\r\n");
#nullable restore
#line 26 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
             if (Model.TotalCars == 0)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d3ee04e196b9aa40d31d86ca36cf55f5f2c35d310828", async() => {
#nullable restore
#line 28 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                                                                          Write(Model.TotalCars);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" <span>cars available</span>\r\n");
#nullable restore
#line 29 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d3ee04e196b9aa40d31d86ca36cf55f5f2c35d312894", async() => {
#nullable restore
#line 32 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                                                                          Write(Model.TotalCars);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" <span>cars available</span>\r\n");
#nullable restore
#line 33 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
        </div>
    </div>
    <div class=""container-fluid"">
        <div class=""row"">
            <div class=""col-lg"">
                <div class=""form-group col-md-8"">
                    <label for=""cf-1"">Where you from</label>
                    <input type=""text"" id=""cf-1"" placeholder=""Your pickup address"" class=""form-control"">
                </div>
                <div class=""form-group col-md-8"">
                    <label for=""cf-2"">Where you go</label>
                    <input type=""text"" id=""cf-2"" placeholder=""Your drop-off address"" class=""form-control"">
                </div>
                <div class=""form-group col-md-8"">
                    <label for=""cf-3"">Journey date</label>
                    <input type=""text"" id=""datepicker"" placeholder=""Your pickup time"" class=""form-control datepicker px-3"">
                </div>
                <div class=""form-group col-md-8"">
                    <label for=""cf-4"">Return date</label>
                    <input type=""text"" id=");
                WriteLiteral(@"""datepicker-return"" placeholder=""Your drop-off time"" class=""form-control datepicker px-3"">
                </div>
                <div class=""row"">
                    <div class=""col-lg-6"">
                        <input type=""submit"" value=""Submit"" class=""btn btn-primary"">
                    </div>
                </div>
            </div>

            <div class=""col"">
                <div class=""carousel-inner"">
                    <div id=""carouselExampleControls"" class=""carousel slide"" data-ride=""carousel"">
");
#nullable restore
#line 66 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                         for (int i = 0; i < Model.Cars.Count; i++)
                        {
                            var car = Model.Cars[i];

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <div");
                BeginWriteAttribute("class", " class=\"", 2866, "\"", 2923, 2);
                WriteAttributeValue("", 2874, "carousel-item", 2874, 13, true);
#nullable restore
#line 69 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
WriteAttributeValue(" ", 2887, i == 0 ? "active" : string.Empty, 2888, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\" width:130%; height: 550px !important;\" id=\"corousel-id\">\r\n                                <img class=\"d-block w-75\"");
                BeginWriteAttribute("src", " src=\"", 3048, "\"", 3062, 1);
#nullable restore
#line 70 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
WriteAttributeValue("", 3054, car.Url, 3054, 8, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("alt", " alt=\"", 3063, "\"", 3098, 2);
#nullable restore
#line 70 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
WriteAttributeValue("", 3069, car.BrandName, 3069, 14, false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
WriteAttributeValue(" ", 3083, car.ModelName, 3084, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                                <div class=\"text\">\r\n                                    <h2 class=\"mb-0\">");
#nullable restore
#line 72 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                                                Write(car.ModelName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\r\n                                    <div class=\"d-flex mb-3\">\r\n                                        <span class=\"cat\">");
#nullable restore
#line 74 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                                                     Write(car.BrandName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n                                        <p class=\"price ml-auto\">$");
#nullable restore
#line 75 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                                                             Write(car.DailyPrice);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span>/day</span></p>\r\n                                    </div>\r\n                                    <p class=\"d-flex mb-0 d-block\"><a href=\"#\" class=\"btn btn-primary py-2 mr-1\">Book now</a> ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d3ee04e196b9aa40d31d86ca36cf55f5f2c35d320024", async() => {
                    WriteLiteral("Details");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 77 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                                                                                                                                                                                                                  WriteLiteral(car.Id);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("</p>\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 80 "D:\Курс програмиране\CSharp - ASP.NET Core\Oversteer\Oversteer\Oversteer.Web\Views\Home\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                    </div>
                    <a class=""carousel-control-prev"" href=""#carouselExampleControls"" role=""button"" data-slide=""prev"">
                        <span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>
                        <span class=""sr-only"">Previous</span>
                    </a>
                    <a class=""carousel-control-next"" href=""#carouselExampleControls"" role=""button"" data-slide=""next"">
                        <span class=""carousel-control-next-icon"" aria-hidden=""true""></span>
                        <span class=""sr-only"">Next</span>
                    </a>
                </div>
            </div>
        </div>
    </div>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<div class=""mb-4""></div>

<div class=""row"">
    <div class=""col-12 text-center"">
        <button class=""btn btn-primary"" id=""statistics-button"">Show Statistics</button>
    </div>
</div>

<div class=""jumbotron jumbotron-fluid d-none"" id=""statistics"">
    <div class=""row"">
        <h2 class=""col-md-3 text-center"" id=""total-cars""></h2>
        <h2 class=""col-md-3 text-center"" id=""total-users""></h2>
        <h2 class=""col-md-3 text-center"" id=""total-companies""></h2>
        <h2 class=""col-md-3 text-center"" id=""total-rents""></h2>
    </div>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"" src=""https://cdn.jsdelivr.net/jquery/latest/jquery.min.js""></script>
    <script type=""text/javascript"" src=""https://cdn.jsdelivr.net/momentjs/latest/moment.min.js""></script>
    <script type=""text/javascript"" src=""https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js""></script>

    <script>
        $('#statistics-button').on('click', e => {
            $.get('/api/statistics', (data) => {
                $('#total-cars').text(data.totalCars + ' Cars');
                $('#total-users').text(data.totalUsers + ' Users');
                $('#total-companies').text(data.totalCompanies + ' Companies');
                $('#total-rents').text(data.totalRents + ' Rents');

                $('#statistics').removeClass('d-none');
                $('#statistics-button').hide();
            });
        });
    </script>

    <script>
        $('#datepicker').daterangepicker(
            {
                ""singleDatePicker"": true,
                """);
                WriteLiteral(@"timePicker"": true,
                ""startDate"": moment().startOf('hour'),
                ""endDate"": moment().startOf('hour').add(32, 'hour'),
                ""opens"": ""center"",
                ""timePicker24Hour"": true,
                ""autoApply"": true,
                ""locale"": {
                    format: 'DD/MM/YYYY HH:mm'
                }
            });
    </script>

    <script>
        $('#datepicker-return').daterangepicker(
            {
                ""singleDatePicker"": true,
                ""timePicker"": true,
                ""startDate"": moment().startOf('hour'),
                ""endDate"": moment().startOf('hour').add(32, 'hour'),
                ""opens"": ""center"",
                ""timePicker24Hour"": true,
                ""locale"": {
                    format: 'DD/MM/YYYY HH:mm'
                }
            });
    </script>

    <script>
        $('#corousel-id').carousel({
            interval: 1000
        })
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
