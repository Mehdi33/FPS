using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(disparu());
	}
    IEnumerator disparu()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	
}
