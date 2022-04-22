using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJet : MonoBehaviour
{
    private static PlayerJet _instance;

    [SerializeField] Joystick joystick;
    public static PlayerJet Instance { get { return _instance; } }
    public GameObject ActualJet;
    [SerializeField] float RollSpeed = 100;
    [SerializeField] float PitchSpeed = 100;

    public float Speed = 10;
    [SerializeField] float SpeedMax = 40;
    [SerializeField] float SpeedMin = 10;
    [SerializeField] Rigidbody RB;
    [SerializeField] GameObject Lazer;
    float Lazertimer = 0;
    [SerializeField] Transform LazerSpawn;

    [SerializeField] Camera camera;
    [SerializeField] Camera Deathcamera;
    [SerializeField] Camera RearCamera;

    public GameObject[] Missiles;

    public List<GameObject> RadarList = new List<GameObject>();
    public List<GameObject> GunRadarList = new List<GameObject>();

    [SerializeField] LayerMask RangeAble;

    public GameObject Target;
    GameObject GunTarget; 
    Rigidbody TargetsRigidbody;
    GameObject OldGunTarget;
    GameObject OldTarget;

    public AudioSource audioSource;

    bool w, a, s, d = false;
    bool LookBehind = false;
    bool SpeedingUp, SpeedingDown = false;
    bool shooting = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Update()
    {
        if (joystick.isActiveAndEnabled == true)
        {
            gameObject.transform.Rotate(Vector3.forward * RollSpeed * -joystick.Horizontal * Time.deltaTime);
            gameObject.transform.Rotate(Vector3.right * PitchSpeed * joystick.Vertical * Time.deltaTime);
        }
        SpeedMaintain();
        SpeedLimiter();
        AutoRangeLazer();
        if (Input.GetKey(KeyCode.D) || d == true)
        {
            Rollright();
        }
        if (Input.GetKey(KeyCode.A) || a == true)
        {
            RollLeft();
        }
        if (Input.GetKey(KeyCode.W) || w == true)
        {
            PitchUp();
        }
        if (Input.GetKey(KeyCode.S) || s == true)
        {
            PitchDown();
        }
        if (Input.GetKey(KeyCode.Q) || SpeedingUp == true)
        {
            SpeedUp();
        }
        if (Input.GetKey(KeyCode.Z) || SpeedingDown == true)
        {
            SpeedDown();
        }
        if ( shooting == true)
        {
            ShootLazer();
        }
        if (Input.GetKey(KeyCode.E) || LookBehind == true)
        {
            camera.enabled = false;
            RearCamera.enabled = true;
        }
        else
        {
            camera.enabled = true;
            RearCamera.enabled = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("FireMissile");
            FireMissile();
        }
    }
    public void Rollright()
    {
        gameObject.transform.Rotate(Vector3.forward * -RollSpeed * Time.deltaTime);
    }
    public void RollLeft()
    {
        gameObject.transform.Rotate(Vector3.forward * RollSpeed * Time.deltaTime);
    }
    public void PitchUp()
    {
        gameObject.transform.Rotate(Vector3.right * PitchSpeed * Time.deltaTime);
    }
    public void PitchDown()
    {
        gameObject.transform.Rotate(Vector3.right * -PitchSpeed * Time.deltaTime);
    }
    public void SpeedMaintain()
    {
            RB.velocity = gameObject.transform.forward * Speed;
        
    }
    public void SpeedUp()
    {
        if (Speed <= SpeedMax)
        {
            Speed += 10 * Time.deltaTime;
        }
    }
    public void SpeedDown()
    {
        if (Speed >= SpeedMin)
        {
            Speed -= 10 * Time.deltaTime;
        }
    }
    void SpeedLimiter()
    {
        if (Speed < SpeedMin)
        {
            Speed = SpeedMin;
        }
        if (Speed > SpeedMax)
        {
            Speed = SpeedMax;
        }
    }
    public void FireLazerContinious()
    {
        if (shooting == true)
        {
            shooting = false;
        }
        else if (shooting == false)
        {
            shooting = true;
        }
    }
    public void ShootLazer()
    {
        Lazertimer += 1 * Time.deltaTime;
        if (Lazertimer > .1f)
        {
            Instantiate(Lazer, LazerSpawn.position, LazerSpawn.transform.rotation);
            AudioManager.Instance.PlayOneShot(audioSource, Sound.AllSoundsTypes.Lazer);
            Lazertimer = 0;
        }
    }
    void AutoRangeLazer()
    {
        if (GunTarget != null)
        {
            if (GunTarget.activeSelf == true)
            {
                if (OldGunTarget != null || OldGunTarget != GunTarget)
                {
                    try
                    {
                        TargetsRigidbody = GunTarget.GetComponent<Rigidbody>();
                        OldGunTarget = GunTarget;
                    }
                    catch
                    {
                        OldGunTarget = GunTarget;
                    }
                }
                if (GunTarget != null)
                {
                    try
                    {
                        LazerSpawn.LookAt(GunTarget.transform.position + TargetsRigidbody.velocity);
                    }
                    catch
                    {
                        LazerSpawn.LookAt(GunTarget.transform.position);
                    }
                }
            }
            else
            {
                GunRadarList.Clear();
                GunTarget = null;
            }
        }
        else
        {
            GunDefaultMode();
        }
        if (GunRadarList.Count > 0)
        {
            for (int i = 0; i < GunRadarList.Count; i++)
            {
                if (GunRadarList[i] != null)
                {
                    if (GunRadarList[i].activeSelf == true)
                    {
                        GunTarget = GunRadarList[i];
                    }
                }
            }
        }
        else
        {
            GunDefaultMode();
        }
    }
    void GunDefaultMode()
    {
        GunTarget = null;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Mathf.Infinity, RangeAble))
        {
            LazerSpawn.LookAt(hit.point);
        }
        Debug.DrawLine(camera.transform.position, hit.point);
    }
    public void LockMissile()
    {
        if (Missiles[5] != null)
        {
            if (RadarList.Count > 0)
            {
                for (int i = 0; i < RadarList.Count; i++)
                {
                    if (RadarList[i] != null)
                    {
                        if (RadarList[i].activeSelf == true)
                        {
                            if (RadarList[i] != OldTarget)
                            {
                                Target = RadarList[i];
                                AudioManager.Instance.PlaySound(audioSource, Sound.AllSoundsTypes.TargetLocked);
                                OldTarget = Target;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    
    public void FireMissile()
    {
        if (Target != null)
        {
            for (int i = 0; i < Missiles.Length; i++)
            {
                if (Missiles[i] != null)
                {
                    Missiles[i].GetComponent<Missile>().Target = Target;
                    Missiles[i] = null;
                    RadarList.Remove(Target);
                    break;
                }
            }
            Target = null;
        }
        for (int i = 0; i < RadarList.Count; i++)
        {
            if (RadarList[i] == null)
            {
                RadarList.Remove(RadarList[i]);
            }
        }
    }
    public void W()
    {
            if (w == false)
            {
                w = true;
            }
            else if (w == true)
            {
                w = false;
            }
        
    }
    public void A()
    {
            if (a == false)
            {
                a = true;
            }
            else if (a == true)
            {
                a = false;
            }
      
    }
    public void S()
    {
            if (s == false)
            {
                s = true;
            }
            else if (s == true)
            {
                s = false;
            }
        
    }
    public void D()
    {
            if (d == false)
            {
                d = true;
            }
            else if (d == true)
            {
                d = false;
            }
        
    }
    public void speedUp()
    {
        if (SpeedingUp == false)
        {
            SpeedingUp = true;
        }
        else if (SpeedingUp == true)
        {
            SpeedingUp = false;
        }
    }
    public void speedDown()
    {
        if (SpeedingDown == true)
        {
            SpeedingDown = false;
        }
        else if (SpeedingDown == false)
        {
            SpeedingDown = true;
        }
    }
    public void SwitchCamera()
    { 
        if (LookBehind == true)
        {
            LookBehind = false;
        }
        else if (LookBehind == false)
        {
            LookBehind = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 11 && collision.gameObject.tag != "Target")
        {
            GameManager.Instance.Reload();
        }
    }
    public void DEAD()
    {
        RB.useGravity = true;
        Deathcamera.gameObject.SetActive(true);
        Deathcamera.gameObject.transform.parent = null;
        RearCamera.enabled = false;
        camera.enabled = false;
        this.enabled = false;
    }
}
