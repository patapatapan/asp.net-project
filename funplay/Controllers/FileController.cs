using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class FileController : BaseController
    {
        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Upload()
        {
            PrgService.SetAction(enAction.Upload, enCardSize.Medium);
            dmFileUpload uploadModel = new dmFileUpload();
            //uploadModel.ControllerName = model.ControllerName;
            //uploadModel.AreaName = model.AreaName;
            //uploadModel.ActionName = model.ActionName;
            //uploadModel.ParmType = model.ParmType;
            //uploadModel.IdIntParm = model.IdIntParm;
            //uploadModel.IdStringParm = model.IdStringParm;
            return View(uploadModel);
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="file">檔案</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(dmFileUpload model)
        {
            FileUpload(model.FileName);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 檔案上傳時另存到指定位置
        /// </summary>
        /// <param name="file">上傳的檔案物件</param>
        /// <param name="filePath">另存的路徑,如 ~/Images/User</param>
        /// <param name="fileName">另存的檔名,如 001.jpg</param>
        /// <returns></returns>
        public string FileUpload(HttpPostedFileBase file)
        {
            string str_message = string.Empty;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    try
                    {
                        string str_file_name = Path.GetFileName(file.FileName);
                        string str_full_name = Path.Combine(Server.MapPath(ActionService.PathName), str_file_name);
                        if (System.IO.File.Exists(str_full_name)) System.IO.File.Delete(str_full_name);
                        file.SaveAs(str_full_name);
                    }
                    catch (Exception ex)
                    {
                        str_message = ex.Message;
                    }
                }
            }
            return str_message;
        }
    }
}