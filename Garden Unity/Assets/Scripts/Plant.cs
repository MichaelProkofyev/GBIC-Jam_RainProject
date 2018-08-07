using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IPickable {
    
    public void OnPicked()
    {
        
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
