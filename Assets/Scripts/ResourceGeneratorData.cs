using System;

[Serializable]
public class ResourceGeneratorData
{
    public float timeToGenerate;
    public ResourceTypeSO resourceType;
    public float resourceDetectionRadius;
    public int maxResourceAmount;
}
