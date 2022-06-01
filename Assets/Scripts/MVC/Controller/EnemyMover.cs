using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace MVC
{
    public class EnemyMover : MonoBehaviour
    {
        private float speed;
        private bool isMoving;
        private bool isCancel;
        public bool IsCancel
        {
            get
            {
                return isCancel;
            }
            set
            {
                isCancel = value;
            }
        }
        public EventHandler targetPositionChanged;

        Task moveTask;
        Vector2 currentView;

        Task randomTask;
        public void Awake()
        {
            speed = GameData.Instance.EnemySpeed;
            isMoving = false;
            isCancel = false;
            stopRandomMove = false;
            currentView = Vector2.up;
        }

        private bool stopRandomMove = false;
        private async Task StartRandomMove()
        {
            stopRandomMove = false;
            while (!stopRandomMove)
            {
                targetPositionChanged?
                    .Invoke(
                        this,
                        new CurrentTargetPositionEventArgs(new Vector2(UnityEngine.Random.Range(1, GameData.Instance.FieldSize), UnityEngine.Random.Range(1, GameData.Instance.FieldSize))));
                await Task.Delay(UnityEngine.Random.Range(300, 5000));
            }
        }
        public void OnEnable()
        {
            isMoving = false;
            isCancel = false;
            stopRandomMove = false;
            StartRandomMove();
            targetPositionChanged += OnTargetPositionChanged;
        }
        public void OnDisable()
        {
            stopRandomMove = true;
            isCancel = true;
            targetPositionChanged -= OnTargetPositionChanged;
        }
        public void OnTargetPositionChanged(object sender, EventArgs eventArgs)
        {
            CurrentTargetPositionEventArgs currentTargetPosition = eventArgs as CurrentTargetPositionEventArgs;
            StartMove(currentTargetPosition.TargetPosition);
        }
        private async Task StartMove(Vector2 target)
        {
            if (isMoving)
            {
                isCancel = true;
                //await moveTask;
            }
            if (moveTask!=null)
            {
                await moveTask;
            }
            moveTask = Move(target);
        }
        private async Task Move(Vector2 target)
        {
            isMoving = true;
            Vector2 currentPosition;
            while (!isCancel)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
                currentPosition = new Vector2(transform.position.x, transform.position.z);
                if (Vector2.Distance(currentPosition, target) <= 0.7)
                {
                    isCancel = true;
                }
                else
                {
                    Vector2 choosingPosibleMove = ChoosePossibleMove(currentPosition, target);

                    Grid.SetGOTypeBycell(GOType.Enemies, Mathf.RoundToInt(choosingPosibleMove.x), Mathf.RoundToInt(choosingPosibleMove.y));

                    await Rotate(currentView, choosingPosibleMove - currentPosition, speed);

                    Vector3 temporaryTargetPosition = new Vector3(choosingPosibleMove.x, 1, choosingPosibleMove.y);

                    await gameObject.MoveGameObject(transform.position, temporaryTargetPosition, speed);

                    Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y));
                }
            }
            isMoving = false;
            isCancel = false;
        }
        private async Task Rotate(Vector2 currentView, Vector2 targetView, float speed)
        {
            float angle = -1 * Vector2.SignedAngle(currentView, targetView);
            float temporaryAngle = Mathf.Abs(angle);
            bool isFinished = false;
            while (!isFinished)
            {
                transform.Rotate(Vector3.up, angle * speed * Time.deltaTime);
                temporaryAngle -= Mathf.Abs(angle * speed * Time.deltaTime);
                if (temporaryAngle <= 0)
                {
                    isFinished = true;
                }
                await Task.Delay((int)Time.deltaTime * 1000);
            }
            this.currentView = targetView;
        }
        private Vector2 ChoosePossibleMove(Vector2 currentPosition, Vector2 targetPosition)
        {
            Vector2 temporaryTargetPosition = Vector2.zero;

            Vector2Int[] vectors = new Vector2Int[4];
            vectors[0] = new Vector2Int(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y) + 1);
            vectors[1] = new Vector2Int(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y) - 1);
            vectors[2] = new Vector2Int(Mathf.RoundToInt(currentPosition.x) + 1, Mathf.RoundToInt(currentPosition.y));
            vectors[3] = new Vector2Int(Mathf.RoundToInt(currentPosition.x) - 1, Mathf.RoundToInt(currentPosition.y));

            float minDistance = 1000000;
            for (int i = vectors.Length - 1; i >= 0; i--)
            {
                GOType goType = Grid.GetGOTypeByCell(vectors[i].x, vectors[i].y);
                if (goType == GOType.None || goType == GOType.Cristall || goType == GOType.Player)
                {
                    float distance = Vector2.Distance(vectors[i], targetPosition);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        temporaryTargetPosition = vectors[i];
                    }
                }
            }

            return temporaryTargetPosition;
        }
    }
}