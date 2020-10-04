using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public NumberControl[] _listNumbers;
    Image m_sprite;
    // Start is called before the first frame update
    void Start()
    {
        _listNumbers = GetComponentsInChildren<NumberControl>();
    }

    // Update is called once per frame
    public void UpdateScore(int Score)
    {
        for (int i = 0; i < _listNumbers.Length; i++)
        {
            _listNumbers[i].UpdateNumber(Score);
        }
    }
}
