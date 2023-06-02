using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class AdoNetSqlClient : BaseClass
{
    /// <summary>
    /// 連線物件
    /// </summary>
    public SqlConnection conn { get; set; }
    /// <summary>
    /// 命令物件
    /// </summary>
    public SqlCommand cmd { get; set; }
    /// <summary>
    /// 連線設定檔名稱
    /// </summary>
    public string ConnName { get; set; }
    /// <summary>
    /// SQL 命令
    /// </summary>
    public string CommandText { get { return cmd.CommandText; } set { cmd.CommandText = value; } }
    /// <summary>
    /// SQL 命令類型
    /// </summary>
    public CommandType CommandType { get { return cmd.CommandType; } set { cmd.CommandType = value; } }
    /// <summary>
    /// StoreProcedure 名稱
    /// </summary>
    public string ProcedureName { get { return cmd.CommandText; } set { cmd.CommandText = value; } }
    /// <summary>
    /// 執行指令後影響的筆數
    /// </summary>
    public int RowAffected { get; set; }
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public string ErrorMessage { get; set; }
    /// <summary>
    /// 回傳執行後是否有記錄
    /// </summary>
    public bool HasRows
    {
        get
        {
            ErrorMessage = "";
            bool bln_hasrows = false;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                bln_hasrows = dr.HasRows;
                dr.Close();
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            return bln_hasrows;
        }
    }
    /// <summary>
    /// AdoNetRepository 建構子
    /// </summary>
    public AdoNetSqlClient()
    {
        ConnName = "dbconn";
        InitComponents();
    }
    /// <summary>
    /// AdoNetRepository 建構子
    /// </summary>
    /// <param name="connName">連線設定檔名稱</param>
    public AdoNetSqlClient(string connName)
    {
        ConnName = connName;
        InitComponents();
    }
    /// <summary>
    /// 初始化物件
    /// </summary>
    private void InitComponents()
    {
        conn = new SqlConnection();
        cmd = new SqlCommand();
        CommandType = CommandType.Text;
        RowAffected = 0;
        ErrorMessage = "";
        cmd.Connection = conn;
        Open();
    }
    /// <summary>
    /// 資料庫連線
    /// </summary>
    public void Open()
    {
        ErrorMessage = "";
        try
        {
            SetConnectionString();
            if (conn.State == ConnectionState.Open) Close();
            conn.Open();
        }
        catch (Exception ex) { ErrorMessage = ex.Message; }
    }
    /// <summary>
    /// 資料庫關閉連線
    /// </summary>
    public void Close()
    {
        conn.Close();
    }
    /// <summary>
    /// 設定連線字串
    /// </summary>
    public void SetConnectionString()
    {
        ErrorMessage = "";
        try
        {
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings[ConnName].ConnectionString;
        }
        catch (Exception ex) { ErrorMessage = ex.Message; }
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(bool closeDb)
    {
        return Execute(CommandText, CommandType, closeDb);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(string commandText, bool closeDb)
    {
        return Execute(commandText, CommandType, closeDb);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandType">命令類型</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(CommandType commandType, bool closeDb)
    {
        return Execute(CommandText, commandType, closeDb);
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="commandType">命令類型</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(string commandText, CommandType commandType, bool closeDb)
    {
        ErrorMessage = "";
        RowAffected = 0;
        try
        {
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            RowAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex) { ErrorMessage = ex.Message; }
        if (closeDb) Close();
        return RowAffected;
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataSet,並自動關閉資料庫連線
    /// </summary>
    /// <returns></returns>
    public DataSet GetDataSet()
    {
        return GetDataSet(true);
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataSet
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public DataSet GetDataSet(bool closeDb)
    {
        ErrorMessage = "";
        DataSet dsValue = new DataSet();
        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dsValue);
            adapter.Dispose();
        }
        catch (SqlException ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        if (closeDb) Close();
        return dsValue;
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataTable,並自動關閉資料庫連線
    /// </summary>
    /// <returns></returns>
    public DataTable GetDataTable()
    {
        return GetDataTable(true);
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataTable
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public DataTable GetDataTable(bool closeDb)
    {
        DataTable dtResult = new DataTable();
        try
        {
            DataSet dsResult = GetDataSet(closeDb);
            if (!string.IsNullOrEmpty(ErrorMessage)) return dtResult;
            if (dsResult == null || dsResult.Tables.Count < 1)
            {
                ErrorMessage = "指令錯誤,無法取得資料!!";
                return dtResult;
            }
            dtResult = dsResult.Tables[0];
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return dtResult;
    }
    /// <summary>
    /// 取得指定欄位的日期型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public DateTime GetValueDateTime(string columnName)
    {
        DateTime dtm_value = new DateTime();
        string str_value = GetValueString(columnName);
        DateTime.TryParse(str_value, out dtm_value);
        return dtm_value;
    }
    /// <summary>
    /// 取得指定欄位的數字型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public decimal GetValueDecimal(string columnName)
    {
        decimal dec_value = 0;
        string str_value = GetValueString(columnName);
        if (!decimal.TryParse(str_value, out dec_value)) dec_value = 0;
        return dec_value;
    }
    /// <summary>
    /// 取得指定欄位的整數型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public int GetValueInt(string columnName)
    {
        int int_value = 0;
        string str_value = GetValueString(columnName);
        if (!int.TryParse(str_value, out int_value)) int_value = 0;
        return int_value;
    }
    /// <summary>
    /// 取得指定欄位的字串型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public string GetValueString(string columnName)
    {
        ErrorMessage = "";
        RowAffected = 0;
        string str_value = "";
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (HasRows)
            {
                dr.Read();
                object obj_value = dr[columnName];
                str_value = (obj_value == null) ? "" : obj_value.ToString();
                dr.Close();
            }
        }
        catch (Exception ex) { ErrorMessage = ex.Message; }
        return str_value;
    }
    /// <summary>
    /// 加入參數
    /// </summary>
    /// <param name="parameterName">參數名稱</param>
    /// <param name="value">參數值</param>
    /// <param name="clearFirst">是否先清除所有參數再加入</param>
    public void ParametersAdd(string parameterName, object value, bool clearFirst)
    {
        if (clearFirst) cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue(parameterName, value);
    }
}