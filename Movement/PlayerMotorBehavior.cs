using UnityEngine;

namespace Starship.Unity.Movement {
  [System.Serializable]
  public class FootstepType {
    public string tagName = "New Footstep Type";

    [Range(0, 1)]
    public float volume = 1;

    public AudioClip[] sounds;
  }

  [RequireComponent(typeof(CharacterController))]
  [RequireComponent(typeof(PlayerCameraBehavior))]
  [RequireComponent(typeof(AudioSource))]
  public class PlayerMotorBehavior : MonoBehaviour {
    //Movement settings.
    public float moveSpeed = 5;
    public float runSpeed = 10;
    public float crouchSpeed = 2.5f;

    [Range(0, 1)]
    public float moveSmoothScale = 0.1f;

    [Range(0, 1)]
    public float airControlPercent = 0.7f;

    [Range(0.01f, 0.5f)]
    public float slopeDetectionRange = 0.3f;

    public bool canMove = true;
    public bool canRun = true;
    public bool canCrouch = true;
    public bool useSlopeDetection = true;

    //Ascending and descending.
    public KeyCode ascendKey = KeyCode.Space;
    public KeyCode descendKey = KeyCode.LeftShift;
    public float ascendSpeed = 5;
    public float descendSpeed = 5;
    public bool canAscend = true;
    public bool canDescend = true;

    //Jumping.
    public enum JumpMethod {
      continuous,
      single
    };

    public JumpMethod jumpMethod;
    public float jumpForce = 10;
    public bool canJump = true;

    //Crouching
    public KeyCode crouchKey = KeyCode.C;
    public LayerMask crouchInteractLayer;
    public float crouchHeight = 1;

    [Range(0, 1)]
    public float crouchSmoothScale = 0.2f;

    //Gravity.
    public float gravityAmount = -20;

    [Range(0, 1)]
    public float gravityScale = 1;

    public float constantGravityForce = 0;

    //Footsteps.
    public FootstepType[] footstepTypes;
    public float walkStepTime = 0.5f;
    public float runStepTime = 0.3f;
    public float crouchStepTime = 0.85f;

    [Range(0, 1)]
    public float walkStepVolume = 0.7f;

    [Range(0, 1)]
    public float runStepVolume = 1;

    [Range(0, 1)]
    public float crouchStepVolume = 0.4f;

    //Jump Sound.
    public AudioClip jumpSound;

    [Range(0, 1)]
    public float jumpVolume = 1;

    //Land sound.
    public enum LandingSoundBase {
      currentGroundType,
      specificSound
    }

    public LandingSoundBase landingSoundBase;
    public AudioClip landingSound;

    [Range(0, 1)]
    public float landingVolume = 1;

    [Range(0, 1)]
    public float footstepDelay = 0.3f;

    public float minVelocity = -5;

    //Ghost mode.
    public int ignoredLayer;
    public bool ghostMode;

    //Passive states.
    public bool isGhosting = false;
    public bool isZeroGravity = false;
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isRunning = false;
    public bool isCrouching = false;
    public bool isAscending = false;
    public bool isDescending = false;

    private CharacterController motor;
    private PlayerCameraBehavior cam;

    private Vector3 moveDirection;
    private Vector2 speedSmoothVelocity;
    private Vector2 currentSpeed;
    private Vector2 targetSpeed;

    private float crouchSmoothVelocity;
    private float defaultCrouchHeight;
    private float turnSmoothVelocity;
    private float ZG_VelocityY;
    private float velocityY;

    private AudioClip currentStepSound;
    private float currentStepVolume;
    private float stepVolume;
    private float footstepTimer;
    private bool canStandUp;
    private bool playedLanding;

    private void Start() {
      motor = GetComponent<CharacterController>();
      cam = GetComponent<PlayerCameraBehavior>();
        
      defaultCrouchHeight = motor.height;
    }

    private bool CustomGroundState() {
      //If we are in zero gravity, this can't be applied since we would stuck to the ground.
      if (isZeroGravity) return motor.isGrounded;

      //If we are already grounded, there is no need to adjust the character's position.
      if (motor.isGrounded) return true;

      //Get the bottom of the character, and use it as the starting point of the ray.
      Vector3 bottom = transform.position - Vector3.up*motor.height/2;
      RaycastHit hit;

      //If we are not grounded, but the ground is close to use, move the character down and update the state.
      if (Physics.Raycast(bottom, Vector3.down, out hit, slopeDetectionRange)) {
        //If the velocity is negative, we are not jumping and the position should be fixed.
        if (motor.velocity.y < 0) {
          motor.Move(new Vector3(0, -hit.distance, 0));
          return true;
        }

        //We are just trying to jump, no need to fix the character's position.
        return false;
      }

      //The ground is too far, no need to fix the character's position.
      return false;
    }

