using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Rewired;

public class PlayerSelectController : MonoBehaviour
{

#pragma warning disable 649
    [System.Serializable]
    private class PlayerSelectInfo
    {
        public RectTransform RectTransform;
        public Text JoinText;
        public Text ReadyText;
        public bool IsReady;
    }
#pragma warning restore 649
    [SerializeField]
    private List<PlayerSelectInfo> m_PlayerSelects;
    [SerializeField]
    private Text m_CountdownText;
    private Dictionary<int, int> m_PlayerIdToSpawnMapping = new Dictionary<int, int>();
    private int playersJoined = 0;
    private int playersReady = 0;
    private IEnumerator m_CountdownCoroutine;

    void Update()
    {
        foreach (var player in ReInput.players.Players)
        {
            if (player.GetButtonDown("Jump"))
            {
                if (m_PlayerIdToSpawnMapping.ContainsKey(player.id))
                    readyPlayer(player.id);
                else if (m_PlayerIdToSpawnMapping.Count < 4)
                    spawnPlayer(player.id);

                if (m_CountdownCoroutine != null)
                {
                    StopCoroutine(m_CountdownCoroutine);
                    m_CountdownText.gameObject.Hide();
                }
            }
        }

        if (playersJoined > 0 && playersReady == playersJoined && m_CountdownText.gameObject.activeSelf == false)
        {
            m_CountdownCoroutine = showCountdown();
            StartCoroutine(m_CountdownCoroutine);
        }
    }

    private void spawnPlayer(int playerid)
    {
        int spawnNum = GameManager.Instance.Players.Count;
        Vector3 spawnPos = m_PlayerSelects[spawnNum].RectTransform.transform.position;
        spawnPos.z = 1;
        PlayerController player = GameManager.Instance.SpawnPlayer(playerid, spawnPos);

        if (player != null)
        {
            playersJoined++;
            m_PlayerIdToSpawnMapping[playerid] = spawnNum;
            player.Character.Rigidbody.isKinematic = true;
            player.SetControlable(false);
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            m_PlayerSelects[spawnNum].JoinText.gameObject.SetActive(false);
            DontDestroyOnLoad(player.gameObject);
        }
    }

    private void readyPlayer(int controller)
    {
        PlayerSelectInfo info = m_PlayerSelects[m_PlayerIdToSpawnMapping[controller]];
        info.IsReady = !info.IsReady;
        info.ReadyText.gameObject.SetActive(info.IsReady);
        if (info.IsReady)
            playersReady++;
        else
            playersReady--;
    }

    private IEnumerator showCountdown()
    {
        m_CountdownText.gameObject.Show();
        for (int i = 5; i > 0; i--)
        {
            m_CountdownText.text = i.ToString();
            AudioController.Play("Countdown");
            yield return new WaitForSeconds(1f);
        }
        AudioController.Play("Fight");
        GameManager.StartGame();

    }
}
