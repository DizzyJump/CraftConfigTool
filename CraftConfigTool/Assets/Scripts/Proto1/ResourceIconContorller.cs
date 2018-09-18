using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceIconContorller : MonoBehaviour {
    public float InitialStrenght;
    public float FlySpeed;
    Vector2 InitialSpeed;
    public float FadeTime;
    public RectTransform Target;
    RectTransform _transform;
    float LastDistance = 0;
    public AnimationCurve curve;
    float fly_time = 0;

	// Use this for initialization
	void Start () {
        _transform = transform as RectTransform;
        InitialSpeed = Random.insideUnitCircle.normalized * Random.Range(0.8f, 1.0f) * InitialStrenght;
        //LastDistance = (Target.position - _transform.position).sqrMagnitude;
    }

    // Update is called once per frame
    void Update () {
        Vector2 dir = Target.position - _transform.position;
        dir.Normalize();
        Vector2 FinalSpeed = dir* FlySpeed;
        float time_val = curve.Evaluate(fly_time);
        Vector2 ResultSpeed = FinalSpeed * time_val + InitialSpeed * (1 - time_val);
        _transform.anchoredPosition = _transform.anchoredPosition + ResultSpeed * Time.deltaTime;
        float NewDistance = (Target.position - _transform.position).sqrMagnitude;
        fly_time += Time.deltaTime / FadeTime;
        if(RectTransformUtility.RectangleContainsScreenPoint(Target, _transform.position))
            Destroy(gameObject);
    }
}
