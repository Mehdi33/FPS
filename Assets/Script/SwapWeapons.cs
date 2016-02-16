using UnityEngine;
using System.Collections;

public class SwapWeapons : MonoBehaviour {

    public GameObject Primary;
    public GameObject Secondary;
    // Use this for initialization
    void Start () {
        Primary.SetActive(true);
        Secondary.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Primary.SetActive(true);
            Secondary.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Primary.SetActive(false);
            Secondary.SetActive(true);
        }
    }
}
