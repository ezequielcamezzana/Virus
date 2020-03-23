using System;
using Managers;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class BaseBullet : MonoBehaviour
    {
        public event Action Execute = delegate { };
        [SerializeField]
        protected float _speed;
        public float Damage { get; set; }

        private TrailRenderer[] _trails;
        protected const float MAX_TIME_LIFE = 5f;
        protected float _currentTimeLife = 0;
        public string OwnerName { get; set; }
        
        #region MonoBehavior
        protected void Awake()
        {
            _trails = GetComponentsInChildren<TrailRenderer>();
        }

        protected void OnDestroy()
        {
            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed -= FixedExecute;
        }
        #endregion MonoBehavior

        protected abstract void FixedExecute();

        public static void InitializeBullet(BaseBullet bulletObj)
        {
            bulletObj.gameObject.SetActive(true);
            bulletObj.Initialize();
        }

        public static void DisposeBullet(BaseBullet bulletObj)
        {
            bulletObj.Dispose();
            bulletObj.gameObject.SetActive(false);
        }
        
        private void Initialize()
        {
            _currentTimeLife = 0;
            transform.position = Vector3.zero;

            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed += FixedExecute;

            // foreach (var trail in _trails)
            // {
            //     trail.time = 1f;
            // }
        }

        private void Dispose()
        {
            if (UpdateManager.Instance) UpdateManager.Instance.ExecuteFixed -= FixedExecute;

            // foreach (var trail in _trails)
            // {
            //     trail.time = 0;
            // }
        }
    }
}

