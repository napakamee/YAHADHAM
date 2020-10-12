using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagementSingleton : Singleton<SceneManagementSingleton>
{
    public bool isQuitingStage { get; set; } = false;
}
