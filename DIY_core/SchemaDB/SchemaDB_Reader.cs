
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

namespace landmetrics_DIY_API.DIY_core.SchemaDB
{
    /// <summary>
    /// This class recognizes the DB source type and calls the correspondent reader
    /// </summary>
    public class SchemaDB_Reader:SchemaDB
    {

        public SchemaDB_Reader()
        { 
        }
        
        /// <summary>
        /// Gets the fieldnames from the datatable
        /// </summary>
        /// <param name="filename">The complete GIS file name</param>
        public SchemaDB_Reader(string filename)
        {
            if (filename.Contains(".shp"))
            {
                this.askShpFieldNames(filename);
            }
            else
            {
                throw new Exception("Filetype not recognized"); 
            }
        }

        /// <summary>
        /// Gets the fieldnames from the datatable and the values from the specified field.
        /// </summary>
        /// <param name="filename">The complete GIS file name</param>
        /// <param name="fieldIndex">The index of a field you want to read</param>
        public SchemaDB_Reader(string filename, int fieldIndex)
        {
            if (filename.Contains(".shp"))
            {
                this.askShpFieldNamesValues(filename,fieldIndex);
            }
            else
            {
                throw new Exception("Filetype not recognized");
            }
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
        /// Gets a list of the specified field values
        /// </summary>
        public ArrayList FieldValues
        {
            get { return _fieldValues ;}
 

        }



        private void askShpFieldNames(string filename)
        {
            SchemaDB_shapefiles shp = new SchemaDB_shapefiles(filename);
            _fieldNames = shp.FieldNames;
        
        }


        private void askShpFieldNamesValues(string filename, int fieldIndex)
        {
            SchemaDB_shapefiles shp = new SchemaDB_shapefiles(filename, fieldIndex);
            
            _fieldNames = shp.FieldNames;
            _fieldValues = shp.FieldValues;
        }



    }
}


