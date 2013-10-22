
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
    /// This class calculates the Core Area at the landscape level --(TCA_l)-- Units: hectares
    /// </summary>
    public class L_TotalCoreArea_V
    {

        /// <summary>
        /// Calculates the Core Area at the landscape level --(TCA_l)-- Units: hectares
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="depthOfEdge">Depth of edge applied as a negative buffer. Must be negative.</param>
        public L_TotalCoreArea_V(string filename, string epsg, double depthOfEdge)
        {
            this.lTotalCoreArea(filename, epsg, depthOfEdge);
        }


        double totalCoreArea = 0;


        /// <summary>
        /// Gets the landscape total core area --(TCA_l)-- Units: hectares
        /// </summary>
        public double TotalCoreArea
        {
            get { return totalCoreArea; }
        }

        
        private void lTotalCoreArea(string filename, string epsg,double depthOfEdge)
        {
            try
            {

                GeometryCoreReader metrics = new GeometryCoreReader(filename, epsg, depthOfEdge);


                for (int i = 0; i < metrics.CoreAreas.Count; i++)
                {
                    totalCoreArea += (double)metrics.CoreAreas[i]; 
                }

                //total core area to hectares
                totalCoreArea = totalCoreArea / 10000;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


