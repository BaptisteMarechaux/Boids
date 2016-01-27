using UnityEngine;
using System.Collections;

public class BoidsManager : MonoBehaviour {

    Boid[] boids;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitializePositions()
    {
        for(int i=0;i<boids.Length;i++)
        {
            boids[i] = new Boid();
        }
    }

    void MoveBoids()
    {
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].velocity = boids[i].velocity + MoveToCenter(boids[i]) + GetCloser(boids[i]) + MatchNeighbours(boids[i]);

            boids[i].position = boids[i].position + boids[i].velocity;
        }
    }

    Vector3 MoveToCenter(Boid b)
    {
        return new Vector3();
    }

    Vector3 GetCloser(Boid b)
    {
        return new Vector3();
    }
    
    Vector3 MatchNeighbours(Boid b)
    {
        return new Vector3();
    }
}
