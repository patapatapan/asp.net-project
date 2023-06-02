using funplay.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;

public class CodeBase : BaseClass
{
    public string EndCode { get { return Environment.NewLine; } }
    public string ApplicationName { get { return "funplay"; } }

    public string ModelsNameSapce { get { return $"{ApplicationName}.Models"; } }

    public string GetControllerFolderName(string areaName)
    {
        if (areaName == "空白") return "~/Controllers";
        return $"~/Areas/{areaName}/Controllers";
    }

    public string GetViewFolderName(string areaName, string controllerName)
    {
        if (controllerName.EndsWith("Controller") && !controllerName.EndsWith("_Controller"))
            controllerName = controllerName.Substring(0, controllerName.Length - 10);
        if (areaName == "空白") return $"~/Views/{controllerName}";
        return $"~/Areas/{areaName}/Views/{controllerName}";
    }

    public string MetadataFolderName { get { return "~/Models/MetadataModel"; } }

    public string RepositoryFolderName { get { return "~/Models/RepositoryModel"; } }

    public string GetMetadataClassName(string className)
    {
        return $"z_meta{className}";
    }

    public string GetControllerClassName(string controllerName)
    {
        return $"{controllerName}Controller";
    }

    public string GetViewClassName(string viewName)
    {
        return $"{viewName}";
    }

    public string GetRepositoryClassName(string className)
    {
        return $"z_repo{className}";
    }

    public string GetControllerFileName(string areaName, string controllerName)
    {
        string str_folder = GetControllerFolderName(areaName);
        return $"{str_folder}/{controllerName}Controller.cs";
    }

    public string GetViewFileName(string areaName, string controllerName, string viewName)
    {
        string str_folder = GetViewFolderName(areaName, controllerName);
        return $"{str_folder}/{viewName}.cshtml";
    }

    public string GetMetadataFileName(string className)
    {
        return $"{MetadataFolderName}/meta{className}.cs";
    }

    public string GetRepositoryFileName(string className)
    {
        return $"{RepositoryFolderName}/repo{className}.cs";
    }

    public List<string> GetAreaList()
    {
        List<string> model = new List<string>();
        model.Add("空白");
        string str_area_folder = "~/Areas";
        string file_path = HttpContext.Current.Server.MapPath(str_area_folder);
        var folders = Directory.GetDirectories(file_path);
        if (folders != null)
        {
            foreach (var folder in folders)
            {
                model.Add(Path.GetFileName(folder));
            }
        }
        return model;
    }

    public List<string> GetLayoutList()
    {
        List<string> model = new List<string>();
        string str_area_folder = "~/Views/Shared";
        string str_file_name = "";
        string file_path = HttpContext.Current.Server.MapPath(str_area_folder);
        var files = Directory.GetFiles(file_path);
        if (files != null)
        {
            foreach (var file in files)
            {
                str_file_name = Path.GetFileName(file);
                str_file_name = str_file_name.Substring(0, str_file_name.Length - 7);
                if (str_file_name != "Error") model.Add(str_file_name);
            }
        }
        return model;
    }

    public List<string> GetAreaControllerList(string areaName)
    {
        string str_folder = "~/Controllers";
        List<string> model = new List<string>();
        if (areaName != "空白") str_folder = $"~/Areas/{areaName}/Controllers";
        string str_file_name = "";
        string file_path = HttpContext.Current.Server.MapPath(str_folder);
        var files = Directory.GetFiles(file_path);
        if (files != null)
        {
            foreach (var file in files)
            {
                str_file_name = Path.GetFileName(file);
                if (str_file_name.Contains("Controller.cs"))
                {
                    if (str_file_name != "HomeController.cs")
                    {
                        str_file_name = str_file_name.Substring(0, str_file_name.Length - 3);
                        model.Add(str_file_name);
                    }
                }
            }
        }
        return model;
    }

