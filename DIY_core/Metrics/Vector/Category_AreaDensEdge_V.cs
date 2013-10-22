
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


namespace landmetrics_DIY_API.DIY_core.Metrics.Vector
{
    /// <summary>
    /// This class calculates the available AreaDensEdge metrics at the class level
    /// </summary>
    public class Category_AreaDensEdge_V
    {
        /// <summary>
        /// Calculates AreaDensEdge metrics at the class level
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier </param>
        /// <param name="fieldIndex">The class field to use</param>
        public Category_AreaDensEdge_V(string filename, string epsg, int fieldIndex)
        {
            this.categoryMetrics(filename, epsg, fieldIndex);
        }


        double totalArea = 0;
        ArrayList _maxPerimeterPerCategory = new ArrayList();
        ArrayList _minPerimeterPerCategory = new ArrayList();
        ArrayList _categoryNames = new ArrayList();
        ArrayList _totalAreasPerCategory = new ArrayList();
        ArrayList _landscapePercentPerCategory = new ArrayList();
        ArrayList _numPatchesPerCategory = new ArrayList();
        ArrayList _patchDensPerCategory= new ArrayList();
        ArrayList _totalPerimetersPerCategory = new ArrayList();
        ArrayList _edgeDensPerCategory = new ArrayList();

        //Landscape Shape Index. 10 en Idefix//falta añadirlo a la lista!!!!
        //ArrayList _landscapeShapeIndexPerCategory = new ArrayList();

        ArrayList _largestPatchIndexPerCategory = new ArrayList();
        ArrayList _largestPatchSizePerCategory = new ArrayList();
        ArrayList _meanPatchSizePerCategory = new ArrayList();
        //Patch Size Standard Desv. 81 en Idefix





        /// <summary>
        /// Gets a list of class names
        /// </summary>
        public ArrayList CategoryNames
        {
            get { return _categoryNames; }
        }
        /// <summary>
        /// Gets a list of class areas -- (CA_c) -- Units: meters 
        /// </summary>
        public ArrayList TotalAreas
        {
            get { return _totalAreasPerCategory; }
        }
        /// <summary>
        /// Gets a list of landscape percent occupied by each class --(PLAND_c)-- Units: percent
        /// </summary>
        public ArrayList LandscapePercent
        {
            get { return _landscapePercentPerCategory; }
        }
        /// <summary>
        /// Gets a list of number of patches by each class --(NP_c)-- Units: none
        /// </summary>
        public ArrayList NumPatches
        {
            get { return _numPatchesPerCategory; }
        }
        /// <summary>
        /// Gets a list of the patch density by each class --(PD_c)-- Units: number per 100 ha
        /// </summary>
        public ArrayList PatchDens
        {
            get { return _patchDensPerCategory; }
        }
        /// <summary>
        /// Gets a list of the total edge length by each class --(TE_c)-- Units: meters
        /// </summary>
        public ArrayList TotalPerimeter
        {
            get { return _totalPerimetersPerCategory; }
        }
        /// <summary>
        /// Gets a list of the edge density by each class --(ED_c)-- Units: meters per hectare
        /// </summary>
        public ArrayList EdgeDens
        {
            get { return _edgeDensPerCategory; }
        }

        ///// <summary>
        ///// Gets a list of the landscape shape index by each class
        ///// </summary>
        //public ArrayList LandscapeShapeIndexPerCategory
        //{
        //    get { return _landscapeShapeIndexPerCategory; }
        //}


