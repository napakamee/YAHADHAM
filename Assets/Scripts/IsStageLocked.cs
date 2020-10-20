using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsStageLocked : MonoBehaviour
{
    private Image buttonImage;
    private Color buttonColor;
    private Color originalColor;
    [SerializeField] string StageName = null;
    [SerializeField] Color lockedTintColor = Color.white;
    [SerializeField] Material GreyScale = null;

    private void Start()
    {
        buttonImage = this.GetComponent<Image>();
        originalColor = buttonImage.color;
        buttonColor = originalColor;
    }

    private void Update()
    {
        switch (StageName) {
            case "Stage1" : if (UnlockCondition.Instance.stage1Clear) { buttonColor = originalColor; buttonImage.material = null; } else { buttonColor = lockedTintColor; buttonImage.material = GreyScale; } break;
            case "Stage2": if (UnlockCondition.Instance.stage2Clear) { buttonColor = originalColor; buttonImage.material = null; } else { buttonColor = lockedTintColor; buttonImage.material = GreyScale; } break;
            case "Stage3": if (UnlockCondition.Instance.stage3Clear) { buttonColor = originalColor; buttonImage.material = null; } else { buttonColor = lockedTintColor; buttonImage.material = GreyScale; } break;
        }
        buttonImage.color = buttonColor;
    }
}