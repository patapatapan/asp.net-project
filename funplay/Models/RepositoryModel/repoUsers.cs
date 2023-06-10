using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;

/// <summary>
/// 使用者主檔 CRUD
/// </summary>
public class z_repoUsers : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// </summary>
    public IEFGenericRepository<Users> repo;
    /// <summary>
    /// 建構子
    /// </summary>
    public z_repoUsers()
    {
        repo = new EFGenericRepository<Users>(new dbEntities());
    }

    public Users GetDapperLoginData()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhereLogin();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("RoleNo", UserService.RoleNo);
            parm.Add("UserNo", UserService.UserNo);
            parm.Add("BaseNo", "User");
            var model = dp.ReadSingle<Users>(str_query, parm);
            return model;
        }
    }

    public List<Users> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText, false);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("BaseNo", "User");
            var model = dp.ReadAll<Users>(str_query, parm);
            return model;
        }
    }

    public List<Users> GetDapperDataList(string roleNo, string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText, true);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("RoleNo", roleNo);
            parm.Add("BaseNo", "User");
            var model = dp.ReadAll<Users>(str_query, parm);
            return model;
        }
    }

    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// </summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        string str_query = @"
SELECT Users.Id, Users.IsValid, Users.UserNo, Users.UserName, Users.Password, 
Users.CodeNo, vi_CodeUser.CodeName, Users.RoleNo, Roles.RoleName, Users.GenderCode, 
CASE Users.GenderCode WHEN 'M' THEN '男' WHEN 'F' THEN '女' ELSE '其它' END AS GenderName, 
Users.DeptNo,Departments.DeptName, Users.TitleNo, Titles.TitleName, 
Users.Birthday, Users.OnboardDate, Users.LeaveDate, Users.ContactEmail, 
Users.ContactTel, Users.ContactAddress, Users.ValidateCode, Users.Remark ,
(SELECT count(*) FROM Securitys WHERE TargetNo = Users.UserNo) AS Programs ,
'~/Images/User/' + Users.UserNo + '.jpg?t=' + REPLACE(REPLACE(REPLACE(CONVERT(varchar(20),GETDATE() , 20) , ' ' , '') , '-' , ''), ':' , '') AS UserImage 
FROM Users 
LEFT OUTER JOIN vi_CodeUser ON Users.CodeNo = vi_CodeUser.CodeNo 
LEFT OUTER JOIN Roles ON Users.RoleNo = Roles.RoleNo 
LEFT OUTER JOIN Titles ON Users.TitleNo = Titles.TitleNo 
LEFT OUTER JOIN Departments ON Users.DeptNo = Departments.DeptNo  
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// </summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhereLogin()
    {
        string str_query = " WHERE (Users.RoleNo = @RoleNo AND Users.UserNo = @UserNo) ";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// </summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText, bool roleNo)
    {
        string str_query = "";
        if (roleNo) str_query += "  WHERE (Users.RoleNo = @RoleNo) ";
        if (!string.IsNullOrEmpty(searchText))
        {
            if (string.IsNullOrEmpty(str_query))
                str_query += "  WHERE ";
            else
                str_query += "  AND ";
            str_query += $"(Users.UserNo LIKE '%{searchText}%' OR ";
            str_query += $"Users.UserName LIKE '%{searchText}%' OR ";
            str_query += $"Departments.DeptName LIKE '%{searchText}%' OR ";
            str_query += $"Titles.TitleName LIKE '%{searchText}%' OR ";
            str_query += $"Users.ContactEmail LIKE '%{searchText}%' OR ";
            str_query += $"Users.ContactTel LIKE '%{searchText}%' OR ";
            str_query += $"Users.ContactAddress LIKE '%{searchText}%' OR ";
            str_query += $"Users.Remark LIKE '%{searchText}%') ";
        }
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// </summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  Users.UserNo";
    }
    /// <summary>
    /// 新增或修改
    /// </summary>
    /// <param name="model"></param>
    public void CreateEdit(Users model)
    {
        repo.CreateEdit(model, model.Id);
    }
    /// <summary>
    /// 刪除
    /// </summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    public void Edit(Users model)
    {
        repo.Update(model);
        repo.SaveChanges();
    }
    /// <summary>
    /// 取得代號
    /// </summary>
    /// <param name="dataNo">Id</param>
    /// <returns></returns>
    public string GetDataNo(int id)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null) str_value = model.UserNo;
        return str_value;
    }
    /// <summary>
    /// 取得名稱
    /// </summary>
    /// <param name="dataNo">編號</param>
    /// <returns></returns>
    public string GetDataName(string dataNo)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.UserNo == dataNo);
        if (model != null) str_value = model.UserName;
        return str_value;
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// </summary>
    /// <param name="id">主鍵值</param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        return (model != null);
    }
    /// <summary>
    /// 檢查 No 是否存在
    /// </summary>
    /// <param name="id">主鍵值</param>
    /// <param name="noValue">編號值</param>
    /// <returns></returns>
    public bool NoExists(int id, string noValue)
    {
        var model = repo.ReadSingle(m => m.Id != id && m.UserNo == noValue);
        return (model != null);
    }
    #endregion
    #region 自定義事件及函數
    /// <summary>
    /// 設定帳號部門及職稱
    /// </summary>
    public void SetUserInfo()
    {
        var model = repo.ReadSingle(m => m.UserNo == UserService.UserNo);
        if (model != null)
        {
            using (z_repoDepartments dept = new z_repoDepartments())
            {
                using (z_repoTitles title = new z_repoTitles())
                {
                    UserService.DeptName = dept.GetDataName(model.DeptNo);
                    UserService.TitleName = title.GetDataName(model.TitleNo);
                }
            }
        }
    }
    /// <summary>
    /// 帳號登入作業
    /// </summary>
    /// <param name="userNo">帳號</param>
    /// <param name="password">密碼</param>
    /// <returns></returns>
    public bool Login(string userNo, string password)
    {
        bool bln_value = false;
        UserService.Logout();
        //處理帳號密碼加密
        if (AppService.EncryptionMode)
        {
            using (CryptographyService cryp = new CryptographyService())
            { password = cryp.SHA256Encode(password); }
            if (AppService.DebugMode)
            {
                var user = repo.ReadSingle(m => m.UserNo == userNo);
                if (user != null && string.IsNullOrEmpty(user.Password))
                {
                    user.Password = password;
                    repo.Update(user);
                    repo.SaveChanges();
                }
            }
        }
        //檢查登入帳密正確性
        var data = repo.ReadSingle(m => m.UserNo == userNo && m.Password == password);
        if (data != null)
        {
            UserService.Login(data.UserNo, data.UserName, data.RoleNo);
            bln_value = true;
        }
        return bln_value;
    }

    /*我自己的登入系統*/
    public int ProjectLogin(string account, string password)
    {
        //bool bln_value = false;
        int state = -1;  //預設登入不成功
        UserService.Logout();
        //處理帳號密碼加密
        if (AppService.EncryptionMode)
        {
            using (CryptographyService cryp = new CryptographyService())
            { password = cryp.SHA256Encode(password); }
            if (AppService.DebugMode)
            {
                var user = repo.ReadSingle(m => m.Account == account);
                if (user != null && string.IsNullOrEmpty(user.Password))
                {
                    user.Password = password;
                    repo.Update(user);
                    repo.SaveChanges();
                }
            }
        }
        //檢查登入帳密正確性
        var data = repo.ReadSingle(m => m.Account == account && m.Password == password);
        if (data != null)
        {
            if (data.IsValid)
            {
                UserService.Login(data.Account, data.UserName, data.RoleNo);
                state = 0;  //符合條件 登入成功
            }
            else
            {
                state = -2; //帳號驗證未通過
            }
            //bln_value = true;
        }
        else
        {
            state = -1; //預防意外程序
        }
        //return bln_value;
        return state;
    }
    /// <summary>
    /// 重設密碼
    /// </summary>
    /// <param name="id">Id</param>
    public bool ResetPassword(int id)
    {
        bool bln_value = false;
        //檢查舊密碼正確性
        var data = repo.ReadSingle(m => m.Id == id);
        if (data != null)
        {
            string str_password = data.UserNo;
            //處理帳號密碼加密
            if (AppService.EncryptionMode)
            {
                using (CryptographyService cryp = new CryptographyService())
                {
                    str_password = cryp.SHA256Encode(data.UserNo);
                }
            }
            //變更為新密碼
            using (DapperRepository dp = new DapperRepository())
            {
                dp.CommandText = "UPDATE Users SET Password =  @Password  WHERE Id = @Id";
                dp.ParametersAdd("Id", id, true);
                dp.ParametersAdd("Password", str_password, false);
                dp.Execute();
                bln_value = true;
            }


            //data.Password = str_password;
            //repo.Update(data);
            //repo.SaveChanges();
            //bln_value = true;
        }
        return bln_value;
    }
    /// <summary>
    /// 變更密碼
    /// </summary>
    /// <param name="oldPassword">舊密碼</param>
    /// <param name="newPassword">新密碼</param>
    /// <returns></returns>
    public bool ChangePassword(string oldPassword, string newPassword)
    {
        bool bln_value = false;
        //處理帳號密碼加密
        if (AppService.EncryptionMode)
        {
            using (CryptographyService cryp = new CryptographyService())
            {
                oldPassword = cryp.SHA256Encode(oldPassword);
                newPassword = cryp.SHA256Encode(newPassword);
            }
        }
        //檢查舊密碼正確性
        var data = repo.ReadSingle(m =>
            m.UserNo == UserService.UserNo &&
            m.RoleNo == UserService.RoleNo &&
            m.Password == oldPassword);
        if (data != null)
        {
            //變更為新密碼
            using (DapperRepository dp = new DapperRepository())
            {
                dp.CommandText = "UPDATE Users SET Password = @Password WHERE RoleNo = @RoleNo AND UserNo = @UserNo";
                dp.ParametersAdd("RoleNo", UserService.RoleNo, true);
                dp.ParametersAdd("UserNo", UserService.UserNo, false);
                dp.ParametersAdd("Password", newPassword, false);
                dp.Execute();
                string str_message = dp.ErrorMessage;
            }
            //data.Password = newPassword;
            //repo.Update(data);
            //repo.SaveChanges();
            bln_value = true;
        }
        return bln_value;
    }
    /// <summary>
    /// 快速變更帳號
    /// </summary>
    /// <param name="userNo">帳號</param>
    /// <param name="roleNo">角色</param>
    /// <returns></returns>
    public bool QuickLogin(string userNo, string roleNo)
    {
        bool bln_value = false;
        UserService.Logout();
        var data = repo.ReadSingle(m => m.UserNo == userNo && m.RoleNo == roleNo);
        if (data != null)
        {
            UserService.Login(data.UserNo, data.UserName, data.RoleNo);
            bln_value = true;
        }
        return bln_value;
    }
    /// <summary>
    /// 設定副標題
    /// </summary>
    /// <param name="userNo">帳號</param>
    /// <param name="titleName">副標題</param>
    public void SetSubHeader(string userNo, string titleName)
    {
        string str_value = $"{titleName}";
        var model = repo.ReadSingle(m => m.UserNo == userNo);
        if (model != null)
        {
            str_value += $"：{userNo} {model.UserName}";
        }
        PrgService.SubHeader = str_value;
    }
    /// <summary>
    /// 驗證使用者註冊驗證碼
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <param name="errorMessage">錯誤訊息</param>
    /// <returns></returns>
    public bool ValidateEmail(string validateCode, ref string errorMessage)
    {
        if (string.IsNullOrEmpty(validateCode))
        {
            errorMessage = "驗證碼空白!!";
            return false;
        }
        var userData = repo.ReadSingle(m => m.ValidateCode == validateCode);
        //檢查是否合法驗證
        if (userData == null)
        {
            errorMessage = "驗證碼不存在!!";
            return false;
        }
        if ((bool)userData.IsValid)
        {
            errorMessage = "會員已驗證，不可重覆驗證!!";
            return false;
        }
        //修改驗證狀態
        userData.IsValid = true;
        repo.Update(userData);
        repo.SaveChanges();
        return true;
    }
    #endregion
}