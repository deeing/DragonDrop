using System.Collections;using System.Collections.Generic;using UnityEngine;public class NestBottom : MonoBehaviour{    //attribute    [SerializeField]    private MovingPlatform movingNestBottom;    private void StopMovingPlatform()
    {
        //some function to stop the nest moving platform
        //disable moving platform function
        movingNestBottom.StopPlatform();    }    private void OnTriggerEnter2D(Collider2D collision)    {        Egg egg = collision.gameObject.GetComponent<Egg>();        if (egg != null)        {            egg.Win();

            //Call StopMovingPlatform() function
            StopMovingPlatform();        }    }}