    public List<string> GetControllerList()
    {
        var result = Assembly.GetExecutingAssembly()
        .GetTypes()
            .Where(type => typeof(Controller).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(m => !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any())
            .GroupBy(x => x.DeclaringType.Name)
            .Select(x => new { Controller = x.Key })
            .ToList();
        List<string> model = new List<string>();
        foreach (var item in result) { model.Add(item.Controller); }
        return model;
    }

    public List<string> GetActionList(string controllerName)
    {
        var result = Assembly.GetExecutingAssembly()
        .GetTypes()
            .Where(type => typeof(Controller).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(m => !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any())
            .GroupBy(x => x.DeclaringType.Name)
            .Select(x => new { Controller = x.Key, Actions = x.Select(s => s.Name).ToList() })
            .ToList();

        var actions = result.Where(m => m.Controller == controllerName)
                .Select(u => u.Actions).FirstOrDefault();

        List<string> model = new List<string>();
        foreach (var item in actions)
        {
            var data = model.Where(m => m.Equals(item)).FirstOrDefault();
            if (data == null) model.Add(item);
        }
        return model;
    }

    public dmColumnProperty GetPropertyType(string columnName, string typeName, string fullName)
    {
        dmColumnProperty property = new dmColumnProperty();
        property.ColumnName = columnName;
        if (typeName == "Nullable`1")
        {
            //System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
            property.AllowNull = "是";
            int int_start = fullName.IndexOf("[[System.") + 9;
            int int_end = fullName.IndexOf(",");
            int int_leng = int_end - int_start;
            property.ColumnType = fullName.Substring(int_start, int_leng);
            property.FullType = "";
            string str_typeName = fullName.Substring(int_start, int_leng);
            if (str_typeName == "Int32") property.FullType = "Nullable<int>";
            if (str_typeName == "Boolean") property.FullType = "Nullable<bool>";
            if (str_typeName == "DateTime") property.FullType = "Nullable<System.DateTime>";
            if (str_typeName == "Decimal") property.FullType = "Nullable<decimal>";
            if (string.IsNullOrEmpty(property.FullType)) property.FullType = $"Nullable<{str_typeName.ToLower()}>";
        }
        else
        {
            property.AllowNull = "否";
            string str_column_type = "";
            if (typeName == "Int32") str_column_type = "int";
            if (typeName == "Boolean") str_column_type = "bool";
            if (typeName == "DateTime") str_column_type = "DateTime";
            if (string.IsNullOrEmpty(str_column_type)) str_column_type = typeName.ToLower();
            property.ColumnType = str_column_type;
            property.FullType = str_column_type;
        }
        return property;
    }

    public Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return
          assembly.GetTypes()
                  .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                  .ToArray();
    }

    public List<string> NameSpaceClasses()
    {
        List<string> values = new List<string>();
        Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), ModelsNameSapce);
        if (typelist != null) values = typelist.Select(x => x.Name).ToList();
        return values;
    }

    public List<dmColumnProperty> GetClassPropertyList(string className)
    {
        return GetClassPropertyList(className, "z_meta");
    }

    public List<dmColumnProperty> GetClassPropertyList(string className, string codeName)
    {
        bool bln_hidden = false;
        bool bln_key = false;
        bool bln_required = false;
        bool bln_checkbox = false;
        string str_display = "";
        string str_dropdown = "";
        string str_format = "";
        string str_default = "";
        List<dmColumnProperty> result = new List<dmColumnProperty>();
        string str_full_name = $"{ModelsNameSapce}.{className}";
        string str_meta_name = $"{codeName}{className}";
        if (string.IsNullOrEmpty(codeName)) str_meta_name = str_full_name;
        PropertyInfo[] myPropertyInfo = null;
        PropertyInfo[] myMetadataInfo = null;
        if (Type.GetType(str_full_name) != null) myPropertyInfo = Type.GetType(str_full_name).GetProperties();
        if (Type.GetType(str_meta_name) != null) myMetadataInfo = Type.GetType(str_meta_name).GetProperties();
        if (myPropertyInfo != null)
        {
            foreach (var item in myPropertyInfo)
            {
                bln_hidden = false;
                bln_key = false;
                bln_required = false;
                bln_checkbox = false;
                str_display = "";
                str_format = "";
                str_dropdown = "";
                str_default = "";
                if (myMetadataInfo != null)
                {
                    PropertyInfo metaName = myMetadataInfo.Where(m => m.Name == item.Name).FirstOrDefault();
                    if (metaName != null)
                    {
                        DisplayAttribute display = (DisplayAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayAttribute));
                        KeyAttribute key = (KeyAttribute)Attribute.GetCustomAttribute(metaName, typeof(KeyAttribute));
                        RequiredAttribute required = (RequiredAttribute)Attribute.GetCustomAttribute(metaName, typeof(RequiredAttribute));
                        DisplayFormatAttribute format = (DisplayFormatAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayFormatAttribute));
                        ColumnAttribute column = (ColumnAttribute)Attribute.GetCustomAttribute(metaName, typeof(ColumnAttribute));
                        DefaultAttribute defaults = (DefaultAttribute)Attribute.GetCustomAttribute(metaName, typeof(DefaultAttribute));

                        bln_key = (key == null) ? false : true;
                        bln_required = (required == null) ? false : true;
                        str_display = (display == null) ? item.Name : display.Name;
                        str_format = (format == null) ? "" : format.DataFormatString;
                        if (column != null)
                        {
                            bln_hidden = column.Hidden;
                            bln_checkbox = column.CheckBox;
                            str_dropdown = column.DropdownClass;
                        }
                        if (defaults != null)
                        {
                            str_default = GetDefaultValue(defaults.DefaultValueType, defaults.DefaultValue).ToString();
                        }
                    }
                }
                if (item.Name == "Id" && bln_key == false) bln_key = true;
                dmColumnProperty prop = GetPropertyType(item.Name, item.PropertyType.Name, item.PropertyType.FullName);
                prop.IsHidden = bln_hidden;
                prop.IsKeyColumn = bln_key;
                prop.IsRequired = bln_required;
                prop.DisplayName = str_display;
                prop.DataFormat = str_format;
                prop.IsCheckBox = bln_checkbox;
                prop.DropdownClass = str_dropdown;
                prop.DefaultValue = str_default;
                result.Add(prop);
            }
        }
        return result;
    }



    public bool ControllerFileExists(string areaName, string controllerName)
    {
        string controllerFile = GetControllerFileName(areaName, controllerName);
        string controllerFulleName = HttpContext.Current.Server.MapPath(controllerFile);
        if (File.Exists(controllerFulleName)) return true;
        return false;
    }

    public bool ViewFileExists(string areaName, string controllerName, string viewName)
    {
        string viewFolder = GetViewFolderName(areaName, controllerName);
        string pathName = HttpContext.Current.Server.MapPath(viewFolder);
        if (!Directory.Exists(pathName)) Directory.CreateDirectory(pathName);
        string viewFile = GetViewFileName(areaName, controllerName, viewName);
        string viewFulleName = HttpContext.Current.Server.MapPath(viewFile);
        if (File.Exists(viewFulleName)) return true;
        return false;
    }

    public bool MetaFileExists(string className)
    {
        string metaFile = GetMetadataFileName(className);
        string metaFulleName = HttpContext.Current.Server.MapPath(metaFile);
        if (File.Exists(metaFulleName)) return true;
        return false;
    }

    public bool RepoFileExists(string className)
    {
        string metaFile = GetRepositoryFileName(className);
        string metaFulleName = HttpContext.Current.Server.MapPath(metaFile);
        if (File.Exists(metaFulleName)) return true;
        return false;
    }

    public bool CreateControllerFile(vmControllerModel model)
    {
        if (ControllerFileExists(model.AreaName, model.ControllerName)) return false;
        CreateViewFolder(model.AreaName, model.ControllerName);
        string controllerFile = GetControllerFileName(model.AreaName, model.ControllerName);
        string controllerFulleName = HttpContext.Current.Server.MapPath(controllerFile);
        using (FileStream fs = File.Create(controllerFulleName))
        {
            Byte[] datas = new UTF8Encoding(true).GetBytes(model.TextResult);
            fs.Write(datas, 0, datas.Length);
        }
        return true;
    }

    public void CreateViewFolder(string areaName, string controllerName)
    {
        string viewFolder = GetViewFolderName(areaName, controllerName);
        string viewFulleName = HttpContext.Current.Server.MapPath(viewFolder);
        if (!Directory.Exists(viewFulleName)) Directory.CreateDirectory(viewFulleName);
    }

    public bool CreateViewFile(vmViewModel model)
    {
        if (ViewFileExists(model.AreaName, model.ControllerName, model.ViewName)) return false;
        string viewFile = GetViewFileName(model.AreaName, model.ControllerName, model.ViewName);
        string viewFulleName = HttpContext.Current.Server.MapPath(viewFile);
        using (FileStream fs = File.Create(viewFulleName))
        {
            Byte[] datas = new UTF8Encoding(true).GetBytes(model.TextResult);
            fs.Write(datas, 0, datas.Length);
        }
        return true;
    }

    public bool CreateMetadataFile(vmMetadataModel model)
    {
        if (MetaFileExists(model.ClassName)) return false;
        string metaFile = GetMetadataFileName(model.ClassName);
        string metaFulleName = HttpContext.Current.Server.MapPath(metaFile);
        using (FileStream fs = File.Create(metaFulleName))
        {
            Byte[] datas = new UTF8Encoding(true).GetBytes(model.TextResult);
            fs.Write(datas, 0, datas.Length);
        }
        return true;
    }

    public bool CreateRepositoryFile(vmRepositoryModel model)
    {
        if (RepoFileExists(model.ClassName)) return false;
        string repoFile = GetRepositoryFileName(model.ClassName);
        string repoFulleName = HttpContext.Current.Server.MapPath(repoFile);
        using (FileStream fs = File.Create(repoFulleName))
        {
            Byte[] datas = new UTF8Encoding(true).GetBytes(model.TextResult);
            fs.Write(datas, 0, datas.Length);
        }
        return true;
    }

    public string GetMetadataSample(string className)
    {
        string key_column = "";
        string is_required = "";
        string display_name = "";
        string column_format = "";
        string str_value = "";
        string str_full_name = $"{ModelsNameSapce}.{className}";
        string str_meta_name = GetMetadataClassName(className);

        str_value += "using System;" + EndCode;
        str_value += "using System.Collections.Generic;" + EndCode;
        str_value += "using System.ComponentModel.DataAnnotations;" + EndCode;
        str_value += "using System.ComponentModel.DataAnnotations.Schema;" + EndCode;
        str_value += "using System.Linq;" + EndCode;
        str_value += "using System.Web;" + EndCode;
        str_value += $"namespace {ModelsNameSapce}" + EndCode;
        str_value += "{" + EndCode;
        str_value += $"    [MetadataType(typeof(z_meta{className}))]" + EndCode;
        str_value += $"    public partial class {className}";
        str_value += "    {" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "}" + EndCode;
        str_value += EndCode;
        str_value += $"public abstract class z_meta{className}";
        str_value += "{" + EndCode;
        PropertyInfo[] myPropertyInfo = Type.GetType(str_full_name).GetProperties();
        PropertyInfo[] myMetadataInfo = Type.GetType(str_meta_name).GetProperties();
        bool bln_hidden = false;
        foreach (var item in myPropertyInfo)
        {
            key_column = "";
            is_required = "";
            display_name = "";
            column_format = "";
            bln_hidden = false;
            PropertyInfo metaName = myMetadataInfo.Where(m => m.Name == item.Name).FirstOrDefault();
            if (metaName != null)
            {
                object[] attrs = metaName.GetCustomAttributes(true);
                if (attrs != null && attrs.Length > 0)
                {
                    foreach (object attr in attrs)
                    {
                        if (attr.GetType().Name == "HiddenAttribute") bln_hidden = true;
                    }
                }

                DisplayAttribute display = (DisplayAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayAttribute));
                KeyAttribute key = (KeyAttribute)Attribute.GetCustomAttribute(metaName, typeof(KeyAttribute));
                RequiredAttribute required = (RequiredAttribute)Attribute.GetCustomAttribute(metaName, typeof(RequiredAttribute));
                DisplayFormatAttribute format = (DisplayFormatAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayFormatAttribute));

                key_column = (key == null) ? "" : "是";
                is_required = (required == null) ? "否" : "是";
                display_name = (display == null) ? item.Name : display.Name;
                column_format = (format == null) ? "" : format.DataFormatString;
            }
            if (key_column == "是")
            {
                str_value += @"
    [Key]
";
            }
            str_value += @"
    [Display(Name = """;
            str_value += display_name;
            str_value += @""")]
