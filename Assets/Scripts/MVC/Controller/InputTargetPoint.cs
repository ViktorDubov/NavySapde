using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MVC
{
    public class InputTargetPoint : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(eventData.position.x, eventData.position.y, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200f))
            {
                //Debug.Log(hit.point);
                Vector2 hitPoint = new Vector2(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.z));
                EventBus.Instance.RiseEvent(EventType.TargetPositionChanged, new CurrentTargetPositionEventArgs(hitPoint));
                //Debug.Log(hitPoint);
            }
        }
    }
}