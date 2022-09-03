using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    private static SceneChangeManager instance = null;

    public static SceneChangeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneChangeManager>();

                if (instance == null)
                {
                    instance = new GameObject("SceneChangeManager").AddComponent<SceneChangeManager>();
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChange()
    {

    }

    public void LoadingSceneChange()
    {

    }
}
