using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public AudioSource MissileAlertSource;
    public int HighestScore1 { get => HighestScore;}
    public int Score1 { get => Score; set => Score = value; }

    public List<GameObject> Target = new List<GameObject>();

    public List<GameObject> Turrets = new List<GameObject>();

    int Score;

    int HighestScore;

    public PlayerJet PlayerJet;
    public BossHealth[] Bosses;
    int SpawnBossAtScore = 50;
    public BossHealth selectedBoss;
    public int GetSpawnBossAtScore { get => SpawnBossAtScore; }

    private void Awake()
    {
        Application.targetFrameRate = 120;
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
        HighestScore = PlayerPrefs.GetInt("HighScore");
    }
    private void Update()
    {
        ScoreCounter();
        BossSpawner();
    }
    void ScoreCounter()
    {
        if (Score1 > HighestScore1)
        {
            PlayerPrefs.SetInt("HighScore", HighestScore);
            HighestScore = Score;
        }
    }
    void BossSpawner()
    {
        if (Score >= SpawnBossAtScore)
        {
            for (int i = 0; i < Target.Count; i++)
            {
                Target[i].SetActive(false);
            }
            for (int i = 0; i < Turrets.Count; i++)
            {
                Turrets[i].SetActive(false);
            }
            int index = Random.Range(0, Bosses.Length);
            selectedBoss = Bosses[index];
            Bosses[index].Health = 150;
            Bosses[index].gameObject.SetActive(true);
            SpawnBossAtScore += 50;
        }
    }
    public void Reload()
    {
        StartCoroutine(DeathWait());
    }
    IEnumerator DeathWait()
    {
        PlayerJet.Instance.DEAD();
        UIManager.Instance.DEAD();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
    public void SpawnTarget()
    {
        for (int Targetnumber = Random.Range(0, Target.Count); Targetnumber < Target.Count; Targetnumber++)
        {
                if (Target[Targetnumber].activeInHierarchy == false)
                {
                    Target[Targetnumber].SetActive(true);
                    SpawnEnemy();
                    break;
                }
        }
    } 
    public void SpawnEnemy()
{
        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject enemy in Turrets)
            {
                if (enemy.activeInHierarchy == false)
                {
                    enemy.SetActive(true);
                    break;
                }
            }
        }
    }
}
