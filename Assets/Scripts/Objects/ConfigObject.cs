using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfigObject : MonoBehaviour
{
    float rotation_degree = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation();
    }
    public void Rotation()
    {
        transform.rotation = Quaternion.Euler(0, rotation_degree, 0);
        rotation_degree = rotation_degree + 10;
    }
    public void test()
    {

    }
}
