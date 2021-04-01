using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DeplacingObject : MonoBehaviour
{
    private Transform maSelection;
    [SerializeField] private ARRaycastManager raycastManager;
    private Touch touch;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<SelectionManager>().getDeplacingMode()) //Si en mode déplacement
        {
            List<ARRaycastHit> hitpoint = new List<ARRaycastHit>();
            raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitpoint, TrackableType.Planes);
            if (hitpoint.Count > 0)
            {
                maSelection = GetComponent<SelectionManager>().getSelectedGameObject();
                maSelection.position = hitpoint[0].pose.position;
                maSelection.rotation = hitpoint[0].pose.rotation;
            }
        }
    }
}
