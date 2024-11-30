using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public Material selectedMaterial;

    public Material selectedObjectOriginalMaterial;

    public GameObject selectedObject;
    public bool isAnObjectSelected;
    private Transform selectedObjectOriginalParentTransform;

    private DataManager dataManager;

    void Start()
    {
        isAnObjectSelected = false;
        dataManager = DataManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the center of the image, and see if the ray is hitting anything.
        GameObject mainCamera = Camera.main.gameObject;
        Vector3 origin = mainCamera.transform.position;
        Vector3 direction = mainCamera.transform.forward;
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;
        bool isThereAHit = Physics.Raycast(ray, out hit);
        rotateObjet();
        // deselect
        if (isAnObjectSelected)
        {
            if (getUserTap() && !checkIfUserIsDragging())
            {
                deselectObject();
            }
            else
            {
                // Still selected.
            }
        }
        // select
        else
        {
            // Nothing is selected now. See if there is a hit from the raycast.
            if (isThereAHit && hit.collider.gameObject.name != "Plane")
            {
                if (getUserTap())
                {
                    Debug.Log("Selected object");
                    selectedObject = hit.collider.gameObject; // Remember the selected object

                    selectedObjectOriginalMaterial = new Material(selectedObject.GetComponent<Renderer>().material);
                    selectedObject.GetComponent<Renderer>().material = selectedMaterial;

                    isAnObjectSelected = true;

                    selectedObjectOriginalParentTransform = selectedObject.transform.parent;
                    selectedObject.transform.parent = mainCamera.transform;


                }
            }
        }
    }

    private bool checkIfUserIsDragging()
    {
        bool isDragging = false;
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                isDragging = true;
            }
        }
        return isDragging;
    }

    private bool getUserTap()
    {
        bool isTap = false;
        // Check for a touch (if we have smart phone).
        if (Input.touchCount > 0)
        {
            // We have a tap on the screen.
            Touch touch = Input.GetTouch(0);
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return true;
            }
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 p = touch.position;
                // Check if touch position is not too close to the edge of the screen.
                float fractScreenBorder = 0.3f;

                if (p.x > fractScreenBorder * Screen.width && p.x < (1 - fractScreenBorder) * Screen.width &&
                p.y > fractScreenBorder * Screen.height && p.y < (1 - fractScreenBorder) * Screen.height)
                {
                    isTap = true;
                }
            }
        }
        else
        {
            // Check for keypress.
            isTap = Input.anyKeyDown && Input.GetKey(KeyCode.Space);
        }
        return isTap;
    }

    // could delete these
    // for selecting object on creation
    /*
    public void createObjectSelect(GameObject obj)
    {
        // deselect any currently selected objects
        if (isAnObjectSelected)
        {
            selectedObject.transform.parent = selectedObjectOriginalParentTransform;
            isAnObjectSelected = false;
        }

        selectedObject = obj;
        isAnObjectSelected = true;
        selectedObjectOriginalParentTransform = selectedObject.transform.parent;
        selectedObject.transform.parent = Camera.main.transform;
    }

    public void forceDeselect()
    {
        if (isAnObjectSelected && selectedObject != null)
        {
            selectedObject.transform.parent = selectedObjectOriginalParentTransform;
            isAnObjectSelected = false;
            selectedObject = null;
        }
    }*/

    private void rotateObjet()
    {
        if (!isAnObjectSelected)
        {
            return;
        }
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Calculating rotation
                float rotationX = touch.deltaPosition.y * rotationSpeed * Time.deltaTime;
                float rotationY = -touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                // applying rotation to selected object
                selectedObject.transform.Rotate(Vector3.up, rotationY, Space.World);
                selectedObject.transform.Rotate(Vector3.right, rotationX, Space.World);
            }
        }
    }

    private void deselectObject()
    {
        if (isAnObjectSelected)
        {
            // The user no longer wants to select this object. Restore its material.
            // disabled because one object has multiple materials and its unnessecary tbh
            selectedObject.GetComponent<Renderer>().material = selectedObjectOriginalMaterial;
            selectedObject.transform.parent = selectedObjectOriginalParentTransform;
            isAnObjectSelected = false;
            dataManager.AddObject(selectedObject);
        }
    }

}
