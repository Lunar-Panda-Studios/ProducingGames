using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    internal static string path;
    public static void save(int savefile)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(GameManager.Instance);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static bool pathExists(int slotNo)
    {
        asignPath(slotNo);
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int checkLevel()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        GameData data = formatter.Deserialize(stream) as GameData;

        stream.Close();

        return data.whichLevel;
    }

    public static GameData load()
    {

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

    public static void asignPath(int savefile)
    {
        switch (savefile)
        {
            case 0:
                {
                    path = Application.persistentDataPath + "/quickSave.save";
                    break;
                }
            case 1:
                {
                    path = Application.persistentDataPath + "/slot1.save";
                    break;
                }
            case 2:
                {
                    path = Application.persistentDataPath + "/slot2.save";
                    break;
                }
            case 3:
                {
                    path = Application.persistentDataPath + "/slot3.save";
                    break;
                }
            default:
                {
                    path = Application.persistentDataPath + "/slot1.save";
                    break;
                }

        }
    }

    public static void delete(int fileSlot)
    {
        asignPath(fileSlot);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
