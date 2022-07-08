using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChain : MonoBehaviour
{
    public List<FoodChain> FoodChainList;
    public float rotateForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Rotate();
        }
    }
    void Rotate()
    {
        if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            foreach(FoodChain foodChain in FoodChainList)
            {
                if (hit.transform.gameObject == gameObject)
                {
                    foodChain.transform.rotation *= Quaternion.Euler(0, rotateForce, 0);
                    print("rot");
                }
                
            }
        }
          
    }
}
