using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARSubsystems;

public class InputManager : MonoBehaviour
{
    // [SerializeField] private <=> à public 
    [SerializeField] private Camera ARCamera;
    [SerializeField] private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Touch touch; //interration user-screen
    private bool objectPlaced;
    private bool instanciated;
    private GameObject objectToPlace;

    void Start()
    {
        objectPlaced = true;
        instanciated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!objectPlaced)
        {
            List<ARRaycastHit> hitpoint = new List<ARRaycastHit>();
            raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitpoint, TrackableType.Planes);
            if (hitpoint.Count > 0)
            {
                if (!instanciated)
                {
                    objectToPlace = Instantiate(DataHandler.Instance.furniture, DataHandler.Instance.furniture.transform.position, DataHandler.Instance.furniture.transform.rotation);
                    instanciated = true;
                }
                objectToPlace.transform.position = hitpoint[0].pose.position;
                objectToPlace.transform.rotation = hitpoint[0].pose.rotation;
            }

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0); //the first screen touch

                //Input.touchCount si doigt sur l'écran
                //Input.GetTouch(0).phase = TouchPhase.Began si la touche a commencé
                //Donc si aucun screen et si la touche n'a pas commencé
                if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
                    return;

                //Si UI a été touché (donc non sur l'endroit où on veut placer l'obj)
                if (IsPointerOverUI(touch))
                    return;

                Debug.Log("Value of placed : " + objectPlaced);
                //Placement si toucher
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    objectPlaced = true;
                    instanciated = false;
                }
            }
        }
        
    }

    bool IsPointerOverUI(Touch touch)
    {
        
        Debug.Log("DEBUG UI");
        //On recupère EventSystem : donne les evenements qui se passe mtn
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        //l'endroit où on veut savoir
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        //Raycast pour savoir si on est face à un UI
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void setObjectPlaced(bool value)
    {
        objectPlaced = value;
    }
    public bool isObjectPlaced()
    {
        return objectPlaced;
    }
    public void destroyObjectGenerated()
    {
        Destroy(objectToPlace);
        instanciated = false;
    }
}
