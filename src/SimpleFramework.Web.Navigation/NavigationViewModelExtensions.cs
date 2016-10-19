
namespace SimpleFramework.Web.Navigation
{
    public static class NavigationViewModelExtensions
    {
        public static string GetClass(this NavigationViewModel model, NavigationNode node, string inputClass = null, string activeClass = "active")
        {
            if (node == null) return null;
            if (model.CurrentNode != null && (node.EqualsNode(model.CurrentNode.Value)))
            {
                if (!string.IsNullOrEmpty(inputClass))
                {
                    inputClass = activeClass + " " + inputClass;
                }
                else
                {
                    inputClass = activeClass;
                }
            }
            if (string.IsNullOrEmpty(node.CssClass))
            {
                return inputClass;
            }
            else
            {
                if (!string.IsNullOrEmpty(inputClass))
                {
                    return inputClass + " " + node.CssClass;
                }
                return node.CssClass;
            }
        }

        public static string GetIcon(this NavigationViewModel model, NavigationNode node)
        {
            if (node == null) return string.Empty;
            if (string.IsNullOrEmpty(node.IconCssClass))
            {
                return string.Empty;

            }
            return "<i class='" + node.IconCssClass + "'></i> ";
        }

    }
}