    private float ControlledSmoothScale() {
      //If we are grouned or in zero gravity or the percentage is 100%, this won't change anything so we return the base value.
      if (isZeroGravity || isGrounded || airControlPercent == 1) return moveSmoothScale;

      //If the percentage is 0%, we don't have any control over the character.
      if (airControlPercent == 0) return float.MaxValue;

      //If neither of the states above are true, we return the calculated modified smooth scale.
      return moveSmoothScale/airControlPercent;
    }

    private void Update() {
      //Running different methods every frame.
      RefreshStates();
      PlayFootsteps();
      CalculateGhostMode();
      CalculateMovement();
    }

    public void UpdateGravity(float newGravityScale, float newMoveSmoothScale, float additionalGravityForce) {
      //Clamp the smoothing values.
      newGravityScale = Mathf.Clamp(newGravityScale, 0, 1);
      newMoveSmoothScale = Mathf.Clamp(newMoveSmoothScale, 0, 1);

      //Apply the changes.
      gravityScale = newGravityScale;
      moveSmoothScale = newMoveSmoothScale;
      velocityY += additionalGravityForce;
    }

    private void CalculateMovement() {
      //Getting the input.
      var inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

      //Adjusting the movement speed.
      if (canMove) {
        if (isRunning) {
          //Running.
          targetSpeed.x = runSpeed*inputDir.x;
          targetSpeed.y = runSpeed*inputDir.y;
        }

        if (isCrouching) {
          //Crouching.
          targetSpeed.x = crouchSpeed*inputDir.x;
          targetSpeed.y = crouchSpeed*inputDir.y;
        }

        if (!isRunning && !isCrouching) {
          //Walking.
          targetSpeed.x = moveSpeed*inputDir.x;
          targetSpeed.y = moveSpeed*inputDir.y;
        }
      }

      //We cannot move, adjust movement speed to 0.
      else if (!canMove) {
        targetSpeed.x = 0;
        targetSpeed.y = 0;
      }

      //Smooth out the movement speed.
      currentSpeed.x = Mathf.SmoothDamp(currentSpeed.x, targetSpeed.x, ref speedSmoothVelocity.x, ControlledSmoothScale());
      currentSpeed.y = Mathf.SmoothDamp(currentSpeed.y, targetSpeed.y, ref speedSmoothVelocity.y, ControlledSmoothScale());

      //Apply gravity.
      velocityY += Time.deltaTime*(gravityAmount*gravityScale);

      //Jumping mechanics.
      if (canJump && isGrounded && !isZeroGravity && canMove) {
        //Continuous jumping method.
        if (jumpMethod == JumpMethod.continuous && Input.GetAxis("Jump") != 0) {
          //Set the velocity.
          velocityY = jumpForce;

          //Landing sound reset.
          playedLanding = false;

          //Jump sound.
          if (jumpSound != null)
            GetComponent<AudioSource>().PlayOneShot(jumpSound, jumpVolume);
        }

        //Single jumping method.
        if (jumpMethod == JumpMethod.single && Input.GetKeyDown(KeyCode.Space)) {
          //Set the velocity.
          velocityY = jumpForce;

          //Landing sound reset.
          playedLanding = false;

          //Jump sound.
          if (jumpSound != null)
            GetComponent<AudioSource>().PlayOneShot(jumpSound, jumpVolume);
        }
      }

      //Crouching mechanics.
      if (canCrouch) {
        //Set the target height.
        float targetHeight = defaultCrouchHeight;

        //Update the target height if needed.
        if (isCrouching) targetHeight = crouchHeight;

        //Calculate and update the players height.
        float lastHeight = motor.height;
        motor.height = Mathf.SmoothDamp(motor.height, targetHeight, ref crouchSmoothVelocity, crouchSmoothScale);

        //Fix the position of the player.
        Vector3 fixedPos = new Vector3(transform.position.x, transform.position.y + (motor.height - lastHeight)/2, transform.position.z);
        transform.position = fixedPos;
      }

      //Standing.
      else if (!canCrouch && motor.height != defaultCrouchHeight) {
        //Calculate and update the players height.
        float lastHeight = motor.height;
        motor.height = Mathf.SmoothDamp(motor.height, defaultCrouchHeight, ref crouchSmoothVelocity, crouchSmoothScale);

        //Fix the position of the player.
        Vector3 fixedPos = new Vector3(transform.position.x, transform.position.y + (motor.height - lastHeight)/2, transform.position.z);
        transform.position = fixedPos;
      }

      //Ascending and descending functionality.
      if (isAscending) ZG_VelocityY = ascendSpeed + constantGravityForce;
      else if (isDescending) ZG_VelocityY = -descendSpeed + constantGravityForce;
      else ZG_VelocityY = 0 + constantGravityForce;

      //Calculating the movement as a vector.
      Vector3 inputVector = new Vector3(currentSpeed.x, 0, currentSpeed.y);

      //Direction in zero gravity.
      if (isZeroGravity)
        moveDirection = cam.playerCamera.transform.TransformDirection(inputVector) + Vector3.up*velocityY;

      //Direction in gravity.
      else if (!isZeroGravity)
        moveDirection = transform.TransformDirection(inputVector) + Vector3.up*velocityY;

      //Move the character.
      motor.Move(moveDirection*Time.deltaTime);

      //Smooth out the difference between the current velocity and the target velocity.
      if (isZeroGravity)
        velocityY = Mathf.Lerp(velocityY, ZG_VelocityY, Time.deltaTime/moveSmoothScale);

      //Reset the velocity.
      if (motor.isGrounded && !isZeroGravity)
        velocityY = 0;
    }

