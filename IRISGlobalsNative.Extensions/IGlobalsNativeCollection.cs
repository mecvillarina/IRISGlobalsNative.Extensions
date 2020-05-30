using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IRISGlobalsNative.Extensions
{
    public interface IGlobalsNativeCollection<T> where T : GlobalsNativeBase
    {
        string CollectionName { get; }
        bool Add(T obj);
        bool AddRange(List<T> objects);
        IQueryable<T> AsQueryable();
        void Clear();
        int Count(Expression<Func<T, bool>> predicate = null);
        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);
        void RemoveAll(Expression<Func<T, bool>> predicate = null);
        void RemoveAt(int index);
        List<T> ToList();
        T Update(T obj, string objectId);
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate = null);
    }
}
