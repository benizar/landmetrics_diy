
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
using landmetrics_DIY_API.DIY_core.Geometry;

namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the available CoreAreaMetrics at the landscape level
    /// </summary>
    public class Landscape_CoreAreaMetrics_V
    {

        /// <summary>
        /// Calculates CoreAreaMetrics at the landscape level
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="depthOfEdge">Depth of edge applied as a negative buffer. Must be negative.</param>
        public Landscape_CoreAreaMetrics_V(string filename, string epsg, double depthOfEdge)
        {
            this.landscapeMetrics(filename, epsg, depthOfEdge);
        }


        double totalCoreArea = 0;
        int numberOfDisjunctCoreAreas = 0;
        double disjunctCoreAreaDensity = 0;


        /// <summary>
        /// Gets the landscape total core area --(TCA_l)-- Units: hectares
        /// </summary>
        public double TotalCoreArea
        {
            get { return totalCoreArea; }
        }

        /// <summary>
        /// Gets the landscape total number of disjunct core areas --(NDCA_l)-- Units: none
        /// </summary>
        public int NumberOfDisjunctCoreAreas
        {
            get { return numberOfDisjunctCoreAreas; }
        }

        /// <summary>
        /// Gets the landscape disjunct corea area density --(DCAD_l)-- Units: number per 100 ha
        /// </summary>
        public double DisjunctCoreAreaDensity
        {
            get { return disjunctCoreAreaDensity; }
        }


        private void landscapeMetrics(string filename, string epsg,double depthOfEdge)
        {
            try
            {

                L_Area_V totalArea = new L_Area_V(filename, epsg);

                GeometryCoreReader metrics = new GeometryCoreReader(filename, epsg, depthOfEdge);


                for (int i = 0; i < metrics.CoreAreas.Count; i++)
                {
                    totalCoreArea += (double)metrics.CoreAreas[i];
                    numberOfDisjunctCoreAreas += (int)metrics.NumCoreAreas[i];  
                }

                //total core area to hectares
                totalCoreArea = totalCoreArea / 10000;

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


