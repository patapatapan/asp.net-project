using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

/// <summary>
/// Dapper Repository 類別
/// </summary>
public class DapperRepository : BaseClass, IDapperRepository
{
    /// <summary>
    /// 私有連線設定檔名稱變數
    /// </summary>
    private string _ConnName = "";

    /// <summary>
    /// 連線設定檔名稱
    /// </summary>
    public string ConnName
    {
        get { return (_ConnName); }
        set { _ConnName = value; SetConnectionString(); }
    }
    /// <summary>
    /// 連線字串
    /// </summary>
    public string ConnectString { get; set; }
    /// <summary>
    /// SQL 命令
    /// </summary>
    public string CommandText { get; set; }
    /// <summary>
    /// StoreProcedure Name
    /// </summary>
    public string ProcedureName { get; set; }
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public string ErrorMessage { get; set; }
    /// <summary>
    /// 執行指令後影響的筆數
    /// </summary>
    public int RowAffected { get; set; }
    /// <summary>
    /// 參數物件
    /// </summary>
    public DynamicParameters Parameter { get; set; }
    /// <summary>
    /// SQL 命令類型
    /// </summary>
    public CommandType CommandType { get; set; }
    /// <summary>
    /// DapperRepository 建構子
    /// </summary>
    public DapperRepository()
    {
        InitComponents();
        ConnName = "dbconn";
    }
    /// <summary>
    /// DapperRepository 建構子
    /// </summary>
    /// <param name="connName">連線設定檔名稱</param>
    public DapperRepository(string connName)
    {
        InitComponents();
        ConnName = connName;
    }
    /// <summary>
    /// 初始化物件
    /// </summary>
    private void InitComponents()
    {
        Parameter = new DynamicParameters();
        CommandType = CommandType.Text;
    }
    /// <summary>
    /// 設定連線字串
    /// </summary>
    public void SetConnectionString()
    {
        ConnectString = WebConfigurationManager.ConnectionStrings[ConnName].ConnectionString;
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <returns></returns>
    public int Execute()
    {
        return Execute(CommandText, Parameter, CommandType);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <returns></returns>
    public int Execute(string commandText)
    {
        return Execute(commandText, Parameter, CommandType);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    public int Execute(DynamicParameters parm)
    {
        return Execute(CommandText, parm, CommandType);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandType">命令類型</param>
    /// <returns></returns>
    public int Execute(CommandType commandType)
    {
        return Execute(CommandText, Parameter, commandType);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="commandType">命令類型</param>
    /// <returns></returns>
    public int Execute(DynamicParameters parm, CommandType commandType)
    {
        return Execute(CommandText, parm, commandType);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    public int Execute(string commandText, DynamicParameters parm)
    {
        return Execute(commandText, parm, CommandType);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="commandType">命令類型</param>
    /// <returns></returns>
    public int Execute(string commandText, CommandType commandType)
    {
        return Execute(commandText, Parameter, commandType);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parameters">參數</param>
    /// <param name="commType">命令類型</param>
    /// <returns></returns>
    public int Execute(string commandText, DynamicParameters parameters, CommandType commType)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            RowAffected = 0;
            ErrorMessage = "";
            try
            {
                if (parameters.ParameterNames.Count() > 0)
                    RowAffected = conn.Execute(sql: commandText, param: parameters, commandType: commType);
                else
                    RowAffected = conn.Execute(sql: commandText, commandType: commType);
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            return RowAffected;
        }
    }
    /// <summary>
    /// 清除所有參數
    /// </summary>
    public void ParametersClear()
    {
        Parameter = new DynamicParameters();
    }
    /// <summary>
    /// 加入參數
    /// </summary>
    /// <param name="parameterName">參數名稱</param>
    /// <param name="value">參數值</param>
    /// <param name="clearFirst">加入參數前是否先清除所有參數</param>
    public void ParametersAdd(string parameterName, object value, bool clearFirst)
    {
        if (clearFirst) Parameter = new DynamicParameters();
        Parameter.Add(parameterName, value);
    }
    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <returns></returns>
    public T ReadSingle<T>()
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                if (Parameter.ParameterNames.Count() > 0)
                    return conn.QueryFirstOrDefault<T>(CommandText, Parameter);
                else
                    return conn.QueryFirstOrDefault<T>(CommandText);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return default(T);
            }
        }
    }
    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <param name="commandText">SQL 命令</param>
    /// <returns></returns>
    public T ReadSingle<T>(string commandText)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                if (Parameter.ParameterNames.Count() > 0)
                    return conn.QueryFirstOrDefault<T>(commandText, Parameter);
                else
                    return conn.QueryFirstOrDefault<T>(commandText);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return default(T);
            }
        }
    }
    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <param name="parameters">參數</param>
    /// <returns></returns>
    public T ReadSingle<T>(DynamicParameters parameters)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                var result = conn.QueryFirstOrDefault<T>(CommandText, parameters);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return default(T);
            }
        }
    }
    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parameters">參數</param>
    /// <returns></returns>
    public T ReadSingle<T>(string commandText, DynamicParameters parameters)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                var result = conn.QueryFirstOrDefault<T>(commandText, parameters);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return default(T);
            }
        }
    }
    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <returns></returns>
    public List<T> ReadAll<T>()
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                if (Parameter.ParameterNames.Count() > 0)
                    return conn.Query<T>(CommandText, Parameter).ToList();
                else
                    return conn.Query<T>(CommandText).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return new List<T>();
            }
        }
    }
    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <param name="commandText">SQL 命令</param>
    /// <returns></returns>
    public List<T> ReadAll<T>(string commandText)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                if (Parameter.ParameterNames.Count() > 0)
                    return conn.Query<T>(commandText, Parameter).ToList();
                else
                    return conn.Query<T>(commandText).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return new List<T>();
            }
        }
    }
    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <param name="parameters">參數</param>
    /// <returns></returns>
    public List<T> ReadAll<T>(DynamicParameters parameters)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                var result = conn.Query<T>(CommandText, parameters).ToList();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return new List<T>();
            }
        }
    }
    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <typeparam name="T">泛型 Entity  型別</typeparam>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parameters">參數</param>
    /// <returns></returns>
    public List<T> ReadAll<T>(string commandText, DynamicParameters parameters)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            ErrorMessage = "";
            try
            {
                var result = conn.Query<T>(commandText, parameters).ToList();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return new List<T>();
            }
        }
    }

    public bool HasRow(string sql)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            var result = conn.QueryFirstOrDefault(sql);
            return (result != null);
        }
    }

    public bool HasRow(string sql, DynamicParameters parameters)
    {
        using (var conn = new SqlConnection(ConnectString))
        {
            var result = conn.QueryFirstOrDefault(sql, parameters);
            return (result != null);
        }
    }

    /// <summary>
    /// 編號唯一值
    /// </summary>
    /// <param name="tableName">表格名稱</param>
    /// <param name="keyName">主鍵欄位名稱</param>
    /// <param name="noName">編號欄位名稱</param>
    public bool NoUnique(string tableName, string keyName, string noName, string value)
    {
        string str_query = $"SELECT {keyName} FROM {tableName} WHERE {keyName} <> @keyValue AND {noName} = @noValue";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("keyValue", SessionService.KeyValue);
        parm.Add("noValue", value);
        return !HasRow(str_query, parm);
    }
}