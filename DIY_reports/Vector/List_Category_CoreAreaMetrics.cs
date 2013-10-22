
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
using landmetrics_DIY_API.DIY_core;
using System.Data;
using landmetrics_DIY_API.DIY_core.Metrics.Vector;
using landmetrics_DIY_API.DIY_enums.VectorMetrics;


namespace landmetrics_DIY_API.DIY_reports.Vector
{
    /// <summary>
    /// This class provides tools to list CoreArea metrics at the class level
    /// </summary>
    public class List_Category_CoreAreaMetrics
    {

        /// <summary>
        /// Lists CoreArea metrics at the class level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Category_CoreAreaMetrics(Category_CoreAreaMetrics_V metrics)
        {
            this.joinMetrics(metrics);
        }



        ArrayList _categoriesAndMetrics = new ArrayList();


        /// <summary>
        /// Gets an object with a complete list of class CoreArea metrics without filters
        /// </summary>
        public ArrayList CategoriesAndMetrics
        {
            get { return _categoriesAndMetrics; }
        }


        private void joinMetrics(Category_CoreAreaMetrics_V metrics)
        {

            //Por último, obtenemos un arraylist donde añadimos todos los objetos
            for (int i = 0; i < metrics.CategoryNames.Count; i++)
            {

                Obj_Category_CoreAreaMetrics metricsObj = new Obj_Category_CoreAreaMetrics(
                    (string)metrics.CategoryNames[i], (double)metrics.TotalCoreArea[i],
                    (double)metrics.CoreAreaPercentageOfLandscape[i], (int)metrics.NumDisjunctCoreArea[i],
                    (double)metrics.DisjunctCoreAreaDensity[i]);

                _categoriesAndMetrics.Add(metricsObj);

            }
        }



        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Category_CoreAreaMetrics_V filter)
        {
            DataTable dt = new DataTable("Metrics_Category_CoreAreaMetrics");

            if ((filter & Options_Category_CoreAreaMetrics_V.CategoryCoreArea) == Options_Category_CoreAreaMetrics_V.CategoryCoreArea)
            {
                dt.Columns.Add("CategoryCoreArea", typeof(double));
            }
            if ((filter & Options_Category_CoreAreaMetrics_V.CategoryName) == Options_Category_CoreAreaMetrics_V.CategoryName)
            {
                dt.Columns.Add("CategoryName", typeof(string));
            }
            if ((filter & Options_Category_CoreAreaMetrics_V.CoreAreaPercentOfLandscape) == Options_Category_CoreAreaMetrics_V.CoreAreaPercentOfLandscape)
            {
                dt.Columns.Add("CoreAreaPercentOfLandscape", typeof(double));
            }
            if ((filter & Options_Category_CoreAreaMetrics_V.DisjunctCoreAreaDensity) == Options_Category_CoreAreaMetrics_V.DisjunctCoreAreaDensity)
            {
                dt.Columns.Add("DisjunctCoreAreaDensity", typeof(double));
            }
            if ((filter & Options_Category_CoreAreaMetrics_V.NumDisjunctCoreAreas) == Options_Category_CoreAreaMetrics_V.NumDisjunctCoreAreas)
            {
                dt.Columns.Add("NumDisjunctCoreAreas", typeof(int));
            }
            if ((filter & Options_Category_CoreAreaMetrics_V.All) == Options_Category_CoreAreaMetrics_V.All)
            {
                    dt.Columns.Add("CategoryCoreArea", typeof(double));
                    dt.Columns.Add("CategoryName", typeof(string));
                    dt.Columns.Add("CoreAreaPercentOfLandscape", typeof(double));
                    dt.Columns.Add("DisjunctCoreAreaDensity", typeof(double));
                    dt.Columns.Add("NumDisjunctCoreAreas", typeof(int));   
            }





            for (int i = 0; i < _categoriesAndMetrics.Count; i++)
            {
                ArrayList _buffer = new ArrayList();


                if ((filter & Options_Category_CoreAreaMetrics_V.CategoryCoreArea) == Options_Category_CoreAreaMetrics_V.CategoryCoreArea)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).CategoryCoreArea);
                }
                if ((filter & Options_Category_CoreAreaMetrics_V.CategoryName) == Options_Category_CoreAreaMetrics_V.CategoryName)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).CategoryName);
                }
                if ((filter & Options_Category_CoreAreaMetrics_V.CoreAreaPercentOfLandscape) == Options_Category_CoreAreaMetrics_V.CoreAreaPercentOfLandscape)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).CoreAreaPercentOfLandscape);
                }
                if ((filter & Options_Category_CoreAreaMetrics_V.DisjunctCoreAreaDensity) == Options_Category_CoreAreaMetrics_V.DisjunctCoreAreaDensity)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).DisjunctCoreAreaDensity);
                }
                if ((filter & Options_Category_CoreAreaMetrics_V.NumDisjunctCoreAreas) == Options_Category_CoreAreaMetrics_V.NumDisjunctCoreAreas)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).NumDisjunctCoreAreas);
                }

                if ((filter & Options_Category_CoreAreaMetrics_V.All) == Options_Category_CoreAreaMetrics_V.All)
                {
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).CategoryCoreArea);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).CategoryName);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).CoreAreaPercentOfLandscape);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).DisjunctCoreAreaDensity);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_CoreAreaMetrics).NumDisjunctCoreAreas);
                }




                dt.Rows.Add(_buffer.ToArray());
            }

            return dt;

        }



    }
}


