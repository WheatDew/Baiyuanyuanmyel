using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginCharacterController : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private OriginCharacter originCharacter;
    private OriginCharacterSelectionSystem characterSelectionSystem;

    public float multiple;
    public float jumpMultiple;
    public float maxVelocity;
    public bool isGround = false;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        characterSelectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        originCharacter = GetComponent<OriginCharacter>();
    }

    private void Update()
    {
        if(OriginKeyboardSystem.isAction)
        CharacterMoveJob();
    }

    public void CharacterMoveJob()
    {
        if (characterSelectionSystem.targetCharacter != null && characterSelectionSystem.targetCharacter == originCharacter)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (m_rigidbody.velocity.x > maxVelocity)
                {
                    m_rigidbody.AddForce((maxVelocity - m_rigidbody.velocity.x) / Time.deltaTime, 0, 0, ForceMode.Acceleration);
                }
                else
                {
                    m_rigidbody.AddForce(multiple, 0, 0, ForceMode.Acceleration);
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (m_rigidbody.velocity.x < -maxVelocity)
                {
                    m_rigidbody.AddForce((-maxVelocity - m_rigidbody.velocity.x) / Time.deltaTime, 0, 0, ForceMode.Acceleration);
                }
                else
                {
                    m_rigidbody.AddForce(-multiple, 0, 0, ForceMode.Acceleration);
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (m_rigidbody.velocity.z > maxVelocity)
                {
                    m_rigidbody.AddForce(0, 0, (maxVelocity - m_rigidbody.velocity.z) / Time.deltaTime, ForceMode.Acceleration);
                }
                else
                {
                    m_rigidbody.AddForce(0, 0, multiple, ForceMode.Acceleration);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (m_rigidbody.velocity.z < -maxVelocity)
                {
                    m_rigidbody.AddForce(0, 0, (-maxVelocity - m_rigidbody.velocity.z) / Time.deltaTime, ForceMode.Acceleration);
                }
                else
                {
                    m_rigidbody.AddForce(0, 0, -multiple, ForceMode.Acceleration);
                }
            }

            //m_rigidbody.velocity = new Vector3(vx, vy, vz);

            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                m_rigidbody.AddForce(Vector3.up * jumpMultiple);
            }

            if (!isGround)
                m_rigidbody.AddForce(Vector3.down * jumpMultiple * 0.01f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}
