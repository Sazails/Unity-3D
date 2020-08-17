using System;
using UnityEngine;

namespace _Project.Scripts._Player
{
    [Serializable]
    public class PlayerHeadBob
    {
        private Transform _camera;

        private float timer = 0F;
        public float bobSpeed = 0.18F;
        public float bobAmount = 0.2F;
        private const float MidPoint = 0.7F;

        public void Init(Transform camera)
        {
            this._camera = camera;
        }
        
        public float GetHeadBobOffset(Vector2 playerInput, float playerMoveSpeed)
        {
            float waveSlice = 0F;
            Vector3 newPos = _camera.position;
            if (Mathf.Abs(playerInput.x) < 0.01F && Mathf.Abs(playerInput.y) < 0.01F)
                timer = 0F;
            else
            {
                waveSlice = Mathf.Sin(timer);
                timer += bobSpeed * playerMoveSpeed;
                if (timer > Mathf.PI * 2)
                    timer -= Mathf.PI * 2;
            }

            if (Math.Abs(waveSlice) > 0F)
            {
                float change = waveSlice * bobAmount;
                float totalAxes = Mathf.Abs(playerInput.x) + Mathf.Abs(playerInput.y);
                totalAxes = Mathf.Clamp(totalAxes, 0F, 1F);
                change *= totalAxes;
                newPos.y = MidPoint + change;
            }
            else
                newPos.y = MidPoint;

            return newPos.y;
        }
    }
}
