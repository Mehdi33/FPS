using UnityEngine;
using System.Collections;

public class ThrowGrenades : MonoBehaviour {

    private PlayerStat playerstat;
    public Rigidbody grenadeCasing;
    public int ejectSpeed = 15;
	// Use this for initialization
	void Start () {
        playerstat = GameObject.Find("PlayerStats").GetComponent<PlayerStat>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(playerstat.grenades >= 1)
        {
            if (Input.GetKeyDown("g"))
            {
                Rigidbody grenade;
                grenade = Instantiate(grenadeCasing, transform.position, transform.rotation) as Rigidbody;
                grenade.velocity = transform.TransformDirection(Vector3.forward * ejectSpeed);
                playerstat.grenades -= 1;
            }
        }
	}
}
