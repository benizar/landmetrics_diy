
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
    /// This class calculates the patch richness density at the landscape level --(PRD_l)-- Units: number per 100 ha
    /// </summary>
    public class L_PatchRichnessDensity_V
    {

        /// <summary>
        /// Calculates DiversityMetrics at the landscape level --(PRD_l)-- Units: number per 100 ha
        /// </summary>
        /// <param name="filename">GIS filename with extension. Filetypes: shapefiles (shp) and soon more.</param>
        /// <param name="epsg">EPSG SRID. European Petroleum Survey Group - Spatial Reference IDentifier</param>
        /// <param name="fieldIndex">The class field to use</param>
        public L_PatchRichnessDensity_V(string filename, string epsg, int fieldIndex)
        {
            this.lPatchRichnessDensity(filename, epsg, fieldIndex);
        }


        int _numLandscapeCategories = 0;
        double _patchRichnessDensity = 0;



        /// <summary>
        /// Gets the landscape patch richness density --(PRD_l)-- Units: number per 100 ha
        /// </summary>
        public double PatchRichnessDensity
        {
            get { return _patchRichnessDensity; }
        }



        private void lPatchRichnessDensity(string filename, string epsg, int fieldIndex)
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
                //landscape area is already in hectares
                _patchRichnessDensity = ((_numLandscapeCategories / totalArea.LandscapeArea))*100;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


    }
}


