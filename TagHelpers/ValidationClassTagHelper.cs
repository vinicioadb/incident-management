using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace v1.TagHelpers
{
    [HtmlTargetElement("input", Attributes = "asp-for")]
    public class ValidationClassTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var existingClassAttribute = output.Attributes.FirstOrDefault(a => a.Name == "class");
            var existingClasses = existingClassAttribute?.Value?.ToString() ?? "";

            if (existingClassAttribute != null && existingClassAttribute.Value is Microsoft.AspNetCore.Html.IHtmlContent htmlContent)
            {
                using (var writer = new StringWriter())
                {
                    htmlContent.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                    existingClasses = writer.ToString();
                }
            }

            if (ViewContext.ModelState.TryGetValue(For.Name, out var entry) && entry.Errors.Count > 0)
            {
                /*
                var newClasses = (entry.Errors.Count > 0) 
                    ? $"{existingClasses} is-invalid".Trim() : $"{existingClasses} is-valid".Trim();
                
                output.Attributes.SetAttribute("class", newClasses);
                */

                var newClasses = $"{existingClasses} is-invalid".Trim();
                output.Attributes.SetAttribute("class", newClasses);
            }
        }
    }
}