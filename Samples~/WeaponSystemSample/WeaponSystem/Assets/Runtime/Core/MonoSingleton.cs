// Author: Aditya Jaiswal, Atharv S. Jain
using UnityEngine;

namespace GameplayMechanicsUMFOSS.Core
{
    /// <summary>
    /// Generic base class for MonoBehaviour singletons.
    /// </summary>
    /// <typeparam name="T">The singleton component type.</typeparam>
    public abstract class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Gets the active singleton instance.
        /// </summary>
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
