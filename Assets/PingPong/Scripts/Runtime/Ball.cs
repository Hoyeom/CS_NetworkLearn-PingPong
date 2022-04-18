using Mirror;
using UnityEngine;

namespace PingPong.Scripts.Runtime
{
    public class Ball : NetworkBehaviour
    {
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private float speed = 6;
        public override void OnStartServer()
        {
            rigid.simulated = true;
            
            rigid.velocity = Vector2.right * speed; 
        }
        
        
    }
}