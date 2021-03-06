﻿
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
    /// This class calculates the mean patch size at the class level --(MPS_c)-- Units: hectares
    /// </summary>
    public class C_MeanPatchSize_V
    {

        /// <summary>
        /// Calculates the mean patch size at the class level --(MPS_c)-- Units: hectares
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier </param>
        /// <param name="fieldIndex">The class field to use</param>
        public C_MeanPatchSize_V(string filename, string epsg, int fieldIndex)
        {
            this.cMeanPatchSize(filename, epsg, fieldIndex);
        }


        ArrayList _meanPatchSizePerCategory = new ArrayList();
        ArrayList _categoryNames = new ArrayList();

        ArrayList _totalAreasPerCategory = new ArrayList();
        ArrayList _numPatchesPerCategory = new ArrayList();

        DataTable _cMeanPatchSize_V = new DataTable("C_Area_V");



        /// <summary>
        /// Gets a list of class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }

        /// <summary>
        /// Gets a list of the mean patch size by each class --(MPS_c)-- Units: hectares
        /// </summary>
        public ArrayList MeanPatchSize
        {
            get { return _meanPatchSizePerCategory; }
        }

        /// <summary>
        /// Gets a list of class names and mean patch sizes --(MPS_c)-- Units: hectares
        /// </summary>
        public DataTable C_Names_MeanPatchSize_V
        {
            get { return _cMeanPatchSize_V; }
        }



        private void cMeanPatchSize(string filename, string epsg, int fieldIndex)
        {
            try
            {

                C_Area_V cArea = new C_Area_V(filename, epsg, fieldIndex);
                _categoryNames = cArea.CategoryNames;
                _totalAreasPerCategory = cArea.TotalAreas;

                C_NumPatches_V cNumPatches = new C_NumPatches_V(filename, epsg, fieldIndex);
                _numPatchesPerCategory = cNumPatches.NumPatches;


                for (int i = 0; i < _totalAreasPerCategory.Count; i++)
                {

                    _meanPatchSizePerCategory.Add(((double)_totalAreasPerCategory[i] / (int)_numPatchesPerCategory[i]) / 10000);
                }



                _cMeanPatchSize_V.Columns.Add("CategoryName", typeof(string));
                _cMeanPatchSize_V.Columns.Add("CategoryMeanPatchSize", typeof(double));

                for (int i = 0; i < _categoryNames.Count; i++)
                {
                    _cMeanPatchSize_V.Rows.Add(_categoryNames[i], _meanPatchSizePerCategory[i]);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}


