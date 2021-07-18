using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    public Rigidbody m_rigidbody;
    public float multiple;
    public float jumpMultiple;
    public bool isGround=false;

    private void Update()
    {
        m_rigidbody.AddForce(Input.GetAxis("Horizontal")*multiple,0 , Input.GetAxis("Vertical") * multiple);

        if (isGround&& Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidbody.AddForce(Vector3.up * jumpMultiple);

        }

        if (!isGround)
            m_rigidbody.AddForce(Vector3.down * jumpMultiple*0.01f);

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
