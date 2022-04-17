using System;
using Mirror;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed = 4; 
        private Vector2 inputVector = Vector2.zero;

        public override void OnStartServer()
        {
            this.FixedUpdateAsObservable()
                .Select(_ => inputVector)
                .Where(x=>x.sqrMagnitude>0)
                .Subscribe(_ => Move());
        }

        private void Start()
        {
            if (!isLocalPlayer) { return; }

            this.UpdateAsObservable()
                .Select(_ => new Vector2(0, Input.GetAxisRaw("Vertical")))
                .Subscribe(CmdInputVec);
        }
        
        [Command]
        private void CmdInputVec(Vector2 input)
        {
            inputVector = input;
        }
        
        private void Move()
        {
            transform.Translate(inputVector * speed * Time.fixedDeltaTime);
            Debug.Log($"뭐냐{inputVector}");
        }
    }
}