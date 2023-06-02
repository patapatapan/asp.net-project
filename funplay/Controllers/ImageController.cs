using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class ImageController : Controller
    {
        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Upload()
        {
            PrgService.Init("圖片管理", "上傳圖片", enCardSize.Medium);
            return View();
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="file">檔案</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            ImageService.FileUpload(file);
            if (ActionService.PriorParmIdType == enPriorParmIdType.None)
                return RedirectToAction(ActionService.PriorAction, ActionService.PriorController, new { area = ActionService.PriorArea });
            if (ActionService.PriorParmIdType == enPriorParmIdType.TypeInt)
            {
                int int_value = (int)ActionService.PriorParmIdValue;
                return RedirectToAction(ActionService.PriorAction, ActionService.PriorController, new { area = ActionService.PriorArea, id = int_value });
            }
            else
            {
                string str_value = ActionService.PriorParmIdValue.ToString();
                return RedirectToAction(ActionService.PriorAction, ActionService.PriorController, new { area = ActionService.PriorArea, id = str_value });
            }
        }

        /// <summary>
        /// 圖片上傳
        /// </summary>
        /// <param name="upload">上傳檔案</param>
        /// <param name="folderName">資料夾名稱</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CKEditorUploadImage(HttpPostedFileBase upload, string folderName)
        {
            string str_folder = $"~/Images/{folderName}";
            string str_path = Server.MapPath(str_folder);

            //如果目錄不存在則建立此目錄
            if (!Directory.Exists(str_path)) Directory.CreateDirectory(str_path);

            //獲取圖片檔案名稱及延伸檔名
            string str_file_name = Path.GetFileName(upload.FileName);
            string str_file_ext = Path.GetExtension(str_file_name).ToLower();

            //用時間來生成新圖片名稱並存檔
            string str_route = "../../";
            if (!string.IsNullOrEmpty(ActionService.Area)) str_route += "../../";
            str_folder = $"{str_route}Images/{folderName}";
            string str_save_name = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + str_file_ext;
            string str_upload_url = str_path + "/" + str_save_name;
            upload.SaveAs(str_upload_url);

            str_upload_url = str_folder + "/" + str_save_name;
            //上傳成功後，返回Json格式的響應
            return Json(new
            {
                uploaded = 1,
                fileName = str_save_name,
                url = str_upload_url
            });
        }
    }
}