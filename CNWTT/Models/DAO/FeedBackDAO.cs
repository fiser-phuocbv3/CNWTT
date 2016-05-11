using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Data.Entity.Core;

namespace CNWTT.Models.DAO
{
    public class FeedBackDAO
    {
        public static IEnumerable<feedback> getListFeedBack(int page, int size)
        {
            IEnumerable<feedback> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.feedbacks
                        .OrderByDescending(f => f.id)
                        .ToPagedList(page, size);
                }
                catch (ArgumentOutOfRangeException e) { }
                catch (Exception e) { }
            }
            return list;
        }

        public static bool addFeedBack(feedback fb)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                try
                {
                    db.feedbacks.Add(fb);
                    db.SaveChanges();
                    b = true;
                }
                catch (EntityCommandExecutionException e) { }
                catch (Exception e) { }
            }
            return b;
        }

        public static bool deleteFeedBack(int idFB)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var fb = db.feedbacks.Find(idFB);
                        db.feedbacks.Remove(fb);
                        db.SaveChanges();
                        dbTransaction.Commit();
                    }
                    catch (EntityCommandExecutionException e) { }
                    catch (Exception e) { }
                }
            }
            return b;
        }
    }
}