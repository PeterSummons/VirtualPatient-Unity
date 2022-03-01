using UnityEngine;
using System.Collections;

public class BlendShape : MonoBehaviour
{
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    float blendOne = 0f;
    float blendTwo = 0f;
    float blendSpeed01 = 0.4f;
    float blendSpeed02 = 1f;
    bool blendOneStart = false;
    bool blendOneFinished = false;
    bool blendTwoStart = false;
    bool blendTwoFinished = false;
    AudioSource audioSource;
    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        blendShapeCount = skinnedMesh.blendShapeCount;
        audioSource = GetComponent<AudioSource>();
    }
    void setRobValue(string emotionName)
    {
        if (emotionName == "quality-dialog.quality-intent" || emotionName == "quantity-dialog.quantity-intent" || emotionName == "severity-dialog.severity-intent" || emotionName == "open-dialog.open-intent")
        {
            blendOne = 0f ;
            blendOneStart = true;
        }
        else if (emotionName != null)
        {
            blendTwo = 0f;
            blendTwoStart = true;
        }
    }
    void Update()
    {
        if (blendOneStart)
        {
            if (!blendOneFinished)
            {
                if (blendOne < 100f)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
                    blendOne += blendSpeed01;
                }
                else
                {
                    blendOneFinished = true;
                }
            }
            else
            {
                if (blendOne > 0f)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
                    blendOne -= blendSpeed01;
                }
                else
                {
                    blendOneStart = false;
                    blendOneFinished = false;
                }
            }
        }
        else if (blendTwoStart)
        {
            if (!blendTwoFinished)
            {
                if (blendTwo < 30f)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendTwo);
                    blendTwo += blendSpeed02;
                }
                else
                {
                    blendTwoFinished = true;
                }
            }
            else
            {
                if (blendTwo > 0f)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendTwo);
                    blendTwo -= blendSpeed02;
                }

                else
                {
                    if (audioSource.isPlaying)
                    {
                        blendTwoFinished = false;
                    }
                    else
                    {
                        blendTwoStart = false;
                        blendTwoFinished = false;
                    }

                }
            }
        }
    }
}