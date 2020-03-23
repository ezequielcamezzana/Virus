using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class UpdateManager : MonoBehaviour
    {
        private static UpdateManager _instance;
        public static UpdateManager Instance => _instance;
        
        public event Action Execute = delegate { };
        public event Action ExecuteFixed = delegate { };
        public event Action ExecuteLate = delegate { };

        public bool IsPause { get; set; }

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
        }

        #region UPDATES

        private void Update()
        {
            if (!IsPause && !Execute.Equals(null))
            {
                Execute();
            }
        }

        private void LateUpdate()
        {
            if (!IsPause && !ExecuteLate.Equals(null)) ExecuteLate();
        }

        private void FixedUpdate()
        {
            if (!IsPause && !ExecuteFixed.Equals(null))ExecuteFixed();
        }

        #endregion UPDATES
    }

    public class CoroutineUpdate
    {

        private event Action _Update = delegate { };
        private bool _activeLoop = true;
        public float time { get; set; }
        public bool activeTime { get; set; }

        public CoroutineUpdate(bool activeTime, float time = 0.3f)
        {
            this.activeTime = activeTime;
            this.time = time;
        }

        public void Subscribe(Action action)
        {
            _Update += action;
        }

        public void Unsubscribe(Action action)
        {
            _Update -= action;
        }

        public void Clean()
        {
            _Update = delegate { };
            _activeLoop = false;
            time = 0;
        }

        /// <summary> Execute coroutine in start and method you use this class. </summary>
        public IEnumerator CoroutineMethod()
        {
            while (_activeLoop)
            {
                if (!UpdateManager.Instance.IsPause)
                {
                    if (activeTime)
                    {
                        _Update();
                        yield return new WaitForSeconds(time);
                    }
                    else
                    {
                        _Update();
                        yield return null;
                    }
                }
                yield return null;
            }
        }
    }
}
