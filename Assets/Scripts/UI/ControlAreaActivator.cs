using UnityEngine;
using Screen = UnityEngine.Screen;

namespace yutokun.SphericalMediaPlayer
{
    public class ControlAreaActivator : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvas;

        // TODO マウスでクリックされたら非表示にする。ドラッグは除外。

        Vector3 prevMousePosition;
        float mouseStopTime;

        void Update()
        {
            var mouseWasClickedInThisFrame = Input.GetMouseButtonDown(0);

            var mouseIsInWindow = Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
                                  Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;

            if (mouseIsInWindow)
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

            var mouseIsMoving = mouseStopTime == 0f;
            if (mouseIsMoving)
            {
                Show();
            }
            else if (mouseWasClickedInThisFrame || mouseStopTime > 3f)
            {
                Hide();
            }

            if (!mouseIsInWindow)
            {
                Hide();
            }
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
