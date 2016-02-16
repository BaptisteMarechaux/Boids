using UnityEngine;
using System.Collections;

public class CircularMovement : MonoBehaviour {
    [SerializeField]
    Transform t;

    [SerializeField]
    float distance;

    float d;

    float r;

    Vector3 v = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        r+=Time.deltaTime;

        d = distance;// * Mathf.Cos(r);

        v = new Vector3(d * Mathf.Cos(r), d * Mathf.Cos(r), d * Mathf.Sin(r));
 
        t.position = v;


    }
}
