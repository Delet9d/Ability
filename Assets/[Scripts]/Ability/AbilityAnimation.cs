using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace _Scripts_.Ability
{
    public class AbilityAnimation : MonoBehaviour
    {
        private AbilitySO _AbilitySO;
        private LayeredAnimation layeredAnimation;

        public void Initialization(AbilitySO AbilitySO, GameObject player)
        {
            _AbilitySO = AbilitySO;
            layeredAnimation = player.GetComponent<LayeredAnimation>();
            layeredAnimation.AnimationSetup(_AbilitySO.AnimationClip);
        }
    }
}