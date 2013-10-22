
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

namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the Simpson's Evenness Index at the landscape level --(SIEI_l)-- Units: none
    /// </summary>
    public class L_SimpsonsEvennessIndex_V
    {

        /// <summary>
        /// Calculates the Simpson's Evenness Index at the landscape level --(SIEI_l)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        public L_SimpsonsEvennessIndex_V(string filename, string epsg, int fieldIndex)
        {
            this.lSimpsonsEvennessIndex(filename, epsg, fieldIndex);
        }



        double _simpsonsEvennessIndex = 0;




        /// <summary>
        /// Gets the landscape Simpson's evenness index --(SIEI_l)-- Units: none
        /// </summary>
        public double SimpsonsEvennessIndex
        {
            get { return _simpsonsEvennessIndex; }
        }




        private void lSimpsonsEvennessIndex(string filename, string epsg, int fieldIndex)
        {
            try 
            {


                L_SimpsonsDiversityIndex_V SIDI = new L_SimpsonsDiversityIndex_V(filename, epsg, fieldIndex);

                L_NumCategories_V PR = new L_NumCategories_V(filename, epsg, fieldIndex);


                _simpsonsEvennessIndex = SIDI.SimpsonsDiversityIndex / (1 - (1 / (double)PR.NumCategories));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }



    }
}


