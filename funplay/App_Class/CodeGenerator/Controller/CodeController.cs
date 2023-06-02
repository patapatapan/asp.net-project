using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using PagedList;
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
using System.Web.Mvc;
using System.Xml.Linq;

public partial class CodeGenerator : CodeBase
{
    public vmControllerModel GetControllerModel(vmCodeModel model)
    {
        vmControllerModel ctrlModel = new vmControllerModel();
        ctrlModel.Id = model.Id;
        ctrlModel.AreaName = model.AreaName;
        ctrlModel.ControllerName = model.ControllerName;
        ctrlModel.PrgNo = model.PrgNo;
        ctrlModel.PrgName = model.PrgName;
        ctrlModel.ClassName = model.ClassName;
        ctrlModel.FolderName = MetadataFolderName;
        ctrlModel.FileName = GetControllerFileName(model.AreaName, model.ClassName);
        ctrlModel.TextResult = GetControllerClass(ctrlModel);
        return ctrlModel;
    }

    public string GetControllerClass(vmControllerModel model)
    {
        string str_value = "";
        string str_controller_name = GetControllerClassName(model.ControllerName);
        str_value += "using System;" + EndCode;
        str_value += "using System.Collections.Generic;" + EndCode;
        str_value += "using System.Data;" + EndCode;
        str_value += "using System.Data.Entity;" + EndCode;
        str_value += "using System.Linq;" + EndCode;
        str_value += "using System.Net;" + EndCode;
        str_value += "using System.Web;" + EndCode;
        str_value += "using System.Web.Mvc;" + EndCode;
        str_value += "using PagedList;" + EndCode;
        str_value += $"using {ModelsNameSapce};" + EndCode;
        str_value += EndCode;
        if (model.AreaName == "空白")
            str_value += $"namespace {ApplicationName}.Controllers" + EndCode;
        else
            str_value += $"namespace {ApplicationName}.Areas.{model.AreaName}.Controllers" + EndCode;
        str_value += "{" + EndCode;
        str_value += "    /// <summary>" + EndCode;
        str_value += $"    /// {str_controller_name} {model.PrgName}" + EndCode;
        str_value += "     /// </summary>" + EndCode;
        str_value += $"    public class {str_controller_name} : BaseController" + EndCode;
        str_value += "    {" + EndCode;
        str_value += "        /// <summary>" + EndCode;
        str_value += "        /// 資料列表" + EndCode;
        str_value += "        /// </summary>" + EndCode;
        str_value += "        /// <param name=\"page\">目前頁數</param>" + EndCode;
        str_value += "        /// <param name=\"pageSize\">每頁筆數</param>" + EndCode;
        str_value += "        /// <param name=\"searchText\">搜尋文字</param>" + EndCode;
        str_value += "        /// <returns></returns>" + EndCode;
        str_value += "        [HttpGet]" + EndCode;
        str_value += "        [LoginAuthorize()]" + EndCode;
        str_value += "        public ActionResult Index(int page = 1, int pageSize = 10, string searchText = \"\")" + EndCode;
        str_value += "        {" + EndCode;
        str_value += "            //檢查瀏覽權限" + EndCode;
        str_value += "            if (!PrgService.IsProgramSecurity(enSecurtyMode.Index))" + EndCode;
        str_value += "                return RedirectToAction(ActionService.Index, ActionService.Home, new { area = ActionService.Area });" + EndCode;
        str_value += EndCode;
        str_value += $"            using (z_repo{model.ClassName} repos = new z_repo{model.ClassName}())" + EndCode;
        str_value += "            {" + EndCode;
        str_value += "                PrgService.SearchText = searchText;" + EndCode;
        str_value += "                PrgService.SetAction(enAction.Index, enCardSize.Max);" + EndCode;
        str_value += "                PrgService.SetProgram();" + EndCode;
        str_value += "                var model = repos.GetDapperDataList(searchText).ToPagedList(page, pageSize);" + EndCode;
        str_value += "                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);" + EndCode;
        str_value += "                ViewBag.SearchText = searchText;" + EndCode;
        str_value += "                ViewBag.PageInfo = $\"第{model.PageNumber}頁,共{model.PageCount}頁\";" + EndCode;
        str_value += "                return View(model);" + EndCode;
        str_value += "            }" + EndCode;
        str_value += "        }" + EndCode;
        str_value += EndCode;
        str_value += "        /// <summary>" + EndCode;
        str_value += "        /// 明細" + EndCode;
        str_value += "        /// </summary>" + EndCode;
        str_value += "        /// <param name=\"id\">記錄 ID</param>" + EndCode;
        str_value += "        /// <returns></returns>" + EndCode;
        str_value += "        [HttpGet]" + EndCode;
        str_value += "        [LoginAuthorize()]" + EndCode;
        str_value += "        public ActionResult Detail(int id = 0)" + EndCode;
        str_value += "        {" + EndCode;
        str_value += $"            using (z_repo{model.ClassName} repos = new z_repo{model.ClassName}())" + EndCode;
        str_value += "            {" + EndCode;
        str_value += "                PrgService.SetAction(enAction.Detail, enCardSize.Medium);" + EndCode;
        str_value += "                var model = repos.repo.ReadSingle(m => m.Id == id);" + EndCode;
        str_value += "                return View(model);" + EndCode;
        str_value += "            }" + EndCode;
        str_value += "        }" + EndCode;
        str_value += EndCode;
        str_value += "        /// <summary>" + EndCode;
        str_value += "        /// 新增/修改" + EndCode;
        str_value += "        /// </summary>" + EndCode;
        str_value += "        /// <param name=\"id\">記錄 ID</param>" + EndCode;
        str_value += "        /// <returns></returns>" + EndCode;
        str_value += "        [HttpGet]" + EndCode;
        str_value += "        [LoginAuthorize()]" + EndCode;
        str_value += "        public ActionResult CreateEdit(int id = 0)" + EndCode;
        str_value += "        {" + EndCode;
        str_value += "            //檢查新增/修改權限" + EndCode;
        str_value += "            if (!PrgService.IsProgramSecurity(enSecurtyMode.CreateEdit, id))" + EndCode;
        str_value += "                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });" + EndCode;
        str_value += EndCode;
        str_value += $"            using (z_repo{model.ClassName} repos = new z_repo{model.ClassName}())" + EndCode;
        str_value += "            {" + EndCode;
        str_value += "                SessionService.KeyValue = id;" + EndCode;
        str_value += "                enAction action = (id == 0) ? enAction.Create : enAction.Edit;" + EndCode;
        str_value += "                PrgService.SetAction(action, enCardSize.Medium);" + EndCode;
        str_value += "                var model = repos.repo.ReadSingle(m => m.Id == id);" + EndCode;
        str_value += "                if (model == null)" + EndCode;
        str_value += "                {" + EndCode;
        str_value += $"                        model = new {model.ClassName}();" + EndCode;
        str_value += "//                    // 設定新增預設值" + EndCode;
        str_value += "//                    using (AttributeService attr = new AttributeService())" + EndCode;
        str_value += "//                    {" + EndCode;

        using (CodeBase codeBase = new CodeBase())
        {
            List<dmColumnProperty> columns = new List<dmColumnProperty>();
            columns = codeBase.GetClassPropertyList(model.ClassName);
            foreach (var column in columns)
            {
                if (!column.IsKeyColumn)
                {
                    string str_type = column.ColumnType.ToLower();
                    if (str_type.Contains("string"))
                    { str_value += $"//                        model.{column.ColumnName} = (string)attr.GetDefaultValue<z_meta{model.ClassName}>(\"{column.ColumnName}\");" + EndCode; }
                    if (str_type.Contains("int"))
                    { str_value += $"//                        model.{column.ColumnName} = (int)attr.GetDefaultValue<z_meta{model.ClassName}>(\"{column.ColumnName}\");" + EndCode; }
                    if (str_type.Contains("decimal"))
                    { str_value += $"//                        model.{column.ColumnName} = (decimal)attr.GetDefaultValue<z_meta{model.ClassName}>(\"{column.ColumnName}\");" + EndCode; }
                    if (str_type.Contains("date"))
                    { str_value += $"//                        model.{column.ColumnName} = (DateTime)attr.GetDefaultValue<z_meta{model.ClassName}>(\"{column.ColumnName}\");" + EndCode; }
                    if (str_type.Contains("bool"))
                    { str_value += $"//                        model.{column.ColumnName} = (bool)attr.GetDefaultValue<z_meta{model.ClassName}>(\"{column.ColumnName}\");" + EndCode; }
                }
            }
        }
        str_value += "//                    }" + EndCode;
        str_value += "                }" + EndCode;
        str_value += "                return View(model);" + EndCode;
        str_value += "            }" + EndCode;
        str_value += "        }" + EndCode;
        str_value += EndCode;
        str_value += "        /// <summary>" + EndCode;
        str_value += "        /// 新增/修改" + EndCode;
        str_value += "        /// </summary>" + EndCode;
        str_value += "        /// <param name=\"model\">資料</param>" + EndCode;
        str_value += "        /// <returns></returns>" + EndCode;
        str_value += "        [HttpPost]" + EndCode;
        str_value += "        [LoginAuthorize()]" + EndCode;
        str_value += $"        public ActionResult CreateEdit({model.ClassName} model)" + EndCode;
        str_value += "        {" + EndCode;
        str_value += "            if (!ModelState.IsValid)" + EndCode;
        str_value += "            {" + EndCode;
        str_value += $"                TempData[\"ErrorMessage\"] = ActionService.SetErrorMessage<z_meta{model.ClassName}>(ModelState);" + EndCode;
        str_value += "                return View(model);" + EndCode;
        str_value += "            }" + EndCode;
        str_value += $"            using (z_repo{model.ClassName} repos = new z_repo{model.ClassName}())" + EndCode;
        str_value += "            {" + EndCode;
        //str_value += "                //檢查重覆值" + EndCode;
        //str_value += "                //if (!string.IsNullOrEmpty(model.No) && repos.NoExists(model.Id , model.No))" + EndCode;
        //str_value += "                //{" + EndCode;
        //str_value += "                //    TempData[\"ErrorMessage\"] = \"重覆輸入!!\";" + EndCode;
        //str_value += "                //    return View(model);" + EndCode;
        //str_value += "                //}" + EndCode;
        str_value += "                repos.CreateEdit(model);" + EndCode;
        str_value += "                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });" + EndCode;
        str_value += "            }" + EndCode;
        str_value += "       }" + EndCode;
        str_value += EndCode;
        str_value += "        /// <summary>" + EndCode;
        str_value += "        /// 刪除" + EndCode;
        str_value += "        /// </summary>" + EndCode;
        str_value += "        /// <param name=\"id\">記錄 ID</param>" + EndCode;
        str_value += "       /// <returns></returns>" + EndCode;
        str_value += "        [HttpPost]" + EndCode;
        str_value += "        [LoginAuthorize()]" + EndCode;
        str_value += "        public ActionResult Delete(int id = 0)" + EndCode;
        str_value += "        {" + EndCode;
        str_value += "            //檢查刪除權限" + EndCode;
        str_value += "            if (!PrgService.IsProgramSecurity(enSecurtyMode.Delete))" + EndCode;
        str_value += "                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });" + EndCode;
        str_value += EndCode;
        str_value += $"            using (z_repo{model.ClassName} repos = new z_repo{model.ClassName}())" + EndCode;
        str_value += "            {" + EndCode;
        str_value += "                repos.Delete(id);" + EndCode;
        str_value += "                dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = \"資料已刪除!!\" };" + EndCode;
        str_value += "                return Json(result, JsonRequestBehavior.AllowGet);" + EndCode;
        str_value += "            }" + EndCode;
        str_value += "        }" + EndCode;
        str_value += EndCode;
        str_value += "        /// <summary>" + EndCode;
        str_value += "        /// 選取" + EndCode;
        str_value += "        /// </summary>" + EndCode;
        str_value += "        /// <param name=\"id\">記錄 ID</param>" + EndCode;
        str_value += "        /// <returns></returns>" + EndCode;
        str_value += "        [HttpGet]" + EndCode;
        str_value += "        [LoginAuthorize()]" + EndCode;
        str_value += "        public ActionResult Select(int id = 0)" + EndCode;
        str_value += "        {" + EndCode;
        str_value += "            PrgService.SelectedId = id;" + EndCode;
        str_value += "            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, page = PrgService.PageNumber, searchText = PrgService.SearchText });" + EndCode;
        str_value += "        }" + EndCode;
        str_value += EndCode;
        str_value += "        /// <summary>" + EndCode;
        str_value += "        /// 查詢" + EndCode;
        str_value += "        /// </summary>" + EndCode;
        str_value += "        /// <returns></returns>" + EndCode;
        str_value += "        [HttpPost]" + EndCode;
        str_value += "        [LoginAuthorize()]" + EndCode;
        str_value += "        public ActionResult Search()" + EndCode;
        str_value += "        {" + EndCode;
        str_value += "            object obj_text = Request.Form[ActionService.SearchText];" + EndCode;
        str_value += "            string str_text = (obj_text == null) ? string.Empty : obj_text.ToString();" + EndCode;
        str_value += "            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, searchText = str_text });" + EndCode;
        str_value += "        }" + EndCode;
        str_value += "    }" + EndCode;
        str_value += "}" + EndCode;
        return str_value;
    }
}