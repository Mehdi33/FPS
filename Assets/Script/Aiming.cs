using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour {

    public Vector3 NormalPos;
    public Vector3 AimPos;
    public GameObject Weapon;

	// Use this for initialization
	void Start () {
        transform.localPosition = NormalPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire2"))
        {
            transform.localPosition = AimPos;
        }

        if (!Input.GetButton("Fire2"))
        {
            transform.localPosition = NormalPos;
        }
    }
}
