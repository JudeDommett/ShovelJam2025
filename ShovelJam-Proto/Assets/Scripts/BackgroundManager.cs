using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveBackground(float distance)
    {
        foreach (var background in backgrounds)
        {
            background.position += Vector3.up * distance;
        }

    }
}
