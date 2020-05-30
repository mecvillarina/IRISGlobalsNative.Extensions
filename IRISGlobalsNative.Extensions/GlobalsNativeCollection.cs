using InterSystems.Data.IRISClient.ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IRISGlobalsNative.Extensions
{
    public class GlobalsNativeCollection<T> : IGlobalsNativeCollection<T> where T : GlobalsNativeBase
    {
        private readonly IRIS _irisNative;

        public string CollectionName { get; private set; }

        public GlobalsNativeCollection(IRIS irisNative, string collectionName = "")
        {
            _irisNative = irisNative;

            CollectionName = $"{collectionName}";
            Initialize();
        }

        #region Private Methods
        private void Initialize()
        {
            var itemList = _irisNative.GetIRISList(GlobalsNativeConstants.CollectionGlobals, CollectionName);
            if (itemList == null)
            {
                itemList = new IRISList();
                _irisNative.Set(itemList, GlobalsNativeConstants.CollectionGlobals, CollectionName);
            }
        }

        private IRISList PopulateCollectionIRISList()
        {
            var irisList = _irisNative.GetIRISList(GlobalsNativeConstants.CollectionGlobals, CollectionName);
            return irisList ?? new IRISList();
        }

        private T ByteArrayToType(byte[] data)
        {
            var jsonStr = System.Text.Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        #endregion

        public bool Add(T obj)
        {
            var irisList = PopulateCollectionIRISList();

            try
            {
                obj.SetObjectId();
                string objJsonValue = JsonConvert.SerializeObject(obj);
                irisList.Add(objJsonValue);
                _irisNative.Set(irisList, GlobalsNativeConstants.CollectionGlobals, CollectionName);

                return true;
            }
            catch { }

            return false;
        }

        public bool AddRange(List<T> objects)
        {
            var irisList = PopulateCollectionIRISList();

            foreach (var obj in objects)
            {
                Console.WriteLine("add");
                obj.SetObjectId();
                string objJsonValue = JsonConvert.SerializeObject(obj);
                irisList.Add(objJsonValue);
            }

            _irisNative.Set(irisList, GlobalsNativeConstants.CollectionGlobals, CollectionName);

            return true;
        }

        public IQueryable<T> AsQueryable()
        {
            var irisList = PopulateCollectionIRISList();

            return irisList.ToList()
               .Select(obj => ByteArrayToType((byte[])obj))
               .AsQueryable<T>();
        }

        public void Clear()
        {
            _irisNative.Kill(GlobalsNativeConstants.CollectionGlobals, CollectionName);
            Initialize();
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return AsQueryable().Count();

            var s = AsQueryable().ToList();
            return AsQueryable().Count(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return AsQueryable().FirstOrDefault();

            return AsQueryable().FirstOrDefault(predicate);
        }

        public void RemoveAll(Expression<Func<T, bool>> predicate = null)
        {
            var items = new List<T>();

            if (predicate == null)
                items = ToList();
            else
                items = Where(predicate).ToList();

            var removedItems = new List<T>();
            foreach (var item in items)
            {
                var itemIndex = items.FindIndex(x => x.ObjectId == item.ObjectId);
                removedItems.Add(item);
            }

            items = items.Except(removedItems).ToList();
            var irisList = new IRISList();
            irisList.AddRange(items);
            _irisNative.Set(irisList, GlobalsNativeConstants.CollectionGlobals, CollectionName);
        }

        public void RemoveAt(int index)
        {
            if (Count() < index + 1)
                throw new IndexOutOfRangeException();

            var irisList = PopulateCollectionIRISList();
            irisList.Remove(index + 1);
            _irisNative.Set(irisList, GlobalsNativeConstants.CollectionGlobals, CollectionName);
        }

        public List<T> ToList()
        {
            return AsQueryable().ToList();
        }

        public T Update(T obj, string objectId)
        {
            var item = FirstOrDefault(x => x.ObjectId == objectId);

            if (item != null)
            {
                var items = ToList();
                var index = items.FindIndex(x => x.ObjectId == objectId);
                obj.ObjectId = objectId;

                var irisList = PopulateCollectionIRISList();
                string objJsonValue = JsonConvert.SerializeObject(obj);

                irisList.Remove(index);
                irisList.Add(objJsonValue);
                _irisNative.Set(irisList, GlobalsNativeConstants.CollectionGlobals, CollectionName);
                return obj;
            }

            return null;

        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return ToList();

            return AsQueryable().Where(predicate);
        }
    }
}
