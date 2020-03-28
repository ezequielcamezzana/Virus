using System;
using Bullets;

namespace Bullets.Events
{
    public interface IObservableBullet 
    {
        void SubscribeDestroyBullet(Action<BaseBullet> observer);
        void UnSubscribeDestroyBullet(Action<BaseBullet> observer);
    }
}
