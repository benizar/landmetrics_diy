
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
    /// This class provides tools to list Diversity metrics at the landscape level
    /// </summary>
    public class List_Landscape_DiversityMetrics
    {
        /// <summary>
        /// Lists Diversity metrics at the landscape level
        /// </summary>
        /// <param name="metrics">The object whose metrics we  want to list</param>
        public List_Landscape_DiversityMetrics(Landscape_DiversityMetrics_V metrics)
        {
            this.joinMetrics(metrics);
        }


        ArrayList _landscapeAndMetrics = new ArrayList();


        /// <summary>
        /// Gets an object with a complete list of landscape Diversity metrics without filters
        /// </summary>
        public ArrayList LandscapeAndMetrics
        {
            get { return _landscapeAndMetrics; }
        }



        private void joinMetrics(Landscape_DiversityMetrics_V metrics)
        {
            //Por último, obtenemos un arraylist donde añadimos todos los objetos
            Obj_Landscape_DiversityMetrics metricsObj = new Obj_Landscape_DiversityMetrics(
                metrics.NumCategories, metrics.PatchRichnessDensity, metrics.ShannonsDiversityIndex,
                metrics.SimpsonsDiversityIndex, metrics.ModifiedSimpsonsDiversityIndex,
                metrics.ShannonsEvennessIndex, metrics.SimpsonsEvennessIndex, metrics.ModifiedSimpsonsEvennessIndex);

            _landscapeAndMetrics.Add(metricsObj);

        }



        /// <summary>
        /// This function is used to choose indexes we need
        /// </summary>
        /// <param name="filter">The metrics we choose. We can customize the list concatenating with "|" </param>
        /// <returns>Returns a datatable with the selected metrics</returns>
        public DataTable filterMetrics(Options_Landscape_DiversityMetrics_V filter)
        {
            DataTable dt = new DataTable("Landscape_DiversityMetrics");

            if ((filter & Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsDiversityIndex) == Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsDiversityIndex)
            {
                dt.Columns.Add("ModifiedSimpsonsDiversityIndex", typeof(double));
            }
            if ((filter & Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsEvennessIndex) == Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsEvennessIndex)
            {
                dt.Columns.Add("ModifiedSimpsonsEvennessIndex", typeof(double));
            }
            if ((filter & Options_Landscape_DiversityMetrics_V.NumLandscapeCategories) == Options_Landscape_DiversityMetrics_V.NumLandscapeCategories)
            {
                dt.Columns.Add("NumLandscapeCategories", typeof(int));
            }
            if ((filter & Options_Landscape_DiversityMetrics_V.PatchRichnessDensity) == Options_Landscape_DiversityMetrics_V.PatchRichnessDensity)
            {
                dt.Columns.Add("PatchRichnessDensity", typeof(double));
            }
            if ((filter & Options_Landscape_DiversityMetrics_V.ShannonsDiversityIndex) == Options_Landscape_DiversityMetrics_V.ShannonsDiversityIndex)
            {
                dt.Columns.Add("ShannonsDiversityIndex", typeof(double));
            }
            if ((filter & Options_Landscape_DiversityMetrics_V.ShanonsEvennessIndex) == Options_Landscape_DiversityMetrics_V.ShanonsEvennessIndex)
            {
                dt.Columns.Add("ShannonsEvennessIndex", typeof(double));
            }
            if ((filter & Options_Landscape_DiversityMetrics_V.SimpsonsDiversityIndex) == Options_Landscape_DiversityMetrics_V.SimpsonsDiversityIndex)
            {
                dt.Columns.Add("SimpsonsDiversityIndex", typeof(double));
            }
            if ((filter & Options_Landscape_DiversityMetrics_V.SimpsonsEvennessIndex) == Options_Landscape_DiversityMetrics_V.SimpsonsEvennessIndex)
            {
                dt.Columns.Add("SimpsonsEvennessIndex", typeof(double));
            }

            if ((filter & Options_Landscape_DiversityMetrics_V.All) == Options_Landscape_DiversityMetrics_V.All)
            {

                    dt.Columns.Add("ModifiedSimpsonsDiversityIndex", typeof(double));
                    dt.Columns.Add("ModifiedSimpsonsEvennessIndex", typeof(double));
                    dt.Columns.Add("NumLandscapeCategories", typeof(int));
                    dt.Columns.Add("PatchRichnessDensity", typeof(double));
                    dt.Columns.Add("ShannonsDiversityIndex", typeof(double));
                    dt.Columns.Add("ShannonsEvennessIndex", typeof(double));
                    dt.Columns.Add("SimpsonsDiversityIndex", typeof(double));
                    dt.Columns.Add("SimpsonsEvennessIndex", typeof(double));

            }






            for(int i =0; i<_landscapeAndMetrics.Count;i++)
            {
                ArrayList _buffer = new ArrayList();

                if ((filter & Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsDiversityIndex) == Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsDiversityIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ModifiedSimpsonsDiversityIndex);
                }

                if ((filter & Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsEvennessIndex) == Options_Landscape_DiversityMetrics_V.ModifiedSimpsonsEvennessIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ModifiedSimpsonsEvennessIndex);
                }
                if ((filter & Options_Landscape_DiversityMetrics_V.NumLandscapeCategories) == Options_Landscape_DiversityMetrics_V.NumLandscapeCategories)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).NumLandscapeCategories);
                }
                if ((filter & Options_Landscape_DiversityMetrics_V.PatchRichnessDensity) == Options_Landscape_DiversityMetrics_V.PatchRichnessDensity)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).PatchRichnessDensity);
                }
                if ((filter & Options_Landscape_DiversityMetrics_V.ShannonsDiversityIndex) == Options_Landscape_DiversityMetrics_V.ShannonsDiversityIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ShannonsDiversityIndex);
                }
                if ((filter & Options_Landscape_DiversityMetrics_V.ShanonsEvennessIndex) == Options_Landscape_DiversityMetrics_V.ShanonsEvennessIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ShannonsEvennessIndex);
                }
                if ((filter & Options_Landscape_DiversityMetrics_V.SimpsonsDiversityIndex) == Options_Landscape_DiversityMetrics_V.SimpsonsDiversityIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).SimpsonsDiversityIndex);
                }
                if ((filter & Options_Landscape_DiversityMetrics_V.SimpsonsEvennessIndex) == Options_Landscape_DiversityMetrics_V.SimpsonsEvennessIndex)
                {
                    _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).SimpsonsEvennessIndex);
                }



                if ((filter & Options_Landscape_DiversityMetrics_V.All) == Options_Landscape_DiversityMetrics_V.All)
                {
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ModifiedSimpsonsDiversityIndex);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ModifiedSimpsonsEvennessIndex);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).NumLandscapeCategories);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).PatchRichnessDensity);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ShannonsDiversityIndex);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).ShannonsEvennessIndex);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).SimpsonsDiversityIndex);
                        _buffer.Add((_landscapeAndMetrics[i] as Obj_Landscape_DiversityMetrics).SimpsonsEvennessIndex);
                }




                dt.Rows.Add(_buffer.ToArray());


            }

            return dt;
        }



    }
}


