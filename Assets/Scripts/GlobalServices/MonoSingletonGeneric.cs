using UnityEngine;

namespace GlobalServices
{
    // Generic singleton class
    // Template for creating other singletons
    public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = (T)this;
                DontDestroyOnLoad(this);
            }
        }
    }
}