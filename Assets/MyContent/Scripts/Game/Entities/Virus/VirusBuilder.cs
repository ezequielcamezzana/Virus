﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class VirusBuilder : MonoBehaviour
{
    private Transform body;
    public int seed;
    public bool generateSeed;

    public void Build(int seed)
    {
        Random.InitState(seed);
        BuildBody();
        //BuildArms();
        BuildMouth();
        BuildTail();
        BuildEyes();
    }


    private void ApplyRandomColor(Transform transform)
    {
        SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = Random.ColorHSV();
        }
    }

    private void ApplyColor(Transform transform, Color color)
    {
        SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = color;
        }
    }

    private void BuildBody()
    {
        body = PartService.Instance.GetPart(Part.Body);
        body = Instantiate(body, transform.position, Quaternion.identity, transform);
        // body.gameObject.layer = LayerMask.NameToLayer("Creatures");
        ApplyRandomColor(body);
        
        // TODO: separate the code of outlines.
        var outlines = GetComponentsInChildren<Transform>()
            .Where(t => t.name.ToLower().Contains("gameobject"));
        var outline = PartService.Instance.GetPart(Part.Outline); 
        var spriteRendererBody = body.gameObject.GetComponent<SpriteRenderer>();
        foreach (var transformOutline in outlines)
        {
            var currentOutline = Instantiate(outline, transformOutline.position, transformOutline.rotation, transformOutline);
            var spriteRenderer = currentOutline.GetComponent<SpriteRenderer>();
            spriteRenderer.color = spriteRendererBody.color;
            // TODO: Load the image set in the component.
            // var spriteRenderer = transformOutline.gameObject.AddComponent<SpriteRenderer>();
            // spriteRenderer.color = Random.ColorHSV();
            // spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        }
    }

    private void BuildTail()
    {
        Transform anchor = GetAnchors(body, Part.Tail).FirstOrDefault();
        Transform _tail = PartService.Instance.GetPart(Part.Tail);
        Transform tail = Instantiate(_tail, anchor.position, anchor.rotation);
        tail.parent = anchor;
        ApplyRandomColor(tail);
    }

    private void BuildMouth()
    {
        Transform anchor = GetAnchors(body, Part.Mouth).FirstOrDefault();
        Transform _mouth = PartService.Instance.GetPart(Part.Mouth);
        Transform mouth = Object.Instantiate(_mouth, anchor.position, anchor.rotation);
        mouth.parent = anchor;
        ApplyRandomColor(mouth);
        //GetShootPoint(mouth); put shopoitn on Motuh could be an option
    }

    private void BuildEyes()
    {
        Transform anchor = GetAnchors(body, Part.Eye).FirstOrDefault();
        Transform eye = PartService.Instance.GetPart(Part.Eye);
        int eyeAmount = Random.Range(1, 4);
        if (eyeAmount > 1)
        {
            Color color = Random.ColorHSV();
            Transform eyesDisplayers = PartService.Instance.GetEyeDisplayer(eyeAmount.ToString());
            eyesDisplayers = Instantiate(eyesDisplayers, anchor.position, anchor.rotation, anchor);
            List<Transform> anchors = GetAnchors(eyesDisplayers, Part.Eye);
            foreach (Transform _anchor in anchors)
            {
                eye = Instantiate(eye, _anchor.position, _anchor.rotation, _anchor);
                ApplyColor(eye, color);
                eye.SetParent(_anchor);
            }
        }
        else
        {
            eye = Instantiate(eye, anchor.position, anchor.rotation);
            ApplyRandomColor(eye);
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