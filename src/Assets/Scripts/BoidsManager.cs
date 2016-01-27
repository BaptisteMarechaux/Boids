using UnityEngine;
using System.Collections;

public class BoidsManager : MonoBehaviour {

    [SerializeField]
    int boidsNumber;
    [SerializeField]
    float distanceTreshold = 5;
    [SerializeField]
    float speedLimit;
    [SerializeField]
    Transform leaderTarget;

    [SerializeField]
    Transform[] boidsTransform;


    Boid[] boids;

	// Use this for initialization
	void Start () {
        InitializePositions();
	}
	
	// Update is called once per frame
	void Update () {
        MoveBoids();
        UpdateBoidsTransform();
	}

    void InitializePositions()
    {
        boids = new Boid[boidsNumber];
        for(int i=0;i<boidsNumber;i++)
        {
            boids[i] = new Boid(i, boidsTransform[i].position);
        }
    }

    void MoveBoids()
    {
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].velocity = boids[i].velocity + MoveToCenter(boids[i]) + GetCloser(boids[i]) + MatchVelocities(boids[i]) + followLeader(boids[i]);

            LimitVelocity(boids[i]);
            /*
            if(i==0)
            {
                boids[i].position = Vector3.Lerp(boids[i].position, leaderTarget.position, Time.deltaTime);
                continue;
            }
            */
            boids[i].position = boids[i].position + boids[i].velocity;
        }
    }

    Vector3 MoveToCenter(Boid b)
    {
        var perceivedCenter = new Vector3(); ;

        for(int i=0;i<boids.Length;i++)
        {
            if(b.id != boids[i].id)
                perceivedCenter += boids[i].position;
        }

        perceivedCenter *= 1 / (boids.Length - 1);

        return (perceivedCenter - b.position)/100;
    }

    Vector3 GetCloser(Boid b)
    {
        var newDistance = new Vector3();
        for(int i=0;i<boids.Length;i++)
        {
            if (b.id != boids[i].id)
            {
                if (Vector3.Distance(boids[i].position, b.position) < distanceTreshold)
                    newDistance = newDistance - (boids[i].position - b.position);
            }
        }
        return newDistance;
    }
    
    Vector3 MatchVelocities(Boid b)
    {
        var perceivedVelocity = new Vector3(); ;

        for (int i = 0; i < boids.Length; i++)
        {
            if (b.id != boids[i].id)
                perceivedVelocity += boids[i].velocity;
        }

        perceivedVelocity *= 1 / (boids.Length - 1);

        return (perceivedVelocity - b.velocity) / 100;
    }

    Vector3 followLeader(Boid b)
    {
        return (leaderTarget.position - b.position) *0.2f;
    }

    void LimitVelocity(Boid b)
    {
        if (Vector3.Magnitude(b.velocity) > speedLimit )
           b.velocity = (b.velocity / Vector3.Magnitude(b.velocity)) * speedLimit;
    }

    void UpdateBoidsTransform()
    {
        for(int i=0;i< boidsTransform.Length;i++)
        {
            boidsTransform[i].position = boids[i].position;
            boidsTransform[i].LookAt(boidsTransform[0]);

        }
    }
}
