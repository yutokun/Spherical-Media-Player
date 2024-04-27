using System.Collections;
using UnityEngine;
using Screen = UnityEngine.Screen;

namespace yutokun.SphericalMediaPlayer
{
    public class ControlAreaActivator : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvas;

        Vector3 prevMousePosition;
        float mouseStopTime;

        float mouseStopTimeAfterClick;
        bool mouseWasClicked;

        static bool MouseIsInWindow => Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
                                       Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;

        void Update()
        {
            BuildConditions();
            ApplyActiveness();
        }

        void BuildConditions()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(DetectClick());
            }

            if (MouseIsInWindow)
            {
                if (prevMousePosition != Input.mousePosition)
                {
                    mouseStopTime = 0f;
                }
                else
                {
                    mouseStopTime += Time.deltaTime;
                }
            }

            prevMousePosition = Input.mousePosition;
        }

        void ApplyActiveness()
        {
            var mouseIsMoving = mouseStopTime == 0f;
            if (mouseIsMoving)
            {
                Show();
            }
            else if (mouseWasClicked || mouseStopTime > 3f)
            {
                Hide();
            }

            if (!MouseIsInWindow)
            {
                Hide();
            }
        }

        IEnumerator DetectClick()
        {
            var mousePositionOnButtonDown = Input.mousePosition;
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            mouseWasClicked = mousePositionOnButtonDown == Input.mousePosition;
            yield return null;
            mouseWasClicked = false;
        }

        void Show()
        {
            canvas.alpha = 1f;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;

            Cursor.visible = true;
        }

        void Hide()
        {
            canvas.alpha = 0f;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;

            Cursor.visible = false;
        }
    }
}
