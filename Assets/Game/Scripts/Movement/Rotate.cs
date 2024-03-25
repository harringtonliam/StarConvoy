using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.Core;


namespace SC.Movement
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] float turnSpeed = 100f;

        private Camera mainCamera;

        public float TurnSpeed { get { return turnSpeed; } }

        public bool isEnabled;

        private float horizontalRotateSpeed;
        private float verticalRotateSpeed;
        float adjustedHorizontalInput;
        float adjustedVerticalInput;
        float gameControllerSensitivity = 0.5f;
        bool usingGameController = false;

        PlayerSettings playerSettings;

        public float HorizontalRodateSpeed {  get { return adjustedHorizontalInput; } }
        public float VerticalRodateSpeed { get { return adjustedVerticalInput; } }

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;

            usingGameController = PlayerPrefs.GetString(PlayerSettings.MouseOrControllerKey) == PlayerSettings.UseControllerSetting;
            playerSettings = GetComponent<PlayerSettings>();
            if (playerSettings != null)
            {
                playerSettings.onSettingsUpdated += GetGameControllerSensitivity;
            }
            GetGameControllerSensitivity();

        }

        private void OnDisable()
        {
            try
            {
                playerSettings.onSettingsUpdated -= GetGameControllerSensitivity;
            }
            catch (Exception)
            {

                Debug.Log("Unable to unscribe playerSettins");
            }
        }

        // Update is called once per frame
        void Update()
        {
            RespondToRotateInput();
        }

        public void SetEnabled(bool enable)
        {
            isEnabled = enable;
        }

        public bool GetEnabled()
        {
            return isEnabled;
        }

        private void RespondToRotateInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float spinInput = Input.GetAxis("Spin");

            PerformRotation(horizontalInput, verticalInput, spinInput);
        }

        private void GetGameControllerSensitivity()
        {
            gameControllerSensitivity = PlayerPrefs.GetFloat(PlayerSettings.JoystickSensitivityKey);
        }

        public void PerformRotation(float horizontalInput, float verticalInput)
        {
            PerformRotation(horizontalInput, verticalInput, 0f);
        }    

        public void PerformRotation(float horizontalInput, float verticalInput, float spinInput)
        {
            if (!isEnabled) return;

            if (usingGameController)
            {
                adjustedVerticalInput = verticalInput * gameControllerSensitivity;
                adjustedHorizontalInput = horizontalInput * gameControllerSensitivity;
            }
            else
            {
                adjustedVerticalInput = verticalInput;
                adjustedHorizontalInput = horizontalInput;
            }

            float rotationThisFrame = turnSpeed * Time.deltaTime;
            if (horizontalInput < 0f || horizontalInput > 0f)
            {
                horizontalRotateSpeed =  rotationThisFrame * adjustedHorizontalInput;
                transform.Rotate(Vector3.up * horizontalRotateSpeed);
            }
            if (verticalInput < 0f || verticalInput > 0f)
            {
                verticalRotateSpeed = rotationThisFrame * adjustedVerticalInput;
                transform.Rotate(Vector3.right * verticalRotateSpeed);
            }

            if (spinInput < 0f || spinInput > 0f)
            {
                transform.Rotate(Vector3.forward * rotationThisFrame * spinInput);
            }

        }
    }

}


