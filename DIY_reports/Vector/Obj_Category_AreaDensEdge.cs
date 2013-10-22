
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

namespace landmetrics_DIY_API.DIY_reports.Vector
{
    internal class Obj_Category_AreaDensEdge
    {

        //En esta clase creamos un objeto complejo a partir de todos los indices 
        //Este objeto dentro de un arrayList
        //permite mostrar estructuradamente los resultados en un datagridview

        public Obj_Category_AreaDensEdge(string categoryName, double categoryArea,
            double landscapePercentPerCategory, int numPatchesPerCategory,
            double patchDensPerCategory, double totalEdgesPerCategory,
            double edgeDensPerCategory,
            double largestPatchIndexPerCategory,
            double largestPatchSizePerCategory, double meanPatchSizePerCategory)
        //

        {
            this.categoryName = categoryName;
            this.categoryArea = categoryArea;
            this.landscapePercentPerCategory = landscapePercentPerCategory;
            this.numPatchesPerCategory = numPatchesPerCategory;
            this.patchDensPerCategory = patchDensPerCategory;
            this.totalEdgesPerCategory = totalEdgesPerCategory;
            this.edgeDensPerCategory = edgeDensPerCategory;
            this.largestPatchIndexPerCategory = largestPatchIndexPerCategory;
            this.largestPatchSizePerCategory = largestPatchSizePerCategory;
            this.meanPatchSizePerCategory = meanPatchSizePerCategory;
        }


        string categoryName;
        double categoryArea = 0;
        double landscapePercentPerCategory = 0;
        int numPatchesPerCategory = 0;
        double patchDensPerCategory = 0;
        double totalEdgesPerCategory = 0;
        double edgeDensPerCategory = 0;
        double largestPatchIndexPerCategory = 0;
        double largestPatchSizePerCategory = 0;
        double meanPatchSizePerCategory = 0;



        public string CategoryName
        {
            get { return categoryName; }
        }

        public double CategoryArea
        {
            get { return categoryArea; }
        }

        public double LandscapePercentPerCategory
        {
            get { return landscapePercentPerCategory; }
        }

        public int NumPatchesPerCategory
        {
            get { return numPatchesPerCategory; }
        }

        public double PatchDensPerCategory
        {
            get { return patchDensPerCategory; }
        }

        public double TotalEdgesPerCategory
        {
            get { return totalEdgesPerCategory; }
        }

        public double EdgeDensPerCategory
        {
            get { return edgeDensPerCategory; }
        }


        public double LargestPatchIndexPerCategory
        {
            get { return largestPatchIndexPerCategory; }
        }

        public double LargestPatchSizePerCategory
        {
            get { return largestPatchSizePerCategory; }
        }

        public double MeanPatchSizePerCategory
        {
            get { return meanPatchSizePerCategory; }
        }


    }
}


