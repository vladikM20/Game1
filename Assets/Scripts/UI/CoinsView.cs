using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private TMP_Text _coinsText;

    private void OnEnable()
    {
        _ball.CoinPickedUp += OnCoinPickedUp;
    }

    private void OnDisable()
    {
        _ball.CoinPickedUp -= OnCoinPickedUp;
    }

    private void OnCoinPickedUp(int value)
    {
        _coinsText.text = value.ToString();
    }
}
