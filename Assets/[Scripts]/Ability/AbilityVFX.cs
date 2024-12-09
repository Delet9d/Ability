using UnityEngine;

namespace _Scripts_.Ability
{
    public class AbilityVFX : MonoBehaviour
    {
        private AbilitySO _AbilitySO;

        public void Initialization(AbilitySO AbilitySO)
        {
            _AbilitySO = AbilitySO;
        }

        public void PlayActivateVFX(Vector3 Position)
        {
            if(_AbilitySO.VisualEffectRelease)
                Instantiate(_AbilitySO.VisualEffectRelease, Position, Quaternion.identity);
        }
        
        public void PlayHitVFX(Vector3 Position)
        {
            if (_AbilitySO.VisualEffectHit)
                Instantiate(_AbilitySO.VisualEffectHit, Position, Quaternion.identity);
            else
                Debug.Log("Where Hit VFX");
        }
        
        public void PlayEndVFX(Vector3 Position)
        {
            if(_AbilitySO.VisualEffectEnd)
                Instantiate(_AbilitySO.ExplosionVFX, Position, Quaternion.identity);
        }
    }
}