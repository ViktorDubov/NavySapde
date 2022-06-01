using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace MVC
{
    public class EnemyEater : MonoBehaviour
    {
        public void OnCollisionEnter(Collision collision)
        {
            PlayerMover player = collision.gameObject.GetComponent<PlayerMover>();
            if (player != null)
            {
                if (player.ISGodMode)
                {
                    GetComponent<EnemyMover>().IsCancel = true;
                }
                else
                {
                    GameData.Instance.CurrentLife--;

                    Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
                    player.ISGodMode = true;
                    gameObject.transform.position = Vector3.up;
                    gameObject.SetActive(false);
                }

            }
        }
        public void OnTriggerEnter(Collider other)
        {
            PlayerMover player = other.gameObject.GetComponent<PlayerMover>();
            if (player != null)
            {
                if (player.ISGodMode)
                {
                    GetComponent<EnemyMover>().IsCancel = true;
                }
                else
                {
                    GameData.Instance.CurrentLife--;

                    Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
                    player.ISGodMode = true;
                    gameObject.transform.position = Vector3.up;
                    gameObject.SetActive(false);
                }

            }
        }
    }
}