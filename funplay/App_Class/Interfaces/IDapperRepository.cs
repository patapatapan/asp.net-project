using Dapper;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Dapper Repository 介面
/// </summary>
public interface IDapperRepository
{
    /// <summary>
    /// 連線設定檔名稱
    /// </summary>
    string ConnName { get; set; }

    /// <summary>
    /// 連線字串
    /// </summary>
    string ConnectString { get; set; }

    /// <summary>
    /// SQL 命令
    /// </summary>
    string CommandText { get; set; }

    /// <summary>
    /// SQL 命令類型
    /// </summary>
    CommandType CommandType { get; set; }

    /// <summary>
    /// StoreProcedure Name
    /// </summary>
    string ProcedureName { get; set; }

    /// <summary>
    /// 執行指令後影響的筆數
    /// </summary>
    int RowAffected { get; set; }

    /// <summary>
    /// 錯誤訊息
    /// </summary>
    string ErrorMessage { get; set; }

    /// <summary>
    /// 參數物件
    /// </summary>
    DynamicParameters Parameter { get; set; }

    /// <summary>
    /// 設定連線字串
    /// </summary>
    void SetConnectionString();

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <returns></returns>
    int Execute();

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <returns></returns>
    int Execute(string commandText);

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    int Execute(DynamicParameters parm);

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandType">命令類型</param>
    /// <returns></returns>
    int Execute(CommandType commandType);

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    int Execute(string commandText, DynamicParameters parm);

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="commandType">命令類型</param>
    /// <returns></returns>
    int Execute(DynamicParameters parm, CommandType commandType);

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="commandType">命令類型</param>
    /// <returns></returns>
    int Execute(string commandText, CommandType commandType);

    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parameters">參數</param>
    /// <param name="commType">命令類型</param>
    /// <returns></returns>
    int Execute(string commandText, DynamicParameters parameters, CommandType commType);

    /// <summary>
    /// 清除所有參數
    /// </summary>
    void ParametersClear();

    /// <summary>
    /// 加入參數
    /// </summary>
    /// <param name="parameterName">參數名稱</param>
    /// <param name="value">參數值</param>
    /// <param name="clearFirst">加入參數前是否先清除所有參數</param>
    void ParametersAdd(string parameterName, object value, bool clearFirst);

    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <returns>取得第一筆符合條件的內容。</returns>
    T ReadSingle<T>();

    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <returns>取得第一筆符合條件的內容。</returns>
    T ReadSingle<T>(string commandText);

    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <param name="parameters">參數</param>
    /// <returns>取得第一筆符合條件的內容。</returns>
    T ReadSingle<T>(DynamicParameters parameters);

    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parameters">參數</param>
    /// <returns>取得第一筆符合條件的內容。</returns>
    T ReadSingle<T>(string commandText, DynamicParameters parameters);

    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    List<T> ReadAll<T>();

    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    List<T> ReadAll<T>(string commandText);

    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <param name="parameters">參數</param>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    List<T> ReadAll<T>(DynamicParameters parameters);

    /// <summary>
    /// 取得泛型 Entity  型別全部筆數的 List。
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="parameters">參數</param>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    List<T> ReadAll<T>(string commandText, DynamicParameters parameters);
}