using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FootTiggers : MonoBehaviour
{

    public enum AxisToTrack
    {
        Hands,
        Feet
    };

    public AxisToTrack TrackedAxis = AxisToTrack.Hands;
    
    [Header("Stomp Settings")]
    [SerializeField]
    GameObject m_StompVFX;

    float m_FloorHeight = -1;

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

    // vfx
    [SerializeField] float m_TriggerFXDistance = 0.92f;
    [SerializeField] float m_TriggerResestDistance = 0.89f;

    float m_LeftJointDistance;
    float m_RightJointDistance;
    bool m_CanSpawnLeft = false;
    bool m_CanSpawnRight = false;

    // debug text
    [SerializeField]
    TextMeshProUGUI m_FloorHeightText;
    [SerializeField]
    TextMeshProUGUI m_LeftFootHeightText;
    [SerializeField]
    TextMeshProUGUI m_RightFootHeightText;

    

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

        /*
        //m_LeftFootHeight = m_LeftFootJoint.transform.position.y - m_FloorHeight;
        //m_RightFootHeight = m_RightFootJoint.transform.position.y - m_FloorHeight;
        m_LeftJointDistance = JointDistance(m_LeftTrackedJoint, m_RootHipJoint);
        m_RightJointDistance = JointDistance(m_RightTrackedJoint, m_RootHipJoint);
        */
        
        //m_FloorHeightText.text = "HipHeight: "+m_RootHitJoint.position.y;
        m_FloorHeightText.text = m_CanSpawnLeft.ToString();
        m_LeftFootHeightText.text = "LeftFoot: "+m_LeftJointDistance.ToString();
        m_RightFootHeightText.text = "RightFoot: "+m_RightJointDistance.ToString();

        if (m_LeftJointDistance <= m_TriggerResestDistance)
        {
            m_CanSpawnLeft = true;
        }

        if (m_RightJointDistance <= m_TriggerResestDistance)
        {
            m_CanSpawnRight = true;
        }
        

        
        // left side
        if (m_CanSpawnLeft && m_LeftJointDistance >= m_TriggerFXDistance)
        {
            
            
            
            /*
            Quaternion VFXRot = Quaternion.identity;
            // arms
            if (TrackedAxis == AxisToTrack.Hands)
            {
                Vector3 dirVector = m_LeftTrackedJoint.position - m_LeftShoulderJoint.position;
                VFXRot = Quaternion.LookRotation(dirVector);
            }
            */
            
            
            Instantiate(m_StompVFX, m_LeftTrackedJoint.transform.position, GetVFXRotation(false));
            m_CanSpawnLeft = false;
        }

        // right side
        if (m_CanSpawnRight && m_RightJointDistance >= m_TriggerFXDistance)
        {
            
            
            /*
            
            Quaternion VFXRot = Quaternion.identity;
            // arms
            if (TrackedAxis == AxisToTrack.Hands)
            {
                Vector3 dirVector = m_RightTrackedJoint.position - m_RightShoulderJoint.position ;
                VFXRot = Quaternion.LookRotation(dirVector);
            }
            */
            Instantiate(m_StompVFX, m_RightTrackedJoint.transform.position, GetVFXRotation(true));
            m_CanSpawnRight = false;
        }
        
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
        if (TrackedAxis == AxisToTrack.Hands)
        {
            return baseJoint.position.y - trackedJoint.position.y;
        }
        // Hands
        else
        {
            float retVal = baseJoint.position.x - trackedJoint.position.x;
            if (retVal < 0) { retVal *= -1;}

            return retVal;
            
            /*
            float retVal = -1;
            if (trackedJoint == m_LeftFootJoint)
            {
                retVal = m_LeftShoulderJoint.position.x - trackedJoint.position.x;
                if (retVal < 0) { retVal *= -1;}
            }
            else
            {
                // right
                retVal = m_RightShoulderJoint.position.x - trackedJoint.position.x;
                if (retVal < 0) { retVal *= -1;}
            }

            return retVal;
            */
        }
        
    }
    
    
}
