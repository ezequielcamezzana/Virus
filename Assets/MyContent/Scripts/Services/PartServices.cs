using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public enum Part
{
    Body, Arm, Eye, Tail, Mouth, Outline
}

public class PartService
{

    private static PartService instance = null;
    public static PartService Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            return new PartService();
        }
    }

    public Transform GetPart(Part part)
    {
        List<Transform> parts = GetParts(part);
        return parts[Random.Range(0, parts.Count)];
    }

    List<Transform> GetParts(Part part)
    {

        List<Transform> filteredParts = new List<Transform>();

        Object[] parts = Resources.LoadAll("Parts/" + part, typeof(Transform));

        foreach (Object obj in parts)
        {
            Transform transform = (Transform)obj;
            filteredParts.Add(transform);
        }
        return filteredParts;
    }

    public Transform GetEyeDisplayer(string amount)
    {
        Transform eyeDisplayer = null;
        Object[] parts = Resources.LoadAll("Parts/Displayer", typeof(Transform));

        foreach (Object obj in parts)
        {
            Transform transform = (Transform)obj;
            if (transform.name.Contains(amount))
            {
                eyeDisplayer = transform;
            }

        }

        return eyeDisplayer;
    }
}
