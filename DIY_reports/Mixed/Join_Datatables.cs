
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
using System.Data;


namespace landmetrics_DIY_API.DIY_reports.Mixed
{
    public class Join_Datatables
    {

        public static DataTable JoinTables(DataTable dtFirst, DataTable dtSecond, string CommonColumn)

        {
            DataTable dtResults = dtFirst.Clone();
            int count=0;
            for (int i = 0; i < dtSecond.Columns.Count; i++)
            {
                if (!dtFirst.Columns.Contains(dtSecond.Columns[i].ColumnName))
                {
                dtResults.Columns.Add(dtSecond.Columns[i].ColumnName, dtSecond.Columns[i].DataType);
                count++;
                }
            }

            DataColumn []columns = new DataColumn[count];
            int j = 0;
            for (int i = 0; i < dtSecond.Columns.Count; i++)
            {
                if (!dtFirst.Columns.Contains(dtSecond.Columns[i].ColumnName))
                {
                columns[j++] = new DataColumn(dtSecond.Columns[i].ColumnName, dtSecond.Columns[i].DataType);
                }
            }

            dtResults.BeginLoadData();
            foreach(DataRow dr in dtFirst.Rows)
            {
                dtResults.Rows.Add(dr.ItemArray);
            }
            foreach(DataRow dr in dtSecond.Rows)
            {
                foreach(DataRow dr1 in dtResults.Rows)
                {
                    if(dr1[CommonColumn].ToString().Equals(dr[CommonColumn].ToString()))
                    {
                        foreach(DataColumn c in columns)
                        {
                            dr1[c.ColumnName] = dr[c.ColumnName];
                        }
                    }
                }
            }
            dtResults.EndLoadData();
            return dtResults;
        }
    }
}


