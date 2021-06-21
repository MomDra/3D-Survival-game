using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    // 충돌한 오브젝트의 컬라이더
    List<Collider> colliderList = new List<Collider>();

    [SerializeField]
    int layerGround; // 지상 레이어
    const int IGNORE_LAYCAST_LAYER = 2;

    [SerializeField]
    Material green;
    [SerializeField]
    Material red;

    bool shouldChange;

    // Update is called once per frame
    void Update()
    {
        if(shouldChange)
            ChangeColor();
    }

    void ChangeColor()
    {
        if(colliderList.Count > 0)
        {
            SetColor(red);
        }
        else
        {
            SetColor(green);
        }
    }

    void SetColor(Material mat)
    {
        foreach(Transform tf_Child in transform)
        {
            var newMaterials = new Material[tf_Child.GetComponent<Renderer>().materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mat;
            }

            tf_Child.GetComponent<Renderer>().materials = newMaterials;
        }
        shouldChange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYCAST_LAYER)
        {
            colliderList.Add(other);
            shouldChange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYCAST_LAYER)
        {
            colliderList.Remove(other);
            shouldChange = true;
        }
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }
}
