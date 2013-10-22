
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
    /// This class provides tools to list CoreArea metrics at the patch level
    /// </summary>
    public class List_Patch_CoreAreaMetrics
    {

        /// <summary>
        /// Lists CoreArea metrics at the patch level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Patch_CoreAreaMetrics(Patch_CoreAreaMetrics_V metrics)
        {
            this.joinMetrics(metrics);
        }



        ArrayList _patchesAndMetrics = new ArrayList();


        /// <summary>
        /// Gets an object with a complete list of patch CoreArea metrics without filters
        /// </summary>
        public ArrayList PatchesAndMetrics
        {
            get { return _patchesAndMetrics; }
        }




        private void joinMetrics(Patch_CoreAreaMetrics_V metrics)
        { 
            //Por último, obtenemos un arraylist donde añadimos todos los objetos
            for(int i=0; i < metrics.PatchNames.Count;i++)
            {
                Obj_Patch_CoreAreaMetrics metricsObj = new Obj_Patch_CoreAreaMetrics
                    ((string)metrics.PatchNames[i], (double)metrics.CoreAreas[i],
                    (int)metrics.NumCoreAreas[i], (double)metrics.CoreAreaIndex[i]);

                
                _patchesAndMetrics.Add(metricsObj);

            }
        }


        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Patch_CoreAreaMetrics_V filter)
        {
            DataTable dt = new DataTable("Patch_CoreAreaMetrics");

            if ((filter & Options_Patch_CoreAreaMetrics_V.CoreArea) == Options_Patch_CoreAreaMetrics_V.CoreArea)
            {
                dt.Columns.Add("CoreArea", typeof(double));
            }
            if ((filter & Options_Patch_CoreAreaMetrics_V.CoreAreaIndex) == Options_Patch_CoreAreaMetrics_V.CoreAreaIndex)
            {
                dt.Columns.Add("CoreAreaIndex", typeof(double));
            }
            if ((filter & Options_Patch_CoreAreaMetrics_V.NumCoreAreas) == Options_Patch_CoreAreaMetrics_V.NumCoreAreas)
            {
                dt.Columns.Add("NumCoreAreas", typeof(int));
            }
            if ((filter & Options_Patch_CoreAreaMetrics_V.PatchName) == Options_Patch_CoreAreaMetrics_V.PatchName)
            {
                dt.Columns.Add("PatchName", typeof(string));
            }


            if ((filter & Options_Patch_CoreAreaMetrics_V.All) == Options_Patch_CoreAreaMetrics_V.All)
            {
                dt.Columns.Add("CoreArea", typeof(double));
                dt.Columns.Add("CoreAreaIndex", typeof(double));
                dt.Columns.Add("NumCoreAreas", typeof(int));
                dt.Columns.Add("PatchName", typeof(string));
            }





            for (int i = 0; i < _patchesAndMetrics.Count; i++)
            {
                ArrayList _buffer = new ArrayList();


                if ((filter & Options_Patch_CoreAreaMetrics_V.CoreArea) == Options_Patch_CoreAreaMetrics_V.CoreArea)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).CoreArea);
                }
                if ((filter & Options_Patch_CoreAreaMetrics_V.CoreAreaIndex) == Options_Patch_CoreAreaMetrics_V.CoreAreaIndex)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).CoreAreaIndex);
                }
                if ((filter & Options_Patch_CoreAreaMetrics_V.NumCoreAreas) == Options_Patch_CoreAreaMetrics_V.NumCoreAreas)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).NumCoreAreas);
                }
                if ((filter & Options_Patch_CoreAreaMetrics_V.PatchName) == Options_Patch_CoreAreaMetrics_V.PatchName)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).PatchName);
                }
                if ((filter & Options_Patch_CoreAreaMetrics_V.All) == Options_Patch_CoreAreaMetrics_V.All)
                {
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).CoreArea);
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).CoreAreaIndex);
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).NumCoreAreas);
                    _buffer.Add((_patchesAndMetrics[i] as Obj_Patch_CoreAreaMetrics).PatchName);
                }




                dt.Rows.Add(_buffer.ToArray());
            }

            return dt;

        }






    }
}


