
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
using GisSharpBlog.NetTopologySuite.IO;
using GisSharpBlog.NetTopologySuite.Geometries;
using System.Collections;

namespace landmetrics_DIY_API.DIY_core.Geometry
{
    internal class GeometryCore_shapefiles:IGeometryCore
    {


        public GeometryCore_shapefiles(string filename, string epsg, double depthOfEdge)
        {
            this.askGeometryCore(filename, epsg, depthOfEdge);
        }


        ArrayList _areas = new ArrayList();
        ArrayList _coreAreas = new ArrayList();
        ArrayList _numCoreAreas = new ArrayList();



        public ArrayList Areas
        {
            get { return _areas; }
        }


        public ArrayList CoreAreas
        {
            get { return _coreAreas; }
        }

        public ArrayList NumCoreAreas
        {
            get { return _numCoreAreas; }
        }



        private void askGeometryCore(string filename, string epsg, double depthOfEdge)
        {
            try
            {
                GeometryFactory f = new GeometryFactory(new PrecisionModel(), Convert.ToInt16(epsg));
                using (ShapefileDataReader dr = new ShapefileDataReader(filename, f))
                {
                    if (dr.RecordCount > 0)
                    {

                        if (dr.ShapeHeader.ShapeType.ToString() == "Polygon")
                        {

                            while (dr.Read())
                            {
                                _areas.Add(dr.Geometry.Area);
                                _coreAreas.Add(dr.Geometry.Buffer(depthOfEdge).Area);
                                _numCoreAreas.Add(dr.Geometry.Buffer(depthOfEdge).NumGeometries);
                            }
                        }

                        else
                        {
                            throw new Exception("Geometry type is not polygon");
                        }

                    }
                    else
                    {
                        throw new Exception("The selected shapefile does not contain any row");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}


