using UnityEngine;
/*
 * 
 * This script created by UNITY COMUNITY and available here http://wiki.unity3d.com/index.php?title=Singleton
 * 
 * 
 */

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;
    public static T instance
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if( m_Instance == null )
            {
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
 
                // Object not found, we create a temporary one
                if( m_Instance == null )
                {
                    Debug2.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
 
                    // Problem during the creation, this should not happen
                    if( m_Instance == null )
                    {
                        Debug2.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                m_Instance.Init();
            }
            return m_Instance;
        }
    }
    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake()
    {
        if( m_Instance == null )
        {
            m_Instance = this as T;
            m_Instance.Init();
        }
    }
 
    // This function is called when the instance is used the first time
    // Put all the initializations you need here, as you would do in Awake
    public virtual void Init(){}
 
    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}