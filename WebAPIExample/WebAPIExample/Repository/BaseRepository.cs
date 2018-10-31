﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebAPIExample.Models;

namespace WebAPIExample.BaseRepository
{
    public class BaseRepository<T> where T : class
    {
        public static Context db = SingletonContext.GetInstance();

        public static T Insert(T entity)
        {
            var returnEntity = db.Set<T>().Add(entity);
            db.SaveChanges();
            return returnEntity;
        }

        public static bool Update(T entity)
        {
            try
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public static bool Delete(T entity)
        {
            try
            {
                db.Entry<T>(entity).State = EntityState.Deleted;
                db.Set<T>().Remove(entity);

                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public static T Search(int id)
        {
            try { return db.Set<T>().Find(id); }
            catch { return null; }
        }

        public static List<T> ReturnList()
        {
            try { return db.Set<T>().ToList(); }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

    }
}