
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


namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the available AreaDensEdge metrics at the patch level
    /// </summary>
    public class Patch_AreaDensEdge_V
    {
        /// <summary>
        /// Calculates AreaDensEdge metrics at the patch level
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">A class field to identify the patches</param>
        public Patch_AreaDensEdge_V(string filename, string epsg, int fieldIndex)
        {
            this.patchMetrics(filename, epsg, fieldIndex);
        }


        ArrayList _patchNames = new ArrayList();
        ArrayList _patchAreas = new ArrayList();
        ArrayList _patchPerimeters = new ArrayList();


        /// <summary>
        /// Gets a list of the patch names for a given field
        /// </summary>
        public ArrayList PatchNames
        {
            get { return _patchNames; }
        }

        /// <summary>
        /// Gets a list of the patch areas --(AREA_p)-- Units: meters
        /// </summary>
        public ArrayList PatchAreas
        {
            get { return _patchAreas; }
        }

        /// <summary>
        /// Gets the patch perimeters --(PERIM_p)-- Units: meters
        /// </summary>
        public ArrayList PatchPerimeters
        {
            get { return _patchPerimeters; }
        }



        private void patchMetrics(string filename, string epsg,int fieldIndex)
        {
            try
            {
                //obtenemos todos los índices de cada shape
                GeometryReader geometryCollection = new GeometryReader(filename, epsg);
                SchemaDB_Reader namesCollection = new SchemaDB_Reader(filename, fieldIndex);


                _patchNames = namesCollection.FieldValues;
                _patchAreas = geometryCollection.Areas;
                _patchPerimeters = geometryCollection.Perimeters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


