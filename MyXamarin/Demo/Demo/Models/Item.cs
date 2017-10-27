#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 27/10/2017 08:18
 * Update       : 27/10/2017 08:18
 * Checklist    : 1.0
 * Status       : OK
 */
#region License
/**************************************************************************************************************
 * Copyright © 2012-2017 SKG™ all rights reserved
 **************************************************************************************************************/
#endregion
#region Description
/**************************************************************************************************************
 * Item for test
 **************************************************************************************************************/
#endregion
#endregion

using System;

namespace Demo.Models
{
    /// <summary>
    /// Item for test
    /// </summary>
    public class Item
    {
        #region -- Properties --

        public string Id { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public DateTime? LogIn { get; set; }

        public int Order { get; set; }

        #endregion
    }
}