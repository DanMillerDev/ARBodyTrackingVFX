using UnityEngine;

public class JointPlacer : MonoBehaviour
{
    [SerializeField] GameObject m_JointPrefab;
    [SerializeField] GameObject m_RootObject;

    void Start()
    {
        Transform[] childTransforms = m_RootObject.GetComponentsInChildren<Transform>();
        
        for(int i = 0; i < childTransforms.Length; i++)
        {
            GameObject newObject = Instantiate(m_JointPrefab, childTransforms[i]);
            newObject.transform.localPosition = Vector3.zero;
        }
    }
}











