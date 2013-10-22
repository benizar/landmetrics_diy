
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
    internal class Obj_Landscape_DiversityMetrics
    {

        public Obj_Landscape_DiversityMetrics(int numLandscapeCategories, double patchRichnessDensity,
            double shannonsDiversityIndex, double simpsonsDiversityIndex, double modifiedSimpsonsDiversityIndex,
            double shanonsEvennessIndex, double simpsonsEvennessIndex, double modifiedSimpsonsEvennessIndex)
        {
            this.numLandscapeCategories = numLandscapeCategories;
            this.patchRichnessDensity = patchRichnessDensity;
            this.shannonsDiversityIndex = shannonsDiversityIndex;
            this.simpsonsDiversityIndex = simpsonsDiversityIndex;
            this.modifiedSimpsonsDiversityIndex = modifiedSimpsonsDiversityIndex;
            this.shanonsEvennessIndex = shanonsEvennessIndex;
            this.simpsonsEvennessIndex = simpsonsEvennessIndex;
            this.modifiedSimpsonsEvennessIndex = modifiedSimpsonsEvennessIndex;
        }


        int numLandscapeCategories = 0;
        double patchRichnessDensity = 0;
        double shannonsDiversityIndex = 0;
        double simpsonsDiversityIndex = 0;
        double modifiedSimpsonsDiversityIndex = 0;
        double shanonsEvennessIndex = 0;
        double simpsonsEvennessIndex = 0;
        double modifiedSimpsonsEvennessIndex = 0;



        public int NumLandscapeCategories
        {
            get { return numLandscapeCategories; }
        }

        public double PatchRichnessDensity
        {
            get { return patchRichnessDensity; }
        }

        public double ShannonsDiversityIndex
        {
            get { return shannonsDiversityIndex; }
        }

        public double SimpsonsDiversityIndex
        {
            get { return simpsonsDiversityIndex; }
        }

        public double ModifiedSimpsonsDiversityIndex
        {
            get { return modifiedSimpsonsDiversityIndex; }
        }

        public double ShannonsEvennessIndex
        {
            get { return shanonsEvennessIndex; }
        }

        public double SimpsonsEvennessIndex
        {
            get { return simpsonsEvennessIndex; }
        }

        public double ModifiedSimpsonsEvennessIndex
        {
            get { return modifiedSimpsonsEvennessIndex; }
        }

    }
}


