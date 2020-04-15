using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeAnimator : MonoBehaviour
{
    private Mesh _animationMesh;
    private SkinnedMeshRenderer _renderer;
    private int _blendShapeCount;

    [SerializeField] [Range(1f, Mathf.Infinity)] 
    private float frameRate = 1f;
    private float frequency => 1.0f / frameRate;

    [SerializeField] [Range(0f, 100f)]
    float blendWeights = 100f;
    
    private void Awake()
    {
        // Assume only one SkinnedMeshRenderer in the object
        _renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        _animationMesh = _renderer.sharedMesh;

        if (_renderer == null)
        {
            throw new NullReferenceException("Object has no SkinnedMeshRenderer");
        }
        
        
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _blendShapeCount = _animationMesh.blendShapeCount;
        StartCoroutine(RunAnimation());
    }

    private IEnumerator RunAnimation()
    {
        while (true)
        {
            // Assume all blend weights start at 0
            for (var i = 0; i < _blendShapeCount; i++)
            {
                _renderer.SetBlendShapeWeight(i, blendWeights);
                
                yield return new WaitForSeconds(frequency);

                _renderer.SetBlendShapeWeight(i, 0f);
            }
            
            yield return new WaitForSeconds(frequency);
        }
    }
}
