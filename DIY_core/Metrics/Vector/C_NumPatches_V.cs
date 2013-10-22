
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
    /// This class calculates the num of patches at the class level --(NP_c)-- Units: none
    /// </summary>
    public class C_NumPatches_V
    {

        /// <summary>
        /// Calculates the num of patches at the class level --(NP_c)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier </param>
        /// <param name="fieldIndex">The class field to use</param>
        public C_NumPatches_V(string filename, string epsg, int fieldIndex)
        {
            this.cNumPatches(filename, epsg, fieldIndex);
        }



        ArrayList _categoryNames = new ArrayList();
        ArrayList _numPatchesPerCategory = new ArrayList();

        DataTable _cNumPatchesPerCategory_V = new DataTable("C_NumPatchesPerCategory_V");




        /// <summary>
        /// Gets a list of class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }
        
        /// <summary>
        /// Gets a list of number of patches by each class --(NP_c)-- Units: none
        /// </summary>
        public ArrayList NumPatches
        {
            get { return _numPatchesPerCategory; }
        }

        /// <summary>
        /// Gets a list of class names and number of patches --(NP_c)-- Units: none
        /// </summary>
        public DataTable C_Names_NumPatchesPerCategory_V
        {
            get { return _cNumPatchesPerCategory_V; }
        }



        private void cNumPatches(string filename, string epsg, int fieldIndex)
        {
            try
            {
                //Obtenemos un arraylist con toda la colección de registros del campo
                ArrayList fieldRows = new ArrayList();
                SchemaDB_Reader colection = new SchemaDB_Reader(filename, fieldIndex);
                fieldRows = colection.FieldValues;

                //obtenemos un arraylist con las categorias encontradas en el campo
                ArrayList categories = new ArrayList();
                SchemaDB_Reader categ = new SchemaDB_Reader();
                categories = categ.categories((ArrayList)fieldRows.Clone());


                _categoryNames = categories;


                for (int i = 0; i < categories.Count; i++)
                {

                    //cada iteración "i" obtenemos los miembros de una nueva categoria 
                    SchemaDB_Reader members = new SchemaDB_Reader();
                    ArrayList categoryMembers=new ArrayList();

                    categoryMembers = members.categoryMembers(fieldRows, (string)categories[i]);


                    _numPatchesPerCategory.Add(categoryMembers.Count);

                }


                _cNumPatchesPerCategory_V.Columns.Add("CategoryName", typeof(string));
                _cNumPatchesPerCategory_V.Columns.Add("NumPatchesPerCategory", typeof(int));

                for (int i = 0; i <  _categoryNames.Count; i++)
                {
                    _cNumPatchesPerCategory_V.Rows.Add(_categoryNames[i], _numPatchesPerCategory[i]);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