    private void RefreshStates() {
      //Getting the input.
      Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

      //Grounded check.
      if (useSlopeDetection) isGrounded = CustomGroundState();
      else isGrounded = motor.isGrounded;

      //We are in zero gravity.
      if (gravityScale == 0) isZeroGravity = true;

      //We are affected by gravity.
      else if (gravityScale > 0) isZeroGravity = false;

      //We are ghosting.
      if (isZeroGravity && ghostMode) isGhosting = true;

      //We are not ghosting.
      else isGhosting = false;

      //We are moving.
      if (inputDir.magnitude != 0 && canMove) isMoving = true;

      //We are standing.
      else if (inputDir.magnitude == 0) isMoving = false;

      //We are running.
      if (Input.GetKey(KeyCode.LeftShift) && inputDir.y == 1 && !isZeroGravity && isMoving && canRun && !isCrouching) isRunning = true;

      //We are walking.
      else isRunning = false;

      //Ascending.
      if (Input.GetKey(ascendKey) && isZeroGravity && canAscend) isAscending = true;
      else isAscending = false;

      //Descending.
      if (Input.GetKey(descendKey) && isZeroGravity && canDescend && canMove) isDescending = true;
      else isDescending = false;
        
      //Updating the standing up state.
      if (isCrouching) {
        Ray frontRay = new Ray(transform.position - Vector3.up*motor.height/2 + new Vector3(0, 0, motor.radius), Vector3.up);
        Ray backRay = new Ray(transform.position - Vector3.up*motor.height/2 + new Vector3(0, 0, -motor.radius), Vector3.up);
        Ray rightRay = new Ray(transform.position - Vector3.up*motor.height/2 + new Vector3(motor.radius, 0, 0), Vector3.up);
        Ray leftRay = new Ray(transform.position - Vector3.up*motor.height/2 + new Vector3(-motor.radius, 0, 0), Vector3.up);

        if (!canStandUp) canStandUp = true;

        if (Physics.Raycast(frontRay, defaultCrouchHeight, crouchInteractLayer)) {
          canStandUp = false;
          Debug.DrawRay(transform.position - Vector3.up*motor.height/2 + new Vector3(0, 0, motor.radius), Vector3.up*defaultCrouchHeight, Color.red);
        }

        if (Physics.Raycast(backRay, defaultCrouchHeight, crouchInteractLayer)) {
          canStandUp = false;
          Debug.DrawRay(transform.position - Vector3.up*motor.height/2 + new Vector3(0, 0, -motor.radius), Vector3.up*defaultCrouchHeight, Color.red);
        }

        if (Physics.Raycast(rightRay, defaultCrouchHeight, crouchInteractLayer)) {
          canStandUp = false;
          Debug.DrawRay(transform.position - Vector3.up*motor.height/2 + new Vector3(motor.radius, 0, 0), Vector3.up*defaultCrouchHeight, Color.red);
        }

        if (Physics.Raycast(leftRay, defaultCrouchHeight, crouchInteractLayer)) {
          canStandUp = false;
          Debug.DrawRay(transform.position - Vector3.up*motor.height/2 + new Vector3(-motor.radius, 0, 0), Vector3.up*defaultCrouchHeight, Color.red);
        }
      }

      else if (!isCrouching) canStandUp = true;

      //Crouching
      if (Input.GetKey(crouchKey) && canCrouch && !isRunning && canMove && !isZeroGravity && isGrounded) isCrouching = true;

      //Standing.
      else if (!Input.GetKey(crouchKey) && canStandUp) isCrouching = false;
    }


