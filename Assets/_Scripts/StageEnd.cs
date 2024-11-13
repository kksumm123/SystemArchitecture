using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.GameState == GameStateType.Playing)
        {
            if (collision.CompareTag("Player"))
                GameManager.Instance.StageEnd();
        }
    }
}
