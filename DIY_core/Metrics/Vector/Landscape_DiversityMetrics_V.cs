
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
    /// This class calculates the available Diversity metrics at the landscape level
    /// </summary>
    public class Landscape_DiversityMetrics_V
    {
        /// <summary>
        /// Calculates DiversityMetrics at the landscape level
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        public Landscape_DiversityMetrics_V(string filename, string epsg, int fieldIndex)
        {
            this.landscapeMetrics(filename, epsg, fieldIndex);
        }


        int _numLandscapeCategories = 0;
        double _patchRichnessDensity = 0;
        //Relative Patch Richness -> el usuario debe especificar un núm máximo de
        //clases. Lo implementaremos si vale la pena.

        double _shannonsDiversityIndex = 0;
        double _simpsonsDiversityIndex = 0;
        double _modifiedSimpsonsDiversityIndex = 0;
        double _shannonsEvennessIndex = 0;
        double _simpsonsEvennessIndex = 0;
        double _modifiedSimpsonsEvennessIndex = 0;


        /// <summary>
        /// Gets the landscape number of classes --(NP_l)-- Units: none
        /// </summary>
        public int NumCategories
        {
            get { return _numLandscapeCategories; }
        }

        /// <summary>
        /// Gets the landscape patch richness density --(PRD_l)-- Units: number per 100 ha
        /// </summary>
        public double PatchRichnessDensity
        {
            get { return _patchRichnessDensity; }
        }

        /// <summary>
        /// Gets the landscape Shannon's diversity index --(SHDI_l)-- Units: none
        /// </summary>
        public double ShannonsDiversityIndex
        {
            get { return _shannonsDiversityIndex; }
        }

        /// <summary>
        /// Gets the landscape Simpson's diversity index --(SIDI_l)-- Units: none
        /// </summary>
        public double SimpsonsDiversityIndex
        {
            get { return _simpsonsDiversityIndex; }
        }

        /// <summary>
        /// Gets the landscape modified Simpson's diversity index --(MSIDI_l)-- Units: none
        /// </summary>
        public double ModifiedSimpsonsDiversityIndex
        {
            get { return _modifiedSimpsonsDiversityIndex; }
        }

        /// <summary>
        /// Gets the landscape Shannon's evenness index --(SHEI_l)-- Units: none
        /// </summary>
        public double ShannonsEvennessIndex
        {
            get { return _shannonsEvennessIndex; }
        }

        /// <summary>
        /// Gets the landscape Simpson's evenness index --(SIEI_l)-- Units: none
        /// </summary>
        public double SimpsonsEvennessIndex
        {
            get { return _simpsonsEvennessIndex; }
        }

        /// <summary>
        /// Gets the landscape modified Simpson's evenness index --(MSIEI_l)-- Units: none
        /// </summary>
        public double ModifiedSimpsonsEvennessIndex
        {
            get { return _modifiedSimpsonsEvennessIndex; }
        }



        private void landscapeMetrics(string filename, string epsg, int fieldIndex)
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

                _numLandscapeCategories = categories.Count;


                L_Area_V totalArea = new L_Area_V(filename, epsg);
                _patchRichnessDensity = ((_numLandscapeCategories / totalArea.LandscapeArea))*100;


                C_LandscapePercent_V diversity = new C_LandscapePercent_V(filename, epsg, fieldIndex);
                
                double _simpsonsDiversityIndexTemp=0;
                double _shannonsDiversityIndexTemp = 0;

                for (int i = 0; i< diversity.CategoryNames.Count;i++)
                {
                    _shannonsDiversityIndexTemp += ((double)diversity.LandscapePercent[i]/100)*Math.Log(((double)diversity.LandscapePercent[i]/100));
                    _simpsonsDiversityIndexTemp += Math.Pow(((double)diversity.LandscapePercent[i]/100), 2);
                    
                }
                _shannonsDiversityIndex = _shannonsDiversityIndexTemp * -1;
                _simpsonsDiversityIndex = 1 - _simpsonsDiversityIndexTemp;
                _modifiedSimpsonsDiversityIndex = -Math.Log(_simpsonsDiversityIndexTemp);
                _shannonsEvennessIndex = _shannonsDiversityIndex / Math.Log(_numLandscapeCategories);
                _simpsonsEvennessIndex = _simpsonsDiversityIndex / (1 - (1 / (double)_numLandscapeCategories));
                _modifiedSimpsonsEvennessIndex = _modifiedSimpsonsDiversityIndex / Math.Log(_numLandscapeCategories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


    }
}


