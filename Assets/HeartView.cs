using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartView : MonoBehaviour
{
    public int HeartNum = 0;

    public void UpdateHeart(int hearts)
    {
        gameObject.SetActive(HeartNum < hearts);
    }
}
