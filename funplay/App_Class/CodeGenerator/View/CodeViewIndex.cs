using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class CodeGenerator : CodeBase
{
    public vmViewModel GetIndexViewModel(vmCodeModel model)
    {
        vmViewModel viewModel = new vmViewModel();
        viewModel.Id = model.Id;
        viewModel.AreaName = model.AreaName;
        viewModel.ControllerName = model.ControllerName;
        viewModel.LayoutName = model.LayoutName;
        viewModel.ClassName = model.ClassName;
        viewModel.ViewName = model.ViewName;
        viewModel.TemplateName = model.TemplateName;
        viewModel.FolderName = GetViewFolderName(model.AreaName, model.ControllerName);
        viewModel.FileName = GetViewFileName(model.AreaName, model.ControllerName, model.ViewName);
        viewModel.TextResult = GetViewIndexClass(viewModel);
        return viewModel;
    }

    public vmViewModel IndexViewModel(vmCodeModel model)
    {
        vmViewModel viewModel = new vmViewModel();
        viewModel.Id = 1;
        viewModel.KeyColumn = model.KeyColumn;
        viewModel.AreaName = model.AreaName;
        viewModel.ControllerName = model.ControllerName;
        viewModel.ViewName = model.ViewName;
        viewModel.ClassName = model.ClassName;
        viewModel.LayoutName = model.LayoutName;
        viewModel.FolderName = GetViewFolderName(model.AreaName, model.ControllerName);
        viewModel.FileName = GetViewFileName(model.AreaName, model.ControllerName, model.ViewName);
        viewModel.TextResult = GetViewIndexClass(viewModel);
        return viewModel;
    }

    public string GetViewIndexClass(vmViewModel model)
    {
        using (CodeBase codeBase = new CodeBase())
        {
            string str_key_name = "Id";
            List<dmColumnProperty> columns = new List<dmColumnProperty>();
            List<dmColumnProperty> columnList = new List<dmColumnProperty>();
            List<string> deleteList = new List<string>();
            deleteList = model.DeleteConfirmColumns.Split(',').ToList();
            columns = codeBase.GetClassPropertyList(model.ClassName);
            columnList = columns.Where(m =>
                    m.IsHidden == false && m.IsKeyColumn == false && m.ColumnName != model.KeyColumn)
                .ToList();
            var data = columns.Where(m => m.IsKeyColumn == true || m.ColumnName == model.KeyColumn).FirstOrDefault();
            if (data != null)
                str_key_name = data.ColumnName;
            else
                str_key_name = model.KeyColumn;

            int int_index = 0;
            string str_value = "";
            str_value += $"@model IEnumerable<{ModelsNameSapce}.{model.ClassName}>" + EndCode;
            str_value += "@{" + EndCode;
            str_value += $"    ViewBag.Title = \"{model.ViewName}\";" + EndCode;
            str_value += $"    Layout = \"~/Views/Shared/{model.LayoutName}.cshtml\";" + EndCode;
            str_value += "}" + EndCode;
            str_value += EndCode;
            str_value += "<div class=\"overflow-scroll\">" + EndCode;
            str_value += "    @Html.Partial(\"~/Views/PartialViews/_PartialFormHeader.cshtml\")" + EndCode;
            str_value += "    <table class=\"table table-bordered\">" + EndCode;
            str_value += "        <tr class=\"table-secondary\">" + EndCode;
            str_value += "            <th>" + EndCode;
            str_value += "              @Html.Partial(\"~/Views/PartialViews/_PartialFormCreate.cshtml\")" + EndCode;
            str_value += "            </th>" + EndCode;

            foreach (var column in columnList)
            {
                str_value += "            <th>" + EndCode;
                str_value += $"              @Html.DisplayNameFor(model => model.{column.ColumnName})" + EndCode;
                str_value += "            </th>" + EndCode;
            }
            str_value += "        </tr>" + EndCode;
            str_value += EndCode;
            str_value += "    @foreach(var item in Model)" + EndCode;
            str_value += "    {" + EndCode;
            str_value += $"        ActionService.RowId = item.{str_key_name};" + EndCode;
            str_value += "        ActionService.RowData = ";

            if (deleteList.Count == 0)
                str_value += "    \"\";" + EndCode;
            else
            {
                int_index = 0;
                foreach (var colName in deleteList)
                {
                    int_index++;
                    if (int_index == 1) str_value += $"(item.{colName} == null) ? \"\" : $\"";
                    str_value += "{item." + colName + "} ";
                }
                str_value += "\";" + EndCode;
            }

            str_value += "        <tr>" + EndCode;
            str_value += "            <td>" + EndCode;
            str_value += "                @Html.Partial(\"~/Views/PartialViews/_PartialFormEdit.cshtml\")" + EndCode;
            str_value += "                @Html.Partial(\"~/Views/PartialViews/_PartialFormDelete.cshtml\")" + EndCode;
            str_value += "            </td>" + EndCode;

            int_index = 0;
            foreach (var column in columnList)
            {
                int_index++;
                if (int_index != columnList.Count)
                    str_value += "            <td>" + EndCode;
                else
                    str_value += "            <td  class=\"table-wrap\">" + EndCode;
                str_value += $"                @Html.DisplayFor(modelItem => item.{column.ColumnName})" + EndCode;
                str_value += "            </td>" + EndCode;
            }
            str_value += "        </tr>" + EndCode;
            str_value += "    }" + EndCode;
            str_value += "    </table>" + EndCode;
            str_value += "</div>" + EndCode;

            return str_value;
        }
    }
}