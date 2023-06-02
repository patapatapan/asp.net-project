using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class CalendarController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            //ActionService.SetCalendarAction(UserService.RoleNo, UserService.UserNo, "個人行事曆");
            PrgService.SetAction(enAction.Calendar, enCardSize.Max);
            return View();
        }

        /// <summary>
        /// 取得某行程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetEvent(int id)
        {
            using (z_repoCalendars calendar = new z_repoCalendars())
            {
                SessionService.CalendarID = id;
                Calendars events = calendar.GetTargetCalendar(id);
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 取得行程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetEvents()
        {
            using (z_repoCalendars calendar = new z_repoCalendars())
            {
                List<dmCalendarEvent> events = calendar.GetTargetCalendar(UserService.RoleNo, UserService.UserNo);
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 新增/修改行程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditEvent()
        {
            using (z_repoCalendars calendars = new z_repoCalendars())
            {
                object obj_allday = Request.Form["EventAllDay"];
                string str_id = Request.Form["eventid"].ToString();
                string str_start_date = Request.Form["StartDate"].ToString();
                string str_start_hour = Request.Form["StartHour"].ToString();
                string str_start_minute = Request.Form["StartMinute"].ToString();
                string str_end_date = Request.Form["EndDate"].ToString();
                string str_end_hour = Request.Form["EndHour"].ToString();
                string str_end_minute = Request.Form["EndMinute"].ToString();
                string str_title = Request.Form["EventTitle"].ToString();
                string str_description = Request.Form["EventDescription"].ToString();
                string str_place_name = Request.Form["EventPlace"].ToString();
                string str_contact_name = Request.Form["EventContactor"].ToString();
                string str_contact_tel = Request.Form["EventContactTel"].ToString();
                string str_place_address = Request.Form["EventAddress"].ToString();
                string str_code_no = Request.Form["EventCodeNo"].ToString();
                string str_room_no = Request.Form["EventRoomNo"].ToString();
                string str_resource = Request.Form["EventResource"].ToString();
                int int_id = int.Parse(str_id);
                string str_allday = (obj_allday == null) ? "off" : obj_allday.ToString();
                str_allday = str_allday.ToLower();
                str_start_hour = (string.IsNullOrEmpty(str_start_hour)) ? "00" : str_start_hour;
                str_start_minute = (string.IsNullOrEmpty(str_start_minute)) ? "00" : str_start_minute;
                str_end_hour = (string.IsNullOrEmpty(str_end_hour)) ? "00" : str_end_hour;
                str_end_minute = (string.IsNullOrEmpty(str_end_minute)) ? "00" : str_end_minute;
                str_start_hour = str_start_hour.PadLeft(2, '0');
                str_start_minute = str_start_minute.PadLeft(2, '0');
                str_end_hour = str_end_hour.PadLeft(2, '0');
                str_end_minute = str_end_minute.PadLeft(2, '0');

                Calendars calendarData = new Calendars();
                //修改行事曆
                if (int_id != 0)
                {
                    calendarData = calendars.repo.ReadSingle(m => m.Id == int_id);
                    if (calendarData == null) return RedirectToAction(ActionService.Index, ActionService.Home, new { area = UserService.RoleNo });
                }
                calendarData.StartDate = DateTime.Parse(str_start_date);
                calendarData.StartTime = $"{str_start_hour}:{str_start_minute}";
                calendarData.EndDate = DateTime.Parse(str_end_date);
                calendarData.EndTime = $"{str_end_hour}:{str_end_minute}";
                calendarData.SubjectName = str_title;
                calendarData.IsFullday = (str_allday == "on") ? true : false;
                calendarData.TargetCode = ActionService.TargetCode;
                calendarData.TargetNo = ActionService.TargetNo;
                calendarData.ColorName = "";
                calendarData.Remark = "";
                calendarData.Description = str_description;
                calendarData.PlaceName = str_place_name;
                calendarData.ContactName = str_contact_name;
                calendarData.ContactTel = str_contact_tel;
                calendarData.PlaceAddress = str_place_address;
                calendarData.RoomNo = str_room_no;
                calendarData.CodeNo = str_code_no;
                calendarData.ResourceText = str_resource;

                if (str_id == "0")
                {
                    //新增行事曆
                    calendars.repo.Create(calendarData);
                }
                else
                {
                    //修改行事曆
                    calendars.repo.Update(calendarData);
                }
                calendars.repo.SaveChanges();

                return RedirectToAction(ActionService.Index, ActionService.Home, new { area = UserService.RoleNo });
            }
        }

        /// <summary>
        /// 刪除行程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteEvent()
        {
            using (z_repoCalendars calendars = new z_repoCalendars())
            {
                int int_id = SessionService.CalendarID;
                var data = calendars.repo.ReadSingle(m => m.Id == int_id);
                if (data != null)
                {
                    calendars.repo.Delete(data);
                    calendars.repo.SaveChanges();
                }
                return RedirectToAction(ActionService.Index, ActionService.Home, new { area = UserService.RoleNo });
            }
        }
    }
}