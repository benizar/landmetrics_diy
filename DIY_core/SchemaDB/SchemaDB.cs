
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

namespace landmetrics_DIY_API.DIY_core.SchemaDB
{
    public abstract class SchemaDB
    {

        public ArrayList categories(ArrayList values)
        {
            try
            {
                int i, j;

                for (i = 0; i < values.Count; i++)
                {
                    for (j = i + 1; j < values.Count; j++)
                    {
                        if (values[i].Equals(values[j]))
                        {
                            values.RemoveAt(j);
                            j = j - 1;
                        }
                    }
                }
                return values;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ArrayList categoryMembers(ArrayList values, string category)
        {
            ArrayList _categoryMembers = new ArrayList();

            int i = 0;

            for (i = 0; i < values.Count; i++)
            {
                if (values[i].Equals(category))
                {
                    _categoryMembers.Add(i);
                }
            }

            return _categoryMembers;

        }
    }
}


