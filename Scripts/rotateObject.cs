using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 40 * Time.deltaTime, 0));
    }
}
