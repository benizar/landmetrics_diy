
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
    /// This class calculates the Core Area  at the patch level --(CORE_p)-- Units: meters
    /// </summary>
    public class P_CoreArea_V
    {

        /// <summary>
        /// Calculates Core Area at the patch level --(CORE_p)-- Units: meters
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">A class field to identify the patches</param>
        /// <param name="depthOfEdge">A given negative edge buffer</param>
        public P_CoreArea_V(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            this.patchMetrics(filename, epsg, fieldIndex, depthOfEdge);
        }

        ArrayList _patchNames = new ArrayList();
        ArrayList _coreAreas = new ArrayList();

        DataTable _pCoreArea_V = new DataTable("P_CoreArea_V");


        /// <summary>
        /// Gets a list of the patch names for a given field
        /// </summary>
        public ArrayList PatchNames
        {
            get { return _patchNames; }
        }

        /// <summary>
        /// Gets a list of the patch core areas --(CORE_p)-- Units: meters
        /// </summary>
        public ArrayList CoreAreas
        {
            get { return _coreAreas; }
        }

        /// <summary>
        /// Gets a list of patch names and Core Areas --(CORE_p)-- Units: meters
        /// </summary>
        public DataTable P_Names_CoreAreas_V
        {
            get { return _pCoreArea_V; }
        }


        private void patchMetrics(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            try
            {

                SchemaDB_Reader namesCollection = new SchemaDB_Reader(filename, fieldIndex);
                _patchNames = namesCollection.FieldValues;


                GeometryCoreReader g = new GeometryCoreReader(filename, epsg, depthOfEdge);

                //obtenemos las coreAreas divididas por 10.000 (en ha.)
                for (int i = 0; i < g.Areas.Count; i++)
                {
                    _coreAreas.Add((double)g.CoreAreas[i]); // 10000);
                }


                _pCoreArea_V.Columns.Add("PatchName", typeof(string));
                _pCoreArea_V.Columns.Add("PatchCoreArea", typeof(double));

                for (int i = 0; i < _patchNames.Count; i++)
                {
                    _pCoreArea_V.Rows.Add(_patchNames[i], _coreAreas[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


