#pragma checksum "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/Shared/Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1c4fb8473cbf0b00e9e45ce85008d3e5fd53b3ff"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
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
#line 1 "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/_ViewImports.cshtml"
using NSE.WebApp.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/_ViewImports.cshtml"
using NSE.WebApp.MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1c4fb8473cbf0b00e9e45ce85008d3e5fd53b3ff", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5c110d505067105aca06c45cf5feffa4d133c34e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NSE.WebApp.MVC.Models.ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/Shared/Error.cshtml"
  
    ViewData["Title"] = "Ocorreu um erro";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/Shared/Error.cshtml"
  
    if (Model == null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        <h2>Ooops! Ocorreu um erro, mas n??o se preocupe. Nosso time ser?? avisado e iremos corrigir em breve!</h2>\r\n    </div>\r\n");
#nullable restore
#line 12 "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/Shared/Error.cshtml"
    }
    else
    {
    ViewData["Title"] = Model.Titulo;


#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        <h2>");
#nullable restore
#line 18 "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/Shared/Error.cshtml"
       Write(Html.Raw(Model.Mensagem));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    </div>\r\n");
#nullable restore
#line 20 "/home/fabio/Downloads/Estudo.Net/NerdStoreEnterprise/src/web/NSE.WebApp.MVC/Views/Shared/Error.cshtml"

    }

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NSE.WebApp.MVC.Models.ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
