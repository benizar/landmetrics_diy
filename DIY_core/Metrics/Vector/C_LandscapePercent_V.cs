﻿
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
    /// This class calculates the landscape percent occupied by each class --(PLAND_c)-- Units: percent
    /// </summary>
    public class C_LandscapePercent_V
    {

        /// <summary>
        /// Calculates the landscape percent occupied by each class --(PLAND_c)-- Units: percent
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier </param>
        /// <param name="fieldIndex">The class field to use</param>
        /// 
         public C_LandscapePercent_V(string filename, string epsg, int fieldIndex)
        {
            this.categoryMetrics(filename, epsg, fieldIndex);
        }


        double totalArea = 0;

        ArrayList _categoryNames = new ArrayList();
        ArrayList _totalAreasPerCategory = new ArrayList();
        ArrayList _landscapePercentPerCategory = new ArrayList();

        DataTable _cLandscapePercent_V = new DataTable("C_LandscapePercent_V");



        /// <summary>
        /// Gets a list of class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }
        
        /// <summary>
        /// Gets a list of landscape percent occupied by each class --(PLAND_c)-- Units: percent
        /// </summary>
        public ArrayList LandscapePercent
        {
            get { return _landscapePercentPerCategory; }
        }

        /// <summary>
        /// Gets a list of class names and landscape percentages --(PLAND_c)-- Units: percent
        /// </summary>
        public DataTable C_Names_LandscapePercent_V
        {
            get { return _cLandscapePercent_V; }
        }



        private void categoryMetrics(string filename, string epsg, int fieldIndex)
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

                //obtenemos todas las áreas de cada shape
                ArrayList areasCollectionArraylist = new ArrayList();
                GeometryReader geometryCollection = new GeometryReader(filename, epsg);



                for (int i = 0; i < categories.Count; i++)
                {

                    double categoryArea = 0;

                    //cada iteración "i" obtenemos los miembros de una nueva categoria 
                    SchemaDB_Reader members = new SchemaDB_Reader();
                    ArrayList categoryMembers=new ArrayList();

                    categoryMembers = members.categoryMembers(fieldRows, (string)categories[i]);


                    //en cada iteración j obtenemos el área total de esa categoria
                    for (int j = 0; j < categoryMembers.Count; j++)
                    {
                        categoryArea += (double)geometryCollection.Areas[(int)categoryMembers[j]];
                        
                    }

                    //añadimos el área de la categoría "i" al arraylist _areasCategorias
                    //y volvemos a pones a 0 la variable de area de caregoria, para calcular
                    //una nueva categoria "i"


                    _totalAreasPerCategory.Add(categoryArea);
                }


                for (int i = 0; i < _totalAreasPerCategory.Count; i++)
                {
                    totalArea += (double)_totalAreasPerCategory[i];
                }



                for (int i = 0; i < _totalAreasPerCategory.Count; i++)
                {
                    _landscapePercentPerCategory.Add(((double)_totalAreasPerCategory[i]/totalArea)*100);
                   
                }

                _cLandscapePercent_V.Columns.Add("CategoryName", typeof(string));
                _cLandscapePercent_V.Columns.Add("LandscapePercent", typeof(double));

                for (int i = 0; i < _categoryNames.Count; i++)
                {
                    _cLandscapePercent_V.Rows.Add(_categoryNames[i], _landscapePercentPerCategory[i]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


