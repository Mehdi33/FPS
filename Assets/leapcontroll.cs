using UnityEngine;
using System.Collections;
using Leap;
using System;

public class leapcontroll : MonoBehaviour {
    Controller m_leapController;
    float m_lastBlastTime = 0.0f;
    public float tiltAngle = 170.0F;
    public float smooth = 2.0F;
    GameObject m_carriedObject;
   bool m_handOpenThisFrame = false;
   bool m_handOpenLastFrame = false;
    public AudioClip walkSound;



    private AudioSource m_AudioSource;
    
    // Use this for initialization
    void Start () {

        m_leapController = new Controller();
        m_leapController.EnableGesture(Gesture.GestureType.TYPESWIPE);

    }
	
	

    

    void OnHandOpen(Hand h)
    {
        m_carriedObject = null;
    }

    void OnHandClose(Hand h)
    {
        RaycastHit hit;
        if(Physics.SphereCast(new Ray(transform.position + transform.forward * 2.0f, transform.forward),2.0f,out hit))
        {
            m_carriedObject = hit.collider.gameObject;
        }
    }

    bool IsHandOpen(Hand h)
    {
        return h.Fingers.Count > 1;
    }

    void Shoot(Hand h)
    {
        FingerList extendedFingerList = h.Fingers.Extended();
        FingerList indexFingerList = h.Fingers.FingerType(Finger.FingerType.TYPE_INDEX);
        Finger indexFinger = indexFingerList[0];
        if (h.IsRight )
        {
           
                if(GameObject.Find("Eject").GetComponent<shotEject>().clip >= 1 && indexFinger.IsExtended && extendedFingerList.Count == 1 && Time.time > GameObject.Find("Eject").GetComponent<shotEject>().nextfire)
                {
                       
                    
                    var bullet = new Rigidbody();
                    GameObject.Find("Eject").GetComponent<shotEject>().nextfire = Time.time + GameObject.Find("Eject").GetComponent<shotEject>().fireRate;
                    bullet = Instantiate(GameObject.Find("Eject").GetComponent<shotEject>().bulletCasing, GameObject.Find("Eject").GetComponent<shotEject>().transform.position, GameObject.Find("Eject").GetComponent<shotEject>().transform.rotation) as Rigidbody;
                    GameObject.Find("Eject").GetComponent<shotEject>().clip -= 1;
                    GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Eject").GetComponent<shotEject>().shotSound);
                    bullet.velocity = GameObject.Find("Eject").GetComponent<shotEject>().transform.TransformDirection(Vector3.left * GameObject.Find("Eject").GetComponent<shotEject>().ejectSpeed);
                    Instantiate(GameObject.Find("Eject").GetComponent<shotEject>().fireShot, GameObject.Find("Eject").GetComponent<shotEject>().transform.position, GameObject.Find("Eject").GetComponent<shotEject>().transform.rotation);
                }
                else
                {
                    
                }
            
        }
    }

    void ProcessLook( Hand handright)
    {
        float rotThreshold = 1f;
        
        float roll = handright.PalmNormal.Roll;
        Debug.Log(roll);
        
        if (roll > rotThreshold)
        {
           
            
            GameObject.Find("FPS").transform.Rotate(Vector3.up, -45 * Time.deltaTime);
            
        }
        else if (roll < -rotThreshold)
        {
           
            GameObject.Find("FPS").transform.Rotate(Vector3.up, 45 * Time.deltaTime);
        }

    }



    void MoveCharacter(Hand leftmost)
    {
        
        if (leftmost.PalmPosition.ToUnityScaled().z > 0.1f   )
        {
            GameObject.Find("FPS").transform.position += GameObject.Find("FPS").transform.forward * 0.1f;
            
        }

        if (leftmost.PalmPosition.ToUnityScaled().z < -0.1f)
        {
            GameObject.Find("FPS").transform.position += GameObject.Find("FPS").transform.forward * -0.1f;
        }

        if (leftmost.PalmPosition.ToUnityScaled().x > -0.08f)
        {
            GameObject.Find("FPS").transform.position += GameObject.Find("FPS").transform.right * 0.1f;
        }

        if (leftmost.PalmPosition.ToUnityScaled().x < -0.12f  )
        {
            GameObject.Find("FPS").transform.position += GameObject.Find("FPS").transform.right * -0.1f;
        }

        
    }

