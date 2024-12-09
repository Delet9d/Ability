using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace _Scripts_.Ability
{
    public class AbilityAnimation : MonoBehaviour
    {
        private AbilitySO _AbilitySO;
        private LayeredAnimation layeredAnimation;
        GameObject playerGameObject;
        Camera playerCamera;
        private bool isRotating = false;
        private float RotatingTimer = 0f;
        public void Initialization(AbilitySO AbilitySO, GameObject player)
        {
            _AbilitySO = AbilitySO;
            playerGameObject = player;
            playerCamera = FindFirstObjectByType<Camera>();
            layeredAnimation = player.GetComponent<LayeredAnimation>();
            layeredAnimation.AnimationSetup(_AbilitySO.AnimationClip);

            isRotating = true;
        }

        private void Update()
        {
            if (isRotating)
            {
                if (_AbilitySO.EntityRotationOnAbilityRelease != EAbilityAngleOnRelease.Angle)
                {
                    isRotating = false;
                    return;
                }
                RotatingTimer += Time.deltaTime;
                Debug.Log(RotatingTimer + "     " + _AbilitySO.AnimationClip.averageDuration);
                playerGameObject.transform.eulerAngles = new Vector3(
                    _AbilitySO.EntityAngleOnAbilityRelease,
                    playerGameObject.transform.eulerAngles.y, 
                    playerGameObject.transform.eulerAngles.z  
                );
                if (RotatingTimer > _AbilitySO.AnimationClip.averageDuration)
                {
                    isRotating = false;
                    RotatingTimer = 0f;
                }
            }
        }
    }
}