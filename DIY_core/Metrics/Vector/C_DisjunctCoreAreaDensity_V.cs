
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
    /// This class calculates the disjunct core area density at the class level --(DCAD_c)-- Units: number per 100 Ha
    /// </summary>
    public class C_DisjunctCoreAreaDensity_V
    {

        /// <summary>
        /// Calculates the disjunct core area density at the class level --(DCAD_c)-- Units: number per 100 Ha
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        /// <param name="depthOfEdge"></param>
        public C_DisjunctCoreAreaDensity_V(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            this.cDisjunctCoreAreaDensity(filename, epsg, fieldIndex, depthOfEdge);
        }

        double totalArea = 0;
        ArrayList _categoryNames = new ArrayList();
        ArrayList _numDisjunctCoreAreasPerCategory = new ArrayList();
        ArrayList _disjunctCoreAreaDensityPerCategory = new ArrayList();

        DataTable _cDisjunctCoreAreaDensity_V = new DataTable("C_DisjunctCoreAreaDensity_V");

        

        /// <summary>
        /// Gets a list of the class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }
        /// <summary>
        /// Gets a list of the disjunct core area densities by class --(DCAD_c)-- Units: number per 100 Ha
        /// </summary>
        public ArrayList DisjunctCoreAreaDensity
        {
            get { return _disjunctCoreAreaDensityPerCategory; }
        }
        /// <summary>
        /// Gets a list of class names and disjunct core area densities --(DCAD_c)-- Units: number per 100 Ha
        /// </summary>
        public DataTable C_Names_DisjunctCoreAreaDensity_V
        {
            get { return _cDisjunctCoreAreaDensity_V; }
        }


        private void cDisjunctCoreAreaDensity(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            try
            {
                C_NumDisjunctCoreArea_V ndca = new C_NumDisjunctCoreArea_V(filename, epsg, fieldIndex, depthOfEdge);
                _numDisjunctCoreAreasPerCategory = ndca.NumDisjunctCoreArea;
                _categoryNames = ndca.CategoryNames;

                GeometryReader geometryCollection = new GeometryReader(filename, epsg);


                for (int i = 0; i < geometryCollection.Areas.Count; i++)
                {
                    totalArea += (double)geometryCollection.Areas[i];
                }



                for (int i = 0; i < _numDisjunctCoreAreasPerCategory.Count; i++)
                {
                    _disjunctCoreAreaDensityPerCategory.Add((((int)_numDisjunctCoreAreasPerCategory[i] / totalArea) * 10000) * 100);
                }


                _cDisjunctCoreAreaDensity_V.Columns.Add("CategoryName", typeof(string));
                _cDisjunctCoreAreaDensity_V.Columns.Add("CategoryDisjunctCoreAreaDensity", typeof(double));

                for (int i = 0; i < _categoryNames.Count; i++)
                {
                    _cDisjunctCoreAreaDensity_V.Rows.Add(_categoryNames[i], _disjunctCoreAreaDensityPerCategory[i]);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}


