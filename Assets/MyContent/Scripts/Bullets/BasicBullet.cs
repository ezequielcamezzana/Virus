using System;
using Bullets.Events;
using UnityEngine;

namespace Bullets
{
    public class BasicBullet : BaseBullet, IObservableBullet
    {
        protected event Action<BaseBullet> OnDestroyBullet = delegate { };

        protected override void FixedExecute()
        {
            _currentTimeLife += Time.deltaTime;
            transform.position += transform.forward * (_speed * Time.deltaTime);

            if (_currentTimeLife >= MAX_TIME_LIFE)
            {
                OnDestroyBullet(this);
            }
        }
        
        #region IObservableBullet
        public void SubscribeDestroyBullet(Action<BaseBullet> observer)
        {
            OnDestroyBullet += observer;
        }

        public void UnSubscribeDestroyBullet(Action<BaseBullet> observer)
        {
            OnDestroyBullet -= observer;
        }
        #endregion IObservableBullet
    }
}

