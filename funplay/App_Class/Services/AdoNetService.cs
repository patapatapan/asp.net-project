using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class AdoNetService : BaseClass
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
    /// ConnectionString 名稱
    /// </summary>
    public string CommName { get; set; }
    /// <summary>
    /// SQL 指令
    /// </summary>
    public string CommandText
    {
        get { return cmd.CommandText; }
        set { cmd.CommandText = value; }
    }
    /// <summary>
    /// SQL 命令類型
    /// </summary>
    public CommandType CommandType
    {
        get { return cmd.CommandType; }
        set { cmd.CommandType = value; }
    }
    public string ErrorMessage { get; set; }
    /// <summary>
    /// 回傳執行後是否有記錄
    /// </summary>
    public bool HasRows
    {
        get
        {
            bool bln_hasrows = false;
            ErrorMessage = "";
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                bln_hasrows = dr.HasRows;
                dr.Close();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return bln_hasrows;
        }
    }
    /// <summary>
    /// 建構子
    /// </summary>
    public AdoNetService()
    {
        ObjectInit();
        CommandType = CommandType.Text;
        CommName = "dbconn";
        Open();
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="commName">ConnectionString 名稱</param>
    /// <param name="commType">命令類別</param>
    public AdoNetService(string commName, CommandType commType)
    {
        ObjectInit();
        CommandType = commType;
        CommName = commName;
        Open();
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="commType">命令類別</param>
    public AdoNetService(CommandType commType)
    {
        ObjectInit();
        CommandType = commType;
        CommName = "dbconn";
        Open();
    }
    /// <summary>
    /// 屬性初始化作業
    /// </summary>
    private void ObjectInit()
    {
        ErrorMessage = "";
        conn = new SqlConnection();
        cmd = new SqlCommand();
        cmd.Connection = conn;
    }
    /// <summary>
    /// 資料庫連線
    /// </summary>
    public void Open()
    {
        if (conn.State == ConnectionState.Open) Close();
        conn.ConnectionString = WebConfigurationManager.ConnectionStrings[CommName].ConnectionString;
        conn.Open();
    }
    /// <summary>
    /// 資料庫關閉連線
    /// </summary>
    public void Close()
    {
        conn.Close();
    }
    /// <summary>
    /// 取得指定欄位的字串型態值
    /// </summary>
    /// <param name="sColName">指定欄位</param>
    /// <returns></returns>
    public string GetValueString(string sColName)
    {
        ErrorMessage = "";
        string str_value = "";
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                str_value = (dr[sColName] == null) ? "" : dr[sColName].ToString();
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return str_value;
    }
    /// <summary>
    /// 取得指定欄位的整數型態值
    /// </summary>
    /// <param name="sColName">指定欄位</param>
    /// <returns></returns>
    public int GetValueInt(string sColName)
    {
        ErrorMessage = "";
        int int_value = 0;
        try
        {
            string str_value = GetValueString(sColName);
            if (string.IsNullOrEmpty(str_value)) str_value = "0";
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return int_value;
    }
    /// <summary>
    /// 取得指定欄位的數字型態值
    /// </summary>
    /// <param name="sColName">指定欄位</param>
    /// <returns></returns>
    public decimal GetValueDecimal(string sColName)
    {
        ErrorMessage = "";
        decimal dec_value = 0;
        try
        {
            string str_value = GetValueString(sColName);
            if (string.IsNullOrEmpty(str_value)) str_value = "0";
            if (!decimal.TryParse(str_value, out dec_value)) dec_value = 0;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return dec_value;
    }
    /// <summary>
    /// 取得指定欄位的日期型態值
    /// </summary>
    /// <param name="sColName">指定欄位</param>
    /// <returns></returns>
    public DateTime GetValueDateTime(string sColName)
    {
        ErrorMessage = "";
        DateTime dtm_value = DateTime.MinValue;
        try
        {
            string str_value = GetValueString(sColName);
            if (!DateTime.TryParse(str_value, out dtm_value)) dtm_value = DateTime.MinValue;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return dtm_value;
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
    /// <param name="bClose">執行後關閉資料庫</param>
    /// <returns></returns>
    public DataTable GetDataTable(bool bClose)
    {
        ErrorMessage = "";
        DataTable dtReturn = new DataTable();
        try
        {
            DataSet dsReturn = GetDataSet(bClose);
            dtReturn = dsReturn.Tables[0];
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return dtReturn;
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
    /// <param name="bClose">執行後關閉資料庫</param>
    /// <returns></returns>
    public DataSet GetDataSet(bool bClose)
    {
        ErrorMessage = "";
        DataSet dsReturn = new DataSet();
        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dsReturn);
            adapter.Dispose();
        }
        catch (SqlException ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        if (bClose) Close();
        return dsReturn;
    }
    /// <summary>
    /// 加入參數
    /// </summary>
    /// <param name="sParameter">參數名稱</param>
    /// <param name="oValue">參數值</param>
    /// <param name="bClear">是否先清除所有參數再加入</param>
    public void ParametersAdd(string sParameter, object oValue, bool bClear)
    {
        if (bClear) cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue(sParameter, oValue);
    }
    /// <summary>
    /// 執行 SQL 命令不回傳值
    /// </summary>
    /// <param name="bClose">是否關閉連線</param>
    public void ExecuteNonQuery(bool bClose)
    {
        ErrorMessage = "";
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        finally
        {
            if (bClose) Close();
        }
    }
}