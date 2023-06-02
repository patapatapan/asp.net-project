using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Linq;

public partial class CodeGenerator : CodeBase
{
    public vmMetadataModel GetMetadataModel(vmCodeModel model)
    {
        vmMetadataModel metaModel = new vmMetadataModel();
        metaModel.Id = model.Id;
        metaModel.KeyColumn = model.KeyColumn;
        metaModel.RequiredColumns = model.RequiredColumns;
        metaModel.ClassName = model.ClassName;
        metaModel.FolderName = MetadataFolderName;
        metaModel.FileName = GetMetadataFileName(model.ClassName);
        metaModel.TextResult = GetMetadataClass(metaModel);
        return metaModel;
    }

    public string GetMetadataClass(vmMetadataModel model)
    {
        using (z_repoPropertyNames propName = new z_repoPropertyNames())
        {
            string str_checkbox = "false";
            string str_hidden = "false";
            string str_default = "";
            string column_type = "";
            string str_value = "";
            string str_display = "";
            string str_message = "";
            string str_dropdown = "";
            string str_class_name = GetMetadataClassName(model.ClassName);
            string str_full_name = $"{ModelsNameSapce}.{model.ClassName}";
            List<string> requiredList = new List<string>();
            if (!string.IsNullOrEmpty(model.RequiredColumns))
            {
                requiredList = model.RequiredColumns.Split(',').ToList();
            }

            str_value += "using System;" + EndCode;
            str_value += "using System.Collections.Generic;" + EndCode;
            str_value += "using System.ComponentModel.DataAnnotations;" + EndCode;
            str_value += "using System.ComponentModel.DataAnnotations.Schema;" + EndCode;
            str_value += "using System.Linq;" + EndCode;
            str_value += "using System.Web;" + EndCode;
            str_value += EndCode;
            str_value += $"namespace {ModelsNameSapce} " + EndCode;
            str_value += "{" + EndCode;
            str_value += $"    [MetadataType(typeof({str_class_name}))]" + EndCode;
            str_value += $"    public partial class {model.ClassName}" + EndCode;
            str_value += "    {" + EndCode;
            str_value += "    }" + EndCode;
            str_value += "}" + EndCode;
            str_value += EndCode;
            str_value += $"public abstract class {str_class_name}" + EndCode;
            str_value += "{" + EndCode;
            PropertyInfo[] myPropertyInfo = Type.GetType(str_full_name).GetProperties();
            int int_index = 0;
            foreach (var item in myPropertyInfo)
            {
                str_default = "";
                str_checkbox = "false";
                var prop = GetPropertyType(item.Name, item.PropertyType.Name, item.PropertyType.FullName);
                column_type = prop.FullType;
                if (item.Name == model.KeyColumn)
                {
                    str_value += "    [Key]" + EndCode;
                }
                else
                {
                    str_display = propName.GetDataName(item.Name);
                    if (string.IsNullOrEmpty(str_display)) str_display = "屬性名稱";

                    str_value += $"    [Display(Name = \"{str_display}\")]" + EndCode;
                    if (requiredList.Count > 0)
                    {
                        if (requiredList.Exists(x => x == item.Name))
                        {
                            str_message = (str_display == "屬性名稱") ? "" : str_display;
                            str_value += $"    [Required(ErrorMessage = \"{str_message}不可空白!!\")]" + EndCode;
                        }
                    }

                    if (!string.IsNullOrEmpty(model.UniqueNoColumn) && item.Name == model.UniqueNoColumn)
                    {
                        str_value += $"    [Unique(\"{model.ClassName}\" , \"{model.KeyColumn}\" , \"{model.UniqueNoColumn}\" , ErrorMessage = \"資料重覆輸入!!\")]" + EndCode;
                    }

                    if (column_type.Contains("DateTime"))
                        str_value += "    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = \"{0:yyyy/MM/dd}\")]" + EndCode;
                    if (column_type.Contains("int"))
                        str_value += "    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = \"{0:N0}\")]" + EndCode;
                    if (column_type.Contains("decimal"))
                        str_value += "    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = \"{0:N2}\")]" + EndCode;
                    if (column_type.Contains("bool"))
                    {
                        str_default = "true";
                        str_checkbox = "true";
                    }
                    if (item.Name.Contains("Email"))
                    {
                        str_value += "    [EmailAddress(ErrorMessage = \"電子信箱格式不正確!!\")]" + EndCode;
                    }
                }
                if (item.Name != model.KeyColumn)
                {
                    str_value += $"    [Column(CheckBox = {str_checkbox} , Hidden = {str_hidden} , DropdownClass = \"{str_dropdown}\")]" + EndCode;
                    if (column_type.Contains("int"))
                        str_value += "    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = \"\")]" + EndCode;
                    else if (column_type.Contains("decimal"))
                        str_value += "    [Default(DefaultValueType = enDefaultValueType.Decimal_0, DefaultValue = \"\")]" + EndCode;
                    else if (column_type.Contains("bool"))
                        str_value += "    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = \"\")]" + EndCode;
                    else if (column_type.Contains("DateTime"))
                        str_value += "    [Default(DefaultValueType = enDefaultValueType.Date_Today, DefaultValue = \"\")]" + EndCode;
                    else if (column_type.Contains("string"))
                        str_value += "    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = \"\")]" + EndCode;
                    else
                        str_value += "    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = \"\")]" + EndCode;
                }
                str_value += $"    public {column_type} {item.Name} {{ get; set; }}" + EndCode;
                int_index++;
            }
            str_value += "}" + EndCode;
            return str_value;
        }
    }
}