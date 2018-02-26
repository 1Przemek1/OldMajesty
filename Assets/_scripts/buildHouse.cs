using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buildHouse : MonoBehaviour
{

    private List<MeshRenderer> components = new List<MeshRenderer>();

    // Use this for initialization
    void Start()
    {
        fillComponents();

        StartCoroutine("build");
    }

    void fillComponents()
    {
        foreach (Transform child in transform)
        {
            MeshRenderer mr = child.GetComponent<MeshRenderer>();
            mr.enabled = false;
            components.Add(mr);
        }
    }

    IEnumerator build()
    {
        components.Reverse();

        foreach (MeshRenderer mr in components)
        {
            mr.enabled = true;
            yield return new WaitForSeconds(1f);
        }

    }
}
