using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField, Range(1f, 7f)]
    private float m_CameraDistance = 3f;
    [SerializeField, Range(1f, 7f)]
    private float m_MinCameraDistance = 2f;

    void FixedUpdate()
    {
        if (GameManager.Instance.Players.Count == 0)
            return;

        Vector3 cameraPosition = transform.position;
        List<PlayerController> playersAlive = GameManager.Instance.Players.Where(p => p.Hearts > 0).ToList();

        if (playersAlive.Count > 0)
        {
            Vector3 relativeCenter = playersAlive.First().transform.position;
            m_CameraDistance = m_MinCameraDistance;

            //calculate center of camera
            if (playersAlive.Count > 1)
            {
                float xMin = playersAlive.Min(p => p.transform.position.x);
                float yMin = playersAlive.Min(p => p.transform.position.y);

                float xMax = playersAlive.Max(p => p.transform.position.x);
                float yMax = playersAlive.Max(p => p.transform.position.y);

                relativeCenter = (new Vector3(xMin, yMin, 0) + new Vector3(xMax, yMax, 0))*0.5f;

                ////calculate distance of camera
                //float maxDistance = 0f;
                //for (int i = 0; i < playersAlive.Count; i++)
                //{
                //    for (int j = i; j < playersAlive.Count; j++)
                //    {
                //        maxDistance = Mathf.Max(
                //            maxDistance,
                //            Vector3.Distance(
                //                new Vector3(playersAlive[i].transform.position.x,
                //                    playersAlive[i].transform.position.y, 0),
                //                new Vector3(playersAlive[j].transform.position.x,
                //                    playersAlive[j].transform.position.y, 0)
                //                )
                //            );
                //    }
                //}
                //m_CameraDistance = Mathf.Clamp(maxDistance, m_MinCameraDistance, 7);
            }

            cameraPosition = new Vector3(relativeCenter.x, relativeCenter.y + 3f, -7f);
        }

        transform.position = Vector3.Slerp(transform.position, cameraPosition, Time.deltaTime*10f);
    }
}
