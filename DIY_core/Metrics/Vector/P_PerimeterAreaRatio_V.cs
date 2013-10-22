
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
    /// This class calculates the Perimeter-Area Ratio at the patch level --(PARA_p)-- Units: none
    /// </summary>
    public class P_PerimeterAreaRatio_V
    {
        /// <summary>
        /// Calculates the Perimeter-Area Ratio at the patch level --(PARA_p)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">A class field to identify the patches</param>
        public P_PerimeterAreaRatio_V(string filename, string epsg, int fieldIndex)
        {
            this.pPerimeterAreaRatio(filename, epsg, fieldIndex);
        }


        ArrayList _patchNames = new ArrayList();
        ArrayList _patchesPerimeterAreaRatio = new ArrayList();

        DataTable _pPerimeterAreaRatio_V = new DataTable("P_PerimeterAreaRatio_V");


        /// <summary>
        /// Gets a list of the patch names for a given field
        /// </summary>
        public ArrayList PatchNames
        {
            get { return _patchNames; }
        }

        /// <summary>
        /// Gets a list of the patch perimeter/area ratio indexes --(PARA_p)-- Units: none
        /// </summary>
        public ArrayList PatchesPerimeterAreaRatio
        {
            get { return _patchesPerimeterAreaRatio; }
        }

        /// <summary>
        /// Gets a list of patch names and perimeter-area ratios --(PARA_p)-- Units: none
        /// </summary>
        public DataTable P_Names_PerimeterAreaRatios_V
        {
            get { return _pPerimeterAreaRatio_V; }
        }



        private void pPerimeterAreaRatio(string filename, string epsg, int fieldIndex)
        {
            try
            {

                GeometryReader geometryCollection = new GeometryReader(filename, epsg);
                SchemaDB_Reader namesCollection = new SchemaDB_Reader(filename, fieldIndex);
                _patchNames = namesCollection.FieldValues;



                for (int i = 0; i < geometryCollection.Areas.Count; i++)
                {
                    _patchesPerimeterAreaRatio.Add((double)geometryCollection.Perimeters[i] / (double)geometryCollection.Areas[i]);
                }


                _pPerimeterAreaRatio_V.Columns.Add("PatchName", typeof(string));
                _pPerimeterAreaRatio_V.Columns.Add("PatchPerimeterAreaRatio", typeof(double));

                for (int i = 0; i < _patchNames.Count; i++)
                {
                    _pPerimeterAreaRatio_V.Rows.Add(_patchNames[i], _patchesPerimeterAreaRatio[i]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}


