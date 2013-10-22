
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
using System.Collections;
using landmetrics_DIY_API.DIY_core.SchemaDB;
using landmetrics_DIY_API.DIY_core.Geometry;


namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the num of patches at the landscape level --(NP_l)-- Units: none
    /// </summary>
    public class L_NumLandscapePatches_V
    {

        /// <summary>
        /// Calculates the num of patches at the landscape level --(NP_l)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        public L_NumLandscapePatches_V(string filename, string epsg)
        {
            this.lNumLandscapePatches(filename, epsg);
        }


         int _numLandscapePatches = 0;


       
        /// <summary>
        /// Gets the number of landscape patches --(NP_l)-- Units: none
        /// </summary>
         public int NumLandscapePatches
         {
             get { return _numLandscapePatches; }
         }



         private void lNumLandscapePatches(string filename, string epsg)
         {
             try
             {

                 GeometryReader geometryCollection = new GeometryReader(filename, epsg);

                 _numLandscapePatches = (int)geometryCollection.Areas.Count;

             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }



    }
}


