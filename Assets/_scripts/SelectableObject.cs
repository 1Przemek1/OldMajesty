using UnityEngine;
using System.Collections;

public class SelectableObject : MonoBehaviour {

    public GameObject selectionCircle;

    public void setSelected()
    {
        GetComponent<Renderer>().material.color = Color.blue;
       // selectionCircle.SetActive(true);
    }

    public void setDeselected()
    {
        GetComponent<Renderer>().material.color = Color.white;
        //selectionCircle.SetActive(false);
    }
}
