using System;
using System.Collections.Generic;
using _Scripts_.Interfaces;
using UnityEngine;

namespace _Scripts_
{
    public class EntityAbilityManager : MonoBehaviour
    {
        [SerializeField] private List<Component> components;

        private void Start()
        {
            foreach (var e in components)
            {
                Debug.Log(e.GetComponent<IAbility>());
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(components[0].TryGetComponent(out IAbility ability))
                    ability.AbilityActivated(gameObject);
                else
                    Debug.LogWarning("No ability activated");

            }
        }

        public void AnimationSetuup()
        {
            if(components[0].TryGetComponent(out AbilityController ability))
                ability.AbilityStateFunc(EAbilityState.Release);
            else
                Debug.LogWarning("No ability activated");
        }

        public void ApplyEffect(AbilitySO Ability)
        {
            Debug.Log("Apply Self Effect");
        }
    }
}