
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
    /// This class provides tools to list CoreArea metrics at the landscape level
    /// </summary>
    public class List_Landscape_CoreAreaMetrics
    {
        /// <summary>
        /// Lists CoreArea metrics at the landscape level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Landscape_CoreAreaMetrics(Landscape_CoreAreaMetrics_V metrics)
        {
            this.joinMetrics(metrics);
        }


        ArrayList _landscapeAndMetrics = new ArrayList();

        /// <summary>
        /// Gets an object with a complete list of landscape Core Area metrics without filters
        /// </summary>
        public ArrayList LandscapeAndMetrics
        {
            get { return _landscapeAndMetrics; }
        }



        private void joinMetrics(Landscape_CoreAreaMetrics_V metrics)
        {
            //Por último, obtenemos un arraylist donde añadimos todos los objetos
            Obj_Landscape_CoreAreaMetrics metricsObj = new Obj_Landscape_CoreAreaMetrics(
            metrics.TotalCoreArea, metrics.NumberOfDisjunctCoreAreas, metrics.DisjunctCoreAreaDensity);

            _landscapeAndMetrics.Add(metricsObj);

        }



        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Landscape_CoreAreaMetrics_V filter)
        {
            DataTable dt = new DataTable("Metrics_Landscape_CoreAreaMetrics");

            if ((filter & Options_Landscape_CoreAreaMetrics_V.DisjunctCoreAreaDensity) == Options_Landscape_CoreAreaMetrics_V.DisjunctCoreAreaDensity)
            {
                dt.Columns.Add("DisjunctCoreAreaDensity", typeof(double));
            }
            if ((filter & Options_Landscape_CoreAreaMetrics_V.NumberOfDisjunctCoreAreas) == Options_Landscape_CoreAreaMetrics_V.NumberOfDisjunctCoreAreas)
            {
                dt.Columns.Add("NumberOfDisjunctCoreAreas", typeof(double));
            }
            if ((filter & Options_Landscape_CoreAreaMetrics_V.TotalCoreArea) == Options_Landscape_CoreAreaMetrics_V.TotalCoreArea)
            {
                dt.Columns.Add("TotalCoreArea", typeof(double));
            }

            if ((filter & Options_Landscape_CoreAreaMetrics_V.All) == Options_Landscape_CoreAreaMetrics_V.All)
            {

                dt.Columns.Add("DisjunctCoreAreaDensity", typeof(double));
                dt.Columns.Add("NumberOfDisjunctCoreAreas", typeof(double));
                dt.Columns.Add("TotalCoreArea", typeof(double));
            }





            for (int i = 0; i < _landscapeAndMetrics.Count; i++)
            {
                ArrayList _buffer = new ArrayList();


                if ((filter & Options_Landscape_CoreAreaMetrics_V.DisjunctCoreAreaDensity) == Options_Landscape_CoreAreaMetrics_V.DisjunctCoreAreaDensity)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_CoreAreaMetrics).DisjunctCoreAreaDensity);
                }
                if ((filter & Options_Landscape_CoreAreaMetrics_V.NumberOfDisjunctCoreAreas) == Options_Landscape_CoreAreaMetrics_V.NumberOfDisjunctCoreAreas)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_CoreAreaMetrics).NumberOfDisjunctCoreAreas);
                }
                if ((filter & Options_Landscape_CoreAreaMetrics_V.TotalCoreArea) == Options_Landscape_CoreAreaMetrics_V.TotalCoreArea)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_CoreAreaMetrics).TotalCoreArea);
                }
                if ((filter & Options_Landscape_CoreAreaMetrics_V.All) == Options_Landscape_CoreAreaMetrics_V.All)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_CoreAreaMetrics).DisjunctCoreAreaDensity);
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_CoreAreaMetrics).NumberOfDisjunctCoreAreas);
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_CoreAreaMetrics).TotalCoreArea);

                }




                dt.Rows.Add(_buffer.ToArray());
            }

            return dt;

        }


    }
}


