
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
    /// This class calculates the disjunct core area density at the landscape level --(DCAD_l)-- Units: number per 100 ha
    /// </summary>
    public class L_DisjunctCoreAreaDensity_V
    {

        /// <summary>
        /// Calculates the disjunct core area density at the landscape level --(DCAD_l)-- Units: number per 100 ha
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="depthOfEdge">Depth of edge applied as a negative buffer. Must be negative.</param>
        public L_DisjunctCoreAreaDensity_V(string filename, string epsg, double depthOfEdge)
        {
            this.lDisjunctCoreAreaDensity(filename, epsg, depthOfEdge);
        }


        int numberOfDisjunctCoreAreas = 0;
        double disjunctCoreAreaDensity = 0;



        /// <summary>
        /// Gets the landscape disjunct corea area density --(DCAD_l)-- Units: number per 100 ha
        /// </summary>
        public double DisjunctCoreAreaDensity
        {
            get { return disjunctCoreAreaDensity; }
        }


        private void lDisjunctCoreAreaDensity(string filename, string epsg, double depthOfEdge)
        {
            try
            {
                
                L_Area_V totalArea = new L_Area_V(filename, epsg);

                GeometryCoreReader metrics = new GeometryCoreReader(filename, epsg, depthOfEdge);


                for (int i = 0; i < metrics.CoreAreas.Count; i++)
                {
                    numberOfDisjunctCoreAreas += (int)metrics.NumCoreAreas[i];  
                }

                //total landscape area is already in hectares
                disjunctCoreAreaDensity = ((numberOfDisjunctCoreAreas / (double)totalArea.LandscapeArea))*100;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


