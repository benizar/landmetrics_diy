
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
    /// This class calculates the patch density at the landscape level --(PD_l)-- Units: number per 100 ha
    /// </summary>
    public class L_PatchesDensity_V
    {

        /// <summary>
        /// Calculates the patch density at the landscape level --(PD_l)-- Units: number per 100 ha
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        public L_PatchesDensity_V(string filename, string epsg)
        {
            this.lPatchesDensity(filename, epsg);
        }

        
         double _landscapeArea = 0;
         int _numLandscapePatches = 0;
         double _landscapePatchesDensity = 0;

        

        /// <summary>
        /// Gets the landscape patches density --(PD_l)-- Units: number per 100 ha
        /// </summary>
         public double LandscapePatchesDensity
         {
             get { return _landscapePatchesDensity; }
         }



         private void lPatchesDensity(string filename, string epsg)
         {
             try
             {
                 
                 L_Area_V LArea = new L_Area_V(filename, epsg);
                 _landscapeArea = LArea.LandscapeArea;


                 L_NumLandscapePatches_V NLP = new L_NumLandscapePatches_V(filename, epsg);
                 _numLandscapePatches = NLP.NumLandscapePatches;


                 _landscapePatchesDensity = ((_numLandscapePatches / _landscapeArea)*10000)*100;


             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }


    }
}


