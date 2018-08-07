using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable {
    void OnPicked();
    void SetPosition(Vector3 position);
}
