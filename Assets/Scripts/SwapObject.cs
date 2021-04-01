using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapObject : MonoBehaviour
{
    private Button btn;
    private SelectionManager SM;
    public GameObject meuble;
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("SelectionHandler").GetComponent<SelectionManager>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ChangeObject);
        meuble.gameObject.tag = "objet";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeObject()
    {
        Transform selection;
        selection = SM.getSelectedGameObject();
        if (selection)
        {
            /*
            //modif taille
            Vector3 SL = selection.localScale;
            float sizeX = meuble.GetComponent<MeshFilter>().mesh.bounds.size.x;
            float sizeY = meuble.GetComponent<MeshFilter>().mesh.bounds.size.y;
            float sizeZ = meuble.GetComponent<MeshFilter>().mesh.bounds.size.z;

            Vector3 rescale = meuble.transform.localScale;

            float newX = (SL.x * rescale.x) / sizeX;
            float newY = (SL.y * rescale.y) / sizeY;
            float newZ = (SL.z * rescale.z) / sizeZ;

            meuble.transform.localScale = new Vector3(newX, newY, newZ);
            //modif taille
            */
            meuble.transform.localScale = selection.localScale;
            float position_y = (meuble.transform.localPosition.x * 0.05f) / 0.1f;
            meuble.transform.localPosition = new Vector3(0, position_y, 0);
            DataHandler.Instance.furniture = meuble;
            GameObject myNewObject = Instantiate(DataHandler.Instance.furniture, selection.position, selection.rotation);
            Debug.Log("DEBUG tag in swap : " + meuble.gameObject.tag);
            Destroy(selection.gameObject);
            SM.setSelectedGameObject(myNewObject.transform);

        }

    }
}
