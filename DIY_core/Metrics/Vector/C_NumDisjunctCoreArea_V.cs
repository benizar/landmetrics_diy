
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
    /// This class calculates the num of disjunct core areas at the class level --(NDCA_c)-- Units: none
    /// </summary>
    public class C_NumDisjunctCoreArea_V
    {

        /// <summary>
        /// Calculates the num of disjunct core areas at the class level --(NDCA_c)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        /// <param name="depthOfEdge"></param>
        public C_NumDisjunctCoreArea_V(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            this.cNumDisjunctCoreArea(filename, epsg, fieldIndex, depthOfEdge);
        }

        ArrayList _categoryNames = new ArrayList();
        ArrayList _numDisjunctCoreAreasPerCategory = new ArrayList();

        DataTable _cNumDisjunctCoreArea_V = new DataTable("C_NumDisjunctCoreArea_V");


        /// <summary>
        /// Gets a list of the class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }
        /// <summary>
        /// Gets a list of the number of disjunct core areas by class --(NDCA_c)-- Units: none
        /// </summary>
        public ArrayList NumDisjunctCoreArea
        {
            get { return _numDisjunctCoreAreasPerCategory; }
        }
        /// <summary>
        /// Gets a list of class names and the num of disjunct core areas --(NDCA_c)-- Units: none
        /// </summary>
        public DataTable C_Names_NumDisjunctCoreArea_V
        {
            get { return _cNumDisjunctCoreArea_V; }
        }



        private void cNumDisjunctCoreArea(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            try
            {
                //Obtenemos un arraylist con toda la colección de registros del campo
                ArrayList fieldRows = new ArrayList();
                SchemaDB_Reader collect = new SchemaDB_Reader(filename,fieldIndex);
                fieldRows = collect.FieldValues;

                //obtenemos un arraylist con las categorias encontradas en el campo
                ArrayList categories = new ArrayList();

                SchemaDB_Reader categ = new SchemaDB_Reader();
                categories =  categ.categories((ArrayList)fieldRows.Clone());

                _categoryNames = categories;


                //obtenemos el num de core areas de cada shape
                P_NumCoreAreas_V numCoreAreas=new P_NumCoreAreas_V(filename, epsg, fieldIndex, depthOfEdge);


                for (int i = 0; i < categories.Count; i++)
                {
                    int numDisjunctCoreAreas = 0;

                    ArrayList categoryMembers = new ArrayList();
                    //cada iteración "i" obtenemos los miembros de una nueva categoria 
                    SchemaDB_Reader members = new SchemaDB_Reader();
                    categoryMembers = members.categoryMembers(fieldRows, (string)categories[i]);


                    //en cada iteración j obtenemos el num de core areas de esa categoria
                    for (int j = 0; j < categoryMembers.Count; j++)
                    {
                        numDisjunctCoreAreas += (int)numCoreAreas.NumCoreAreas[(int)categoryMembers[j]];
                    }

                    //añadimos la core-área de la categoría "i" al arraylist _coreAreasCategorias
                    //y volvemos a pones a 0 la variable de area de caregoria, para calcular
                    //una nueva categoria "i"

                    _numDisjunctCoreAreasPerCategory.Add(numDisjunctCoreAreas);

                }


                _cNumDisjunctCoreArea_V.Columns.Add("CategoryName", typeof(string));
                _cNumDisjunctCoreArea_V.Columns.Add("CategoryNumDisjunctCoreArea", typeof(int));

                for (int i = 0; i < _categoryNames.Count; i++)
                {
                    _cNumDisjunctCoreArea_V.Rows.Add(_categoryNames[i], _numDisjunctCoreAreasPerCategory[i]);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


