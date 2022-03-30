using System;
using UnityEngine;

namespace _Project.Scripts._Player
{
    [Serializable]
    public class PlayerMouseLook
    {
        private Transform _player;
        private Camera _camera;

        private float rotX;
        private float rotY;

        private const int TiltAngle = 88;
        
        public float sensitivity = 2F;
        
        public void Init(Transform player, Camera camera)
        {
            this._player = player;
            this._camera = camera;
        }

        public void SetRotation(Quaternion rot)
        {
            this.rotX = rot.eulerAngles.x;
            this.rotY = rot.eulerAngles.y;
        }

        public void UpdateCamera(float cameraOffsetY = 0.7F)
        {
            rotX -= Input.GetAxis("Mouse Y") * sensitivity;
            rotY += Input.GetAxis("Mouse X") * sensitivity;
            
            ClampRotations();

            _player.rotation = Quaternion.Euler(0F, rotY, 0F);
            _camera.transform.rotation = Quaternion.Euler(rotX, rotY, 0F);
            _camera.transform.position = _player.position + new Vector3(0, cameraOffsetY, 0);
        }

        private void ClampRotations()
        {
            if (rotX > TiltAngle)
                rotX = TiltAngle;
            else if (rotX < -TiltAngle)
                rotX = -TiltAngle;

            if (rotY >= 360F)
                rotY -= 360F;
            else if (rotY <= -360F)
                rotY += 360F;
        }
    }
}