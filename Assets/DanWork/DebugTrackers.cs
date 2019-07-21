using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DebugTrackers : MonoBehaviour
{
    public BoneController.JointIndices m_TrackedJoint;
    BoneController m_BoneController;

    void Start()
    {
        m_BoneController = GetComponentInParent<BoneController>();
    }
    
    void Update()
    {
        this.transform.position = m_BoneController.GetTrackedJoint(m_TrackedJoint).position;
    }
}
