using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.GetComponent<AsciiLevelLoader>().CurrentLevel++; 
    }
}
