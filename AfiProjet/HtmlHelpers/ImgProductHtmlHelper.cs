using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfiProjet.HtmlHelpers
{
    public static class ImgProductHtmlHelper
    {
        public static IHtmlContent ImgProduct(this IHtmlHelper helper, int id, object htmlAttributes=null)
        {
            // Create tag builder
            var builder = new TagBuilder("img");


            // Add attributes
            builder.MergeAttribute("src", $"/product/{id}.gif");
            builder.MergeAttribute("alt", $"produit no {id}");
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return  builder.RenderSelfClosingTag();
        }
    }
}
