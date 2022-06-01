using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;


namespace MVC
{
    public class PlayerMover : MonoBehaviour
    {
        private float speed;
        private bool isMoving;
        private bool isCancel;
        public EventHandler targetPositionChanged;

        private Animator anim;


        Task moveTask;
        Vector2 currentView;
        private bool isGodMode;
        public bool ISGodMode
        {
            get
            {
                return isGodMode;
            }
            set
            {
                isGodMode = value;
                SetGodModAfterSomeSecond(10);
            }
        }
        private async Task SetGodModAfterSomeSecond(int seconds)
        {
            await Task.Delay(seconds * 1000);
            isGodMode = false;
        }
        //private CancellationTokenSource cts;
        public void Awake()
        {
            speed = GameData.Instance.PlayerSpeed;
            isMoving = false;
            isCancel = false;
            currentView = Vector2.up;
            anim = GetComponent<Animator>();
        }
        public void OnEnable()
        {
            //Debug.Log("Start Listening");
            targetPositionChanged += OnTargetPositionChanged;
            EventBus.Instance.StartListening(EventType.TargetPositionChanged, targetPositionChanged);
        }
        public void OnDisable()
        {
            EventBus.Instance.StopListening(EventType.TargetPositionChanged, targetPositionChanged);
            targetPositionChanged -= OnTargetPositionChanged;
        }
        public void OnTargetPositionChanged(object sender, EventArgs eventArgs)
        {
            //Debug.Log("Start Move");
            CurrentTargetPositionEventArgs currentTargetPosition = eventArgs as CurrentTargetPositionEventArgs;
            StartMove(currentTargetPosition.TargetPosition);
        }
        private async Task StartMove(Vector2 target)
        {
            anim.SetBool("IsRun", true);
            if (isMoving)
            {
                isCancel = true;
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

                if (Vector2.Distance(currentPosition, target) <= 0.5)
                {
                    isCancel = true;
                }
                else
                {
                    Vector2 choosingPosibleMove = ChoosePossibleMove(currentPosition, target);

                    Grid.SetGOTypeBycell(GOType.Player, Mathf.RoundToInt(choosingPosibleMove.x), Mathf.RoundToInt(choosingPosibleMove.y));

                    await Rotate(currentView, choosingPosibleMove - currentPosition, speed);

                    Vector3 temporaryTargetPosition = new Vector3(choosingPosibleMove.x, 1, choosingPosibleMove.y);

                    await gameObject.MoveGameObject(transform.position, temporaryTargetPosition, speed);

                    Grid.SetGOTypeBycell(GOType.None, Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y));
                }
            }
            isMoving = false;
            isCancel = false;
            anim.SetBool("IsRun", false);
        }
        private async Task Rotate(Vector2 currentView, Vector2 targetView, float speed)
        {
            float angle = -1 * Vector2.SignedAngle(currentView, targetView);
            float temporaryAngle = Mathf.Abs(angle);
            bool isFinished = false;
            while (!isFinished)
            {
                transform.Rotate(Vector3.up, angle * speed * Time.deltaTime); //
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
            vectors[1] = new Vector2Int(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y) -1);
            vectors[2] = new Vector2Int(Mathf.RoundToInt(currentPosition.x) + 1, Mathf.RoundToInt(currentPosition.y));
            vectors[3] = new Vector2Int(Mathf.RoundToInt(currentPosition.x) - 1, Mathf.RoundToInt(currentPosition.y));

            Dictionary<Vector2Int, float> vecDis = new Dictionary<Vector2Int, float>();
            //Debug.Log("Start");
            float minDistance = 1000000;
            if (ISGodMode)
            {
                for (int i = vectors.Length - 1; i >= 0; i--)
                {
                    GOType goType = Grid.GetGOTypeByCell(vectors[i].x, vectors[i].y);
                    if (goType == GOType.None || goType == GOType.Cristall )
                    {
                        float distance = Vector2.Distance(vectors[i], targetPosition);
                        //Debug.Log($"vectors[i] {vectors[i]} distance {distance}");
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            temporaryTargetPosition = vectors[i];
                        }
                    }
                }
            }
            else
            {
                for (int i = vectors.Length - 1; i >= 0; i--)
                {
                    GOType goType = Grid.GetGOTypeByCell(vectors[i].x, vectors[i].y);
                    if (goType == GOType.None || goType == GOType.Cristall  || goType == GOType.Player)
                    {
                        float distance = Vector2.Distance(vectors[i], targetPosition);
                        //Debug.Log($"vectors[i] {vectors[i]} distance {distance}");
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            temporaryTargetPosition = vectors[i];
                        }
                    }
                }
            }

            return temporaryTargetPosition;
        }
    }
}