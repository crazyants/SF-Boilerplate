
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Web.Navigation
{
    public class NavigationOptions
    {
        public NavigationOptions()
        { }

        public string RootTreeBuilderName { get; set; } = "SimpleFramework.Web.Navigation.XmlNavigationTreeBuilder";

        public string NavigationMapJsonFileName { get; set; } = "navigation.json";
        public string NavigationMapXmlFileName { get; set; } = "navigation.xml";
    }
}
