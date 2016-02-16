using UnityEngine;
using System.Collections;

public class DestroyFireShot : MonoBehaviour {


    public float time;
	// Use this for initialization
	void Start () {
        StartCoroutine(disparu());
    }
    IEnumerator disparu()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}
