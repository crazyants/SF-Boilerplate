using System;

namespace SF.Web.Control.JqGrid.Infrastructure.Enums
{
    /// <summary>
    /// Defines available operators for search fields for jqGrid
    /// </summary>
    [Flags]
    public enum JqGridSearchOperators
    {
        /// <summary>
        /// Equal
        /// </summary>
        Eq = 1,
        /// <summary>
        /// Not equal
        /// </summary>
        Ne = 2,
        /// <summary>
        /// Combines equal and not equal
        /// </summary>
        EqualOrNotEqual = JqGridSearchOperators.Eq | JqGridSearchOperators.Ne,
        /// <summary>
        /// Less
        /// </summary>
        Lt = 4,
        /// <summary>
        /// Less or equal
        /// </summary>
        Le = 8,
        /// <summary>
        /// Greater
        /// </summary>
        Gt = 16,
        /// <summary>
        /// Greater or equal
        /// </summary>
        Ge = 32,
        /// <summary>
        /// Begins with
        /// </summary>
        Bw = 64,
        /// <summary>
        /// Does not begin with
        /// </summary>
        Bn = 128,
        /// <summary>
        /// Is in
        /// </summary>
        In = 256,
        /// <summary>
        /// Is not in
        /// </summary>
        Ni = 512,
        /// <summary>
        /// Ends with
        /// </summary>
        Ew = 1024,
        /// <summary>
        /// Does not end with
        /// </summary>
        En = 2048,
        /// <summary>
        /// Contains
        /// </summary>
        Cn = 4096,
        /// <summary>
        /// Does not contain
        /// </summary>
        Nc = 8192,
        /// <summary>
        /// Is null
        /// </summary>
        Nu = 16384,
        /// <summary>
        /// Is not null
        /// </summary>
        Nn = 32768,
        /// <summary>
        /// Combines equal, not equal, less, less or equal, greater, greater or equal, is null, is not null.
        /// </summary>
        NoTextOperators = JqGridSearchOperators.Eq | JqGridSearchOperators.Ne | JqGridSearchOperators.Lt | JqGridSearchOperators.Le | JqGridSearchOperators.Gt | JqGridSearchOperators.Ge | JqGridSearchOperators.Nu | JqGridSearchOperators.Nn,
        /// <summary>
        /// Combines equal, not equal, begins with, does not begin with, ends with, does not end with, contains and does not contain, is null, is not null
        /// </summary>
        TextOperators = JqGridSearchOperators.Eq | JqGridSearchOperators.Ne | JqGridSearchOperators.Bw | JqGridSearchOperators.Bn | JqGridSearchOperators.Ew | JqGridSearchOperators.En | JqGridSearchOperators.Cn | JqGridSearchOperators.Nc | JqGridSearchOperators.Nu | JqGridSearchOperators.Nn,
        /// <summary>
        /// Combines is null, is not null
        /// </summary>
        NullOperators = JqGridSearchOperators.Nu | JqGridSearchOperators.Nn
    }
}
