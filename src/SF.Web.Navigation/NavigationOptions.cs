
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Navigation
{
    public class NavigationOptions
    {
        public NavigationOptions()
        { }

        public string RootTreeBuilderName { get; set; } = "SF.Web.Navigation.XmlNavigationTreeBuilder";

        public string NavigationMapJsonFileName { get; set; } = "config/navigation.json";
        public string NavigationMapXmlFileName { get; set; } = "config/navigation.xml";
    }
}
