﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Zarządza kontrolą nad postacią gracza, w tym ruchem, skakaniem, interakcją z otoczeniem i animacjami.
/// </summary>
/// <remarks>
/// Klasa PlayerController umożliwia kontrolowanie postaci gracza w środowisku 3D. Obejmuje obsługę
/// ruchu na ziemi i w powietrzu, skakania, wspinaczki.
/// </remarks>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera; ///< Główna kamera śledząca gracza.

    [SerializeField]
    private Animator animatorPlayer; ///< Animator zarządzający animacjami postaci gracza.

    [SerializeField]
    private GameObject ragdollPlayer; ///< Obiekt ragdoll reprezentujący postać gracza.

    [SerializeField]
    private ConfigurableJoint[] ragdollParts; ///< Części ciała ragdoll gracza.

    [SerializeField]
    public HandController HandControllerRight, HandControllerLeft; ///< Kontrolery dla prawej i lewej ręki gracza.

    [SerializeField]
    public bool useControls = true; ///< Określa, czy kontroler gracza jest aktywny.

    [SerializeField]
    public string ragdollLayer = "Player", forwardBackAxis = "Vertical", leftRightAxis = "Horizontal", jump = "Jump", reachLeft = "Fire1", reachRight = "Fire2"; ///< Parametry kontrolera i akcji.

    [SerializeField]
    private float moveSpeed = 6f, turnSpeed = 6f, jumpForce = 6f, HeightCheck = 1.1f; ///< Parametry ruchu gracza.

    [SerializeField]
    private LayerMask ignoreGroundCheckOn; ///< Warstwy ignorowane przy sprawdzaniu kolizji z ziemią.


    private float timer, cachedMoveSpeed, mouseYaxis = 0.5f;
    private bool jumping, isJumping, jumpAxisUsed, inAir, climbing, ragdollMode, cooledDown = true;
    private Vector3 Direction, climbToPositionForward, climbToPositionUp;
    private Vector2 InputDirection;
    private int animLayer = 0;
    
    private Rigidbody physicsBody;
    private ConfigurableJoint physicsJoint;
    
    private JointDrive DriveOff, DriveLow, DriveMedium, DriveHigh, DriveOnController, DriveOnRagdoll;

    /// <summary>
    /// Inicjalizuje zmienne i ustawienia początkowe dla postaci gracza.
    /// </summary>
    void Awake()
    {
        cachedMoveSpeed = moveSpeed;
        
        physicsBody = ragdollPlayer.GetComponent<Rigidbody>();
        physicsJoint = ragdollPlayer.GetComponent<ConfigurableJoint>();
        
        DriveOff = new JointDrive();
        DriveOff.positionSpring = 150;
        DriveOff.positionDamper = 500;
        DriveOff.maximumForce = Mathf.Infinity;
        
        DriveLow = new JointDrive();
        DriveLow.positionSpring = 500;
        DriveLow.positionDamper = 600;
        DriveLow.maximumForce = Mathf.Infinity;
        
        DriveMedium = new JointDrive();
        DriveMedium.positionSpring = 2000;
        DriveMedium.positionDamper = 600;
        DriveMedium.maximumForce = Mathf.Infinity;
        
        DriveHigh = new JointDrive();
        DriveHigh.positionSpring = 12000;
        DriveHigh.positionDamper = 1000;
        DriveHigh.maximumForce = Mathf.Infinity;
        
        DriveOnController = new JointDrive();
        DriveOnController.positionSpring = 15000;
        DriveOnController.positionDamper = 2000;
        DriveOnController.maximumForce = Mathf.Infinity;
        
        DriveOnRagdoll = new JointDrive();
        DriveOnRagdoll.positionSpring = 20000;
        DriveOnRagdoll.positionDamper = 600;
        DriveOnRagdoll.maximumForce = Mathf.Infinity;
        
        foreach (ConfigurableJoint part in ragdollParts)
        {
           part.slerpDrive = DriveOnRagdoll;  
        }

    }

    /// <summary>
    /// Wykonuje aktualizacje co klatkę, zarządzając ruchem gracza, skakaniem i animacjami.
    /// </summary>
    void Update()
    {
        PlayerGrounded();
        
        if(useControls)
        {
            if(!ragdollMode)
            {
                PlayerInput();
                PlayerSpeed();
                PlayerRotation();
                PlayerJump();
            }
            
            if(!inAir)
            {
                PlayerMovement();
            }
            
            else if(inAir)
            {
                PlayerMovementAir();
            }
            
        }
        
        if(ragdollPlayer.activeSelf)
        {
            PlayerAnimations();
        }
    }

    /// <summary>
    /// Wykonuje aktualizacje co klatkę fizyki, zarządzając ruchem gracza podczas wspinaczki.
    /// </summary>
    void FixedUpdate()
    {
        if(useControls)
        {
            PlayerJumping();
        }
        
        PlayerClimbMovement();
    }



    //-----------------------------------------------------------------------------------------------



    /// <summary>
    /// Sprawdza, czy gracz jest na ziemi i zarządza przełączaniem między trybami ragdoll i kontrolowanym ruchem.
    /// </summary>
    void PlayerGrounded()
    {
        Ray ray = new Ray (ragdollPlayer.transform.position, -Vector3.up);
		RaycastHit hit;
		
		//Balance when ground is detected
        if (Physics.Raycast(ray, out hit, HeightCheck, ~ignoreGroundCheckOn) && inAir && !ragdollMode)
        {
            inAir = false;
            physicsJoint.slerpDrive = DriveOnController;
            
            ragdollParts[1].slerpDrive = DriveOnRagdoll;
            ragdollParts[2].slerpDrive = DriveOnRagdoll;
            ragdollParts[3].slerpDrive = DriveOnRagdoll;
            ragdollParts[4].slerpDrive = DriveOnRagdoll;
            ragdollParts[5].slerpDrive = DriveOnRagdoll;
            ragdollParts[6].slerpDrive = DriveOnRagdoll;
		}
		
		//Fall when ground is not detected
		else if(!Physics.Raycast(ray, out hit, HeightCheck, ~ignoreGroundCheckOn) && !inAir && !ragdollMode && physicsBody.velocity.y > -10)
		{
            inAir = true;
            physicsJoint.slerpDrive = DriveLow;
            
            ragdollParts[1].slerpDrive = DriveOff;
            ragdollParts[2].slerpDrive = DriveOff;
            ragdollParts[3].slerpDrive = DriveOff;
            ragdollParts[4].slerpDrive = DriveOff;
            ragdollParts[5].slerpDrive = DriveOff;
            ragdollParts[6].slerpDrive = DriveOff;
		}
        
        //Switch to ragdoll mode when falling fast
        else if(!Physics.Raycast(ray, out hit, HeightCheck, ~ignoreGroundCheckOn) && inAir && !ragdollMode && physicsBody.velocity.y < -10)
		{
            ragdollMode = true;
            cooledDown = false;
            
            physicsJoint.slerpDrive = DriveOff;
            
            ActivateRagdoll();
		}
    }



    /// <summary>
    /// Pomaga graczowi wstawać z trybu ragdoll.
    /// </summary>
    public void PlayerGetUp()
    {
        //Slowly transition from ragdoll to active ragdoll
        if(ragdollMode && !cooledDown)
        {
            cooledDown = true;
            StartCoroutine(waitCoroutine());
            
            IEnumerator waitCoroutine()
            {
                yield return new WaitForSeconds(2);
                
                foreach (ConfigurableJoint part in ragdollParts)
                {
                   part.slerpDrive = DriveMedium;  
                }
                
                ragdollParts[7].slerpDrive = DriveOff;
                ragdollParts[8].slerpDrive = DriveOff;
                ragdollParts[9].slerpDrive = DriveOff;
                ragdollParts[10].slerpDrive = DriveOff;
                ragdollParts[11].slerpDrive = DriveOff;
                ragdollParts[12].slerpDrive = DriveOff;
                ragdollParts[13].slerpDrive = DriveOff;
                ragdollParts[14].slerpDrive = DriveOff;
                
                yield return new WaitForSeconds(0.5f);
                
                foreach (ConfigurableJoint part in ragdollParts)
                {
                   part.slerpDrive = DriveHigh;  
                }
                
                ragdollParts[7].slerpDrive = DriveOff;
                ragdollParts[8].slerpDrive = DriveOff;
                ragdollParts[9].slerpDrive = DriveOff;
                ragdollParts[10].slerpDrive = DriveOff;
                ragdollParts[11].slerpDrive = DriveOff;
                ragdollParts[12].slerpDrive = DriveOff;
                ragdollParts[13].slerpDrive = DriveOff;
                ragdollParts[14].slerpDrive = DriveOff;
                
                yield return new WaitForSeconds(0.5f);
                
                physicsJoint.slerpDrive = DriveLow;
                
                yield return new WaitForSeconds(0.5f);
                
                physicsJoint.slerpDrive = DriveMedium;
                
                foreach (ConfigurableJoint part in ragdollParts)
                {
                   part.slerpDrive = DriveMedium;  
                }
                
                yield return new WaitForSeconds(0.5f);
                
                physicsJoint.slerpDrive = DriveHigh;
                
                foreach (ConfigurableJoint part in ragdollParts)
                {
                   part.slerpDrive = DriveHigh;  
                }
                
                yield return new WaitForSeconds(0.5f);
                
                ragdollMode = false;
                DeactivateRagdoll();
            }
        }
    }



    /// <summary>
    /// Przetwarza wejścia od gracza, takie jak ruch czy akcje.
    /// </summary>
    void PlayerInput()
    {
        if(Input.GetAxisRaw(forwardBackAxis) > 0)
        {
            InputDirection = new Vector2(InputDirection.x, 1);
            
            if(animatorPlayer.GetFloat("BlendVertical") < 1)
            {
                var vert = animatorPlayer.GetFloat("BlendVertical");
                animatorPlayer.SetFloat("BlendVertical", vert += 0.05f);
            }
        }
        
        else if(Input.GetAxisRaw(forwardBackAxis) < 0)
        {
            InputDirection = new Vector2(InputDirection.x, -1);
            
            if(animatorPlayer.GetFloat("BlendVertical") > -1)
            {
                var vert = animatorPlayer.GetFloat("BlendVertical");
                animatorPlayer.SetFloat("BlendVertical", vert -= 0.05f);
            }
        }
        
        else if(Input.GetAxisRaw(forwardBackAxis) == 0)
        {
            InputDirection = new Vector2(InputDirection.x, 0);
            
            if(animatorPlayer.GetFloat("BlendVertical") != 0)
            {
                animatorPlayer.SetFloat("BlendVertical", 0f);
            }
        }
        
        if(Input.GetAxisRaw(leftRightAxis) > 0)
        {
            InputDirection = new Vector2(1, InputDirection.y);
            
            if(animatorPlayer.GetFloat("BlendHorizontal") < 1)
            {
                var horiz = animatorPlayer.GetFloat("BlendHorizontal");
                animatorPlayer.SetFloat("BlendHorizontal", horiz += 0.05f);
            }
        }
        
        else if(Input.GetAxisRaw(leftRightAxis) < 0)
        {
            InputDirection = new Vector2(-1, InputDirection.y);
            
            if(animatorPlayer.GetFloat("BlendHorizontal") > -1)
            {
                var horiz = animatorPlayer.GetFloat("BlendHorizontal");
                animatorPlayer.SetFloat("BlendHorizontal", horiz -= 0.05f);
            }
        }
        
        else if(Input.GetAxisRaw(leftRightAxis) == 0)
        {
            InputDirection = new Vector2(0, InputDirection.y);
            
            if(animatorPlayer.GetFloat("BlendHorizontal") != 0)
            {
                animatorPlayer.SetFloat("BlendHorizontal", 0f);
            }
        }
    }



    /// <summary>
    /// Zarządza ruchem postaci gracza na ziemi.
    /// </summary>
    void PlayerMovement()
    {
        Direction = ragdollPlayer.transform.rotation * new Vector3(Input.GetAxisRaw(leftRightAxis), 0.0f, Input.GetAxisRaw(forwardBackAxis));
        Direction.y = 0f;
        physicsBody.velocity = Vector3.Lerp(physicsBody.velocity, (Direction * moveSpeed) + new Vector3(0, physicsBody.velocity.y, 0), 0.8f);
    }

    /// <summary>
    /// Zarządza ruchem postaci gracza w powietrzu.
    /// </summary>
    void PlayerMovementAir()
    {
        Direction = ragdollPlayer.transform.rotation * new Vector3(Input.GetAxisRaw(leftRightAxis), 0.0f, Input.GetAxisRaw(forwardBackAxis));
        Direction.y = 0f;
        physicsBody.AddForce(Direction * (moveSpeed * 50));
    }

    /// <summary>
    /// Zarządza ruchem gracza podczas wspinaczki.
    /// </summary>
    void PlayerClimbMovement()
    {
        
        Ray ray = new Ray (ragdollParts[11].transform.position, ragdollParts[11].transform.forward);
		RaycastHit hit;
        
        Debug.DrawRay(ragdollParts[11].transform.position, Vector3.forward, Color.red);
        
        if (HandControllerRight.GrabbedObject != null && HandControllerRight.GrabbedObject.isKinematic && HandControllerLeft.GrabbedObject != null && HandControllerLeft.GrabbedObject.isKinematic && !Physics.Raycast(ray, out hit, 1, ~ignoreGroundCheckOn) && !ragdollPlayer.GetComponent<Rigidbody>().isKinematic && !climbing && jumping)
        {
        
            climbing = true;
            ragdollPlayer.GetComponent<Rigidbody>().isKinematic = true;
            useControls = false;
            
            foreach (ConfigurableJoint part in ragdollParts)
            {
               part.gameObject.GetComponent<Collider>().enabled = false;  
            }
            
            climbToPositionUp = ragdollParts[11].transform.position;
            climbToPositionForward = ragdollParts[11].transform.position + ragdollParts[11].transform.forward * 1;

            //Left Hand
            if(HandControllerLeft != null)
            {   
                if(HandControllerLeft.hasJoint)
                {
                    HandControllerLeft.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                    HandControllerLeft.hasJoint = false;
                    HandControllerLeft.GrabbedObject = null;
                }

                if(HandControllerLeft.hasJoint && HandControllerLeft.gameObject.GetComponent<FixedJoint>() == null)
                {
                    HandControllerLeft.hasJoint = false;
                    HandControllerLeft.GrabbedObject = null;
                }
            }

            //Right Hand
            if(HandControllerRight != null)
            {
                if(HandControllerRight.hasJoint)
                {
                    HandControllerRight.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                    HandControllerRight.hasJoint = false;
                    HandControllerRight.GrabbedObject = null;
                }

                if(HandControllerRight.hasJoint && HandControllerRight.gameObject.GetComponent<FixedJoint>() == null)
                {
                    HandControllerRight.hasJoint = false;
                    HandControllerRight.GrabbedObject = null;
                }
            }
		}
        
        else if(Vector3.Distance(ragdollPlayer.transform.position, climbToPositionUp) > 0.1f && ragdollPlayer.GetComponent<Rigidbody>().isKinematic && climbing)
        {
            ragdollPlayer.transform.position = Vector3.MoveTowards(ragdollPlayer.transform.position, climbToPositionUp, 4 * Time.deltaTime);
        }
        
        else if(Vector3.Distance(ragdollPlayer.transform.position, climbToPositionUp) <= 0.1f && ragdollPlayer.GetComponent<Rigidbody>().isKinematic && climbing)
        {
            climbing = false;
        }
        
        else if(Vector3.Distance(ragdollPlayer.transform.position, climbToPositionForward) > 0.1f && ragdollPlayer.GetComponent<Rigidbody>().isKinematic && !climbing)
        {
            ragdollPlayer.transform.position = Vector3.MoveTowards(ragdollPlayer.transform.position, climbToPositionForward, 4 * Time.deltaTime);
        }
        
        else if(Vector3.Distance(ragdollPlayer.transform.position, climbToPositionForward) <= 0.1f && ragdollPlayer.GetComponent<Rigidbody>().isKinematic && !climbing)
        {
            ragdollPlayer.GetComponent<Rigidbody>().isKinematic = false;
            jumping = false;
            useControls = true;
            
            foreach (ConfigurableJoint part in ragdollParts)
            {
               part.gameObject.GetComponent<Collider>().enabled = true;  
            }
        }
    }

    /// <summary>
    /// Dostosowuje prędkość ruchu postaci w zależności od kierunku ruchu.
    /// </summary>
    void PlayerSpeed()
    {
        //walk forward speed
        if(InputDirection == new Vector2(0,1))
        {
            moveSpeed = cachedMoveSpeed;
        }
        
        //walk backward speed
        else if(InputDirection == new Vector2(0,-1))
        {
            moveSpeed = cachedMoveSpeed / 1.5f;
        }
        
        //walk diagonal speed
        else if(InputDirection != new Vector2(0,1) && InputDirection != new Vector2(0,-1))
        {
            moveSpeed = cachedMoveSpeed / 1.7f;
        }
    }



    /// <summary>
    /// Zarządza obrotem postaci gracza, aby odpowiadał kierunkowi ruchu.
    /// </summary>
    void PlayerRotation()
    {
        var lookPos = mainCamera.transform.forward;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        physicsJoint.targetRotation = Quaternion.Slerp(physicsJoint.targetRotation, Quaternion.Inverse(rotation), Time.deltaTime * turnSpeed);
    }



    /// <summary>
    /// Zarządza skakaniem postaci gracza.
    /// </summary>
    void PlayerJump()
    {
        if(Input.GetAxis(jump) > 0)
        {
            if(!jumpAxisUsed)
            {
                if(!inAir)
                {
                    jumping = true;
                    physicsJoint.slerpDrive = DriveLow;
                }
                
                else if(inAir)
                {
                    if(HandControllerRight.GrabbedObject != null && HandControllerRight.GrabbedObject.isKinematic)
                    {
                        jumping = true;
                        physicsJoint.slerpDrive = DriveLow;
                    }
                    
                    else if(HandControllerLeft.GrabbedObject != null && HandControllerLeft.GrabbedObject.isKinematic)
                    {
                        jumping = true;
                        physicsJoint.slerpDrive = DriveLow;
                    }
                }
            }

            jumpAxisUsed = true;
        }
        
        else
        {
            jumpAxisUsed = false;
        }
    }



    /// <summary>
    /// Zarządza logiką skakania postaci w kontekście fizyki.
    /// </summary>
    void PlayerJumping()
    {
        if(jumping && !inAir)
        {
            isJumping = true;
                
            var v3 = physicsBody.transform.up * jumpForce;
            v3.x = physicsBody.velocity.x;
            v3.z = physicsBody.velocity.z;
            physicsBody.velocity = v3;
        }
        
        else if(jumping && inAir)
        {
            isJumping = true;
                
            var v3up = physicsBody.transform.up * jumpForce;
            v3up.x = physicsBody.velocity.x;
            v3up.z = physicsBody.velocity.z;
            physicsBody.velocity = v3up;
            
            var v3forward = mainCamera.transform.forward * jumpForce;
            physicsBody.AddForce(v3forward * jumpForce * 200);
        }

		if (isJumping)
		{
			timer = timer + Time.fixedDeltaTime;
				
			if (timer > 0.2f)
			{
				timer = 0.0f;
				jumping = false;
				isJumping = false;
                inAir = true;
			}
		}
    }



    /// <summary>
    /// Zarządza animacjami postaci gracza w zależności od jej ruchów i akcji.
    /// </summary>
    void PlayerAnimations()
    {   
        if(useControls && !ragdollMode)
        {
            //mouse Y axis recording
            if(Input.GetAxisRaw("Mouse Y") > 0)
            {
                if(mouseYaxis <= 1)
                {
                    mouseYaxis = mouseYaxis += (Input.GetAxisRaw("Mouse Y") / 14);
                }

                if(mouseYaxis > 1)
                {
                    mouseYaxis = 1f;
                }
            }

            else if(Input.GetAxisRaw("Mouse Y") < 0)
            {
                if(mouseYaxis >= -1)
                {
                    mouseYaxis = mouseYaxis += (Input.GetAxisRaw("Mouse Y") / 14);
                }

                if(mouseYaxis < -1)
                {
                    mouseYaxis = -1f;
                }
            }


            //Look Height
            animatorPlayer.SetFloat("BlendLook", mouseYaxis);


            //Reach Left
            if(Input.GetAxisRaw("Fire1") != 0)
            {
                if(animatorPlayer.GetLayerWeight(3) != 1)
                {
                    animatorPlayer.SetLayerWeight(3, 1f);
                    animatorPlayer.Play("ReachLeftArm", -1, 0f);
                }

                animatorPlayer.SetFloat("BlendReachLeft", mouseYaxis);
            }

            if(Input.GetAxisRaw("Fire1") == 0)
            {
                if(animatorPlayer.GetLayerWeight(3) > 0)
                {
                    animatorPlayer.SetLayerWeight(3, 0f);
                    animatorPlayer.SetFloat("BlendReachLeft", 0.5f);
                }
            }


            //Reach Right
            if(Input.GetAxisRaw("Fire2") != 0)
            {
                if(animatorPlayer.GetLayerWeight(2) != 1)
                {
                    animatorPlayer.SetLayerWeight(2, 1f);
                    animatorPlayer.Play("ReachRightArm", -1, 0f);
                }

                animatorPlayer.SetFloat("BlendReachRight", mouseYaxis);
            }

            if(Input.GetAxisRaw("Fire2") == 0)
            {
                if(animatorPlayer.GetLayerWeight(2) > 0)
                {
                    animatorPlayer.SetLayerWeight(2, 0f);
                    animatorPlayer.SetFloat("BlendReachRight", 0.5f);
                }
            }


            //Move Direction
            if(!isPlaying(animatorPlayer, "DirectionMotion") && !jumping && !inAir && !ragdollMode)
            {
                animatorPlayer.Play("DirectionMotion");
            }

            //In Air
            if(!jumping && !isPlaying(animatorPlayer, "Jump") && !isPlaying(animatorPlayer, "Air") && inAir && !ragdollMode && !ragdollPlayer.GetComponent<Rigidbody>().isKinematic)
            {
                animatorPlayer.Play("Air");
            }
            
            //Jump
            if(jumping && !isPlaying(animatorPlayer, "Jump") && !isPlaying(animatorPlayer, "Air") && !inAir && !ragdollMode)
            {
                animatorPlayer.Play("Jump");
            }
        }
        
        //Getup
        if(!jumping && !isPlaying(animatorPlayer, "Air") && !isPlaying(animatorPlayer, "Getup") && !inAir && ragdollMode && !cooledDown)
        {
            animatorPlayer.Play("Getup");
        }
        
        //Climb
        else if(!isPlaying(animatorPlayer, "Climb") && ragdollPlayer.GetComponent<Rigidbody>().isKinematic && !ragdollMode)
        {
            animatorPlayer.Play("Climb");
        }
    }

    /// <summary>
    /// Sprawdza, czy określona animacja jest obecnie odtwarzana.
    /// </summary>
    /// <param name="anim">Animator, który jest sprawdzany.</param>
    /// <param name="stateName">Nazwa stanu animacji.</param>
    /// <returns>Prawda, jeśli animacja jest odtwarzana.</returns>
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) && anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
        {
            return true;
        }
        
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Aktywuje tryb ragdoll dla postaci.
    /// </summary>
    public void KnockDown()
    {
        physicsJoint.slerpDrive = DriveOff;
        ActivateRagdoll();
        
        ragdollMode = true;
        inAir = true;
        cooledDown = false;
    }

    /// <summary>
    /// Aktywuje tryb ragdoll dla postaci.
    /// </summary>
    void ActivateRagdoll()
    {
        foreach (ConfigurableJoint part in ragdollParts)
        {
           part.slerpDrive = DriveOff;  
        }
    }

    /// <summary>
    /// Dezaktywuje tryb ragdoll dla postaci.
    /// </summary>
    void DeactivateRagdoll()
    {
        foreach (ConfigurableJoint part in ragdollParts)
        {
           part.slerpDrive = DriveOnRagdoll;  
        }
    }
}
