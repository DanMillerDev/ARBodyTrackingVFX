using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerFXGestures : MonoBehaviour
{
    [SerializeField]
    Transform m_LeftHandJoint;
    [SerializeField]
    Transform m_RightHandJoint;

    
    public TextMeshProUGUI DistanceDebugText;

    float m_HandDistance;
    float m_CurrentTime = 0;
    static float s_ResetDistanceThreshold = 0.7f;

    Vector3 m_CenterPointBetweenHands;
    
    // CLAP
    [Header("Clap Settings")]
    [SerializeField]
    bool m_Clap = false;
    [SerializeField]
    GameObject m_ClapVFX;

    static float s_SpawnClapThreshold = 0.3f;
    static float s_ClapRespawnTime = 0.6f;
    bool m_ClapSpawnable = true;
    
    // floating orb
    [Header("Orb Settings")]
    [SerializeField]
    bool m_FloatingOrb = true;
    
    [SerializeField]
    GameObject m_OrbVFX;

    static float s_SpawnOrbMin = 0.3f;
    static float s_SpawnOrbMax = 0.85f;

    GameObject m_SpawnedOrbVFX = null;


    

    void Update()
    {
        m_HandDistance = Vector3.Distance(m_LeftHandJoint.position, m_RightHandJoint.position);
        m_CenterPointBetweenHands = ((m_LeftHandJoint.position + m_RightHandJoint.position) / 2);
        DistanceDebugText.text = m_HandDistance.ToString();

        // clap
        if (m_Clap)
        {
            if (m_ClapSpawnable && m_HandDistance <= s_SpawnClapThreshold)
            {
                Instantiate(m_ClapVFX, m_CenterPointBetweenHands, Quaternion.identity);
                m_ClapSpawnable = false;
                m_CurrentTime = 0;
            }
        }

        if (m_FloatingOrb)
        {
            if (m_HandDistance >= s_SpawnOrbMin && m_HandDistance <= s_SpawnOrbMax)
            {
                if (m_SpawnedOrbVFX == null)
                {
                    m_SpawnedOrbVFX = Instantiate(m_OrbVFX, m_CenterPointBetweenHands, Quaternion.identity);
                }

                m_SpawnedOrbVFX.transform.position = m_CenterPointBetweenHands;
            }
            else
            {
                if (m_SpawnedOrbVFX != null)
                {
                    Destroy(m_SpawnedOrbVFX);
                    m_SpawnedOrbVFX = null;
                }

            }
        }


        m_CurrentTime += Time.deltaTime;
        
        if (m_CurrentTime >= s_ClapRespawnTime)
        {
            m_ClapSpawnable = true;
        }
        
    }
    

}
