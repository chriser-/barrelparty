using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private List<PlayerController> m_Players = new List<PlayerController>();
    [SerializeField] private PlayerController m_PlayerPrefab;
    private Dictionary<int, PlayerController> m_PlayerIdToPlayerMapping = new Dictionary<int, PlayerController>();

    public List<PlayerController> Players
    {
        get { return m_Players; }
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
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

    public static void StartGame(Action done = null)
    {
        LoadScene(3, () =>
        {
            AudioController.PlayMusic("Game Music");
            Instance.Players.ForEach(p =>
            {
                p.SetControlable(true);
                p.transform.position = new Vector3(0, 0, (p.PlayerNum*2) - 5);
                //offset a litle bit to prevent spawning on top
                if (done != null)
                    done();
            });
        });
    }

    public static bool IsLoadingLevel
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