    void HandCallbacks(Hand h)
    {
        if (m_handOpenThisFrame && m_handOpenLastFrame == false)
        {
            OnHandOpen(h);
        }

        if (m_handOpenThisFrame == false && m_handOpenLastFrame == true)
        {
            OnHandClose(h);
        }
    }

    void MoveCarriedObject()
    {
        if (m_carriedObject != null)
        {
            Vector3 targetPos = transform.position + new Vector3(transform.forward.x, 0, transform.forward.z) * 5.0f;
            Vector3 deltaVec = targetPos - m_carriedObject.transform.position;
            if (deltaVec.magnitude > 0.1f)
            {
                m_carriedObject.GetComponent<Rigidbody>().velocity = (deltaVec) * 10.0f;
            }
            else
            {
                m_carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    void FixedUpdate()
    {
        Frame f = m_leapController.Frame();
        HandList foremosthand = f.Hands;
        
        Hand leftmost = foremosthand.Leftmost;
        Hand rightmost = foremosthand.Rightmost;
        Hand frontmost = foremosthand.Frontmost;
        GestureList gestures = f.Gestures();

        if (foremosthand != null && foremosthand.Count == 2)
        {
            
            Reload( gestures);
            ProcessLook( rightmost);
            MoveCharacter(leftmost);
            //Viser(leftmost);
            ChangeWeapons(gestures);
            
            Shoot(rightmost);
        }
        m_handOpenLastFrame = m_handOpenThisFrame;
    }

    void ChangeWeapons(GestureList gestures)
    {

        for (int i = 0; i < gestures.Count; i++)
        {
            Gesture gesture = gestures[i];
            if (gesture.Type == Gesture.GestureType.TYPECIRCLE)
            {
                CircleGesture circle = new CircleGesture(gesture);
                float turns = circle.Progress;
                Debug.Log(turns);
                if (turns == 1)
                {
                    GameObject.Find("FPSController").GetComponent<SwapWeapons>().Primary.SetActive(true);
                    GameObject.Find("FPSController").GetComponent<SwapWeapons>().Secondary.SetActive(false);
                }
                if (turns == 2)
                {
                    GameObject.Find("FPSController").GetComponent<SwapWeapons>().Primary.SetActive(false);
                    GameObject.Find("FPSController").GetComponent<SwapWeapons>().Secondary.SetActive(true);
                }
            }
        }
            
    }

    void Viser(Hand leftmost)
    {
        FingerList extendedFingerList = leftmost.Fingers.Extended();
        if (extendedFingerList.Count == 0)
        {
            
            GameObject.Find("M4A1 Sopmod").transform.localPosition = GameObject.Find("M4A1 Sopmod").GetComponent<Aiming>().AimPos;
        }

        if (extendedFingerList.Count > 0)
        {
            
            GameObject.Find("M4A1 Sopmod").transform.localPosition = GameObject.Find("M4A1 Sopmod").GetComponent<Aiming>().NormalPos;
        }
    }

    void Reload( GestureList gestures)
    {
        for (int i = 0; i < gestures.Count; i++)
        {
            Gesture gesture = gestures[i];
            if (gesture.Type == Gesture.GestureType.TYPESWIPE)
            {
                if (GameObject.Find("Eject").GetComponent<shotEject>().reloadsoundplay == true)
                {
                    GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Eject").GetComponent<shotEject>().reloadSound);
                }


                if (GameObject.Find("Eject").GetComponent<shotEject>().reserve > 30)
                {
                    GameObject.Find("Eject").GetComponent<shotEject>().reserve -= GameObject.Find("Eject").GetComponent<shotEject>().maxclip - GameObject.Find("Eject").GetComponent<shotEject>().clip;

                    GameObject.Find("Eject").GetComponent<shotEject>().clip += GameObject.Find("Eject").GetComponent<shotEject>().maxclip - GameObject.Find("Eject").GetComponent<shotEject>().clip;
                }

                if (GameObject.Find("Eject").GetComponent<shotEject>().reserve < 30)
                {

                    GameObject.Find("Eject").GetComponent<shotEject>().clip += GameObject.Find("Eject").GetComponent<shotEject>().reserve;

                    GameObject.Find("Eject").GetComponent<shotEject>().reserve -= GameObject.Find("Eject").GetComponent<shotEject>().maxclip - GameObject.Find("Eject").GetComponent<shotEject>().clip;

                }
            }
        }
    }
}
