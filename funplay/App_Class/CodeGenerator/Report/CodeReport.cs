using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

public partial class CodeGenerator : CodeBase
{
    public vmViewModel CreateReportModel(vmCodeModel model)
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
        viewModel.TextResult = GetViewCreateEdit1Class(viewModel);
        return viewModel;
    }
}