        /// <summary>
        /// Gets a list of the largest patch index by each class --(LPI_c)-- Units: percent
        /// </summary>
        public ArrayList LargestPatchIndex
        {
            get { return _largestPatchIndexPerCategory; }
        }
        /// <summary>
        /// Gets a list of the largest patch size by each class. Units: meters
        /// </summary>
        public ArrayList LargestPatchSize
        {
            get { return _largestPatchSizePerCategory; }
        }
        /// <summary>
        /// Gets a list of the mean patch size by each class --(MPS_c)-- Units: hectares
        /// </summary>
        public ArrayList MeanPatchSize
        {
            get { return _meanPatchSizePerCategory; }
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
                GeometryReader geometryCollection = new GeometryReader(filename, epsg);



                for (int i = 0; i < categories.Count; i++)
                {

                    double categoryArea = 0;
                    double categoryEdge = 0;
                    double largestPatchSize = 0;

                    //cada iteración "i" obtenemos los miembros de una nueva categoria 
                    SchemaDB_Reader members = new SchemaDB_Reader();
                    ArrayList categoryMembers=new ArrayList();

                    categoryMembers = members.categoryMembers(fieldRows, (string)categories[i]);


                    _numPatchesPerCategory.Add(categoryMembers.Count);


                    //en cada iteración j obtenemos el área total de esa categoria
                    for (int j = 0; j < categoryMembers.Count; j++)
                    {
                        categoryArea += (double)geometryCollection.Areas[(int)categoryMembers[j]];
                        categoryEdge += (double)geometryCollection.Perimeters[(int)categoryMembers[j]];


                        if ((double)geometryCollection.Areas[(int)categoryMembers[j]] > largestPatchSize)
                        {
                            largestPatchSize = (double)geometryCollection.Areas[(int)categoryMembers[j]];
                        }

                        
                    }

                    //añadimos el área de la categoría "i" al arraylist _areasCategorias
                    //y volvemos a pones a 0 la variable de area de caregoria, para calcular
                    //una nueva categoria "i"


                    _totalAreasPerCategory.Add(categoryArea);
                    _totalPerimetersPerCategory.Add(categoryEdge);
                    _largestPatchSizePerCategory.Add(largestPatchSize);
                }


                for (int i = 0; i < _totalAreasPerCategory.Count; i++)
                {
                    totalArea += (double)_totalAreasPerCategory[i];
                }

                //Calculo del mayor perimetro y la mayor area en todo el paisaje
                 //double maxEdgeTmp = 0;
                 //for (int i = 0; i < geometryCollection.Perimeters.Count; i++)
                 //{
                 //    if ((double)geometryCollection.Perimeters[i] > maxEdgeTmp)
                 //    {
                 //        maxEdgeTmp = (double)geometryCollection.Perimeters[i];
                 //    }
                 //   _maxPerimeterPerCategory.Add(maxEdgeTmp);
                 //}

                //Calculo del menor perimetro en todo el paisaje
                 
                 //for (int i = 0; i < geometryCollection.Perimeters.Count; i++)
                 //{
                 //    double minEdgeTmp = (double)_maxPerimeterPerCategory[i];
                 //    if ((double)geometryCollection.Perimeters[i] < minEdgeTmp)
                 //    {
                 //        minEdgeTmp = (double)geometryCollection.Perimeters[i];
                 //    }
                 //    _minPerimeterPerCategory.Add(minEdgeTmp);
                 //}
                 




                for (int i = 0; i < _totalAreasPerCategory.Count; i++)
                {
                    _landscapePercentPerCategory.Add(((double)_totalAreasPerCategory[i]/totalArea)*100);
                    _patchDensPerCategory.Add((((int)_numPatchesPerCategory[i] / totalArea)*10000)*100);
                    _edgeDensPerCategory.Add(((double)_totalPerimetersPerCategory[i] / totalArea) *10000);
                    //_landscapeShapeIndexPerCategory.Add((double)_totalPerimetersPerCategory[i]/(double)_minPerimeterPerCategory[i]);
                    _largestPatchIndexPerCategory.Add(((double)_largestPatchSizePerCategory[i] /totalArea)*100);
                    _meanPatchSizePerCategory.Add(((double)_totalAreasPerCategory[i] / (int)_numPatchesPerCategory[i]) / 10000);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


