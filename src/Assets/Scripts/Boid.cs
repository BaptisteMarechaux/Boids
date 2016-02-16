using UnityEngine;
using System.Collections;

public class Boid {

    public  Vector3 position;
    public Vector3 velocity;
    public int id;

    public Boid(int id_, Vector3 pos_)
    {
        this.id = id_;
        this.position = pos_;
        this.velocity = new Vector3();
        
    }
    ~Boid() { }

}
