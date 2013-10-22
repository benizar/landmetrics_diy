
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
    /// This class calculates the largest patch index at the class level --(LPI_c)-- Units: percent
    /// </summary>
    public class C_LargestPatchIndex_V
    {

        /// <summary>
        /// Calculates the largest patch index at the class level --(LPI_c)-- Units: percent
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier </param>
        /// <param name="fieldIndex">The class field to use</param>
        public C_LargestPatchIndex_V(string filename, string epsg, int fieldIndex)
        {
            this.cLargestPatchIndex(filename, epsg, fieldIndex);
        }


        double totalArea = 0;
        ArrayList _categoryNames = new ArrayList();
        ArrayList _largestPatchIndexPerCategory = new ArrayList();

        DataTable _cLargestPatchIndex_V = new DataTable("C_LargestPatchIndex_V");




        /// <summary>
        /// Gets a list of class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }

        /// <summary>
        /// Gets a list of the largest patch index by each class --(LPI_c)-- Units: percent
        /// </summary>
        public ArrayList LargestPatchIndex
        {
            get { return _largestPatchIndexPerCategory; }
        }

        /// <summary>
        /// Gets a list of class names and largest patch index per category --(LPI_c)-- Units: percent
        /// </summary>
        public DataTable C_Names_LargestPatchIndex_V
        {
            get { return _cLargestPatchIndex_V; }
        }



        private void cLargestPatchIndex(string filename, string epsg, int fieldIndex)
        {
            try
            {

                C_Area_V cAreas = new C_Area_V(filename, epsg, fieldIndex);


                for (int i = 0; i < cAreas.TotalAreas.Count; i++)
                {
                    totalArea += (double)cAreas.TotalAreas[i];
                }


                C_LargestPatchSize_V LPS = new C_LargestPatchSize_V(filename, epsg, fieldIndex);
                _categoryNames = LPS.CategoryNames;



                for (int i = 0; i < _categoryNames.Count; i++)
                {
                    _largestPatchIndexPerCategory.Add(((double)LPS.LargestPatchSize[i] / totalArea) * 100);
                }


                _cLargestPatchIndex_V.Columns.Add("CategoryName", typeof(string));
                _cLargestPatchIndex_V.Columns.Add("LargestPatchIndex", typeof(double));

                for (int i = 0; i < _categoryNames.Count; i++)
                {
                    _cLargestPatchIndex_V.Rows.Add(_categoryNames[i], _largestPatchIndexPerCategory[i]);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


