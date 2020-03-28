using System.Collections.Generic;

namespace Managers
{
    public class Pool<T>
    {
        private List<PoolObject<T>> _poolList;
        public delegate T CallbackFactory();

        private int _count;
        private readonly bool _isDynamic = false;
        private readonly PoolObject<T>.PoolCallback _init;
        private readonly PoolObject<T>.PoolCallback _finalize;
        private readonly CallbackFactory _factoryMethod;

        public Pool(int initialStock, CallbackFactory factoryMethod, PoolObject<T>.PoolCallback initialize, PoolObject<T>.PoolCallback finalize, bool isDinamic)
        {
            _poolList = new List<PoolObject<T>>();

            _factoryMethod = factoryMethod;
            _isDynamic = isDinamic;
            _count = initialStock;
            _init = initialize;
            _finalize = finalize;

            for (int i = 0; i < _count; i++)
            {
                _poolList.Add(new PoolObject<T>(_factoryMethod(), _init, _finalize));
            }
        }

        public PoolObject<T> GetPoolObject()
        {
            for (var i = 0; i < _count; i++)
            {
                if (_poolList[i].isActive) continue;
                _poolList[i].isActive = true;
                return _poolList[i];
            }

            if (!_isDynamic) return null;

            var poolObject = new PoolObject<T>(_factoryMethod(), _init, _finalize) 
                {isActive = true};
            _poolList.Add(poolObject);
            _count++;
            return poolObject;
        }

        public T GetObjectFromPool()
        {
            for (var i = 0; i < _count; i++)
            {
                if (_poolList[i].isActive) continue;
                _poolList[i].isActive = true;
                return _poolList[i].GetObj;
            }

            if (!_isDynamic) return default(T);
            
            var poolObject = new PoolObject<T>(_factoryMethod(), _init, _finalize) 
                {isActive = true};
            _poolList.Add(poolObject);
            _count++;
            return poolObject.GetObj;
        }

        public void DisablePoolObject(T obj)
        {
            foreach (var poolObj in _poolList)
            {
                if (!poolObj.GetObj.Equals(obj)) continue;
                poolObj.isActive = false;
                return;
            }
        }
    }

}
