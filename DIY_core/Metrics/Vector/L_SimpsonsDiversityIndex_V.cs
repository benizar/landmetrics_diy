
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
    /// This class calculates the Simpson's diversity index at the landscape level --(SIDI_l)-- Units: none
    /// </summary>
    public class L_SimpsonsDiversityIndex_V
    {

        /// <summary>
        /// Calculates the Simpson's diversity index at the landscape level --(SIDI_l)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        public L_SimpsonsDiversityIndex_V(string filename, string epsg, int fieldIndex)
        {
            this.lSimpsonsDiversityIndex(filename, epsg, fieldIndex);
        }



        double _simpsonsDiversityIndex = 0;

       

        /// <summary>
        /// Gets the landscape Simpson's diversity index --(SIDI_l)-- Units: none
        /// </summary>
        public double SimpsonsDiversityIndex
        {
            get { return _simpsonsDiversityIndex; }
        }




        private void lSimpsonsDiversityIndex(string filename, string epsg, int fieldIndex)
        {
            try 
            {

                C_LandscapePercent_V diversity = new C_LandscapePercent_V(filename, epsg, fieldIndex);
                
                double _simpsonsDiversityIndexTemp=0;

                for (int i = 0; i< diversity.CategoryNames.Count;i++)
                {
                    _simpsonsDiversityIndexTemp += Math.Pow(((double)diversity.LandscapePercent[i]/100), 2);

                }
                _simpsonsDiversityIndex = 1 - _simpsonsDiversityIndexTemp;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


    }
}


