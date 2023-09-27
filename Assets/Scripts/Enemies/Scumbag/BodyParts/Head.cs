using UnityEngine;

public class Head : BodyPart 
{
    protected override void Awake()
    {
        _localPosition = new Vector3(-0.02565908f, 0.9213767f, 0f);
        base.Awake();
    }

    protected override void Start() {}
}