using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

public partial class CodeGenerator : CodeBase
{
    public vmViewModel CreateEditNViewModel(vmCodeModel model, int ColCount)
    {
        vmViewModel viewModel = new vmViewModel();
        viewModel.Id = 1;
        viewModel.AreaName = model.AreaName;
        viewModel.ControllerName = model.ControllerName;
        viewModel.ViewName = model.ViewName;
        viewModel.ClassName = model.ClassName;
        viewModel.LayoutName = model.LayoutName;
        viewModel.FolderName = GetViewFolderName(model.AreaName, model.ControllerName);
        viewModel.FileName = GetViewFileName(model.AreaName, model.ControllerName, model.ViewName);
        viewModel.TextResult = GetViewCreateEditNClass(viewModel, ColCount);
        return viewModel;
    }

    public string GetViewCreateEditNClass(vmViewModel model, int ColCount)
    {
        using (CodeBase codeBase = new CodeBase())
        {
            int index = 0;
            string str_key_name = "Id";
            int int_block = 12 / ColCount;
            int int_space1 = ColCount * 2;
            int int_space2 = 12 - int_space1;
            List<dmColumnProperty> columns = new List<dmColumnProperty>();
            List<dmColumnProperty> hiddenList = new List<dmColumnProperty>();
            List<dmColumnProperty> dropdownList = new List<dmColumnProperty>();
            List<dmColumnProperty> columnList = new List<dmColumnProperty>();
            columns = codeBase.GetClassPropertyList(model.ClassName);
            hiddenList = columns.Where(m => m.IsHidden == true).ToList();
            columnList = columns.Where(m => m.IsHidden == false && m.IsKeyColumn == false).ToList();
            dropdownList = columns.Where(m => m.DropdownClass != "").ToList();

            var data = columns.Where(m => m.IsKeyColumn == true).FirstOrDefault();
            if (data != null) str_key_name = data.ColumnName;

            string str_value = "";
            str_value += $"@model {ModelsNameSapce}.{model.ClassName}" + EndCode;
            str_value += EndCode;
            str_value += "@{" + EndCode;
            str_value += "    ViewBag.Title = \"CreateEdit2\";" + EndCode;
            str_value += "    Layout = \"~/Views/Shared/_LayoutAdmin.cshtml\";" + EndCode;
            str_value += $"    ActionService.RowId = Model.{str_key_name};" + EndCode;
            if (dropdownList.Count > 0)
            {
                foreach (var dropClass in dropdownList)
                {
                    str_value += $"    List<SelectListItem> {dropClass.DropdownClass} = new List<SelectListItem>();" + EndCode;
                }
                str_value += "    using (ListItemData listData = new ListItemData())" + EndCode;
                str_value += "    {" + EndCode;
                foreach (var dropClass in dropdownList)
                {
                    str_value += $"        {dropClass.DropdownClass} = listData.{dropClass.DropdownClass}();" + EndCode;
                }
                str_value += "    }" + EndCode;
            }
            else
            {
                str_value += "    //List<SelectListItem> DataList = new List<SelectListItem>();" + EndCode;
                str_value += "    //using (ListItemData listData = new ListItemData())" + EndCode;
                str_value += "    //{" + EndCode;
                str_value += "        //DataList = listData.ModuleList();" + EndCode;
                str_value += "    //}" + EndCode;
            }
            str_value += "}" + EndCode;
            str_value += EndCode;
            str_value += "@using (Html.BeginForm())" + EndCode;
            str_value += "{" + EndCode;
            str_value += "    @Html.AntiForgeryToken()" + EndCode;
            str_value += "    <div class=\"form-horizontal mt-2\">" + EndCode;
            str_value += "        @Html.ValidationSummary(true, \"\", new { @class = \"text-danger\" })" + EndCode;
            str_value += $"        @Html.HiddenFor(model => model.{str_key_name})" + EndCode;
            if (hiddenList != null && hiddenList.Count > 0)
            {
                foreach (var item in hiddenList)
                {
                    str_value += $"        @Html.HiddenFor(model => model.{item.ColumnName})" + EndCode;
                }
            }
            str_value += EndCode;
            if (columnList != null && columnList.Count > 0)
            {
                index = 0;
                foreach (var item in columnList)
                {
                    index++;
                    if (index % ColCount == 1)
                    {
                        str_value += "        <div class=\"row\">" + EndCode;
                    }
                    str_value += $"        <div class=\"col-md-{int_block} border-bottom border-start border-end  border-top\">" + EndCode;
                    str_value += "            <div class=\"row form-group\">" + EndCode;
                    str_value += $"                <div class=\"col-md-{int_space1} border-end\">" + EndCode;
                    str_value += $"                    @Html.LabelFor(model => model.{item.ColumnName}, htmlAttributes: new " + "{ @class = \"control-label text-success fw-bold\" })" + EndCode;
                    str_value += "                </div>" + EndCode;
                    str_value += $"                <div class=\"col-md-{int_space2}\">" + EndCode;
                    if (item.ColumnType.Contains("bool") || item.IsCheckBox)
                    {
                        str_value += "                    <div class=\"checkbox\">" + EndCode;
                        str_value += $"                        @Html.EditorFor(model => model.{item.ColumnName})" + EndCode;
                        str_value += $"                        @Html.ValidationMessageFor(model => model.{item.ColumnName}, " + "\"\", new { @class = \"text-danger\" })" + EndCode;
                        str_value += "                    </div>" + EndCode;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.DropdownClass))
                        {
                            str_value += $"                @Html.DropDownListFor(model => model.{item.ColumnName}, {item.DropdownClass}, new " + "{ @class = \"form-control selectpicker\",data_live_search = \"true\"})" + EndCode;
                        }
                        else if (item.ColumnType.Contains("DateTime"))
                        {
                            str_value += $"                @Html.EditorFor(model => model.{item.ColumnName}, " + "new { htmlAttributes = new { @class = \"form-control  edit-control datepicker\" } })" + EndCode;
                        }
                        else
                        {
                            str_value += $"                @Html.EditorFor(model => model.{item.ColumnName}, " + "new { htmlAttributes = new { @class = \"form-control  edit-control\" } })" + EndCode;
                        }
                        str_value += $"                @Html.ValidationMessageFor(model => model.{item.ColumnName}, " + "\"\", new { @class = \"text-danger\" })" + EndCode;
                    }
                    str_value += "                </div>" + EndCode;
                    str_value += "            </div>" + EndCode;
                    str_value += "        </div>" + EndCode;
                    if (index % ColCount == 0)
                    {
                        str_value += "    </div>" + EndCode;
                    }
                }
            }

            if (index % ColCount > 0)
            {
                for (int i = 1; i <= ColCount - index; i++)
                {
                    str_value += $"            <div class=\"col-md-{int_block} border-bottom border-start border-end  border-top\">" + EndCode;
                    str_value += "            </div>" + EndCode;
                }
            }
            str_value += "        </div>" + EndCode;

            str_value += "        <div class=\"row mt-3\">" + EndCode;
            str_value += "            <div class=\"col-md-12\">" + EndCode;
            str_value += "                @Html.Partial(\"~/Views/PartialViews/_PartialFormSumit.cshtml\")" + EndCode;
            str_value += "            </div>" + EndCode;
            str_value += "        </div>" + EndCode;
            str_value += "    </div>" + EndCode;
            str_value += "}" + EndCode;
            return str_value;
        }
    }
}