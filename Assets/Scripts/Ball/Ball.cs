using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private int _coins;

    public event UnityAction<int> CoinPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            _coins++;
            coin.gameObject.SetActive(false);
            CoinPickedUp?.Invoke(_coins);
        }
    }
}
