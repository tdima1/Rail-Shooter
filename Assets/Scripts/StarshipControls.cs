using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class StarshipControls : MonoBehaviour
{
   private float _horizontalInput;
   private float _verticalInput;
   [Tooltip("In ms^-1")][SerializeField] private float _shipStrafingSpeed = 8f;
   [Tooltip("In ms^-1")] [SerializeField] private float _shipAscendingSpeed = 8f;
   [Tooltip("In meters")] [SerializeField] private float _shipMovementRangeHorizontal = 7.5f;
   [Tooltip("In meters")] [SerializeField] private float _shipMovementRangeVertical = 4.5f;

   [SerializeField] private float _pitchPositionFactor = -4f;
   [SerializeField] private float _pitchTiltFactor = -20f;

   [SerializeField] private float _yawPositionFactor = 5f;

   [SerializeField] private float _rollPositionFactor = 2.5f;
   [SerializeField] private float _rollTiltFactor = -30f;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      HandleStarshipTranslation();
      HandleStarshipRotation();
   }

   private void HandleStarshipRotation()
   {
      float pitchFromPosition = transform.localPosition.y * _pitchPositionFactor;
      float pitchFromTilt = _verticalInput * _pitchTiltFactor;
      float pitch = pitchFromPosition + pitchFromTilt;

      float yaw = transform.localPosition.x * _yawPositionFactor;

      float rollFromPosition = transform.localPosition.x * _rollPositionFactor;
      float rollFromTilt = _horizontalInput * _rollTiltFactor;
      float roll = rollFromPosition + rollFromTilt;

      transform.localRotation = Quaternion.Euler(new Vector3(pitch, yaw, roll));
   }

   private void HandleStarshipTranslation()
   {
      _horizontalInput = Input.GetAxis("Horizontal");
      _verticalInput = Input.GetAxis("Vertical");
      float shipOffsetHorizontal = _horizontalInput * Time.deltaTime * _shipStrafingSpeed;
      float rawShipOffsetHorizontal = transform.localPosition.x + shipOffsetHorizontal;
      float clampedShipOffsetHorizontal = Mathf.Clamp(rawShipOffsetHorizontal, -_shipMovementRangeHorizontal, _shipMovementRangeHorizontal);

      float shipOffsetVertical = _verticalInput * Time.deltaTime * _shipAscendingSpeed;
      float rawShipOffsetVertical = transform.localPosition.y + shipOffsetVertical;
      float clampedShipOffsetVertical = Mathf.Clamp(rawShipOffsetVertical, -_shipMovementRangeVertical, _shipMovementRangeVertical);

      transform.localPosition = new Vector3(clampedShipOffsetHorizontal, clampedShipOffsetVertical, transform.localPosition.z);
   }
}
