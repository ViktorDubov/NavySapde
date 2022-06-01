using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;

namespace MVC
{
    public static class GOExtensions
    {
        public static async Task MoveGameObject(this GameObject gameObject, Vector3 startPosition, Vector3 finishPosition, float speed)
        {
            bool isFinish = false;
            Vector3 moveVector = finishPosition - startPosition;
            float distance = Vector3.Distance(finishPosition, startPosition);
            while (!isFinish)
            {
                gameObject.transform.Translate(speed * moveVector * Time.deltaTime, Space.World);
                if (Vector3.Distance(gameObject.transform.position, startPosition) >= distance)
                {
                    gameObject.transform.position = finishPosition;
                    isFinish = true;
                }
                await Task.Delay(Mathf.RoundToInt(Time.deltaTime*1000));
            }
        }
        public static int DistanceXZ(this GameObject activeGO, GameObject targetGO)
        {
            int distance =
                    Mathf.Abs((int)Mathf.Round(activeGO.transform.position.x) - (int)Mathf.Round(targetGO.transform.position.x))
                    +
                    Mathf.Abs((int)Mathf.Round(activeGO.transform.position.z) - (int)Mathf.Round(targetGO.transform.position.z));
            return distance;
        }
        public static int DistanceXZ(this GameObject activeGO, Vector3 targetPosition)
        {
            int distance =
                    Mathf.Abs((int)Mathf.Round(activeGO.transform.position.x) - (int)Mathf.Round(targetPosition.x))
                    +
                    Mathf.Abs((int)Mathf.Round(activeGO.transform.position.z) - (int)Mathf.Round(targetPosition.z));
            return distance;
        }
    }
}