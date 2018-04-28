using UnityEngine;

namespace Assets.Scripts.Movement {
    public class PlayerCameraBehavior : MonoBehaviour {
        //Mouse look and player camera.
        public Camera playerCamera;
        public Transform headTransform;
        public float mouseSensitivity = 5;

        [Range(0, 1)]
        public float mouseSmoothScale = 0.05f;

        [Range(0, 90)]
        public float maxLookAngle = 75;

        public bool allowRotation = true;

        //Head bobbing.
        public float walkBobbingSpeed = 0.12f;
        public float runBobbingSpeed = 0.18f;
        public float crouchBobbingSpeed = 0.08f;
        public Vector2 moveAmount = new Vector2(0.1f, 0.1f);
        public bool useHeadBob = true;

        //Landing motion.
        public float minVelocity = -5;
        public float dropAmount = 0.3f;
        public float dropSpeed = 0.15f;
        public bool useLandingMotion = true;

        private PlayerMotorBehavior motor;

        private Vector3 startPoint;
        private Vector3 initialTarget;
        private Vector3 dropTarget;
        private Vector3 dropVelocity;
        private Vector3 headBobMoveSmoothVelocity;
        private Vector3 headBobRotSmoothVelocity;
        private Vector3 rotationSmoothVelocity;
        private Vector3 currentRotation;

        private float yaw;
        private float pitch;
        private float headBobTimer;

        private bool playLanding;
        private float landingProgress;
        private float landingVelocity;

        private void Start() {
            //Initialization.
            motor = GetComponent<PlayerMotorBehavior>();

            //Get the starting position of the head transform.
            if (headTransform != null) {
                startPoint = headTransform.localPosition;
            }

            //Keeping the players starting rotation.
            if (playerCamera != null) {
                yaw = transform.eulerAngles.y;
                pitch = playerCamera.transform.localEulerAngles.x;
                currentRotation.Set(pitch, yaw, playerCamera.transform.localEulerAngles.z);
                initialTarget = playerCamera.transform.localPosition;
            }
        }

        private void Update() {
            //Disable the mouse look functionality.
            if (!allowRotation) return;

            //Get the current mouse input.
            yaw += Input.GetAxis("Mouse X")*mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y")*mouseSensitivity;

            //Clamp the camera rotation.
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            //Calculate and rotate the camera.
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, mouseSmoothScale);

            //Rotating the player.
            Vector3 bodyRotation = new Vector3(0, currentRotation.y, 0);
            transform.localEulerAngles = bodyRotation;

            //Rotating the camera.
            Vector3 cameraRotation = new Vector3(currentRotation.x, 0, currentRotation.z);
            playerCamera.transform.localEulerAngles = cameraRotation;
        }

        private void FixedUpdate() {
            //Headbobbing mechanics.
            if (useHeadBob) {
                if (motor.isGrounded && motor.canMove) {
                    //Variables for the head bob.
                    float waveSliceX = 0f;
                    float waveSliceY = 0f;

                    //If we are not moving, reset the timer.
                    if (!motor.isMoving)
                        headBobTimer = 0;

                    //We are moving.
                    else {
                        //Build up the variables for the head bob.
                        waveSliceX = Mathf.Cos(headBobTimer);
                        waveSliceY = Mathf.Sin(headBobTimer*2);

                        //Adjust to run speed.
                        if (motor.isRunning)
                            headBobTimer = headBobTimer + runBobbingSpeed;

                        //Adjust to crouch speed.
                        else if (motor.isCrouching)
                            headBobTimer = headBobTimer + crouchBobbingSpeed;

                        //Adjust to walk speed.
                        else if (!motor.isRunning && !motor.isCrouching)
                            headBobTimer = headBobTimer + walkBobbingSpeed;

                        //Update the timer.
                        if (headBobTimer > Mathf.PI*2)
                            headBobTimer = headBobTimer - (Mathf.PI*2);
                    }

                    //If there is change.
                    if (waveSliceX != 0) {
                        //Calculate the amount of change.
                        float translateChangeX = waveSliceX*moveAmount.x;
                        float translateChangeY = waveSliceY*moveAmount.y;
                        float totalAxes = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

                        //Clamp the change.
                        totalAxes = Mathf.Clamp(totalAxes, 0, 1);
                        translateChangeX = totalAxes*translateChangeX;
                        translateChangeY = totalAxes*translateChangeY;

                        //Finally, move the camera.
                        Vector3 cameraPos = new Vector3(startPoint.x + translateChangeX, startPoint.y - translateChangeY, headTransform.localPosition.z);
                        headTransform.localPosition = Vector3.SmoothDamp(headTransform.localPosition, cameraPos, ref headBobMoveSmoothVelocity, 0.1f);
                    }

                    //If there is no change.
                    else {
                        //Move the camera to its original position.
                        Vector3 cameraPos = new Vector3(startPoint.x, startPoint.y, headTransform.localPosition.z);
                        headTransform.localPosition = Vector3.SmoothDamp(headTransform.localPosition, cameraPos, ref headBobMoveSmoothVelocity, 0.1f);
                    }
                }

                else if (!motor.isGrounded) {
                    //Move the camera to its original position.
                    Vector3 cameraPos = new Vector3(startPoint.x, startPoint.y, headTransform.localPosition.z);
                    headTransform.localPosition = Vector3.SmoothDamp(headTransform.localPosition, cameraPos, ref headBobMoveSmoothVelocity, 0.1f);
                }
            }

            else if (!useHeadBob && headTransform != null) {
                //playerCamera.transform.position = headTransform.transform.position;
                //return;
                //Set the camera's position to its default position.
                Vector3 cameraPos = new Vector3(startPoint.x, startPoint.y, headTransform.localPosition.z);
                headTransform.localPosition = cameraPos;
            }

            if (playerCamera != null) {
                //Start the landing motion.
                if (playLanding) {
                    //Calculate the progress.
                    landingProgress = Mathf.SmoothDamp(landingProgress, 1, ref landingVelocity, dropSpeed*2);

                    //If the camera hasn't reached the drop height yet, set the target.
                    if (landingProgress < 0.5f) dropTarget.Set(initialTarget.x, initialTarget.y - dropAmount, initialTarget.z);
                    else playLanding = false;
                }

                //Returning to the defaut height.
                if (!playLanding) {
                    //Set the target and reset the progress.
                    dropTarget = initialTarget;
                    landingProgress = 0;
                }

                //Interpolate the camera's position to the target position.
                if (headTransform.localPosition != dropTarget) playerCamera.transform.localPosition = Vector3.SmoothDamp(playerCamera.transform.localPosition, dropTarget, ref dropVelocity, dropSpeed);
            }
        }

        //Triggers the landing head motion.
        public void PlayLandingMotion() {
            //Start the landing motion if the it's enabled.
            if (useLandingMotion) playLanding = true;
        }
    }
}