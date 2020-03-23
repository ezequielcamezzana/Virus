using Bullets;
using Bullets.Events;
using UnityEngine;

namespace Managers
{
    public class BulletsManager : MonoBehaviour, IObserverBullet
    {
        public GameObject basicBulletPrefab;
        private Pool<BaseBullet> _basicBulletPool;
        private static BulletsManager _instance;
        public static BulletsManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                _instance = this;
            }
            else
            {
                _instance = this;
            }
            _basicBulletPool = new Pool<BaseBullet>(100, BulletFactory, BaseBullet.InitializeBullet, BaseBullet.DisposeBullet, true);
        }

        public BaseBullet GetBasicBullet()
        {
            return _basicBulletPool.GetObjectFromPool();
        }

        private BaseBullet BulletFactory()
        {
            var bulletObj = Instantiate(basicBulletPrefab).GetComponent<BaseBullet>();
            var eventsBullet = (IObservableBullet)bulletObj;
            eventsBullet.SubscribeDestroyBullet(OnDestroyBullet);
            
            return bulletObj;
        }

        private void ReturnBulletToPool(BaseBullet bullet)
        {
            _basicBulletPool.DisablePoolObject(bullet);
        }

        #region IObserverBullet
        public void OnDestroyBullet(BaseBullet bulletObj)
        {
            ReturnBulletToPool(bulletObj);
        }
        #endregion IObserverBullet

    }
}
