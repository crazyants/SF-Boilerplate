using System;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator edit action.
    /// </summary>
    public class JqGridNavigatorEditActionOptions : JqGridNavigatorModifyActionOptions, IJqGridNavigatorPageableFormActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the options for keyboard navigation.
        /// </summary>
        public JqGridFormKeyboardNavigation NavigationKeys { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the pager buttons should appear on the form.
        /// </summary>
        public bool ViewPagerButtons { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the form should be recreated every time the dialog is activeted with the new options from colModel (if they are changed).
        /// </summary>
        public bool RecreateForm { get; set; }

        /// <summary>
        /// Gets or sets value indicating where the row just added should be placed.
        /// </summary>
        public JqGridNewRowPositions AddedRowPosition { get; set; }

        /// <summary>
        /// Gets or sets the information which is placed just after the modal header as additional row.
        /// </summary>
        public string TopInfo { get; set; }

        /// <summary>
        /// Gets or sets the information which is placed just after the buttons of the form as additional row.
        /// </summary>
        public string BottomInfo { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if add action dialog should be cleared after submiting.
        /// </summary>
        public bool ClearAfterAdd { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if add action dialog should be closed after submiting.
        /// </summary>
        public bool CloseAfterAdd { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if edit action dialog should be closed after submiting.
        /// </summary>
        public bool CloseAfterEdit { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the user should confirm the changes uppon saving or cancel them.
        /// </summary>
        public bool CheckOnSubmit { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the user should be prompted when leaving unsaved changes.
        /// </summary>
        public bool CheckOnUpdate { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the form can be saved by pressing a key.
        /// </summary>
        public bool SaveKeyEnabled { get; set; }

        /// <summary>
        /// Gets or sets the key for saving.
        /// </summary>
        public char SaveKey { get; set; }

        /// <summary>
        /// Gets or sets the icon for the save button.
        /// </summary>
        public JqGridFormButtonIcon SaveButtonIcon { get; set; }

        /// <summary>
        /// Gets or sets the icon for the close button.
        /// </summary>
        public JqGridFormButtonIcon CloseButtonIcon { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the data for the new row is loaded from the grid (if navigator buttons are enabled in edit mode).
        /// </summary>
        public string AfterClickPgButtons { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised fires immediately after all actions and events are completed and the row is inserted or updated in the grid.
        /// </summary>
        public string AfterComplete { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before checking the values (if checking is defined in colModel via editrules option).
        /// </summary>
        public string BeforeCheckValues { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after the previous or next button is clicked, before leaving the current row (if navigator buttons are enabled in edit mode).
        /// </summary>
        public string OnClickPgButtons { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised only once when creating the form for editing and adding.
        /// </summary>
        public string OnInitializeForm { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when error occurs from the ajax call and can be used for better formatting of the error messages.
        /// </summary>
        public string ErrorTextFormat { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorEditActionOptions class.
        /// </summary>
        public JqGridNavigatorEditActionOptions()
        {
            Width = JqGridOptionsDefaults.Navigator.EditActionWidth;
            NavigationKeys = new JqGridFormKeyboardNavigation();
            ViewPagerButtons = JqGridOptionsDefaults.Navigator.ViewPagerButtons;
            RecreateForm = JqGridOptionsDefaults.Navigator.RecreateForm;
            AddedRowPosition = JqGridOptionsDefaults.Navigator.AddedRowPosition;
            ClearAfterAdd = JqGridOptionsDefaults.Navigator.ClearAfterAdd;
            CloseAfterAdd = JqGridOptionsDefaults.Navigator.CloseAfterAdd;
            CloseAfterEdit = JqGridOptionsDefaults.Navigator.CloseAfterEdit;
            CheckOnSubmit = JqGridOptionsDefaults.Navigator.CheckOnSubmit;
            CheckOnUpdate = JqGridOptionsDefaults.Navigator.CheckOnUpdate;
            TopInfo = String.Empty;
            BottomInfo = String.Empty;
            SaveKeyEnabled = JqGridOptionsDefaults.Navigator.SaveKeyEnabled;
            SaveKey = JqGridOptionsDefaults.Navigator.SaveKey;
            SaveButtonIcon = JqGridFormButtonIcon.SaveIcon;
            CloseButtonIcon = JqGridFormButtonIcon.CloseIcon;

            AfterClickPgButtons = null;
            AfterComplete = null;
            BeforeCheckValues = null;
            OnClickPgButtons = null;
            OnInitializeForm = null;
            ErrorTextFormat = null;
        }
        #endregion
    }
}
