using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    public class GameObjectFactory
    {
        private const string PREFABS_FOLDER = "Prefabs";

        public static T Instantiate<T>(string pathFromPrefabFolder) where T : Object
        {
            var prefab = Resources.Load<T>(Path.Join(PREFABS_FOLDER, pathFromPrefabFolder)); // Replace "PrefabName" with the name of your prefab file

            if (prefab != null)
            {
                return GameObject.Instantiate(prefab); 
            }
            else
            {
                Debug.LogError("Prefab not found!");
                return null;
            }
        }

    }
}