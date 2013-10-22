
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
using landmetrics_DIY_API.DIY_enums.VectorMetrics;
using System.Data;
using landmetrics_DIY_API.DIY_core.Metrics.Vector;


namespace landmetrics_DIY_API.DIY_reports.Vector
{
    /// <summary>
    /// This class provides tools to list AreaDensEdge metrics at the class level
    /// </summary>
    public class List_Category_AreaDensEdge
    {

        /// <summary>
        /// Lists AreaDensEdge metrics at the class level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Category_AreaDensEdge(Category_AreaDensEdge_V metrics)
        {
            this.joinMetrics(metrics);
        }



        ArrayList _categoriesAndMetrics = new ArrayList();

        /// <summary>
        /// Gets an object with a complete list of class AreaDensEdge metrics without filters
        /// </summary>
        public ArrayList CategoriesAndMetrics
        {
            get { return _categoriesAndMetrics; }
        }




        private void joinMetrics(Category_AreaDensEdge_V metrics)
        {

            //Por último, obtenemos un arraylist donde añadimos todos los objetos
            for(int i=0; i < metrics.CategoryNames.Count;i++)
            {

                Obj_Category_AreaDensEdge metricsObj = new Obj_Category_AreaDensEdge
                    ((string)metrics.CategoryNames[i], (double)metrics.TotalAreas[i],
                    (double)metrics.LandscapePercent[i], (int)metrics.NumPatches[i],
                    (double)metrics.PatchDens[i], (double)metrics.TotalPerimeter[i],
                    (double)metrics.EdgeDens[i],
                    (double)metrics.LargestPatchIndex[i],
                    (double)metrics.LargestPatchSize[i], (double)metrics.MeanPatchSize[i]);
            //

                
                _categoriesAndMetrics.Add(metricsObj);

            }
        }


        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Category_AreaDensEdge_V filter)
        {
            DataTable dt = new DataTable("Metrics_Category_AreaDensEdge");

            if ((filter & Options_Category_AreaDensEdge_V.CategoryArea) == Options_Category_AreaDensEdge_V.CategoryArea)
            {
                dt.Columns.Add("CategoryArea", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.CategoryName) == Options_Category_AreaDensEdge_V.CategoryName)
            {
                dt.Columns.Add("CategoryName", typeof(string));
            }
            if ((filter & Options_Category_AreaDensEdge_V.EdgeDensPerCategory) == Options_Category_AreaDensEdge_V.EdgeDensPerCategory)
            {
                dt.Columns.Add("EdgeDensPerCategory", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.LandscapePercentPerCategory) == Options_Category_AreaDensEdge_V.LandscapePercentPerCategory)
            {
                dt.Columns.Add("LandscapePercentPerCategory", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.LargestPatchIndexPerCategory) == Options_Category_AreaDensEdge_V.LargestPatchIndexPerCategory)
            {
                dt.Columns.Add("LargestPatchIndexPerCategory", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.LargestPatchSizePerCategory) == Options_Category_AreaDensEdge_V.LargestPatchSizePerCategory)
            {
                dt.Columns.Add("LargestPatchSizePerCategory", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.MeanPatchSizePerCategory) == Options_Category_AreaDensEdge_V.MeanPatchSizePerCategory)
            {
                dt.Columns.Add("MeanPatchSizePerCategory", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.NumPatchesPerCategory) == Options_Category_AreaDensEdge_V.NumPatchesPerCategory)
            {
                dt.Columns.Add("NumPatchesPerCategory", typeof(int));
            }
            if ((filter & Options_Category_AreaDensEdge_V.PatchDensPerCategory) == Options_Category_AreaDensEdge_V.PatchDensPerCategory)
            {
                dt.Columns.Add("PatchDensPerCategory", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.TotalEdgesPerCategory) == Options_Category_AreaDensEdge_V.TotalEdgesPerCategory)
            {
                dt.Columns.Add("TotalEdgesPerCategory", typeof(double));
            }
            if ((filter & Options_Category_AreaDensEdge_V.All) == Options_Category_AreaDensEdge_V.All)
            {
                dt.Columns.Add("CategoryArea", typeof(double));
                dt.Columns.Add("CategoryName", typeof(string));
                dt.Columns.Add("EdgeDensPerCategory", typeof(double));
                dt.Columns.Add("LandscapePercentPerCategory", typeof(double));
                dt.Columns.Add("LargestPatchIndexPerCategory", typeof(double));
                dt.Columns.Add("LargestPatchSizePerCategory", typeof(double));
                dt.Columns.Add("MeanPatchSizePerCategory", typeof(double));
                dt.Columns.Add("NumPatchesPerCategory", typeof(int));
                dt.Columns.Add("PatchDensPerCategory", typeof(double));
                dt.Columns.Add("TotalEdgesPerCategory", typeof(double));
            }



            for (int i = 0; i < _categoriesAndMetrics.Count; i++)
            {
                ArrayList _buffer = new ArrayList();


                if ((filter & Options_Category_AreaDensEdge_V.CategoryArea) == Options_Category_AreaDensEdge_V.CategoryArea)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).CategoryArea);
                }
                if ((filter & Options_Category_AreaDensEdge_V.CategoryName) == Options_Category_AreaDensEdge_V.CategoryName)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).CategoryName);
                }
                if ((filter & Options_Category_AreaDensEdge_V.EdgeDensPerCategory) == Options_Category_AreaDensEdge_V.EdgeDensPerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).EdgeDensPerCategory);
                }
                if ((filter & Options_Category_AreaDensEdge_V.LandscapePercentPerCategory) == Options_Category_AreaDensEdge_V.LandscapePercentPerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).LandscapePercentPerCategory);
                }
                if ((filter & Options_Category_AreaDensEdge_V.LargestPatchIndexPerCategory) == Options_Category_AreaDensEdge_V.LargestPatchIndexPerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).LargestPatchIndexPerCategory);
                }
                if ((filter & Options_Category_AreaDensEdge_V.LargestPatchSizePerCategory) == Options_Category_AreaDensEdge_V.LargestPatchSizePerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).LargestPatchSizePerCategory);
                }
                if ((filter & Options_Category_AreaDensEdge_V.MeanPatchSizePerCategory) == Options_Category_AreaDensEdge_V.MeanPatchSizePerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).MeanPatchSizePerCategory);
                }
                if ((filter & Options_Category_AreaDensEdge_V.NumPatchesPerCategory) == Options_Category_AreaDensEdge_V.NumPatchesPerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).NumPatchesPerCategory);
                }
                if ((filter & Options_Category_AreaDensEdge_V.PatchDensPerCategory) == Options_Category_AreaDensEdge_V.PatchDensPerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).PatchDensPerCategory);
                }
                if ((filter & Options_Category_AreaDensEdge_V.TotalEdgesPerCategory) == Options_Category_AreaDensEdge_V.TotalEdgesPerCategory)
                {
                    _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).TotalEdgesPerCategory);
                }



                if ((filter & Options_Category_AreaDensEdge_V.All) == Options_Category_AreaDensEdge_V.All)
                {
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).CategoryArea);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).CategoryName);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).EdgeDensPerCategory);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).LandscapePercentPerCategory);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).LargestPatchIndexPerCategory);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).LargestPatchSizePerCategory);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).MeanPatchSizePerCategory);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).NumPatchesPerCategory);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).PatchDensPerCategory);
                        _buffer.Add((_categoriesAndMetrics[i] as Obj_Category_AreaDensEdge).TotalEdgesPerCategory);
                }



                dt.Rows.Add(_buffer.ToArray());
            }

            return dt;

        }




    }
}


