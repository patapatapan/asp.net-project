using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class repoHtmlEditor : BaseClass
{
    public void UpdateHtmlText()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = $"UPDATE {ActionService.PriorTableName} ";
            str_query += $"SET {ActionService.PriorTextName} = @TextValue ";
            str_query += $"WHERE {ActionService.PriorKeyName} = @KeyValue";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("TextValue", ActionService.PriorTextValue);
            parm.Add("KeyValue", ActionService.PriorKeyValue);
            dp.Execute(str_query, parm);
        }
    }
}