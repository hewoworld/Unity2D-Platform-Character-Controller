using UnityEngine;

namespace Shinjingi
{
    [RequireComponent(typeof(Controller))]
    public class WallInteractor : MonoBehaviour
    {
        public bool WallJumping { get; private set; }

        [Header("Wall Slide")]
        [SerializeField][Range(0.1f, 5f)] private float _wallSlideMaxSpeed = 2f;
        [Header("Wall Jump")]
        [SerializeField] private Vector2 _wallJumpClimb = new Vector2(4f, 12f);
        [SerializeField] private Vector2 _wallJumpBounce = new Vector2(10.7f, 10f);
        [SerializeField] private Vector2 _wallJumpLeap = new Vector2(14f, 12f);
        
        private Ground _Ground;
        private Rigidbody2D _body;
        private Controller _controller;

        private Vector2 _velocity;
        private bool _onWall, _onGround, _desiredJump;
        private float _wallDirectionX;

        // Start is called before the first frame update
        void Start()
        {
            _Ground = GetComponent<Ground>();
            _body = GetComponent<Rigidbody2D>();
            _controller = GetComponent<Controller>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_onWall && !_onGround)
            {
                _desiredJump |= _controller.input.RetrieveJumpInput();
            }
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;
            _onWall = _Ground.OnWall;
            _onGround = _Ground.OnGround;
            _wallDirectionX = _Ground.ContactNormal.x;

            #region Wall Slide
            if(_onWall)
            {
                if(_velocity.y < -_wallSlideMaxSpeed)
                {
                    _velocity.y = -_wallSlideMaxSpeed;
                }
            }
            #endregion

            #region Wall Jump

            if((_onWall && _velocity.x == 0) || _onGround)
            {
                WallJumping = false;
            }

            if(_desiredJump)
            {
                if(-_wallDirectionX == _controller.input.RetrieveMoveInput())
                {
                    _velocity = new Vector2(_wallJumpClimb.x * _wallDirectionX, _wallJumpClimb.y);
                    WallJumping = true;
                    _desiredJump = false;
                }
                else if(_controller.input.RetrieveMoveInput() == 0)
                {
                    _velocity = new Vector2(_wallJumpBounce.x * _wallDirectionX, _wallJumpBounce.y);
                    WallJumping = true;
                    _desiredJump = false;
                }
                else
                {
                    _velocity = new Vector2(_wallJumpLeap.x * _wallDirectionX, _wallJumpLeap.y);
                    WallJumping = true;
                    _desiredJump = false;
                }
            }
            #endregion

            _body.velocity = _velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _Ground.EvaluateCollision(collision);

            if(_Ground.OnWall && !_Ground.OnGround && WallJumping)
            {
                _body.velocity = Vector2.zero;
            }
        }
    }
}