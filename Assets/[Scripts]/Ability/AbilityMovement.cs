using DG.Tweening;
using UnityEngine;

namespace _Scripts_.Ability
{
    public class AbilityMovement : MonoBehaviour
    {
        private AbilitySO _AbilitySO;
        private Transform CameraTransform;
        public void Initialization(AbilitySO AbilitySO, Transform PlayerTransform)
        {
            _AbilitySO = AbilitySO;
            playerGameObject = PlayerTransform.gameObject;
            CameraTransform = FindFirstObjectByType<Camera>().transform.parent;
        }
        
        
        [SerializeField] private GameObject playerGameObject;

        [SerializeField] private float Timer = 0f;

        public void RotatePlayer()
        {
            if (_AbilitySO.EntityRotationOnAbilityRelease == EAbilityAngleOnRelease.None)
                return;
            
            Vector3 cameraForward = CameraTransform.forward;
            cameraForward.y = 0;
            if (cameraForward.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
                
                playerGameObject.transform.DORotateQuaternion(targetRotation, 1 / 100f)
                    .OnUpdate(() =>
                    {
                        Timer += Time.deltaTime;
                        Debug.Log($"Rotating Timer: {Timer}");
                    })
                    .OnComplete(() =>
                    {
                        Debug.Log("Rotation Complete!");
                        Timer = 0f;
                    });
            }
            
            
            
            if (_AbilitySO.EntityRotationOnAbilityRelease == EAbilityAngleOnRelease.Angle)
            {
                
            }

            if (_AbilitySO.EntityRotationOnAbilityRelease == EAbilityAngleOnRelease.ToTarget)
            {
                
            }
            
            //To Stop Func
        }
    }
}