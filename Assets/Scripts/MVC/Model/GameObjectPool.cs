using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pools;

namespace MVC
{
    public class GameObjectPool : Pool<GameObject>
    {
        public GameObjectPool(GameObject gameObjectPrefab, int size) : base(size)
        {
            GameObject root = new GameObject("Root");
            root.transform.position = Vector3.zero;
            root.transform.rotation = Quaternion.Euler(Vector3.zero);
            for (int i = 0; i < size; i++)
            {
                Elements[i] = GameObject.Instantiate(gameObjectPrefab, root.transform);
                Elements[i].transform.SetParent(root.transform);
                Elements[i].SetActive(false);
            }
        }
        /// <summary>
        /// Если все GameObjects активны, то возвращает null.
        /// </summary>
        /// <returns></returns>
        public override GameObject GetNext()
        {
            for (int i = 0; i < Size; i++)
            {
                if (Elements[i].activeSelf == false)
                {
                    return Elements[i];
                }
            }
            return null;
        }
        public void DisableAll()
        {
            for (int i = 0; i < Size; i++)
            {
                Elements[i].SetActive(false);
            }
        }
        public GameObject GetElement(int num)
        {
            return Elements[num];
        }
    }
}