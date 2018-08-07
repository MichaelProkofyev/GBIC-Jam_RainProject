using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState
{
    NORMAL,
    HOLDING
}

public class PlayerController : MonoBehaviour {

    [SerializeField] private ParticleSystem clickParticlesPrefab;
    private ParticleSystem clickParticles;
    private int groundLayerMask;

    private CursorState cursorState;
    private IPickable pickedObject;

	// Use this for initialization
	void Start () {
        groundLayerMask = LayerMask.GetMask("Ground");
        clickParticles = Instantiate(clickParticlesPrefab, transform);
	}
	
	// Update is called once per frame
	void Update () {

        switch (cursorState)
        {
            case CursorState.NORMAL:
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
                    {
                        TryPickUp(hit);
                    }
                }
                break;
            case CursorState.HOLDING:
                if (pickedObject != null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, groundLayerMask))
                    {
                        if (hit.collider != null) //Could it be null ?
                        {
                            Vector3 newPosition = hit.point + Vector3.up;
                            pickedObject.SetPosition(newPosition);
                        }
                    }
                }
                break;
        }
    }

    void TryUseAtPoint(RaycastHit hit)
    {
        
    }

    void TryPickUp(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                clickParticles.transform.position = hit.point;
                clickParticles.Emit(10);
                //print(hit.point);
                return;
            }
            
            var pickableObject = hit.collider.GetComponent<IPickable>();
            if (pickableObject != null)
            {
                pickableObject.OnPicked();
                pickedObject = pickableObject;
                print("icked!");
                cursorState = CursorState.HOLDING;
            }
        }
    }
}
