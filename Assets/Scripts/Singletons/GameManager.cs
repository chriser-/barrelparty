using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum States
    {
        MainMenu,
        Game,
        PauseMenu,
        GameOver
    }

    [SerializeField]
    private States gameState = States.MainMenu;

    [SerializeField]
    private float gameTime;

    [SerializeField]
    private List<PlayerController> m_Players = new List<PlayerController>();
    [SerializeField] private PlayerController m_PlayerPrefab;
    private Dictionary<int, PlayerController> m_PlayerIdToPlayerMapping = new Dictionary<int, PlayerController>();
    [SerializeField] private Material[] materials;
    public Material[] Materials { get { return materials; } }

    public int startLives = 5;

    public List<PlayerController> Players
    {
        get { return m_Players; }
    }

    public float timePlayed { get { return gameTime; } }

    public float ambientIntensity = 1.0f;

    void Start()
    {
        gameState = States.MainMenu;
        Cursor.visible = false;
    }

    void Update()
    {
        if (gameState == States.Game)
        {
            gameTime += Time.deltaTime;
        }
        /*
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }
        for (int i = 1; i < 29; i++)
        {
            string axis = "Joy1Axis" + i;
            if (Mathf.Abs(Input.GetAxis(axis)) > 0.01f)
            {
                Debug.Log(axis + ": " + Input.GetAxis(axis));
            }
        }

        for (int i = 0; i < 127; i++)
        {
            foreach (var controller in ReInput.controllers.Controllers)
            {
                if (controller.GetButtonDown(i))
                {
                    Debug.Log("Button " + i);
                }
            }
        }
        */
    }

    public PlayerController SpawnPlayer(int playerid, Vector3 spawnPos)
    {
        if (!m_PlayerIdToPlayerMapping.ContainsKey(playerid))
        {
            PlayerController newPlayer = Instantiate(m_PlayerPrefab).GetComponent<PlayerController>();
            newPlayer.transform.position = spawnPos;
            newPlayer.PlayerNum = Players.Count;
            newPlayer.PlayerId = playerid;
            //newPlayer.OnDie += newPlayerDie;
            //newPlayer.SetCharacterMaterial(m_PlayerMaterials[m_PlayerIdToPlayerMapping.Count]);
            m_PlayerIdToPlayerMapping[playerid] = newPlayer;
            return newPlayer;
        }
        return null;
    }

    public void OnDeath()
    {
        int count = 0;
        foreach(PlayerController p in m_Players)
        {
            if (p.Hearts > 0) count++;
        }
        if (count < 1 || (count == 1 && m_Players.Count > 1)) OnEndRound();
    }

    public void StartGame(Action done = null)
    {
        RenderSettings.ambientIntensity = ambientIntensity;
        LoadScene(3, () =>
        {
            gameState = States.Game;
            AudioController.PlayMusic("Game Music");
            gameTime = 0f;
            Players.ForEach(p =>
            {
                p.SetControlable(true);
                p.transform.position = new Vector3(0, 0, (p.PlayerNum*2) - 5);
                //offset a litle bit to prevent spawning on top
                if (done != null)
                    done();
            });
        });
    }

    public void OnEndRound()
    {
        gameState = States.GameOver;
        LoadScene(4, () =>
        {
            AudioController.PlayMusic("Gameover");
        });
    }

    public void BackToMainMenu()
    {
        gameState = States.MainMenu;
        LoadScene(1, () =>
        {
            foreach (var item in m_Players)
            {
                Destroy(item.gameObject);
            }
            m_Players.Clear();
            m_PlayerIdToPlayerMapping.Clear();
            AudioController.PlayMusic("Menu");
        });
    }

    public bool IsLoadingLevel
    {
        get;
        private set;
    }

    public static void LoadScene(int scene, Action done = null)
    {
        Instance.StartCoroutine(Instance.loadScene((int)scene, done));
    }

    private IEnumerator loadScene(int index, Action done)
    {
        IsLoadingLevel = true;
        //TODO: loading screen
        var asyncSceneLoad = SceneManager.LoadSceneAsync(index);
        while (asyncSceneLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncSceneLoad.allowSceneActivation = true;
        while (!asyncSceneLoad.isDone)
            yield return null;
        if (done != null)
            done();
        IsLoadingLevel = false;
    }

}
