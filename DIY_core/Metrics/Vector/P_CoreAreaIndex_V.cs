
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
    /// This class calculates the Core Area Index at the patch level --(CAI_p)-- Units: Percent
    /// </summary>
    public class P_CoreAreaIndex_V
    {

        /// <summary>
        /// Calculates the Core Area Index at the patch level --(CAI_p)-- Units: Percent
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">A class field to identify the patches</param>
        /// <param name="depthOfEdge">A given negative edge buffer</param>
        public P_CoreAreaIndex_V(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            this.pCoreAreaIndex(filename, epsg, fieldIndex, depthOfEdge);
        }

        ArrayList _patchNames = new ArrayList();
        ArrayList _coreAreaIndex=new ArrayList();

        DataTable _pCoreAreaIndex_V = new DataTable("P_CoreAreaIndex_V");


        /// <summary>
        /// Gets a list of the patch names for a given field
        /// </summary>
        public ArrayList PatchNames
        {
            get { return _patchNames; }
        }


        /// <summary>
        /// Gets a list of the patch core area index --(CAI_p)-- Units: Percent
        /// </summary>
        public ArrayList CoreAreaIndex
        {
            get{return _coreAreaIndex;}
        }


        /// <summary>
        /// Gets a list of patch names and CoreAreaIndex --(CAI_p)-- Units: Percent
        /// </summary>
        public DataTable P_Names_CoreAreaIndex_V
        {
            get { return _pCoreAreaIndex_V; }
        }



        private void pCoreAreaIndex(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            try
            {

                SchemaDB_Reader namesCollection = new SchemaDB_Reader(filename, fieldIndex);
                _patchNames = namesCollection.FieldValues;


                GeometryCoreReader g = new GeometryCoreReader(filename, epsg, depthOfEdge);


                //el siguiente indice se calcula en porcentajes, por lo que las unidades deben
                //ser las mismas.
                for (int i = 0; i < g.Areas.Count; i++)
                {
                    _coreAreaIndex.Add(((double)g.CoreAreas[i] / (double)g.Areas[i]) * 100);
                }


                _pCoreAreaIndex_V.Columns.Add("PatchName", typeof(string));
                _pCoreAreaIndex_V.Columns.Add("PatchCoreAreaIndex", typeof(double));

                for (int i = 0; i < _patchNames.Count; i++)
                {
                    _pCoreAreaIndex_V.Rows.Add(_patchNames[i], _coreAreaIndex[i]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


