
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
    /// This class provides tools to list Shape metrics at the patch level
    /// </summary>
    public class List_Patch_ShapeMetrics
    {

        /// <summary>
        /// Lists Shape metrics at the patch level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Patch_ShapeMetrics(Patch_ShapeMetrics_V metrics)
        {
            this.joinMetrics(metrics);
        }



        ArrayList _patchesAndMetrics = new ArrayList();


        /// <summary>
        /// Gets an object with a complete list of patch Shape metrics without filters
        /// </summary>
        public ArrayList PatchesAndMetrics
        {
            get { return _patchesAndMetrics; }
        }




        private void joinMetrics(Patch_ShapeMetrics_V metrics)
        { 

            //Por último, obtenemos un arraylist donde añadimos todos los objetos
            for(int i=0; i < metrics.PatchNames.Count;i++)
            {

                Obj_Patch_ShapeMetrics metricsObj = new Obj_Patch_ShapeMetrics
                    ((string)metrics.PatchNames[i], (double)metrics.PatchesPerimeterAreaRatio[i], 
                    (double)metrics.PatchesFractalDimension[i]);
                
                _patchesAndMetrics.Add(metricsObj);

            }
        }


        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Patch_ShapeMetrics_V filter)
        {

            DataTable dt = new DataTable("Patch_ShapeMetrics");

            if ((filter & Options_Patch_ShapeMetrics_V.patchFractalDimension) == Options_Patch_ShapeMetrics_V.patchFractalDimension)
            {
                dt.Columns.Add("PatchFractalDimension", typeof(double));
            }
            if ((filter & Options_Patch_ShapeMetrics_V.patchName) == Options_Patch_ShapeMetrics_V.patchName)
            {
                dt.Columns.Add("PatchName", typeof(string));
            }
            if ((filter & Options_Patch_ShapeMetrics_V.patchPerimeterAreaRatio) == Options_Patch_ShapeMetrics_V.patchPerimeterAreaRatio)
            {
                dt.Columns.Add("PatchPerimeterAreaRatio", typeof(double));
            }
            if ((filter & Options_Patch_ShapeMetrics_V.All) == Options_Patch_ShapeMetrics_V.All)
            {
                dt.Columns.Add("PatchFractalDimension", typeof(double));
                dt.Columns.Add("PatchName", typeof(string));
                dt.Columns.Add("PatchPerimeterAreaRatio", typeof(double));
            }



            for (int i = 0; i < _patchesAndMetrics.Count; i++)
            {
                ArrayList _buffer = new ArrayList();



                if ((filter & Options_Patch_ShapeMetrics_V.patchFractalDimension) == Options_Patch_ShapeMetrics_V.patchFractalDimension)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_ShapeMetrics).PatchFractalDimension);
                }
                if ((filter & Options_Patch_ShapeMetrics_V.patchName) == Options_Patch_ShapeMetrics_V.patchName)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_ShapeMetrics).PatchName);
                }
                if ((filter & Options_Patch_ShapeMetrics_V.patchPerimeterAreaRatio) == Options_Patch_ShapeMetrics_V.patchPerimeterAreaRatio)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_ShapeMetrics).PatchPerimeterAreaRatio);
                }
                if ((filter & Options_Patch_ShapeMetrics_V.All) == Options_Patch_ShapeMetrics_V.All)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_ShapeMetrics).PatchFractalDimension);
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_ShapeMetrics).PatchName);
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_ShapeMetrics).PatchPerimeterAreaRatio);
                }




                dt.Rows.Add(_buffer.ToArray());
            }

            return dt;

        }



    }
}


