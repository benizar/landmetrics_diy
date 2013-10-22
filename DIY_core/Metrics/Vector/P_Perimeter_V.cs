
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
using landmetrics_DIY_API.DIY_core.Geometry;
using landmetrics_DIY_API.DIY_core.SchemaDB;
using System.Data;


namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the perimeter at the patch level --(PERIM_p)-- Units: meters
    /// </summary>
    public class P_Perimeter_V
    {

        /// <summary>
        /// Calculates perimeter at the patch level --(PERIM_p)-- Units: meters
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">A class field to identify the patches</param>
        public P_Perimeter_V(string filename, string epsg, int fieldIndex)
        {
            this.patchMetrics(filename, epsg, fieldIndex);
        }


        ArrayList _patchNames = new ArrayList();
        ArrayList _patchPerimeters = new ArrayList();

        DataTable _pPerimeter_V = new DataTable("P_Perimeter_V");


        /// <summary>
        /// Gets a list of the patch names for a given field
        /// </summary>
        public ArrayList PatchNames
        {
            get { return _patchNames; }
        }


        /// <summary>
        /// Gets the patch perimeters --(PERIM_p)-- Units: meters
        /// </summary>
        public ArrayList PatchPerimeters
        {
            get { return _patchPerimeters; }
        }

        /// <summary>
        /// Gets a list of patch names and perimeters --(PERIM_p)-- Units: meters
        /// </summary>
        public DataTable P_Names_Perimeters_V
        {
            get { return _pPerimeter_V; }
        }



        private void patchMetrics(string filename, string epsg,int fieldIndex)
        {
            try
            {
                //obtenemos todos los índices de cada shape
                GeometryReader geometryCollection = new GeometryReader(filename, epsg);
                SchemaDB_Reader namesCollection = new SchemaDB_Reader(filename, fieldIndex);


                _patchNames = namesCollection.FieldValues;
                _patchPerimeters = geometryCollection.Perimeters;


                _pPerimeter_V.Columns.Add("PatchName", typeof(string));
                _pPerimeter_V.Columns.Add("PatchPerimeter", typeof(double));

                for (int i = 0; i < _patchNames.Count; i++)
                {
                    _pPerimeter_V.Rows.Add(_patchNames[i], _patchPerimeters[i]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


