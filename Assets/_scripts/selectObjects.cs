using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class selectObjects : MonoBehaviour
{
    public RawImage objectImage;
    public GameObject objectImageObject;
    public Text objectName;
    //private bool selectedObject = false;
    private GameObject selectedObject;
    [SerializeField]
    private Camera m_Camera;

    void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    void Update()
    {
        selectAction();
        deselectAction();
        moveAction();
    }

    private void moveAction()
    {
        if (Input.GetMouseButtonDown(0) && selectedObject)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.tag == "map")
            {
                AUnit obj = selectedObject.GetComponent<AUnit>();

                obj.playMoveSound();
                obj.rotateToDirection(hit.point);
                obj.moveToDestination(hit.point);
                obj.triggerWalkAnimation();
            }
        }
    }

    private void selectAction()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.tag == "unit")
            {
                if (selectedObject)
                    deselect();

                hit.transform.GetComponent<IObject>().playSelectSound();
                hit.transform.GetComponent<IObject>().objectSelected();
                objectImage.texture = hit.transform.GetComponent<IObject>().getObjectIcon();
                objectName.text = hit.transform.GetComponent<IObject>().getObjectName();
                selectedObject = hit.transform.gameObject;
                objectImageObject.SetActive(true);
            }

        }
    }

    private void deselectAction()
    {
        if (Input.GetMouseButtonDown(1) && selectedObject)
        {
            //deselect();
        }
    }

    private void deselect()
    {
        selectedObject.GetComponent<IObject>().objectDeselected();
        selectedObject = null;
        objectImage.texture = null;
        objectName.text = null;
        objectImageObject.SetActive(false);
    }
}
