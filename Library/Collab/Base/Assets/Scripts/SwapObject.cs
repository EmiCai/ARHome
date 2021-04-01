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

            meuble.transform.localScale = selection.localScale;
            float position_y = (meuble.transform.localPosition.x * 0.05f) / 0.1f;
            meuble.transform.localPosition = new Vector3(0, position_y, 0);
            DataHandler.Instance.furniture = meuble;
            GameObject myNewObject = Instantiate(DataHandler.Instance.furniture, selection.position, selection.rotation);
            Destroy(selection.gameObject);
            SM.setSelectedGameObject(myNewObject.transform);

        }

    }
}
