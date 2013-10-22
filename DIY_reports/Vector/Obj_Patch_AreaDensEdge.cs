
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
    internal class Obj_Patch_AreaDensEdge
    {

        //En esta clase creamos un objeto complejo a partir de todos los indices 
        //Este objeto dentro de un arrayList permite mostrar estructuradamente 
        //los resultados en un datagridview

        public Obj_Patch_AreaDensEdge(string patchName, double patchArea, double patchEdge)
        {
            this.patchName = patchName;
            this.patchArea = patchArea;
            this.patchEdge = patchEdge;
        }


        private string patchName;
        private double patchArea;
        private double patchEdge;



        public string PatchName
        {
            get { return patchName; }
        }

        public double PatchArea
        {
            get { return patchArea; }
        }

        public double PatchEdge
        {
            get { return patchEdge; }
        }


    }
}


