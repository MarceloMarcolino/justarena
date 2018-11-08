using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;
	private Animator animator;

	//[SerializedField]
	public float moveSpeed = 300;
	//[SerializedField]
	public float turnSpeed = 5f;

    public string PlayerNumber;

    private int animaatk;//animaçao dos botoes
    private float atfo=0;//combo forte
    private float atle =0;//combo leve
    private bool precionou=false;//se precionaou 
  
    private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator> ();
	}
    private void Update()
    {
        var horizontal = Input.GetAxis(("P" + PlayerNumber + "Horizontal"));
        var vertical = Input.GetAxis(("P" + PlayerNumber + "Vertical"));
        var movement = new Vector3(horizontal, 0, vertical);
        var Quad = Input.GetAxis(("P" + PlayerNumber + "Fire1"));
        var Xxis = Input.GetAxis(("P" + PlayerNumber + "Fire2"));
        var Bola = Input.GetAxis(("P" + PlayerNumber + "Fire3"));
        var Tria = Input.GetAxis(("P" + PlayerNumber + "Jump"));

        characterController.SimpleMove(movement * Time.deltaTime * moveSpeed);//Movimento
        animator.SetFloat("Speed", movement.magnitude);                  //
        //if (animaatk == 4) characterController.SimpleMove(movement * Time.deltaTime * moveSpeed);
        //   if (animaatk != 0) {
        //       animator.SetFloat("Speed", 0);
        //       characterController.SimpleMove(movement * Time.deltaTime * moveSpeed * 0);
        //   }

        if (movement.magnitude > 0)
        {       //Movimento
            Quaternion newDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
        }
        if (animaatk == 0) precionou = true; //Ver qual botao foi precionada
        if (Quad == 1)            animaatk = 1;//Atk leve
        else if (Tria == 1)       animaatk = 2;//Atk forte
        else if (Bola == 1)       animaatk = 3;//Quebra def
        else if (Xxis == 1)       animaatk = 4;//Dash
        else {precionou = false;  animaatk = 0;}//Nenhum
        
        if (precionou)        {//Acoes pra cada Botao
            precionou = false;
            switch (animaatk)
            {
                case 1:   // Atk leve
                    atle = atle + 1;
                    atfo = 0;
                    if (atle == 4) atle = 1; 
                    print("hi");
                    break;
                case 2:   //Atk forte
                    atle = 0;
                    atfo = atfo + 1;
                    if (atfo == 3)  atfo = 1; 
                    print("hello");
                    break;
                case 3:   //Quebra def
                    atle = 0;
                    atfo = 0;
                    print("world");
                    break;
                case 4:   //Dash
                    atle = 0;
                    atfo = 0;
                    print("!!!!");
                    break;
            }
        }
        
        animator.SetInteger("Attks", animaatk);//
        animator.SetFloat("AtLe", (atle));     //Tranfere para unity2
        animator.SetFloat("AtFo", (atfo));     //
    //    print(animaatk);
        
        


    }
}