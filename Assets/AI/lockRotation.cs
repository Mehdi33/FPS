using UnityEngine;

public class lockRotation : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
