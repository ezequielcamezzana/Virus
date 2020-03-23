using System;
using Bullets;

namespace Bullets.Events
{
    public interface IObserverBullet
    {
        void OnDestroyBullet(BaseBullet bulletObj);
    }
}