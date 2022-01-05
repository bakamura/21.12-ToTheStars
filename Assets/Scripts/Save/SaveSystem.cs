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

    public static SaveData LoadData() {
        string path = Application.persistentDataPath + "/Save.monke";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else {
            Debug.LogWarning("LoadError: No Save Data was Found!");
            return null;
        }
    }

    public static void DeleteData() {
        string path = Application.persistentDataPath + "/Save.monke";
        if (File.Exists(path)) File.Delete(path);
        else Debug.LogWarning("DeleteError: No Save Data was Found!");
    }
}
