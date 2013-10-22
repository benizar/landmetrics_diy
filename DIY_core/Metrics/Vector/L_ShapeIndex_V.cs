
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
    /// This class calculates the shape index at the landscape level --(LSI_l)-- Units: none
    /// </summary>
    public class L_ShapeIndex_V
    {

        /// <summary>
        /// Calculates the shape index at the landscape level --(LSI_l)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        public L_ShapeIndex_V(string filename, string epsg)
        {
            this.lShapeIndex(filename, epsg);
        }

         double _maxLandscapeEdge = 0;
         double _minLandscapeEdge = 0;
         double _landscapePerimeter = 0;
         double _landscapeShapeIndex = 0;

       

        /// <summary>
        /// Gets the landscape shape index --(LSI_l)-- Units: none
        /// </summary>
         public double LandscapeShapeIndex
         {
             get { return _landscapeShapeIndex; }
         }

        


         private void lShapeIndex(string filename, string epsg)
         {
             try
             {

                 GeometryReader geometryCollection = new GeometryReader(filename, epsg);


                 for (int i = 0; i < geometryCollection.Areas.Count; i++)
                 {
                     _landscapePerimeter += (double)geometryCollection.Perimeters[i];
                 }

                 //Calculo del mayor perimetro en todo el paisaje
                 double maxEdgeTmp = 0;
                 for (int i = 0; i < geometryCollection.Perimeters.Count; i++)
                 {
                     if ((double)geometryCollection.Perimeters[i] > maxEdgeTmp)
                     {
                         maxEdgeTmp = (double)geometryCollection.Perimeters[i];
                     }

                 }
                 _maxLandscapeEdge = maxEdgeTmp;

                 

                 //Calculo del menor perimetro en todo el paisaje
                 double minEdgeTmp = _maxLandscapeEdge;
                 for (int i = 0; i < geometryCollection.Perimeters.Count; i++)
                 {
                     if ((double)geometryCollection.Perimeters[i] < minEdgeTmp)
                     {
                         minEdgeTmp = (double)geometryCollection.Perimeters[i];
                     }
                 }
                 _minLandscapeEdge = minEdgeTmp;


                 _landscapeShapeIndex = _landscapePerimeter / _minLandscapeEdge;

             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }


    }
}


