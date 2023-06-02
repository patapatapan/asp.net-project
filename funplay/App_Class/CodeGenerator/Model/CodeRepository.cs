using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

public partial class CodeGenerator : CodeBase
{
    public vmRepositoryModel GetRepositoryModel(vmCodeModel model)
    {
        vmRepositoryModel repoModel = new vmRepositoryModel();
        repoModel.Id = model.Id;
        repoModel.KeyColumn = model.KeyColumn;
        repoModel.ClassName = model.ClassName;
        repoModel.SortColumns = model.SortColumns;
        repoModel.NoColumn = model.NoColumn;
        repoModel.NameColumn = model.NameColumn;
        repoModel.FolderName = MetadataFolderName;
        repoModel.FileName = GetRepositoryFileName(model.ClassName);
        repoModel.TextResult = GetRepositoryClass(repoModel);
        return repoModel;
    }

    public string GetRepositoryClass(vmRepositoryModel model)
    {
        string str_value = "";
        int int_index = 0;
        string str_columns = "";
        string column_type = "";
        string str_class_name = GetRepositoryClassName(model.ClassName);
        List<string> whereList = new List<string>();
        string str_full_name = $"{ModelsNameSapce}.{model.ClassName}";
        PropertyInfo[] myPropertyInfo = Type.GetType(str_full_name).GetProperties();
        str_value += $"using {ModelsNameSapce};" + EndCode;
        str_value += "using System;" + EndCode;
        str_value += "using System.Collections.Generic;" + EndCode;
        str_value += "using System.Linq;" + EndCode;
        str_value += "using System.Web;" + EndCode;
        str_value += "using Dapper;" + EndCode;
        str_value += EndCode;
        str_value += "/// <summary>" + EndCode;
        str_value += $"/// {model.ClassName} CRUD" + EndCode;
        str_value += "/// </summary>" + EndCode;
        str_value += $"public class {str_class_name} : BaseClass" + EndCode;
        str_value += "{" + EndCode;
        str_value += "    #region 建構子及 CRUD" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// Repository 變數" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += $"    public IEFGenericRepository<{model.ClassName}> repo;" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 建構子" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += $"    public {str_class_name}()" + EndCode;
        str_value += "    {" + EndCode;
        str_value += $"        repo = new EFGenericRepository<{model.ClassName}>(new dbEntities());" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 以 Dapper 來讀取資料集合" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// <param name=\"searchText\">查詢條件</param>" + EndCode;
        str_value += "    /// <returns></returns>" + EndCode;
        str_value += $"    public List<{model.ClassName}> GetDapperDataList(string searchText)" + EndCode;
        str_value += "    {" + EndCode;
        str_value += "        using (DapperRepository dp = new DapperRepository())" + EndCode;
        str_value += "        {" + EndCode;
        str_value += "            string str_query = GetSQLSelect();" + EndCode;
        str_value += "            str_query += GetSQLWhere(searchText);" + EndCode;
        str_value += "            str_query += GetSQLOrderBy();" + EndCode;
        str_value += "            //DynamicParameters parm = new DynamicParameters();" + EndCode;
        str_value += "            //parm.Add(\"parmName\", \"parmValue\");" + EndCode;
        str_value += $"            var model = dp.ReadAll<{model.ClassName}>(str_query);" + EndCode;
        str_value += "            return model;" + EndCode;
        str_value += "        }" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 取得 SQL 欄位及表格名稱" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// <returns></returns>" + EndCode;
        str_value += "    private string GetSQLSelect()" + EndCode;
        str_value += "    {" + EndCode;
        str_value += "        string str_query = @\"" + EndCode;
        str_value += "SELECT " + EndCode;
        int_index = 0;
        foreach (var item in myPropertyInfo)
        {
            if (!string.IsNullOrEmpty(str_columns)) str_columns += ", ";
            str_columns += item.Name;
            int_index++;
            if (int_index % 5 == 0) str_columns += EndCode;
            var prop = GetPropertyType(item.Name, item.PropertyType.Name, item.PropertyType.FullName);
            column_type = prop.ColumnType;
            if (column_type == "string") whereList.Add(item.Name);
        }
        str_value += str_columns;
        str_value += $" FROM {model.ClassName} " + EndCode;
        str_value += "\";" + EndCode;
        str_value += "        return str_query;" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 取得 SQL 條件式" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// <param name=\"searchText\">查詢文字</param>" + EndCode;
        str_value += "    /// <returns></returns>" + EndCode;
        str_value += "    private string GetSQLWhere(string searchText)" + EndCode;
        str_value += "    {" + EndCode;
        str_value += "        string str_query = \"\";" + EndCode;
        str_value += "        if (!string.IsNullOrEmpty(searchText))" + EndCode;
        str_value += "        {" + EndCode;

        if (whereList.Count > 0)
        {
            str_value += "            str_query += \" WHERE (\";" + EndCode;
            int_index = 0;
            foreach (string colName in whereList)
            {
                int_index++;
                str_value += "            str_query += $\"";
                str_value += $"{colName} LIKE ";
                str_value += "'%{searchText}%' ";
                if (int_index == whereList.Count)
                    str_value += " \";";
                else
                    str_value += " OR \";";
                str_value += EndCode;
            }
            str_value += "            str_query += \") \";" + EndCode;
        }
        str_value += "        }" + EndCode;
        str_value += "        return str_query;" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 取得 SQL 排序" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// <returns></returns>" + EndCode;
        str_value += "    private string GetSQLOrderBy()" + EndCode;
        str_value += "    {" + EndCode;
        str_value += $"        return \" ORDER BY  {model.SortColumns}\";" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 新增或修改" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// <param name=\"model\"></param>" + EndCode;
        str_value += $"    public void CreateEdit({model.ClassName} model)" + EndCode;
        str_value += "    {" + EndCode;
        str_value += $"        repo.CreateEdit(model, model.{model.KeyColumn});" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 刪除" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// <param name=\"id\">Id</param>" + EndCode;
        str_value += "    public void Delete(int id)" + EndCode;
        str_value += "    {" + EndCode;
        str_value += $"        var model = repo.ReadSingle(m => m.{model.KeyColumn} == id);" + EndCode;
        str_value += "        if (model != null) repo.Delete(model, true);" + EndCode;
        str_value += "    }" + EndCode;
        if (!string.IsNullOrEmpty(model.NoColumn) && !string.IsNullOrEmpty(model.NameColumn))
        {
            str_value += "    /// <summary>" + EndCode;
            str_value += "    /// 取得名稱" + EndCode;
            str_value += "    /// <summary>" + EndCode;
            str_value += "    /// <param name=\"dataNo\">編號</param>" + EndCode;
            str_value += "    /// <returns></returns>" + EndCode;
            str_value += "    public string GetDataName(string dataNo)" + EndCode;
            str_value += "    {" + EndCode;
            str_value += "        string str_value = \"\";" + EndCode;
            str_value += $"        var model = repo.ReadSingle(m => m.{model.NoColumn} == dataNo);" + EndCode;
            str_value += $"        if (model != null) str_value = model.{model.NameColumn};" + EndCode;
            str_value += "        return str_value;" + EndCode;
            str_value += "    }" + EndCode;
        }
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// 檢查 Id 是否存在" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += "    /// <param name=\"id\">主鍵值</param>" + EndCode;
        str_value += "    /// <returns></returns>" + EndCode;
        str_value += "    public bool IdExists(int id)" + EndCode;
        str_value += "    {" + EndCode;
        str_value += $"        var model = repo.ReadSingle(m => m.{model.KeyColumn} == id);" + EndCode;
        str_value += "        return (model != null);" + EndCode;
        str_value += "    }" + EndCode;
        //str_value += "    /// <summary>" + EndCode;
        //str_value += "    /// 檢查是否重覆值" + EndCode;
        //str_value += "    /// <summary>" + EndCode;
        //str_value += "    /// <param name=\"id\">主鍵值</param>" + EndCode;
        //str_value += "    /// <param name=\"noValue\">編號值</param>" + EndCode;
        //str_value += "    /// <returns></returns>" + EndCode;
        //if (!string.IsNullOrEmpty(model.NoColumn))
        //{
        //    str_value += "    public bool NoExists(int id, string noValue)" + EndCode;
        //    str_value += "    {" + EndCode;
        //    str_value += $"        var model = repo.ReadSingle(m =>m.{model.KeyColumn} != id && m.{model.NoColumn} == noValue);" + EndCode;
        //    str_value += "        return (model != null);" + EndCode;
        //    str_value += "    }" + EndCode;
        //}
        //else
        //{
        //    str_value += "    //public bool NoExists(int id, string noValue)" + EndCode;
        //    str_value += "    //{" + EndCode;
        //    str_value += "    //    var model = repo.ReadSingle(m =>m.Id != id && m.No == noValue);" + EndCode;
        //    str_value += "    //    return (model != null);" + EndCode;
        //    str_value += "    //}" + EndCode;
        //}
        str_value += "    #endregion" + EndCode;
        str_value += "    #region 自定義事件及函數" + EndCode;
        str_value += "    #endregion" + EndCode;
        str_value += "}" + EndCode;
        return str_value;
    }
}