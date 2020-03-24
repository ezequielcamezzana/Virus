﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VirusBuilder : MonoBehaviour
{
    private Transform body;
    public int seed;

    void Awake()
    {
        Random.InitState(seed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate();
        }
    }

    public void Generate()
    {
        BuildBody();
        BuildArms();
        BuildMouth();
        BuildTail();
        BuildEyes();
    }

    private void BuildBody()
    {
        body = PartService.Instance.GetPart(Part.Body);
        body = Instantiate(body, transform.position, Quaternion.identity, transform);
        // body.gameObject.layer = LayerMask.NameToLayer("Creatures");
    }

    private void BuildTail()
    {
        Transform _tail = PartService.Instance.GetPart(Part.Tail);
        Transform anchor = GetAnchors(body, Part.Tail)[0];
        Transform tail = Instantiate(_tail, anchor.position, anchor.rotation);
        tail.parent = anchor;
    }

    private void BuildMouth()
    {
        Transform _mouth = PartService.Instance.GetPart(Part.Mouth);
        Transform anchor = GetAnchors(body, Part.Mouth)[0];
        Transform mouth = Object.Instantiate(_mouth, anchor.position, anchor.rotation);
        mouth.parent = anchor;
        //GetShootPoint(mouth); put shopoitn on Motuh could be an option
    }

    private void BuildEyes()
    {
        Transform anchor = GetAnchors(body, Part.Eye)[0];
        Transform eye = PartService.Instance.GetPart(Part.Eye);
        int eyeAmount = Random.Range(1, 4);
        Debug.Log(eyeAmount);
        if (eyeAmount > 1)
        {
            Transform eyesDisplayers = PartService.Instance.GetEyeDisplayer(eyeAmount.ToString());
            eyesDisplayers = Instantiate(eyesDisplayers, anchor.position, anchor.rotation, anchor);
            List<Transform> anchors = GetAnchors(eyesDisplayers, Part.Eye);
            foreach (Transform _anchor in anchors)
            {
                eye = Instantiate(eye, _anchor.position, _anchor.rotation, _anchor);
                eye.SetParent(_anchor);
            }
        }
        else
        {
            eye = Instantiate(eye, anchor.position, anchor.rotation);
            eye.SetParent(anchor);
        }
    }

    private void BuildArms()
    {
        int anchorIndex = 0;
        List<Transform> armAnchors = GetAnchors(body, Part.Arm);
        int armAmount = Random.Range(2, armAnchors.Count) / 2;
        for (int i = 0; i < armAmount; i++)
        {
            Transform leg = PartService.Instance.GetPart(Part.Arm);
            //Instantiate First Leg
            Transform anchor = armAnchors[anchorIndex];
            leg = Instantiate(leg, anchor.position, anchor.rotation);
            leg.SetParent(anchor);
            anchorIndex++;
            //Instantiate Second Leg
            anchor = armAnchors[anchorIndex];
            leg = Instantiate(leg, anchor.position, anchor.rotation);
            leg.SetParent(anchor);
            anchorIndex++;
        }
    }

    private List<Transform> GetAnchors(Transform body, Part part)
    {
        return GetChildrenByName(part.ToString(), body);
    }

    public static List<Transform> GetChildrenByName(string name, Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform eachChild in parent)
        {
            if (eachChild.name.ToLower().Contains(name.ToLower()))
            {
                children.Add(eachChild);
            }
        }
        return children;
    }
}