";
            if (is_required == "是")
            {
                str_value += @"
    [Required(ErrorMessage = ""不可空白!!"")]
";
            }
            if (!string.IsNullOrEmpty(column_format))
            {
                str_value += @"
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = """;
                str_value += column_format;
                str_value += @""")]
";
            }
            str_value += @"
    public 
";
            str_value += item.DeclaringType.ToString();
            str_value += $" {item.Name} ";
            str_value += @"{ get; set; }
";
        }
        return str_value;
    }

    public object GetDefaultValue(enDefaultValueType defaultType, string defaultValue)
    {
        string str_name = Enum.GetName(typeof(enDefaultValueType), defaultType);
        if (str_name.Contains("String")) return GetDefaultStringValue(defaultType, defaultValue);
        if (str_name.Contains("Int")) return GetDefaultIntValue(defaultType, defaultValue);
        if (str_name.Contains("Decimal")) return GetDefaultDecimalValue(defaultType, defaultValue);
        if (str_name.Contains("Boolean")) return GetDefaultBooleanValue(defaultType, defaultValue);
        if (str_name.Contains("Date")) return GetDefaultDateTimeValue(defaultType, defaultValue);
        return defaultValue;
    }

    public string GetDefaultStringValue(enDefaultValueType defaultType, string defaultValue)
    {
        if (defaultType == enDefaultValueType.String_Custom) return defaultValue;
        if (defaultType == enDefaultValueType.String_CompanyName) return AppService.CompanyName;
        if (defaultType == enDefaultValueType.String_CompanyNo) return AppService.CompanyNo;
        if (defaultType == enDefaultValueType.String_CompanyShortName) return AppService.CompanyShortName;
        if (defaultType == enDefaultValueType.String_N) return "N";
        if (defaultType == enDefaultValueType.String_RoleName) return UserService.RoleName;
        if (defaultType == enDefaultValueType.String_RoleNo) return UserService.RoleNo;
        if (defaultType == enDefaultValueType.String_UserName) return UserService.UserName;
        if (defaultType == enDefaultValueType.String_UserNo) return UserService.UserNo;
        if (defaultType == enDefaultValueType.String_Y) return "Y";
        if (defaultType == enDefaultValueType.String_Guid) return Guid.NewGuid().ToString().Replace("-", "");
        if (defaultType == enDefaultValueType.String_Space) return "";
        return defaultValue;
    }
    public int GetDefaultIntValue(enDefaultValueType defaultType, string defaultValue)
    {
        int int_value = 0;
        if (!int.TryParse(defaultValue, out int_value)) int_value = 0;
        if (defaultType == enDefaultValueType.Int_Custom) return int_value;
        if (defaultType == enDefaultValueType.Int_0) return 0;
        if (defaultType == enDefaultValueType.Int_100) return 100;
        if (defaultType == enDefaultValueType.Int_1) return 1;
        if (defaultType == enDefaultValueType.Int_10) return 10;
        if (defaultType == enDefaultValueType.Int_Year) return DateTime.Today.Year;
        if (defaultType == enDefaultValueType.Int_Month) return DateTime.Today.Month;
        if (defaultType == enDefaultValueType.Int_Day) return DateTime.Today.Day;

        return int_value;
    }
    public decimal GetDefaultDecimalValue(enDefaultValueType defaultType, string defaultValue)
    {
        decimal dec_value = 0;
        if (!decimal.TryParse(defaultValue, out dec_value)) dec_value = 0;
        if (defaultType == enDefaultValueType.Decimal_Custom) return dec_value;
        if (defaultType == enDefaultValueType.Decimal_0) return 0;
        if (defaultType == enDefaultValueType.Decimal_100) return 100;
        if (defaultType == enDefaultValueType.Decimal_1) return 1;
        if (defaultType == enDefaultValueType.Decimal_10) return 10;
        return dec_value;
    }
    public bool GetDefaultBooleanValue(enDefaultValueType defaultType, string defaultValue)
    {
        bool bln_value = false;
        if (!bool.TryParse(defaultValue, out bln_value)) bln_value = false;
        if (defaultType == enDefaultValueType.Boolean_False) return bln_value;
        if (defaultType == enDefaultValueType.Boolean_False) return false;
        if (defaultType == enDefaultValueType.Boolean_True) return true;
        return bln_value;
    }
    public DateTime GetDefaultDateTimeValue(enDefaultValueType defaultType, string defaultValue)
    {
        DateTime dtm_value = DateTime.MinValue;
        if (!DateTime.TryParse(defaultValue, out dtm_value)) dtm_value = DateTime.MinValue;
        if (defaultType == enDefaultValueType.Date_Custom) return dtm_value;
        if (defaultType == enDefaultValueType.Date_AddDays)
        {
            int int_days = 0;
            if (!int.TryParse(defaultValue, out int_days)) int_days = 0;
            return DateTime.Today.AddDays(int_days);
        }
        if (defaultType == enDefaultValueType.Date_NextMonthFirst) return DateTime.Today.ezMonthFirstDate().AddMonths(1);
        if (defaultType == enDefaultValueType.Date_NextMonthLast) return DateTime.Today.ezMonthFirstDate().AddMonths(1).ezMonthLastDate();
        if (defaultType == enDefaultValueType.Date_NextYearFirst) return DateTime.Today.ezYearFirstDate().AddYears(1);
        if (defaultType == enDefaultValueType.Date_NextYearLast) return DateTime.Today.ezYearLastDate().AddYears(1);
        if (defaultType == enDefaultValueType.Date_Now) return DateTime.Now;
        if (defaultType == enDefaultValueType.Date_PriorMonthFirst) return DateTime.Today.ezMonthFirstDate().AddMonths(-1);
        if (defaultType == enDefaultValueType.Date_PriorMonthLast) return DateTime.Today.ezMonthFirstDate().AddMonths(-1).ezMonthLastDate();
        if (defaultType == enDefaultValueType.Date_PriorYearFirst) return DateTime.Today.ezYearFirstDate().AddYears(-1);
        if (defaultType == enDefaultValueType.Date_PriorYearLast) return DateTime.Today.ezYearLastDate().AddYears(-1);
        if (defaultType == enDefaultValueType.Date_ThisMonthFirst) return DateTime.Today.ezMonthFirstDate();
        if (defaultType == enDefaultValueType.Date_ThisMonthLast) return DateTime.Today.ezMonthLastDate();
        if (defaultType == enDefaultValueType.Date_ThisYearFirst) return DateTime.Today.ezYearFirstDate();
        if (defaultType == enDefaultValueType.Date_ThisYearLast) return DateTime.Today.ezYearLastDate();
        if (defaultType == enDefaultValueType.Date_Today) return DateTime.Today;
        return dtm_value;
    }
}