using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] listNumbers;
    Image m_sprite;
    public int UnitOfNumber = 1;

    private void Awake()
    {
        m_sprite = GetComponent<Image>();
    }
    public void UpdateNumber (int number)
    {
        number = (number / UnitOfNumber) % 10;
        m_sprite.sprite = listNumbers[number];
    }
}
