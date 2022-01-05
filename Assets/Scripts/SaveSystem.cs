using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveData() {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/Save.monke", FileMode.Create);

        SaveData data = new SaveData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadData() {
        if (File.Exists(Application.persistentDataPath + "/Save.monke")) {

        }
        else Debug.Log("No Save Data was Found!");
    }

    // File.Delete(path);

}
