using UnityEngine;
using System.Collections;

public class Boid {

    public  Vector3 position;
    public Vector3 velocity;

    public Boid()
    {
        this.position = new Vector3();
        this.velocity = new Vector3();
        
    }
    ~Boid() { }

}
