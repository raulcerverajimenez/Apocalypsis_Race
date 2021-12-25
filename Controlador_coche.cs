using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador_coche : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    
    private float steerAngle;
    private bool isBreaking;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 30f; // máximo ángulo de giro
    public float motorForce = 3000f;
    public float brakeForce = 0f;

    public float f_total;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // entradas 
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //giro
        verticalInput = Input.GetAxis("Vertical"); //avance
        isBreaking = Input.GetKey(KeyCode.Space); //freno
    }

    // manejo de volante
    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    //manejo motor
    private void HandleMotor()
    {
        //tración delantera, sólo empujan las ruedas de delante
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        
        f_total = verticalInput * motorForce;


        brakeForce = isBreaking ? 3000f : 0f;

        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }


    //actualización de las ruedas
    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    //actualización de la posición de las ruedas
    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

   

}