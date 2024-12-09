using UnityEngine;

namespace _Scripts_.Ability
{
    public class AbilitySound : MonoBehaviour
    {
        private AbilitySO _AbilitySO;

        public void Initialization(AbilitySO AbilitySO)
        {
            _AbilitySO = AbilitySO;
        }
    }
}