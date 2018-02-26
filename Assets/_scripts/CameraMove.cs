using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{

    private int SCREEN_WIDHT = 0;
    private int SCREEN_HEIGHT = 0;

    public double WIDTH_OFFSET = 0;
    public double HEIGHT_OFFSET = 0;
    public double MAX_ZOOM_IN = 6;
    public double MAX_ZOOM_OUT = 10;
    public double MAX_ROTATION_UP = 0;
    public double MAX_ROTATION_DOWN = 65;

    public float scrollSpeed = 0;

    public Texture2D cursor;


    private Quaternion targetRotation;
    float targetRotationY;
    float targetRotationX;

    void Start()
    {
        targetRotation = transform.rotation;
        targetRotationY = transform.rotation.y;
        targetRotationX = transform.rotation.x;

        SCREEN_WIDHT = Screen.width;
        SCREEN_HEIGHT = Screen.height;
        WIDTH_OFFSET = Screen.width * 0.08;
        HEIGHT_OFFSET = Screen.height * 0.08;


        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translation = Vector3.zero;

        // move 
       /* if (Input.mousePosition.x > SCREEN_WIDHT - WIDTH_OFFSET)
            translation += Vector3.right * Time.deltaTime * scrollSpeed;// move right
        else if (Input.mousePosition.x < WIDTH_OFFSET)
            translation += Vector3.left * Time.deltaTime * scrollSpeed;// move left
        else if (Input.mousePosition.y > SCREEN_HEIGHT - HEIGHT_OFFSET)
            translation += Vector3.forward * Time.deltaTime * scrollSpeed; //move up
        else if (Input.mousePosition.y < HEIGHT_OFFSET)
            translation += Vector3.back * Time.deltaTime * scrollSpeed; //move down
        */
        // zoon
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > MAX_ZOOM_IN)
            translation = transform.forward; //new Vector3(0, -1, 1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < MAX_ZOOM_OUT)
            translation -= transform.forward;
        else
            translation = Vector3.zero;

        //GetComponent<Camera>().transform.position += translation;
        transform.position += Vector3.Lerp(transform.position, translation * 2, 5);

        if (Input.GetMouseButton(1))
        {
            targetRotationY += Input.GetAxis("Mouse X") * 4;

            if (targetRotationX - Input.GetAxis("Mouse Y") * 4 + transform.rotation.x < MAX_ROTATION_DOWN &&
                targetRotationX - Input.GetAxis("Mouse Y") * 4 + transform.rotation.x > MAX_ROTATION_UP)
                targetRotationX -= Input.GetAxis("Mouse Y") * 4;

            targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
        }


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (1.0f));

    }
}
