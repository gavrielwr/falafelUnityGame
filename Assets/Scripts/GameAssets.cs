using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

   private static GameAssets instance;

    public static GameAssets GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform pfFalafel;
    public Transform pfFries;
    public Transform pfChili;
    public Transform pfSalad;
    public Transform pfEggplant;
    public Transform pfCoriander;

}
