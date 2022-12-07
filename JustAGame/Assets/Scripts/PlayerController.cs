using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent Landed;
    public UnityEvent Dead;

    [SerializeField] private float _jumpforce;
    [SerializeField] private ContactFilter2D _platform;

    private Rigidbody2D _rigidbody;
    private bool _isonplatform => _rigidbody.IsTouching(_platform);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (_isonplatform == true)
            _rigidbody.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.transform.parent != null)
        {

            if (collisionObject.transform.parent.TryGetComponent(out Platform platform))
                platform.StopMovement();
        }


        if (collisionObject.CompareTag("PlatformWall"))
            Dead?.Invoke();

        else if (collisionObject.CompareTag("Platform"))
        {
            collisionObject.tag = "Untagged";
            Landed?.Invoke();
        }
    }
}
