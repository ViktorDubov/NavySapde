using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace MVC
{
    public class CrisstalEating : MonoBehaviour
    {
        //public void OnCollisionEnter(Collision collision)
        //{
        //    Debug.Log(collision);
        //    PlayerMover player = collision.gameObject.GetComponent<PlayerMover>();
        //    if (player != null)
        //    {
        //        //Debug.Log(player);
        //        GameData.Instance.CurrentScore++;
        //        GameData.Instance.CurrentLife++;
        //        Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        //        gameObject.SetActive(false);
        //    }
        //    EnemyMover enemy = collision.gameObject.GetComponent<EnemyMover>();
        //    if (enemy != null)
        //    {
        //        //Debug.Log(enemy);
        //        Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        //        gameObject.SetActive(false);
        //    }
        //}
        public void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
            PlayerMover player = other.gameObject.GetComponent<PlayerMover>();
            if (player != null)
            {
                //Debug.Log(player);
                GameData.Instance.CurrentScore++;
                GameData.Instance.CurrentLife++;
                Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
                gameObject.SetActive(false);
            }
            EnemyMover enemy = other.gameObject.GetComponent<EnemyMover>();
            if (enemy != null)
            {
                //Debug.Log(enemy);
                Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
                gameObject.SetActive(false);
            }
        }
    }
}