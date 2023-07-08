public class HomingBallController : BallController
{
    private void Update()
    {
        CalculateMoveDirection();
    }
    
    protected override void FixedUpdate()
    {
        _rb.velocity = _moveDirection * initialForce;
        base.FixedUpdate();
    }
}