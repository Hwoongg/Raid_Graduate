using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    static AppManager instance;
    static GameObject container;

    [HideInInspector] public bool isOnline = false;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        System.Type ty = GetType();

        gameObject.name = ty.ToString();
        
    }

    static public AppManager Instance()
    {
        if (instance == null)
        {
            container = new GameObject();
            instance = container.AddComponent<AppManager>();
        }

        return instance;
    }
}
