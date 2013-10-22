
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



namespace landmetrics_DIY_API.DIY_core.SchemaDB
{
    /// <summary>
    /// This class provides attributes and methods for asking the shapefile datatable
    /// </summary>
    public class SchemaDB_shapefiles:SchemaDB
    {

        public SchemaDB_shapefiles()
        { 
        }


        /// <summary>
        /// Gets the fieldnames from the datatable
        /// </summary>
        /// <param name="filename">The complete shapefile name</param>
        public SchemaDB_shapefiles(string filename)
        {
            this.fieldNames(filename);
        }

        /// <summary>
        /// Gets the fieldnames from the datatable and the values from the specified field.
        /// </summary>
        /// <param name="filename">The complete shapefile name</param>
        /// <param name="fieldIndex">The index of a field you want to read</param>
        public SchemaDB_shapefiles(string filename, int fieldIndex)
        {
            this.fieldNames(filename);
            this.fieldValues(filename, fieldIndex);
        }


        ArrayList _fieldNames = new ArrayList();
        ArrayList _fieldValues = new ArrayList();


        /// <summary>
        /// Gets a list of the field names
        /// </summary>
        public ArrayList FieldNames
        {
            get { return _fieldNames; }
        }


        /// <summary>
        /// Gets the field values of the specified field
        /// </summary>
        public ArrayList FieldValues
        {
            get { return _fieldValues; }
        }


        private void fieldNames(string filename)
        {
            try
            {
                GeometryFactory f = new GeometryFactory(new PrecisionModel(), 23030);
                using (ShapefileDataReader dr = new ShapefileDataReader(filename, f))
                {
                    if (dr.RecordCount > 0)
                    {

                        if (dr.ShapeHeader.ShapeType.ToString() == "Polygon")
                        {
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                _fieldNames.Add(dr.GetName(i));
                            }
                        }

                        else
                        {
                            throw new Exception("Geometry type is not polygon");
                        } 

                    }
                    else
                    {
                        throw new Exception("The selected shapefile does not contain any row");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void fieldValues(string filename, int fieldIndex)
        {
            try
            {

                GeometryFactory f = new GeometryFactory(new PrecisionModel(), 23030);
                using (ShapefileDataReader dr = new ShapefileDataReader(filename, f))
                {
                    while (dr.Read())
                    {
                        //obtenemos todos los valores del campo seleccionado
                        _fieldValues.Add(dr.GetValue(fieldIndex).ToString());
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}


