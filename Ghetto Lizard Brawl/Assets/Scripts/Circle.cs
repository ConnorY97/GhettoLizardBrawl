using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Lizard>().Knockout();
    }
}
