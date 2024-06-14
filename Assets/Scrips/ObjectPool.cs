using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPool>();
            }   
            return _instance;   
        }
    }    
    private List<GameObject>_poollist = new List<GameObject>();
    private Stack<GameObject> _poolStack = new Stack<GameObject>();
    private Queue<GameObject> _poolqueue = new Queue<GameObject>();

    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate (prefab, transform);
            obj.SetActive(false);
            obj.name = prefab + $" ({i}) ";
            _poolqueue.Enqueue (obj);

        }    
    }

    public bool CanSpawn()
    {
        return _poolqueue.Count > 0;
    }

    public GameObject PickOne(Transform parent)
    {
        var obj = _poolqueue.Dequeue ();
        obj.transform.SetParent (parent);
        
        return obj;
    }    

    public void ReturnOne(GameObject obj)
    {
        obj.SetActive (false);
        obj.transform.SetParent (transform);
        _poolqueue.Enqueue(obj);
    }    
    // Update is called once per frame
    void Update()
    {
        
    }
     
}
