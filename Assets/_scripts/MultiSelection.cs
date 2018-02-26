using UnityEngine;
using System.Collections;

public class MultiSelection : MonoBehaviour
{
    bool isSelecting = false;
    Vector3 mousePosition;

    void Update()
    {
        //delete old selection, start new
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition = Input.mousePosition;

            foreach (SelectableObject selectableObject in FindObjectsOfType<SelectableObject>())
                selectableObject.setDeselected();
        }
        //select new
        if (Input.GetMouseButtonUp(0))
        {     
            SelectableObject[] selectedObjects;
            foreach (SelectableObject selectableObject in FindObjectsOfType<SelectableObject>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    selectableObject.setSelected();
                }
            }
            isSelecting = false;
        }
    }


    void OnGUI()
    {
        if (isSelecting)
        {
            var rect = GuiUtils.GetScreenRect(mousePosition, Input.mousePosition);
            GuiUtils.drawRectBorders(rect, 3, new Color(1, 1, 1, 0.5f));
            GuiUtils.drawRectFilled(rect, new Color(1, 1, 1, 0.25f));
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            GuiUtils.GetViewportBounds(camera, mousePosition, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }
}

public static class GuiUtils
{
    static readonly Texture2D texture;

    static GuiUtils()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
    }

    public static void drawRectFilled(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, texture);
        GUI.color = Color.white;
    }

    public static void drawRectBorders(Rect rect, float thickness, Color color)
    {
        // Top
        GuiUtils.drawRectFilled(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        GuiUtils.drawRectFilled(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        GuiUtils.drawRectFilled(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        GuiUtils.drawRectFilled(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}