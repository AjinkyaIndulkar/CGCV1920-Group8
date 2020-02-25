using UnityEngine;

namespace umu7.BlockKuzushi3D.Scripts
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance => _instance == null ? FindOrCreateComponentOfType() : _instance;

        private static T FindOrCreateComponentOfType()
        {
            return FindObjectOfType<T>() ?? new GameObject(typeof(T).Name).AddComponent<T>();
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}