using System.Collections;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class ViewDragger : MonoBehaviour
    {
        [SerializeField]
        Image dragger;

        [SerializeField]
        Transform camera;

        [SerializeField]
        float sensitivity = 0.1f;

        bool Dragging { get; set; }

        void Awake()
        {
            dragger.OnPointerDownAsObservable()
                   .Subscribe(_ => StartCoroutine(Drag()))
                   .AddTo(this);

            dragger.OnPointerUpAsObservable()
                   .Subscribe(_ => Dragging = false)
                   .AddTo(this);
        }

        IEnumerator Drag()
        {
            var initialRotation = camera.rotation;
            var initialPosition = Input.mousePosition;

            Dragging = true;
            while (Dragging)
            {
                var currentPosition = Input.mousePosition;
                var delta = currentPosition - initialPosition;
                delta *= sensitivity;

                var rotationX = Quaternion.AngleAxis(delta.y, Vector3.right);
                var rotationY = Quaternion.AngleAxis(-delta.x, Vector3.up);
                var rotation = initialRotation * rotationY * rotationX;

                var cameraUpFacingUpwards = Vector3.Dot(rotation * Vector3.up, Vector3.up) > 0f;
                if (cameraUpFacingUpwards)
                {
                    camera.rotation = rotation;
                }
                else
                {
                    var yOnlyRotation = initialRotation * rotationY;
                    camera.rotation = yOnlyRotation;
                }

                var euler = camera.eulerAngles;
                euler.z = 0f;
                camera.eulerAngles = euler;

                yield return null;
            }
        }
    }
}
