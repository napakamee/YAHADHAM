using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UnlockCondition : Singleton<UnlockCondition>
{
    protected UnlockCondition() { }

    public bool stage1Clear { get; set; } = true;
    public bool stage2Clear { get; set; }
    public bool stage3Clear { get; set; }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        Debug.Log("Saved file at " + Application.persistentDataPath + "/save.dat");
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(stage1Clear, stage2Clear, stage3Clear);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        Debug.Log("loaded file from " + Application.persistentDataPath + "/save.dat");
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("No data was found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        stage1Clear = data.Stage1ulk;
        stage2Clear = data.Stage2ulk;
        stage3Clear = data.Stage3ulk;
    }
}

[System.Serializable]
public class GameData
{
    public bool Stage1ulk { get; set; }
    public bool Stage2ulk { get; set; }
    public bool Stage3ulk { get; set; }

    public GameData(bool st1ulk, bool st2ulk, bool st3ulk)
    {
        Stage1ulk = st1ulk;
        Stage2ulk = st2ulk;
        Stage3ulk = st3ulk;
    }
}