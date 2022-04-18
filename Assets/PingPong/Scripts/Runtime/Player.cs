using Mirror;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PingPong.Scripts.Runtime
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed = 4;
        private Vector2 inputVector = Vector2.zero;

        public override void OnStartServer() => ServerMoveStream();
        private void Start()
        {
            if (!isLocalPlayer) { return; }
            InputStream();
        }

        #region Stream

        private void ServerMoveStream() => this.FixedUpdateAsObservable()
            .Select(_ => inputVector)
            .Where(x => x.sqrMagnitude > 0)
            .Subscribe(Move);

        private void InputStream() => this.UpdateAsObservable()
            .Select(_ => new Vector2(0, Mathf.Min(Input.GetAxisRaw("Vertical"),1)))
            .Subscribe(ClientInput);

        #endregion

        [Client]
        private void ClientInput(Vector2 value) => CmdInputVector(value);
        [Command]
        private void CmdInputVector(Vector2 value) => SetInputVector(value);
        [Server]
        private void SetInputVector(Vector2 value) => inputVector = value;
        private void Move(Vector2 value) => transform.Translate(value * speed * Time.fixedDeltaTime);
    }
}