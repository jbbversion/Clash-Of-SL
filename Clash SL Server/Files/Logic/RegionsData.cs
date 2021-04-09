﻿/*
 * Program : Clash Of SL Server
 * Description : A C# Writted 'Clash of SL' Server Emulator !
 *
 * Authors:  Sky Tharusha <Founder at Sky Production>,
 *           And the Official DARK Developement Team
 *
 * Copyright (c) 2021  Sky Production
 * All Rights Reserved.
 */

using CSS.Files.CSV;

namespace CSS.Files.Logic
{
    internal class RegionsData : Data
    {
        #region Public Constructors

        public RegionsData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string DisplayName { get; set; }
        public bool IsCountry { get; set; }
        public string Name { get; set; }
        public string TID { get; set; }

        #endregion Public Properties
    }
}