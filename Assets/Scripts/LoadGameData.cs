using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameData : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        UnlockCondition.Instance.LoadFile();
    }
}
