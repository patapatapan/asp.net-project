using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

/// <summary>
/// 代表一個Repository的interface。
/// </summary>
/// <typeparam name="T">任意model的class</typeparam>
public interface IEFGenericRepository<T>
{
    /// <summary>
    /// 資料模型
    /// </summary>
    DbContext Context { get; set; }

    /// <summary>
    /// 新增一筆資料。
    /// </summary>
    /// <param name="entity">要新增到的Entity</param>
    void Create(T entity);

    /// <summary>
    /// 新增或修改一筆資料到資料庫。
    /// </summary>
    /// <param name="entity">要新增到資料的庫的Entity</param>
    /// <param name="id">ID</param>
    void CreateEdit(T entity, int id);

    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <param name="predicate">要取得的Where條件。</param>
    /// <returns>取得第一筆符合條件的內容。</returns>
    T ReadSingle(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// 取得Entity全部筆數的IQueryable。
    /// </summary>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    IQueryable<T> ReadAll();

    /// <summary>
    /// 取得Entity全部筆數的IQueryable。
    /// </summary>
    /// <param name="predicate">要取得的Where條件。</param>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    IQueryable<T> ReadAll(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// 更新一筆資料的內容。
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    void Update(T entity);

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    void Delete(T entity);

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    /// <param name="saveChange">是否要存檔</param>
    void Delete(T entity, bool saveChange);

    /// <summary>
    /// 儲存異動。
    /// </summary>
    string SaveChanges();
}

