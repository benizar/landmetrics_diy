
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
using landmetrics_DIY_API.DIY_core.Geometry;


namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the edge density at the landscape level --(ED_l)-- Units: meters per hectare
    /// </summary>
    public class L_EdgeDensity_V
    {

        /// <summary>
        /// Calculates Edge density at the landscape level --(ED_l)-- Units: meters per hectare
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        public L_EdgeDensity_V(string filename, string epsg)
        {
            this.lEdgeDensity(filename, epsg);
        }


            double _landscapeArea = 0;
            double _landscapePerimeter = 0;
            double _landscapeEdgeDensity = 0;



            /// <summary>
            /// Gets the landscape Edge density --(ED_l)-- Units: meters per hectare
            /// </summary>
            public double LandscapeEdgeDensity
            {
                get { return _landscapeEdgeDensity; }
            }


            private void lEdgeDensity(string filename, string epsg)
            {
                try
                {

                    GeometryReader geometryCollection = new GeometryReader(filename, epsg);


                    for (int i = 0; i < geometryCollection.Areas.Count; i++)
                    {
                        _landscapeArea += (double)geometryCollection.Areas[i];
                        _landscapePerimeter += (double)geometryCollection.Perimeters[i];
                    }

                    _landscapeArea = _landscapeArea / 10000;
                    _landscapeEdgeDensity = (_landscapePerimeter / _landscapeArea) * 10000;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }
    }


