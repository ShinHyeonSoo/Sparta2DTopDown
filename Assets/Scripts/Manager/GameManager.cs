using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [SerializeField]
    private string _playerTag;

    public ObjectPool ObjectPool { get; private set; }
    public Transform Player { get; private set; }

    private void Awake()
    {
        if(_instance != null) Destroy(gameObject);
        _instance = this;

        Player = GameObject.FindGameObjectWithTag(_playerTag).transform;
        ObjectPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
