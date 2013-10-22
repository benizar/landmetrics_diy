
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
    /// This class calculates the available AreaDensEdge metrics at the landscape level
    /// </summary>
    public class Landscape_AreaDensEdge_V
    {
        /// <summary>
        /// Calculates AreaDensEdge at the landscape level
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
         public Landscape_AreaDensEdge_V(string filename, string epsg)
        {
            this.landscapeMetrics(filename, epsg);
        }

         double _maxLandscapeArea = 0;
         double _maxLandscapeEdge = 0;
         double _minLandscapeEdge = 0;
         double _landscapeArea = 0;
         int _numLandscapePatches = 0;
         double _landscapePatchesDensity = 0;
         double _landscapePerimeter = 0;
         double _landscapeEdgeDensity = 0;
         double _landscapeShapeIndex = 0;
         double _landscapeLargestPatchIndex = 0;

        /// <summary>
        /// Gets the total landscape area --(TA_l)-- Units: hectares
        /// </summary>
         public double LandscapeArea
         {
             get { return _landscapeArea; }
         }

        /// <summary>
        /// Gets the number of landscape patches --(NP_l)-- Units: none
        /// </summary>
         public int NumLandscapePatches
         {
             get { return _numLandscapePatches; }
         }

        /// <summary>
        /// Gets the landscape patches density --(PD_l)-- Units: number per 100 ha
        /// </summary>
         public double LandscapePatchesDensity
         {
             get { return _landscapePatchesDensity; }
         }

        /// <summary>
        /// Gets the total landscape perimeter --(TE_l)-- Units: meters
        /// </summary>
         public double LandscapePerimeter
         {
             get { return _landscapePerimeter; }
         }

        /// <summary>
        /// Gets the landscape Edge density --(ED_l)-- Units: meters per hectare
        /// </summary>
         public double LandscapeEdgeDensity
         {
             get { return _landscapeEdgeDensity; }
         }

         /// <summary>
         /// Gets the landscape shape index --(LSI_l)-- Units: none
         /// </summary>
         public double LandscapeShapeIndex
         {
             get { return _landscapeShapeIndex; }
         }

        /// <summary>
        /// Gets the lanscape largest patch index --(LPI_l)-- Units: percent
        /// </summary>
         public double LandscapeLargestPatchIndex
         {
             get { return _landscapeLargestPatchIndex; }
         }






         private void landscapeMetrics(string filename, string epsg)
         {
             try
             {

                 GeometryReader geometryCollection = new GeometryReader(filename, epsg);

                 _numLandscapePatches = (int)geometryCollection.Areas.Count;


                 for (int i = 0; i < geometryCollection.Areas.Count; i++)
                 {
                     _landscapeArea += (double)geometryCollection.Areas[i];
                     _landscapePerimeter += (double)geometryCollection.Perimeters[i];
                 }

                 //Calculo del mayor perimetro y la mayor area en todo el paisaje
                 double maxAreaTmp = 0;
                 double maxEdgeTmp = 0;
                 for (int i = 0; i < geometryCollection.Perimeters.Count; i++)
                 {
                     if ((double)geometryCollection.Perimeters[i] > maxEdgeTmp)
                     {
                         maxEdgeTmp = (double)geometryCollection.Perimeters[i];
                     }

                     if ((double)geometryCollection.Areas[i] > maxAreaTmp)
                     {
                         maxAreaTmp = (double)geometryCollection.Areas[i];
                     }
                 }
                 _maxLandscapeEdge = maxEdgeTmp;
                 _maxLandscapeArea = maxAreaTmp;

                 _landscapeLargestPatchIndex=(_maxLandscapeArea/_landscapeArea)*100;
                 

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


                 _landscapeArea = _landscapeArea / 10000;
                 _landscapePatchesDensity = ((_numLandscapePatches / _landscapeArea)*10000)*100;
                 _landscapeEdgeDensity = (_landscapePerimeter / _landscapeArea)*10000;
                 _landscapeShapeIndex = _landscapePerimeter / _minLandscapeEdge;

             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }


    }
}


