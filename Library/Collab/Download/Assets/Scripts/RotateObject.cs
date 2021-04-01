using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private bool dejaEnRotation;
    private float rotation;
    private Transform maSelection;
    // Start is called before the first frame update
    void Start()
    {
        dejaEnRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SelectionManager>().getRotationMode()) //Si mode rotation activ�
        {
            Debug.Log("Mode rotation");
            if (!dejaEnRotation)
            {
                maSelection = GetComponent<SelectionManager>().getSelectedGameObject();
                rotation = maSelection.rotation.y;
                dejaEnRotation = true;
            }           

            if (Input.touchCount > 0) // S'il y a au moins un doigt sur l'�cran
            {
                Touch touch;
                touch = Input.GetTouch(0); //On travaille avec qu'un doigt, le premier qui a touch� l'�cran
                if (touch.phase == TouchPhase.Moved) //Un doigt a boug� sur l'�cran
                {
                    if (touch.deltaPosition.x > 0) //Droite, positif
                    {
                        rotation = rotation - 4;
                        if (rotation == 0) //Si on atteint la limite
                            rotation = 360;
                        maSelection.localEulerAngles = new Vector3(maSelection.rotation.x, rotation, maSelection.rotation.z);
                    }
                    if (touch.deltaPosition.x < 0) //gauche, n�gatif
                    {
                        rotation = rotation + 4;
                        if (rotation == 360) //Si on atteint la limite
                            rotation = 0;
                        maSelection.localEulerAngles = new Vector3(maSelection.rotation.x, rotation, maSelection.rotation.z);
                    }
                }
            }
        }
        else
        {
            dejaEnRotation = false;
        }
    }
}

