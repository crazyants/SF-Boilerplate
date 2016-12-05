
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace SF.Web.Navigation
{
    [HtmlTargetElement(Attributes = MatchingAttributeName)]
    public class DataAttributesTagHelper : TagHelper
    {
        private const string MatchingAttributeName = "cwn-data-attributes";

        [HtmlAttributeName(MatchingAttributeName)]
        public List<DataAttribute> DataAttributes { get; set; } = null;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(MatchingAttributeName);

            if ((DataAttributes != null)&&(DataAttributes.Count > 0))
            {
                foreach(var att in DataAttributes)
                {
                    output.Attributes.Add(att.Attribute, att.Value);
                }
            }

            
            
        }
    }
}
