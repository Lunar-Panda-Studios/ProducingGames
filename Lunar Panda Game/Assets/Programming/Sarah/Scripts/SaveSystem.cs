using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void save(TestingSave manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        //Saved data here
        GameData data = new GameData(manager);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData load()
    {
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            GameData data = formatter.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }
        else
        {
            //print("Error no data");
            return null;
        }
    }

    public static void delete()
    {
        string path = Application.persistentDataPath + "player.save";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
