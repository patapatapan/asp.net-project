using funplay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using static Dapper.SqlMapper;

/// <summary>
/// 實作Entity Framework Generic Repository 的 Class。
/// </summary>
/// <typeparam name="TEntity">EF Model 裡面的Type</typeparam>
public class EFGenericRepository<TEntity> : IEFGenericRepository<TEntity>
    where TEntity : class
{
    public DbContext Context { get; set; }

    /// <summary>
    /// 建構EF一個Entity的Repository，需傳入此Entity的Context。
    /// </summary>
    /// <param name="inContext">Entity所在的Context</param>
    public EFGenericRepository(DbContext inContext)
    {
        Context = inContext;
    }

    /// <summary>
    /// 新增一筆資料到資料庫。
    /// </summary>
    /// <param name="entity">要新增到資料的庫的Entity</param>
    public void Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// 新增或修改一筆資料到資料庫。
    /// </summary>
    /// <param name="entity">要新增到資料的庫的Entity</param>
    /// <param name="id">ID</param>
    public void CreateEdit(TEntity entity, int id)
    {
        if (id == 0)
            Create(entity);
        else
            Update(entity);
        SaveChanges();
    }

    /// <summary>
    /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
    /// </summary>
    /// <param name="predicate">要取得的Where條件。</param>
    /// <returns>取得第一筆符合條件的內容。</returns>
    public TEntity ReadSingle(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
    }

    /// <summary>
    /// 取得Entity全部筆數的IQueryable。
    /// </summary>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    public IQueryable<TEntity> ReadAll()
    {
        return Context.Set<TEntity>().AsQueryable();
    }

    /// <summary>
    /// 取得Entity全部筆數的IQueryable。
    /// </summary>
    /// <param name="predicate">要取得的Where條件。</param>
    /// <returns>Entity全部筆數的IQueryable。</returns>
    public IQueryable<TEntity> ReadAll(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).AsQueryable();
    }

    /// <summary>
    /// 更新一筆Entity內容。
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    public void Update(TEntity entity)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Modified;
    }

    /// <summary>
    /// 更新一筆Entity的內容。只更新有指定的Property。
    /// </summary>
    /// <param name="entity">要更新的內容。</param>
    /// <param name="updateProperties">需要更新的欄位。</param>
    public void Update(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties)
    {
        Context.Configuration.ValidateOnSaveEnabled = false;

        Context.Entry<TEntity>(entity).State = EntityState.Unchanged;

        if (updateProperties != null)
        {
            foreach (var property in updateProperties)
            {
                Context.Entry<TEntity>(entity).Property(property).IsModified = true;
            }
        }
    }

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    public void Delete(TEntity entity)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    /// <param name="saveChange">是否要存檔</param>
    public void Delete(TEntity entity, bool saveChange)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        if (saveChange) SaveChanges();
    }

    /// <summary>
    /// 儲存異動。
    /// </summary>
    public string SaveChanges()
    {
        string str_message = "";
        try
        {
            Context.SaveChanges();

            // 因為Update 單一model需要先關掉validation，因此重新打開
            if (Context.Configuration.ValidateOnSaveEnabled == false)
            {
                Context.Configuration.ValidateOnSaveEnabled = true;
            }
        }
        catch (Exception ex) { str_message = ex.Message; }
        return str_message;
    }
}