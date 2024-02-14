using System;
using System.Collections;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;

namespace Platformer
{
    public class CameraManager : MonoBehaviour 
    {
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookCam;

        [Header("Settings")]
        [SerializeField, Range(0.5f, 3f)] float speedMultipier = 1f;

        bool isRBMPressed;
        bool cameraMovementLook;

        void OnEnable()
        {
            input.Look += OnLook;
            input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }

         void OnDisable()
        {
            input.Look -= OnLook;
            input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
        {
            if (cameraMovementLook)
            {
                return;
            }
            if (isDeviceMouse && ! isRBMPressed)
            {
                return;
            }
            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            freeLookCam.m_XAxis.m_InputAxisValue = cameraMovement.x * speedMultipier * deviceMultiplier;
            freeLookCam.m_YAxis.m_InputAxisValue = cameraMovement.y * speedMultipier * deviceMultiplier;
        }

        void OnEnableMouseControlCamera()
        {
            isRBMPressed = true;

            //lock & hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(OnDisableMouseForFrame());
        }

        private IEnumerator OnDisableMouseForFrame()
        {
            cameraMovementLook = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLook = false;
        }

        void OnDisableMouseControlCamera()
        {
            isRBMPressed = false;

            //unlock & make visible cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //reset camera axis
            freeLookCam.m_XAxis.m_InputAxisValue = 0f;
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }
    }
}
