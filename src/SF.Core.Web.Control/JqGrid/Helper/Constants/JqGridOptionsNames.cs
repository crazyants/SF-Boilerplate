namespace SF.Web.Control.JqGrid.Helper.Constants
{
    internal static class JqGridOptionsNames
    {
        internal const string CAPTION = "caption";
        internal const string COLUMNS_NAMES_FIELD = "colNames";
        internal const string COLUMNS_MODEL_FIELD = "colModel";
        internal const string DATA_STRING = "datastr";
        internal const string DATA_TYPE = "datatype";
        internal const string DYNAMIC_SCROLLING_MODE = "scroll";
        internal const string DYNAMIC_SCROLLING_TIMEOUT = "scrollTimeout";
        internal const string EXPAND_COLUMN = "ExpandColumn";
        internal const string EXPAND_COLUMN_CLICK = "ExpandColClick";
        internal const string FOOTER_ENABLED = "footerrow";
        internal const string HEIGHT = "height";
        internal const string GROUPING_ENABLED = "grouping";
        internal const string GROUPING_VIEW = "groupingView";
        internal const string JSON_READER = "jsonReader";
        internal const string METHOD_TYPE = "mtype";
        internal const string PAGER = "pager";
        internal const string PARAMETERS_NAMES = "prmNames";
        internal const string ROWS_LIST = "rowList";
        internal const string ROWS_NUMBER = "rowNum";
        internal const string SORTING_NAME = "sortname";
        internal const string SORTING_ORDER = "sortorder";
        internal const string SUBGRID_ENABLED = "subGrid";
        internal const string SUBGRID_ULR = "subGridUrl";
        internal const string SUBGRID_WIDTH = "subGridWidth";
        internal const string SUBGRID_BEFORE_EXPAND = "subGridBeforeExpand";
        internal const string SUBGRID_ROW_EXPANDED = "subGridRowExpanded";
        internal const string SUBGRID_ROW_COLAPSED = "subGridRowColapsed";
        internal const string SUBGRID_MODEL = "subGridModel";
        internal const string TREE_GRID_ENABLED = "treeGrid";
        internal const string TREE_GRID_MODEL = "treeGridModel";
        internal const string URL = "url";
        internal const string USER_DATA_ON_FOOTER = "userDataOnFooter";
        internal const string VIEW_RECORDS = "viewrecords";

        internal static class ColumnModel
        {
            internal const string NAME_FIELD = "name";
            internal const string ALIGNMENT = "align";
            internal const string CELL_ATTRIBUTES = "cellattr";
            internal const string CLASSES = "classes";
            internal const string FIXED = "fixed";
            internal const string FROZEN = "frozen";
            internal const string HIDE_IN_DIALOG = "hidedlg";
            internal const string INDEX_FIELD = "index";
            internal const string INITIAL_SORTING_ORDER_FIELD = "firstsortorder";
            internal const string RESIZABLE = "resizable";
            internal const string SORTABLE_FIELD = "sortable";
            internal const string SORT_TYPE_FIELD = "sorttype";
            internal const string SUMMARY_TYPE = "summaryType";
            internal const string SUMMARY_TEMPLATE = "summaryTpl";
            internal const string TITLE = "title";
            internal const string WIDTH = "width";
            internal const string VIEWABLE = "viewable";
            internal const string FORMATTER_FIELD = "formatter";
            internal const string UNFORMATTER_FIELD = "unformat";
            internal const string FORMATTER_OPTIONS_FIELD = "formatoptions";

            internal static class Formatter
            {
                internal const string ADD_PARAM = "addParam";
                internal const string BASE_LINK_URL = "baseLinkUrl";
                internal const string DECIMAL_PLACES = "decimalPlaces";
                internal const string DECIMAL_SEPARATOR = "decimalSeparator";
                internal const string DEFAULT_VALUE = "defaultValue";
                internal const string DISABLED = "disabled";
                internal const string ID_NAME = "idName";
                internal const string OUTPUT_FORMAT = "newformat";
                internal const string PREFIX = "prefix";
                internal const string SHOW_ACTION = "showAction";
                internal const string SOURCE_FORMAT = "srcformat";
                internal const string SUFFIX = "suffix";
                internal const string TARGET = "target";
                internal const string THOUSANDS_SEPARATOR = "thousandsSeparator";
                internal const string EDIT_BUTTON = "editbutton";
                internal const string DELETE_BUTTON = "delbutton";
                internal const string USE_FORM_EDITING = "editformbutton";
                internal const string EDIT_OPTIONS = "editOptions";
                internal const string DELETE_OPTIONS = "delOptions";
                internal const string LABEL = "label";
                internal const string TEXT = "text";
                internal const string ICONS = "icons";
                internal const string PRIMARY = "primary";
                internal const string SECONDARY = "secondary";
            }
        };

        internal static class GroupingView
        {
            internal const string FIELDS = "groupField";
            internal const string ORDERS = "groupOrder";
            internal const string TEXTS = "groupText";
            internal const string SUMMARY = "groupSummary";
            internal const string COLUMN_SHOW = "groupColumnShow";
            internal const string IS_IN_THE_SAME_GROUP_CALLBACKS = "isInTheSameGroup";
            internal const string FORMAT_DISPLAY_FIELD_CALLBACKS = "formatDisplayField";
            internal const string SUMMARY_ON_HIDE = "showSummaryOnHide";
            internal const string DATA_SORTED = "groupDataSorted";
            internal const string COLLAPSE = "groupCollapse";
            internal const string PLUS_ICON = "plusicon";
            internal const string MINUS_ICON = "minusicon";
        }

        internal static class InlineNavigator
        {
            internal const string KEYS = "keys";
            internal const string ON_EDIT_FUNCTION = "onEdit";
            internal const string SUCCESS_FUNCTION = "onSuccess";
            internal const string URL = "url";
            internal const string AFTER_SAVE_FUNCTION = "afterSave";
            internal const string ERROR_FUNCTION = "onError";
            internal const string AFTER_RESTORE_FUNCTION = "afterRestore";
            internal const string RESTORE_AFTER_ERROR = "restoreAfterError";
            internal const string METHOD_TYPE = "mtype";
            internal const string EXTRA_PARAM = "extraparam";
        }

        internal static class JsonReader
        {
            internal const string PAGE_INDEX = "page";
            internal const string RECORDS = "root";
            internal const string RECORD_ID = "id";
            internal const string RECORD_VALUES = "cell";
            internal const string REPEAT_ITEMS = "repeatitems";
            internal const string SUBGRID = "subgrid";
            internal const string TOTAL_PAGES_COUNT = "total";
            internal const string TOTAL_RECORDS_COUNT = "records";
            internal const string USER_DATA = "userdata";
        }

        internal static class Navigator
        {
            internal const string ADDED_ROW_POSITION = "addedrow";
            internal const string AFTER_CLICK_PG_BUTTONS = "afterclickPgButtons";
            internal const string AFTER_COMPLETE = "afterComplete";
            internal const string AFTER_SHOW_FORM = "afterShowForm";
            internal const string AFTER_SUBMIT = "afterSubmit";
            internal const string AJAX_DELETE_OPTIONS = "ajaxDelOptions";
            internal const string AJAX_EDIT_OPTIONS = "ajaxEditOptions";
            internal const string BEFORE_CHECK_VALUES = "beforeCheckValues";
            internal const string BEFORE_SUBMIT = "beforeSubmit";
            internal const string BEFORE_INIT_DATA = "beforeInitData";
            internal const string BEFORE_SHOW_FORM = "beforeShowForm";
            internal const string BOTTOM_INFO = "bottominfo";
            internal const string CANCEL_ICON = "cancelicon";
            internal const string CHECK_ON_SUBMIT = "checkOnSubmit";
            internal const string CHECK_ON_UPDATE = "checkOnUpdate";
            internal const string CLEAR_AFTER_ADD = "clearAfterAdd";
            internal const string CLOSE_AFTER_ADD = "closeAfterAdd";
            internal const string CLOSE_AFTER_EDIT = "closeAfterEdit";
            internal const string CLOSE_ICON = "closeicon";
            internal const string CLOSE_ON_ESCAPE = "closeOnEscape";
            internal const string DATA_HEIGHT = "dataheight";
            internal const string DATA_WIDTH = "datawidth";
            internal const string DELETE_ICON = "delicon";
            internal const string DELETE_EXTRA_DATA = "delData";
            internal const string EDIT_EXTRA_DATA = "editData";
            internal const string ERROR_TEXT_FORMAT = "errorTextFormat";
            internal const string DRAGABLE = "drag";
            internal const string HEIGHT = "height";
            internal const string LEFT = "left";
            internal const string MODAL = "modal";
            internal const string NAVIGATION_KEYS = "navkeys";
            internal const string ON_CLICK_PG_BUTTONS = "onclickPgButtons";
            internal const string ON_CLICK_SUBMIT = "onclickSubmit";
            internal const string ON_CLOSE = "onClose";
            internal const string ON_INITIALIZE_FORM = "onInitializeForm";
            internal const string OVERLAY = "overlay";
            internal const string RECREATE_FORM = "recreateForm";
            internal const string RELOAD_AFTER_SUBMIT = "reloadAfterSubmit";
            internal const string RESIZE = "resize";
            internal const string SAVE_ICON = "saveicon";
            internal const string SAVE_KEY = "savekey";
            internal const string SERIALIZE_DELETE_DATA = "serializeDelData";
            internal const string SERIALIZE_EDIT_DATA = "serializeEditData";
            internal const string TOP = "top";
            internal const string TOP_INFO = "topinfo";
            internal const string USE_JQ_MODAL = "jqModal";
            internal const string URL = "url";
            internal const string METHOD_TYPE = "mtype";
            internal const string WIDTH = "width";
            internal const string VIEW_PAGER_BUTTONS = "viewPagerButtons";
        }

        internal static class ParametersNames
        {
            internal const string PAGE_INDEX = "page";
            internal const string RECORDS_COUNT = "rows";
            internal const string SORTING_NAME = "sort";
            internal const string SORTING_ORDER = "order";
            internal const string SEARCHING = "search";
            internal const string ID = "id";
            internal const string OPERATOR = "oper";
            internal const string EDIT_OPERATOR = "editoper";
            internal const string ADD_OPERATOR = "addoper";
            internal const string DELETE_OPERATOR = "deloper";
            internal const string SUBGRID_ID = "subgridid";
            internal const string PAGES_COUNT = "npage";
            internal const string TOTAL_ROWS = "totalrows";
        }

        internal static class SubgridModel
        {
            internal const string NAMES = "name";
            internal const string WIDTHS = "width";
            internal const string ALIGNMENTS = "align";
            internal const string MAPPINGS = "mapping";
            internal const string PARAMETERS = "params";
        }
    }
}
