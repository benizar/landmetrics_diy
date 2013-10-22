﻿
//Landmetrics_DIY_API. Class library for landscape metrics calculations.
//Copyright (C) 2009 Benito M. Zaragozí
//Authors: Benito M. Zaragozí (www.gisandchips.org)
//Send comments and suggestions to benito.zaragozi@ua.es

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Text;

namespace landmetrics_DIY_API.DIY_enums.VectorMetrics
{
    /// <summary>
    /// Enumerates the available DiversityMetrics at the landscape level
    /// </summary>
    [Flags]
    public enum Options_Landscape_DiversityMetrics_V
    {
        NumLandscapeCategories=1,
        PatchRichnessDensity=2,
        ShannonsDiversityIndex=4,
        SimpsonsDiversityIndex=8,
        ModifiedSimpsonsDiversityIndex=16,
        ShanonsEvennessIndex=32,
        SimpsonsEvennessIndex=64,
        ModifiedSimpsonsEvennessIndex=128,
        All=256
    }


}


