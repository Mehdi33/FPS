using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour {

    public int ammo = 30;
    public GameObject eject;

    private shotEject shoteject;
    private bool showGUI;

	// Use this for initialization
	void Start () {

       
    }
	
	// Update is called once per frame
	void Update () {
	    if(showGUI == true)
        {
            if (Input.GetKeyDown("e"))
            {
                GameObject.Find("Eject").GetComponent<shotEject>().reserve += ammo;
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            showGUI = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            showGUI = false;
        }
    }

    void OnGUI()
    {
        if (showGUI == true)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 12, 200, 25), "Press E to pickup ammo");
        }
    }
}
