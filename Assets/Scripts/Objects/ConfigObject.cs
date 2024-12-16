using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfigObject : MonoBehaviour
{
    float rotation_degree = 10;
    float scale_Speed = 1;

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

    public void Length()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.1f * scale_Speed, transform.localScale.z);
    }

    public void Short()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.1f * scale_Speed, transform.localScale.z);
    }

    public void Wide()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.1f * scale_Speed, transform.localScale.y, transform.localScale.z);
    }

    public void Narrow()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.1f * scale_Speed, transform.localScale.y, transform.localScale.z);
    }
}
