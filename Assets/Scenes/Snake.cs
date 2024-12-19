using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;

    private void Start()
    {
#pragma warning disable IDE0028 // Simplify collection initialization
        segments = new List<Transform>();
#pragma warning restore IDE0028 // Simplify collection initialization
        segments.Add(this.transform);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
     
        }
        else if(Input.GetKey(KeyCode.A)){
            direction = Vector2.left;
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            
        }
    }

    private void FixedUpdate() //run in specific time, related to physics 
    {
        for(int i = segments.Count-1; i>0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    private void ResetState()
    {
      for(int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food"){
            Grow();
        }
        else if (other.CompareTag("Finish")){
            ResetState();
            
        }
    }
}
