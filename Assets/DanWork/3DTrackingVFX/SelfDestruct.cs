using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    float m_DestoryTime = 1.0f;
    void Start()
    {
        Destroy(this.gameObject, m_DestoryTime);
    }
}
