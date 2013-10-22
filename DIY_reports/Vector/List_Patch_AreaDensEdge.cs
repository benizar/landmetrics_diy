
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
using GisSharpBlog.NetTopologySuite.IO;
using GisSharpBlog.NetTopologySuite.Geometries;
using System.Collections;
using landmetrics_DIY_API.DIY_core;
using System.Data;
using landmetrics_DIY_API.DIY_core.Metrics.Vector;
using landmetrics_DIY_API.DIY_enums.VectorMetrics;


namespace landmetrics_DIY_API.DIY_reports.Vector
{
    /// <summary>
    /// This class provides tools to list AreaDensEdge metrics at the patch level
    /// </summary>
    public class List_Patch_AreaDensEdge
    {

        /// <summary>
        /// Lists AreaDensEdge metrics at the patch level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Patch_AreaDensEdge(Patch_AreaDensEdge_V metrics)
        {
            this.joinMetrics(metrics);
        }



        ArrayList _patchesAndMetrics = new ArrayList();


        /// <summary>
        /// Gets an object with a complete list of patch AreaDensEdge metrics without filters
        /// </summary>
        public ArrayList PatchesAndMetrics
        {
            get { return _patchesAndMetrics; }
        }


        private void joinMetrics(Patch_AreaDensEdge_V metrics)
        { 
            //Por último, obtenemos un arraylist donde añadimos todos los objetos
            for(int i=0; i < metrics.PatchAreas.Count;i++)
            {

                Obj_Patch_AreaDensEdge metricsObj = new Obj_Patch_AreaDensEdge
                    ((string)metrics.PatchNames[i], (double)metrics.PatchAreas[i], 
                    (double)metrics.PatchPerimeters[i]);
                
                _patchesAndMetrics.Add(metricsObj);

            }
        }


        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Patch_AreaDensEdge_V filter)
        {
            DataTable selectedMetrics = new DataTable("Patch_AreaDensEdge");

            if ((filter & Options_Patch_AreaDensEdge_V.PatchName) == Options_Patch_AreaDensEdge_V.PatchName)
            {
                selectedMetrics.Columns.Add("PatchName", typeof(string));
            }
            if ((filter & Options_Patch_AreaDensEdge_V.PatchArea) == Options_Patch_AreaDensEdge_V.PatchArea)
            {
                selectedMetrics.Columns.Add("PatchArea", typeof(double));
            }
            if ((filter & Options_Patch_AreaDensEdge_V.PatchEdge ) == Options_Patch_AreaDensEdge_V.PatchEdge)
            {
                selectedMetrics.Columns.Add("PatchEdge", typeof(double));
            }
            if ((filter & Options_Patch_AreaDensEdge_V.All) == Options_Patch_AreaDensEdge_V.All)
            {
                selectedMetrics.Columns.Add("PatchName", typeof(string));
                selectedMetrics.Columns.Add("PatchArea", typeof(double));
                selectedMetrics.Columns.Add("PatchEdge", typeof(double));
            }
            


            for(int i =0; i<_patchesAndMetrics.Count;i++)
            {
                ArrayList _buffer = new ArrayList();

                if ((filter & Options_Patch_AreaDensEdge_V.PatchName) == Options_Patch_AreaDensEdge_V.PatchName)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_AreaDensEdge).PatchName);
                }
                if ((filter & Options_Patch_AreaDensEdge_V.PatchArea) == Options_Patch_AreaDensEdge_V.PatchArea)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_AreaDensEdge).PatchArea);
                }
                if ((filter & Options_Patch_AreaDensEdge_V.PatchEdge) == Options_Patch_AreaDensEdge_V.PatchEdge)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_AreaDensEdge).PatchEdge);
                }
                if ((filter & Options_Patch_AreaDensEdge_V.All) == Options_Patch_AreaDensEdge_V.All)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_AreaDensEdge).PatchName);
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_AreaDensEdge).PatchArea);
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_AreaDensEdge).PatchEdge);
                }


                selectedMetrics.Rows.Add(_buffer.ToArray());
            }

            return selectedMetrics;
 
        }



    }
}


