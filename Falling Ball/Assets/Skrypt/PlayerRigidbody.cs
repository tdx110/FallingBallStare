using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRigidbody : MonoBehaviour
{
    [SerializeField]
    private Collider2D[] KillPlayerList;
    [SerializeField]
    private Collider2D[] AddGoldList;
    [SerializeField]
    private UnityEvent GameOverEvent;
    [SerializeField]
    private UnityEvent AddCoinEvent;
    [SerializeField]
    private UnityEvent LevelCompleteEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Stop")
        {
            LevelCompleteEvent.Invoke();
        }

        foreach (Collider2D collider2DKill in KillPlayerList)
        {
            if (collider2DKill.name == collision.gameObject.name)
            {
                GameOverEvent.Invoke();
                break;
            }
        }
        foreach (Collider2D collider2DAddGold in AddGoldList)
        {
            if (collider2DAddGold.name == collision.gameObject.name)
            {
                AddCoinEvent.Invoke();
                collision.gameObject.name = "Punkt";
                break;
            }
        }

    }
}
