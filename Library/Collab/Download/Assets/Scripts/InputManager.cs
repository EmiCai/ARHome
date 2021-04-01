using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // [SerializeField] private <=> � public 
    [SerializeField] private Camera ARCamera;
    [SerializeField] private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Touch touch; //interration user-screen
    private bool objectPlaced;

    void Start()
    {
        objectPlaced = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0); //the first screen touch

            //Input.touchCount si doigt sur l'�cran
            //Input.GetTouch(0).phase = TouchPhase.Began si la touche a commenc�
            //Donc si aucun screen et si la touche n'a pas commenc�
            if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
                return;

            //Si UI a �t� touch� (donc non sur l'endroit o� on veut placer l'obj)
            if (IsPointerOverUI(touch))
                return;

            Debug.Log("Value of placed : " + objectPlaced);
            //lancer un ray � partir de point vers la sc�ne AR et converti le point en ray.
            if (!objectPlaced)
            {
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                if (raycastManager.Raycast(ray, hits))
                {
                    //position du hit
                    Pose pose = hits[0].pose;
                    Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation);
                    objectPlaced = true;
                }
            }
        }
    }

    bool IsPointerOverUI(Touch touch)
    {
        
        Debug.Log("DEBUG UI");
        //On recup�re EventSystem : donne les evenements qui se passe mtn
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        //l'endroit o� on veut savoir
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        //Raycast pour savoir si on est face � un UI
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void setObjectPlaced(bool value)
    {
        objectPlaced = value;
    }
}
