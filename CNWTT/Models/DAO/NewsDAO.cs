using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using PagedList;

namespace CNWTT.Models.DAO
{
    public class NewsDAO
    {
        public static IList<news> list = null;
        public static news n = null;

        //get list news 5
        public static IList<news> getListNews5()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                string sql = "SELECT TOP 5 * FROM dbo.news ORDER BY id DESC";
                try
                {
                    list = db.news.SqlQuery(sql).ToList();
                }
                catch (EntityCommandExecutionException e)
                {
                    Console.WriteLine("Exception-NewsDAO:getListNews5-" + e.ToString());
                }
            }
            return list;
        }

        //get news from id
        public static news getNewsId(int idN)
        {
            n = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    n = db.news.Where(ne => ne.id == idN).Single();
                }
                catch (EntityCommandExecutionException e)
                {
                    Console.WriteLine("Exception-NewsDAO:getNewsId-" + e.ToString());
                }
                catch (EntityException e)
                {
                    Console.WriteLine("Exception-NewsDAO:getNewsId-" + e.ToString());
                }
            }
            return n;
        }

        //get list all
        public static IList<news> getAllList()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.news.ToList();
                }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e){ }
            }
            return list;
        }

        public static IEnumerable<news> getListPage(int page, int pageSize)
        {
            IEnumerable<news> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.news
                        .OrderByDescending(n => n.id)
                        .ToPagedList(page, pageSize);
                }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e){ }
            }
            return list;
        }

        public static int addNews(news news)
        {
            int idN = -1;
            using (var db = new cnwttEntities())
            {
                try
                {                   
                    db.news.Add(news);
                    db.SaveChanges();
                    idN = news.id;                   
                }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e) { }
            }
            return idN;
        }

        //edit news
        public static bool editNews(news news)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                try
                {                    
                    var n = db.news.Find(news.id);
                    n.title = news.title;
                    n.describe = news.describe;
                    n.content = news.content;
                    n.picture = news.picture;
                    db.SaveChanges();
                    b = true;                   
                }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e){ }
            }
            return b;
        }

        //delete news
        public static bool deleteNews(int idNews)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {                  
                        news news = db.news.Find(idNews);
                        db.news.Remove(news);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        b = true;                  
                    }
                    catch (EntityCommandExecutionException e){ }
                    catch (EntityException e){ }
                }
            }
            return b;        
        }
    }
}