using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackedBodyVFX : MonoBehaviour
{
    public enum AxisToTrack
    {
        Hands,
        Feet
    };

    public AxisToTrack TrackedAxis = AxisToTrack.Hands;
    
    [Header("VFX To Spawn")]
    [SerializeField]
    GameObject m_SpawnedVFX;

    [Header("Assigned Joints")]
    [SerializeField]
    Transform m_LeftTrackedJoint;
    [SerializeField]
    Transform m_RightTrackedJoint;
    [SerializeField]
    Transform m_RootHipJoint;
    [SerializeField]
    Transform m_LeftShoulderJoint;
    [SerializeField]
    Transform m_RightShoulderJoint;

    // trigger distances
    [SerializeField] float m_TriggerFXDistance = 0.92f;
    [SerializeField] float m_TriggerResetDistance = 0.89f;

    float m_LeftJointDistance;
    float m_RightJointDistance;
    bool m_CanSpawnLeft = false;
    bool m_CanSpawnRight = false;

    // debug text
    [SerializeField]
    TextMeshProUGUI m_LeftJointDistanceText;
    [SerializeField]
    TextMeshProUGUI m_RightJointDistanceText;

    void Update()
    {
        if (TrackedAxis == AxisToTrack.Hands)
        {
            m_LeftJointDistance = JointDistance(m_LeftShoulderJoint, m_LeftTrackedJoint);
            m_RightJointDistance = JointDistance(m_RightShoulderJoint, m_RightTrackedJoint);
        }
        else
        {
            m_LeftJointDistance = JointDistance(m_RootHipJoint, m_LeftTrackedJoint);
            m_RightJointDistance = JointDistance(m_RootHipJoint, m_RightTrackedJoint);
        }

        // left side
        if (m_LeftJointDistance <= m_TriggerResetDistance)
        {
            m_CanSpawnLeft = true;
        }
        
        if (m_CanSpawnLeft && m_LeftJointDistance >= m_TriggerFXDistance)
        {          
            Instantiate(m_SpawnedVFX, m_LeftTrackedJoint.transform.position, GetVFXRotation(false));
            m_CanSpawnLeft = false;
        }

        // right side
        if (m_RightJointDistance <= m_TriggerResetDistance)
        {
            m_CanSpawnRight = true;
        }
        
        if (m_CanSpawnRight && m_RightJointDistance >= m_TriggerFXDistance)
        {

            Instantiate(m_SpawnedVFX, m_RightTrackedJoint.transform.position, GetVFXRotation(true));
            m_CanSpawnRight = false;
        }
        
        //debug text
        m_LeftJointDistanceText.text = "Left: "+m_LeftJointDistance.ToString();
        m_RightJointDistanceText.text = "Right: "+m_RightJointDistance.ToString(); 
    }

    Quaternion GetVFXRotation(bool rhs)
    {
        Quaternion retVal = Quaternion.identity;
        if (TrackedAxis == AxisToTrack.Hands)
        {
            if (rhs)
            {
                return Quaternion.LookRotation(m_RightTrackedJoint.position - m_RightShoulderJoint.position);
            }
            else
            {
                return Quaternion.LookRotation(m_LeftTrackedJoint.position - m_LeftShoulderJoint.position);
            }    
        }
        return retVal;
    }

    float JointDistance(Transform trackedJoint, Transform baseJoint)
    {
        if (TrackedAxis == AxisToTrack.Feet)
        {
            return trackedJoint.position.y - baseJoint.position.y;
        }
        // Hands
        else
        {
            float retVal = baseJoint.position.x - trackedJoint.position.x;
            if (retVal < 0) { retVal *= -1; }

            return retVal;
        }
    }
}
