using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    [SerializeField] TextMeshProUGUI HighScore;
    [SerializeField] TextMeshProUGUI CurrentScore;
    [SerializeField] TextMeshProUGUI Speed;
    [SerializeField] TextMeshProUGUI Altitude;
    [SerializeField] TextMeshProUGUI MissileAlert;
    [SerializeField] Canvas InGameCanvas;
    [SerializeField] Canvas DeathCanvas;
    [SerializeField] TextMeshProUGUI DeathScore;

    [SerializeField] Canvas BossCanvas;
    [SerializeField] TextMeshProUGUI BossHealth;

    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject JoyStick;
    [SerializeField] GameObject WASD;
    [SerializeField] Text SwitchJoystickandWASDButton;

    [SerializeField] GameObject TipsMenu;

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
    private void Start()
    {
        if (PlayerPrefs.GetInt("TipsMenu") == 0)
        {
            OpenCloseTipsMenu();
        }
    }
    private void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        HighScore.text = " HighScore : " + GameManager.Instance.HighestScore1.ToString();
        CurrentScore.text = " Score : " + GameManager.Instance.Score1.ToString() + " / " + GameManager.Instance.GetSpawnBossAtScore + " Boss";
        DeathScore.text = " Score : " + GameManager.Instance.Score1.ToString();
        if (GameManager.Instance.PlayerJet != null)
        {
            float speed = Mathf.RoundToInt(GameManager.Instance.PlayerJet.Speed * 10);
            Speed.text = " Speed : " + speed.ToString();
            Altitude.text = " Altitude " + Mathf.RoundToInt(GameManager.Instance.PlayerJet.gameObject.transform.position.y).ToString();
        }
        if (BossCanvas.enabled == true)
        {
            BossHealth.text = " BOSS HEALTH : " + GameManager.Instance.selectedBoss.Health.ToString();
        }
    }
    public void DisplayMissileAlert(bool boolean)
    {
        MissileAlert.enabled = boolean;
    }
    public void DEAD()
    {
        InGameCanvas.enabled = false;
        DeathCanvas.enabled = true;
    }
    public void SetBossEnabled(bool boolean)
    {
        BossCanvas.enabled = boolean;
    }
    public void OpenCloseOptionsMenu()
    {
        if (OptionsMenu.activeSelf == false)
        {
            OptionsMenu.SetActive(true);
        }
        else
        {
            OptionsMenu.SetActive(false);
        }
    }
    public void SwitchJoyStickandWASD()
    {
        if (WASD.activeSelf == true)
        {
            JoyStick.SetActive(true);
            WASD.SetActive(false);
            SwitchJoystickandWASDButton.text = " JoyStick : Yes ";
        }
        else
        {
            WASD.SetActive(true);
            JoyStick.SetActive(false);
            SwitchJoystickandWASDButton.text = " JoyStick : No ";
        }
    }
    public void OpenCloseTipsMenu()
    {
        if (TipsMenu.activeSelf == false)
        {
            TipsMenu.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("TipsMenu", 1);
            TipsMenu.SetActive(false);
        }
    }
}
