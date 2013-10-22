
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
    /// This class calculates the shannon's diversity index at the landscape level --(SHDI_l)-- Units: none
    /// </summary>
    public class L_ShannonsDiversityIndex_V
    {

        /// <summary>
        /// Calculates the shannon's diversity index at the landscape level --(SHDI_l)-- Units: none
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        public L_ShannonsDiversityIndex_V(string filename, string epsg, int fieldIndex)
        {
            this.lShannonsDiversityIndex(filename, epsg, fieldIndex);
        }



        double _shannonsDiversityIndex = 0;



        /// <summary>
        /// Gets the landscape Shannon's diversity index --(SHDI_l)-- Units: none
        /// </summary>
        public double ShannonsDiversityIndex
        {
            get { return _shannonsDiversityIndex; }
        }



        private void lShannonsDiversityIndex(string filename, string epsg, int fieldIndex)
        {
            try 
            { 
                //Obtenemos un arraylist con toda la colección de registros del campo
                ArrayList fieldRows = new ArrayList();
                SchemaDB_Reader collect = new SchemaDB_Reader(filename, fieldIndex);
                fieldRows = collect.FieldValues;

                //obtenemos un arraylist con las categorias encontradas en el campo
                ArrayList categories = new ArrayList();
                SchemaDB_Reader categ = new SchemaDB_Reader();
                categories = categ.categories((ArrayList)fieldRows.Clone());


                C_LandscapePercent_V diversity = new C_LandscapePercent_V(filename, epsg, fieldIndex);


                double _shannonsDiversityIndexTemp = 0;

                for (int i = 0; i< diversity.CategoryNames.Count;i++)
                {
                    _shannonsDiversityIndexTemp += ((double)diversity.LandscapePercent[i]/100)*Math.Log(((double)diversity.LandscapePercent[i]/100));    
                }

                _shannonsDiversityIndex = _shannonsDiversityIndexTemp * -1;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }



    }
}


