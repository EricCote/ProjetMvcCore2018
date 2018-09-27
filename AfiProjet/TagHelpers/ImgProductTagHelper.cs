using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfiProjet.TagHelpers
{
    public class ImgProductTagHelper: TagHelper
    {
        public int Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.Add(
                     "src", $"/Product/{Id}.gif");
            
        }
    }
}





