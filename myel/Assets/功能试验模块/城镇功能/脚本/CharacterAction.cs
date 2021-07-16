using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    public Rigidbody m_rigidbody;
    public float multiple;

    private void Update()
    {
        m_rigidbody.AddForce(Input.GetAxis("Horizontal")*multiple,0 , Input.GetAxis("Vertical") * multiple);
    }
}
