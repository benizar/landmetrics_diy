
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
using System.Data;


namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the core area percentage of landscape at the class level --(CPLAND_c)-- Units: percent
    /// </summary>
    public class C_CoreAreaPercentOfLandscape_V
    {
        /// <summary>
        /// Calculates the core area percentage of landscape at the class level --(CPLAND_c)-- Units: percent
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        /// <param name="depthOfEdge"></param>
        public C_CoreAreaPercentOfLandscape_V(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            this.cCoreAreaPercentOfLandscape(filename, epsg, fieldIndex, depthOfEdge);
        }

        double totalArea = 0;
        ArrayList _categoryNames = new ArrayList();
        ArrayList _coreAreaPerCategory = new ArrayList();
        ArrayList _coreAreaPercentOfLandscape = new ArrayList();


        DataTable _cCoreAreaPercentOfLandscape_V = new DataTable("C_CoreAreaPercentOfLandscape_V");



        /// <summary>
        /// Gets a list of the class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }
        /// <summary>
        /// Gets a list of the class core area percentage of landscape --(CPLAND_c)-- Units: percent
        /// </summary>
        public ArrayList CoreAreaPercentageOfLandscape
        {
            get { return _coreAreaPercentOfLandscape; }
        }
        /// <summary>
        /// Gets a list of class names and the class core area percentage of landscape --(CPLAND_c)-- Units: percent
        /// </summary>
        public DataTable C_Names_CoreAreaPercentageOfLandscape_V
        {
            get { return _cCoreAreaPercentOfLandscape_V; }
        }



        private void cCoreAreaPercentOfLandscape(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            try
            {

                C_Area_V areas = new C_Area_V(filename, epsg, fieldIndex);
                _categoryNames = areas.CategoryNames;


                for (int i = 0; i < areas.TotalAreas.Count; i++)
                {
                    totalArea += (double)areas.TotalAreas[i];
                }


                C_CoreArea_V coreAreas = new C_CoreArea_V(filename, epsg, fieldIndex, depthOfEdge);
                _coreAreaPerCategory = coreAreas.TotalCoreArea;



                for (int i = 0; i < _coreAreaPerCategory.Count; i++)
                {
                    _coreAreaPercentOfLandscape.Add(((double)_coreAreaPerCategory[i] / totalArea) * 100);
                }


                _cCoreAreaPercentOfLandscape_V.Columns.Add("CategoryName", typeof(string));
                _cCoreAreaPercentOfLandscape_V.Columns.Add("CategoryCoreAreaPercentOfLandscape", typeof(double));

                for (int i = 0; i < _categoryNames.Count; i++)
                {
                    _cCoreAreaPercentOfLandscape_V.Rows.Add(_categoryNames[i], _coreAreaPercentOfLandscape[i]);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}


