using System;
using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed = 4;
        private Vector2 inputVector = Vector2.zero;

        public override void OnStartServer()
        {
            
        }

        private void Start()
        {
            if (!isLocalPlayer) { return; }
            
        }

        private void Move()
        {
            transform.Translate(new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime);
        }
    }
}