namespace SimpleFramework.Web.BreadCrumb
{
    /// <summary>
    /// Represents the current BreadCrumb
    /// </summary>
    public class BreadCrumb
    {
        /// <summary>
        /// A constant URL of the current item
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// Title of the current item
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// An optional glyph icon of the current item
        /// </summary>
        public string GlyphIcon { set; get; }

        /// <summary>
        /// Oder of the current item in the final list
        /// </summary>
        public int Order { set; get; }
    }
}