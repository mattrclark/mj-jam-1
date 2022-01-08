using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public bool IsDisposed { get; private set; }
        
        protected void KillMe()
        {
            OnKilled();
            Destroy(gameObject);
            IsDisposed = true;
        }

        protected virtual void OnKilled()
        {
        }

        protected void WaitForSeconds(float seconds, Action oc = null)
        {
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(seconds);
            
                oc?.Invoke();    
            }

            StartCoroutine(Wait());
        }
    }
}