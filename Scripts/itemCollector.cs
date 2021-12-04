using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollector : MonoBehaviour
{
    public GameObject coinParent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.FindWithTag("Player").GetComponent<playerController>().coins += 1;
            Destroy(coinParent);
        }
    }
}
