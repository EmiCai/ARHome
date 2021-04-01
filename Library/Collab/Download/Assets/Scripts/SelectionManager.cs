using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    //Pour passer en mode s�l�ction sur un objet cliqu�
    private bool selectedMode;
    private Transform selectedGameObject;
    private float startTime;
    public GameObject panelDeSelection;
    public GameObject boutonMeuble;
    public GameObject panelContenuMeuble;
    public GameObject panelGenerationCube;

    //Les differents mode possible en mode selection
    private bool rotationMode;
    private bool transformerMode;
    private bool deplacingMode;
    private bool simulatingMode;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 0.0f;
        selectedMode = false;
        rotationMode = false;
        transformerMode = false;
        simulatingMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Selection Mode : " + selectedMode);
        SwitchSelectMode();
    }

    private void SwitchSelectMode()
    {
        if (Input.touchCount > 0) // S'il y a au moins un doigt sur l'�cran
        {
            Touch touch; 
            touch = Input.GetTouch(0); //On travaille avec qu'un doigt, le premier qui a touch� l'�cran
            var ray = Camera.main.ScreenPointToRay(touch.position); //je dois r�cup position de la o� j'ai toucher
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) //J'ai touch�
            {
                if (startTime == 0.0f && touch.phase == TouchPhase.Stationary) //On lance le Timer si pas lancer et qu'on est en train de rester � la m�me position
                {
                    startTime = Time.time;
                    Debug.Log("Enable Time started at : " + startTime);
                }else if (startTime != 0.0f && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Moved)) //Si il y a timer et qu'on a arret� de rester � la m�me position
                {
                    var currentTime = Time.time; //On r�cup le temps
                    var finalTime = currentTime - startTime; //On r�cup�re la diff�rence par rapport � quand on a commenc� le timer
                    Debug.Log("Enable Time ended at : " + currentTime); 
                    Debug.Log("Enable Elapsed time : " + finalTime);
                    startTime = 0.0f; //On reset le debut du timer
                    if (!selectedMode) //Si �tait pas en mode selection
                    {
                        if (finalTime >= 3.0f) //Si 3sec se sont �coul�s
                        {
                            Select(hit.transform);
                        }
                    }
                    else //Si j'�tait en mode selection
                    {
                        if (finalTime >= 3.0f) //Si 3sec se sont �coul�s
                        {
                            UnSelect();
                        }
                    }

                }
            }
        }
    }
    public void Select(Transform maSelection)
    {
        if(maSelection.gameObject.tag=="objet")
        {
            selectedMode = true; //On passe en mode selection
            selectedGameObject = maSelection;
            panelDeSelection.SetActive(true);
            boutonMeuble.SetActive(true);
            panelContenuMeuble.SetActive(true);

        }
    }
    public void UnSelect()
    {
        selectedMode = false;
        rotationMode = false;
        transformerMode = false;
        selectedGameObject = null;
        panelDeSelection.SetActive(false);
        boutonMeuble.SetActive(false);
        panelContenuMeuble.SetActive(false);
        panelGenerationCube.SetActive(true);
    }
    public Transform getSelectedGameObject()
    {
        return selectedGameObject;
    }
    public void setSelectedGameObject(Transform newObj)
    {
        selectedGameObject = newObj;
    }
    public void SetRotationMode(bool value)
    {
        if (selectedMode)
            rotationMode = value;
        else //Blindage
            rotationMode = false;
    }
    public bool getRotationMode()
    {
        return rotationMode;
    }
    public void SetTransformerMode(bool value)
    {
        if (selectedMode)
            transformerMode = value;
        else //Blindage
            transformerMode = false;
    }
    public bool getTransformerMode()
    {
        return transformerMode;
    }
    public void SetDeplacingMode(bool value)
    {
        deplacingMode = value;
    }
    public bool getDeplacingMode()
    {
        return deplacingMode;
    }

}