    private int LastFootstepSound = 0;

    private void OnControllerColliderHit(ControllerColliderHit hit) {
      //If there are any available footsteps.
      if (footstepTypes.Length != 0) {
        //Loop through the footstep types array.
        for (int i = 0; i < footstepTypes.Length; i++) {
          //If the ground type matches the current step in the array, continue.
          if (hit.transform.CompareTag(footstepTypes[i].tagName)) {
            //If the array is not empty, set the current step sound and volume.
            if (footstepTypes[i].sounds.Length != 0) {
              //Set the current footstep sound and volume.
              var sound = Random.Range(0, footstepTypes[i].sounds.Length);

              if (sound == LastFootstepSound) {
                sound += 1;

                if (sound >= footstepTypes[i].sounds.Length) {
                  sound = 0;
                }
              }

              LastFootstepSound = sound;

              currentStepSound = footstepTypes[i].sounds[sound];
              currentStepVolume = footstepTypes[i].volume;
              break;
            }
          }
        }
      }

      //If we had high enough velocity to play landing animation.
      if (velocityY < cam.minVelocity && !playedLanding) {
        //Play the landing animation.
        cam.PlayLandingMotion();
      }

      //If we had high enough velocity to play landing sound.
      if (velocityY < minVelocity && !playedLanding) {
        //Update the timer
        footstepTimer = Time.time + footstepDelay;

        //If the landing is based on the current ground type.
        if (landingSoundBase == LandingSoundBase.currentGroundType) {
          //Play landing sound.
          var source = GetComponent<AudioSource>();
          source.PlayOneShot(currentStepSound, currentStepVolume*landingVolume);
        }

        //If the landing is based on a specific sound.
        else if (landingSoundBase == LandingSoundBase.specificSound) {
          //Play landing sound.
          if (landingSound != null)
            GetComponent<AudioSource>().PlayOneShot(landingSound, landingVolume);
        }

        //Update the landing sound state.
        playedLanding = true;
      }
    }

    private void PlayFootsteps() {
      //If we are grounded, continue.
      if (isGrounded && isMoving && canMove && footstepTypes.Length != 0) {
        //If we can play the next step sound.
        if (footstepTimer < Time.time) {
          //If running.
          if (isRunning) {
            footstepTimer = Time.time + runStepTime;
            stepVolume = currentStepVolume*runStepVolume;
          }

          //If crouching.
          else if (isCrouching) {
            footstepTimer = Time.time + crouchStepTime;
            stepVolume = currentStepVolume*crouchStepVolume;
          }

          //If walking.
          else if (!isRunning && !isCrouching) {
            footstepTimer = Time.time + walkStepTime;
            stepVolume = currentStepVolume*walkStepVolume;
          }

          //Play the current footstep with the current volume.
          var source = GetComponent<AudioSource>();
          source.pitch = Random.Range(0.7f, 1.1f);
          source.PlayOneShot(currentStepSound, stepVolume);
        }
      }
    }

    private void CalculateGhostMode() {
      //If ghost mode is enabled, and currently the collision is active between the layers.
      if (isGhosting && !Physics.GetIgnoreLayerCollision(gameObject.layer, ignoredLayer)) {
        //Disable the collision between the player's layer and the selected layer.
        Physics.IgnoreLayerCollision(gameObject.layer, ignoredLayer, true);
      }

      //If ghost mode is disabled, and currently the collision is disabled between the layers.
      if (!isGhosting && Physics.GetIgnoreLayerCollision(gameObject.layer, ignoredLayer)) {
        //Enable the collision between the player's layer and the selected layer.
        Physics.IgnoreLayerCollision(gameObject.layer, ignoredLayer, false);
      }
    }
  }
}