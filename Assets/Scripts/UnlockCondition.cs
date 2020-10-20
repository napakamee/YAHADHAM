using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCondition : Singleton<UnlockCondition>
{
    protected UnlockCondition() {}

    public bool stage1Clear { get; set; } = true;
    public bool stage2Clear { get; set; }
    public bool stage3Clear { get; set; }
}
