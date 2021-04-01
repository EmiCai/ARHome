using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button genererButton;
    private InputManager forLimitation;
    public GameObject cube;
    public InputField largeur;
    public InputField longueur;
    public InputField hauteur;

    private SelectionManager maSelection;
    public Button ButtonTourner;
    public Button ButtonSupprimer;
    public Button ButtonTransformer;
   

    // Start is called before the first frame update
    void Start()
    {
        maSelection = GameObject.Find("SelectionHandler").GetComponent<SelectionManager>();
        forLimitation = GameObject.Find("InputManager").GetComponent<InputManager>();
        genererButton.onClick.AddListener(GenererCube);
        ButtonTourner.onClick.AddListener(ActiverRotation);
        ButtonSupprimer.onClick.AddListener(Supprimer);
        ButtonTransformer.onClick.AddListener(ActiverTransformation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenererCube()
    {
        Debug.Log("BOUTON CLIQUER");
        if (largeur.text != "" && longueur.text != "" && hauteur.text != "")//il faudra vérifier si c'est un nombre si on a le temps
        {
            float local_x = float.Parse(largeur.text);
            float local_y = float.Parse(longueur.text);
            float local_z = float.Parse(hauteur.text);
            Debug.Log("DEBUG TAILLE SAISIE : " + local_x + " , " + local_y + " , " + local_z);

            DataHandler.Instance.furniture = cube;
            //Elle change le scale (c'est pas la vrai taille qu'on entre)
            cube.transform.localScale = new Vector3(local_x, local_y, local_z);
            //vu que ca change le scale, l'objet ne sera plus au sol, faut le remettre au sol
            float position_y = (local_y * 0.05f) / 0.1f;
            cube.transform.localPosition = new Vector3(0, position_y, 0);
            forLimitation.setObjectPlaced(false);
            cube.gameObject.tag = "objet";
        }
    }
    void ActiverRotation()
    {
        if(!maSelection.getRotationMode()) //Si pas en mode select
            maSelection.SetRotationMode(true); //Utile pour un autre script
        else
            maSelection.SetRotationMode(false);
    }
    void Supprimer()
    {
        Destroy(maSelection.getSelectedGameObject().gameObject); //On supprime
        maSelection.UnSelect(); //On deséléction car l'object n'existe plus
    }
    void ActiverTransformation()
    {
        if(!maSelection.getTransformerMode())
            maSelection.SetTransformerMode(true); //Utile pour un autre script
        else
            maSelection.SetTransformerMode(false);
    }

}
