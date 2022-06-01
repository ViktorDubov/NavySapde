using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MVC
{
    [Serializable]
    public class SavedGameData
    {
        public int Record { get; set; }

        public static void Save(SavedGameData savedGameData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
            bf.Serialize(file, savedGameData);
            file.Close();
            Debug.Log("Game data saved!");
        }
        public static SavedGameData Load()
        {
            SavedGameData savedGameData = new SavedGameData();
            if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                  File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
                savedGameData = (SavedGameData)bf.Deserialize(file);
                file.Close();
                Debug.Log("Game data loaded!");
            }
            else
            {
                Debug.LogError("There is no save data! Create new!");
                savedGameData.Record = 0;
                Save(savedGameData);
            }
            return savedGameData;
        }
    }
}