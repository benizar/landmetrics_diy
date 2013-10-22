
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
    /// This class provides tools to list AreaDensEdge metrics at the landscape level
    /// </summary>
    public class List_Landscape_AreaDensEdge
    {

        /// <summary>
        /// Lists AreaDensEdge metrics at the landscape level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Landscape_AreaDensEdge(Landscape_AreaDensEdge_V metrics)
        {
            this.joinMetrics(metrics);
        }


        ArrayList _landscapeAndMetrics = new ArrayList();


        /// <summary>
        /// Gets an object with a complete list of landscape AreaDensEdge metrics without filters
        /// </summary>
        public ArrayList LandscapeAndMetrics
        {
            get { return _landscapeAndMetrics; }
        }


        private void joinMetrics(Landscape_AreaDensEdge_V metrics)
        {
            //Por último, obtenemos un arraylist donde añadimos todos los objetos
                Obj_Landscape_AreaDensEdge metricsObj = new Obj_Landscape_AreaDensEdge(metrics.LandscapeArea,
                    metrics.NumLandscapePatches,metrics.LandscapePatchesDensity, metrics.LandscapePerimeter,
                    metrics.LandscapeEdgeDensity, metrics.LandscapeShapeIndex, metrics.LandscapeLargestPatchIndex);

                _landscapeAndMetrics.Add(metricsObj);

        }

        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Landscape_AreaDensEdge_V filter)
        {
            DataTable dt = new DataTable("Metrics_Landscape_AreaDensEdge");

            if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeArea) == Options_Landscape_AreaDensEdge_V.LandscapeArea)
            {
                dt.Columns.Add("LandscapeArea", typeof(double));
            }
            if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeEdge) == Options_Landscape_AreaDensEdge_V.LandscapeEdge)
            {
                dt.Columns.Add("LandscapeEdge", typeof(double));
            }
            if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeEdgeDensity) == Options_Landscape_AreaDensEdge_V.LandscapeEdgeDensity)
            {
                dt.Columns.Add("LandscapeEdgeDensity", typeof(double));
            }
            if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeLargestPatchIndex) == Options_Landscape_AreaDensEdge_V.LandscapeLargestPatchIndex)
            {
                dt.Columns.Add("LandscapeLargestPatchIndex", typeof(double));
            }
            if ((filter & Options_Landscape_AreaDensEdge_V.LandscapePatchesDensity) == Options_Landscape_AreaDensEdge_V.LandscapePatchesDensity)
            {
                dt.Columns.Add("LandscapePatchesDensity", typeof(double));
            }
            if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeShapeIndex) == Options_Landscape_AreaDensEdge_V.LandscapeShapeIndex)
            {
                dt.Columns.Add("LandscapeShapeIndex", typeof(double));
            }
            if ((filter & Options_Landscape_AreaDensEdge_V.NumLandscapePatches) == Options_Landscape_AreaDensEdge_V.NumLandscapePatches)
            {
                dt.Columns.Add("NumLandscapePatches", typeof(int));
            }

            if ((filter & Options_Landscape_AreaDensEdge_V.All) == Options_Landscape_AreaDensEdge_V.All)
            {
                    dt.Columns.Add("LandscapeArea", typeof(double));
                    dt.Columns.Add("LandscapeEdge", typeof(double));
                    dt.Columns.Add("LandscapeEdgeDensity", typeof(double));
                    dt.Columns.Add("LandscapeLargestPatchIndex", typeof(double));
                    dt.Columns.Add("LandscapePatchesDensity", typeof(double));
                    dt.Columns.Add("LandscapeShapeIndex", typeof(double));
                    dt.Columns.Add("NumLandscapePatches", typeof(int));
            }






            for (int i = 0; i < _landscapeAndMetrics.Count; i++)
            {
                ArrayList _buffer = new ArrayList();


                if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeArea) == Options_Landscape_AreaDensEdge_V.LandscapeArea)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeArea);
                }
                if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeEdge) == Options_Landscape_AreaDensEdge_V.LandscapeEdge)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeEdge);
                }
                if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeEdgeDensity) == Options_Landscape_AreaDensEdge_V.LandscapeEdgeDensity)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeEdgeDensity);
                }
                if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeLargestPatchIndex) == Options_Landscape_AreaDensEdge_V.LandscapeLargestPatchIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeLargestPatchIndex);
                }
                if ((filter & Options_Landscape_AreaDensEdge_V.LandscapePatchesDensity) == Options_Landscape_AreaDensEdge_V.LandscapePatchesDensity)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapePatchesDensity);
                }
                if ((filter & Options_Landscape_AreaDensEdge_V.LandscapeShapeIndex) == Options_Landscape_AreaDensEdge_V.LandscapeShapeIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeShapeIndex);
                }
                if ((filter & Options_Landscape_AreaDensEdge_V.NumLandscapePatches) == Options_Landscape_AreaDensEdge_V.NumLandscapePatches)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).NumLandscapePatches);
                }


                if ((filter & Options_Landscape_AreaDensEdge_V.All) == Options_Landscape_AreaDensEdge_V.All)
                {
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeArea);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeEdge);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeEdgeDensity);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeLargestPatchIndex);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapePatchesDensity);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).LandscapeShapeIndex);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_AreaDensEdge).NumLandscapePatches);
                }




                dt.Rows.Add(_buffer.ToArray());
            }

            return dt;

        }



    }
}


