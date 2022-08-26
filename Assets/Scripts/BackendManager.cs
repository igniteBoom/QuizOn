using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
    private static BackendManager instance = null;

    public static BackendManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BackendManager>();

                if (instance == null)
                {
                    instance = new GameObject("BackendManager").AddComponent<BackendManager>();
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
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            // �ʱ�ȭ ���� �� ����
            Debug.Log("�ʱ�ȭ ����!");
        }
        else
        {
            // �ʱ�ȭ ���� �� ����
            Debug.LogError("�ʱ�ȭ ����!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
