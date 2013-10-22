
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

namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the available CoreArea metrics at the class level
    /// </summary>
    public class Category_CoreAreaMetrics_V
    {

        /// <summary>
        /// Calculates CoreAreaMetrics at the class level
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        /// <param name="depthOfEdge"></param>
        public Category_CoreAreaMetrics_V(string filename, string epsg, int fieldIndex, double depthOfEdge)
        {
            this.CategoryMetrics(filename, epsg, fieldIndex, depthOfEdge);
        }

        double totalArea = 0;
        ArrayList _categoryNames = new ArrayList();
        ArrayList _coreAreaPerCategory = new ArrayList();
        ArrayList _coreAreaPercentOfLandscape = new ArrayList();
        ArrayList _numDisjunctCoreAreasPerCategory = new ArrayList();
        ArrayList _disjunctCoreAreaDensityPerCategory = new ArrayList();
        

        /// <summary>
        /// Gets a list of the class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }
        /// <summary>
        /// Gets a list of the class core areas --(TCA_c)-- Units: meters
        /// </summary>
        public ArrayList TotalCoreArea
        {
            get { return _coreAreaPerCategory; }
        }
        /// <summary>
        /// Gets a list of the class core area percentage of landscape --(CPLAND_c)-- Units: percent
        /// </summary>
        public ArrayList CoreAreaPercentageOfLandscape
        {
            get { return _coreAreaPercentOfLandscape; }
        }
        /// <summary>
        /// Gets a list of the number of disjunct core areas by class --(NDCA_c)-- Units: none
        /// </summary>
        public ArrayList NumDisjunctCoreArea
        {
            get { return _numDisjunctCoreAreasPerCategory; }
        }
        /// <summary>
        /// Gets a list of the disjunct core area densities by class --(DCAD_c)-- Units: number per 100 Ha
        /// </summary>
        public ArrayList DisjunctCoreAreaDensity
        {
            get { return _disjunctCoreAreaDensityPerCategory; }
        }



        private void CategoryMetrics(string filename, string epsg, int fieldIndex, double depthOfEdge)
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


                //obtenemos todas las core-áreas de cada shape
                Patch_CoreAreaMetrics_V coreAreas = new Patch_CoreAreaMetrics_V(filename, epsg, fieldIndex, depthOfEdge);



                for (int i = 0; i < categories.Count; i++)
                {
                    double categoryCoreArea = 0;
                    int numDisjunctCoreAreas = 0;

                    ArrayList categoryMembers = new ArrayList();
                    //cada iteración "i" obtenemos los miembros de una nueva categoria 
                    SchemaDB_Reader members = new SchemaDB_Reader();
                    categoryMembers = members.categoryMembers(fieldRows, (string)categories[i]);


                    //en cada iteración j obtenemos el área total de esa categoria
                    for (int j = 0; j < categoryMembers.Count; j++)
                    {
                        categoryCoreArea += (double)coreAreas.CoreAreas[(int)categoryMembers[j]];
                        numDisjunctCoreAreas += (int)coreAreas.NumCoreAreas[(int)categoryMembers[j]];
                    }

                    //añadimos la core-área de la categoría "i" al arraylist _coreAreasCategorias
                    //y volvemos a pones a 0 la variable de area de caregoria, para calcular
                    //una nueva categoria "i"

                    _coreAreaPerCategory.Add(categoryCoreArea);
                    _numDisjunctCoreAreasPerCategory.Add(numDisjunctCoreAreas);

                }

                ArrayList _pAreas = new ArrayList();
                P_Area_V areas = new P_Area_V(filename, epsg, fieldIndex);
                _pAreas = areas.PatchAreas;



                for (int i = 0; i < _pAreas.Count; i++)
                {
                    totalArea += (double)_pAreas[i];
                }



                for (int i = 0; i < _coreAreaPerCategory.Count; i++)
                {
                    _coreAreaPercentOfLandscape.Add(((double)_coreAreaPerCategory[i] / totalArea) * 100);
                    _disjunctCoreAreaDensityPerCategory.Add((((int)_numDisjunctCoreAreasPerCategory[i] / totalArea) * 10000) * 100);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


