using UnityEngine;
using System.Collections;

public class shotEject : MonoBehaviour
{

    public int ejectSpeed = 100;
    public double fireRate = 0.5;
    public Rigidbody bulletCasing;

    public double nextfire = 0.0;
    private bool fullAuto = false;

    public int clip = 30;
    public int maxclip = 30;
    public int reserve = 300;
    public int minreserve = 0;
    public bool isReload = true;
    public bool reloadsoundplay = false;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public bool automatique = true;
    public GameObject fireShot;

    
    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextfire )
        {
            if(clip >= 1)
            {
                var bullet = new Rigidbody();
                nextfire = Time.time + fireRate;
                bullet = Instantiate(bulletCasing, transform.position, transform.rotation) as Rigidbody;
                clip -= 1;
                GetComponent<AudioSource>().PlayOneShot(shotSound);
                bullet.velocity = transform.TransformDirection(Vector3.left * ejectSpeed);
                Instantiate(fireShot, transform.position, transform.rotation);
            }
            
        }
        if(automatique == true)
        {
            if (Input.GetKeyDown("v"))
            {
                fullAuto = !fullAuto;
            }
        }
        

        if (Input.GetKeyDown("r"))
        {
            if (reloadsoundplay == true)
            {
                GetComponent<AudioSource>().PlayOneShot(reloadSound);
            }

            
            if(reserve > 30 )
            {
                RemoveReserve();
                
                clip += maxclip - clip;
            }

            if(reserve < 30 )
            {
               
                clip += reserve;
                
                RemoveReserve();

            }

            

        }

        if(reserve < 1)
        {
            reserve = 0;
        }

        


        if (fullAuto == true)
        {
            fireRate = 0.10;
        }
        else
        {
            fireRate = 0.5;
        }

        if(clip == maxclip)
        {
            reloadsoundplay = false;
        }

        if(clip < maxclip)
        {
            reloadsoundplay = true;
        }

        if (reserve == 0)
        {
            reloadsoundplay = false;
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 150, 30),clip+" / "+reserve);
    }
    void RemoveReserve()
    {

        reserve -= maxclip - clip;
    }
}
