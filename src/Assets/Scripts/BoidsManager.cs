using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class BoidsManager : MonoBehaviour {

    [SerializeField]
    int boidsNumber;
    [SerializeField][Range(0,20)]
    float distanceTreshold = 5;
    [SerializeField][Range(0, 5)]
    float speedLimit;
    [SerializeField]
    Transform leaderTarget;
    [SerializeField]
    Vector3 windVector;
    [SerializeField]
    Transform targetPlace;

    [SerializeField]
    Transform mainCameraTransform;

    [SerializeField]
    PositionBound bounds;

    [SerializeField]
    bool followingLeader;

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

        mainCameraTransform.LookAt(BoidsCenter());

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
            boids[i].velocity = boids[i].velocity + MoveToCenter(boids[i]) + GetCloser(boids[i]) + MatchVelocities(boids[i]) + wind() + BoundPosition(boids[i]);
            if (followingLeader)
            {
                boids[i].velocity += tendToPlace(boids[i]) + followLeader(boids[i]);
            }
                
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
        //Cohésion
        var perceivedCenter = new Vector3(0,0,0); ;

        for(int i=0;i<boidsNumber;i++)
        {
            if(b.id != boids[i].id)
                perceivedCenter += boids[i].position;
        }

        perceivedCenter /= (boids.Length - 1);

        return (perceivedCenter - b.position)/100;
    }

    Vector3 BoidsCenter()
    {
        var perceivedCenter = new Vector3(0,0,0); ;

        for (int i = 0; i < boids.Length; i++)
        {
            perceivedCenter.x += boids[i].position.x;
            perceivedCenter.y += boids[i].position.y;
            perceivedCenter.z += boids[i].position.z;
        }

        perceivedCenter /= (boids.Length);

        return perceivedCenter;
    }

    Vector3 GetCloser(Boid b)
    {
        //Séparation
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
        //Alignement
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
        for (int i = 0; i < boidsTransform.Length; i++)
        {
            boidsTransform[i].position = boids[i].position;
            if(followingLeader)
            {
                boidsTransform[i].LookAt(leaderTarget.position);
            }
            else
            {
                boidsTransform[i].forward = Vector3.Lerp(boidsTransform[i].forward, boids[i].velocity.normalized, 5*Time.deltaTime);
            }
            

        }

    }

    Vector3 wind()
    {
        return windVector;
    }

    Vector3 tendToPlace (Boid b)
    {
        return (targetPlace.position-b.position) / 100;
    }

    Vector3 fleeFromPredator (Boid b)
    {
        return new Vector3();
    }

    Vector3 BoundPosition(Boid b)
    {
        Vector3 v = new Vector3();
        if (b.position.x > bounds.xMax)
        {
            v.x = -10;
        }
        else if(b.position.x < bounds.xMin)
        {
            v.x = 10;
        }
        else if(b.position.y > bounds.yMax)
        {
            v.y = -10;
        }
        else if(b.position.y < bounds.yMin)
        {
            v.y = 10;
        }
        else if(b.position.z > bounds.zMax)
        {
            v.z = -10;
        }
        else if(b.position.z < bounds.zMin)
        {
            v.z = 10;
        }
        return v;

    }
}
