using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Entrada del jugador
    public float steerInput, accelInput;
    public float steerAngle, brakeForceValue;
    public bool isBraking;

    // Configuración del coche
    [SerializeField] private float enginePower, brakingPower, maxTurnAngle;
    [SerializeField] private float topSpeed = 75f;

    // Evento que notifica cuando el coche excede la velocidad límite
    public static event Action OnExceedSpeedLimit;

    // Ruedas (Colliders)
    [SerializeField] private WheelCollider frontLeftCollider, frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider, rearRightCollider;
    [SerializeField] private Transform frontLeftMesh;
    [SerializeField] private Transform frontRightMesh;
    [SerializeField] private Transform rearLeftMesh;
    [SerializeField] private Transform rearRightMesh;


    // Centro de masa
    [SerializeField] private Vector3 massCenterOffset;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass += massCenterOffset;
    }

    private void FixedUpdate()
    {
        ProcessInput();
        ControlAcceleration();
        ControlSteering();
        UpdateWheelMeshes();

        CheckSpeedLimit();
    }

    // Procesar Entrada del Jugador
    private void ProcessInput()
    {
        steerInput = Input.GetAxis("Horizontal");
        accelInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    // Controlar Aceleración y Frenado
    private void ControlAcceleration()
    {
        if (rb.velocity.magnitude < topSpeed)
        {
            frontLeftCollider.motorTorque = accelInput * enginePower;
            frontRightCollider.motorTorque = accelInput * enginePower;
        }
        else
        {
            frontLeftCollider.motorTorque = 0f;
            frontRightCollider.motorTorque = 0f;
        }

        brakeForceValue = isBraking ? brakingPower : 0f;
        ApplyBrakes();
    }

    // Aplicar Frenado
    private void ApplyBrakes()
    {
        frontRightCollider.brakeTorque = brakeForceValue;
        frontLeftCollider.brakeTorque = brakeForceValue;
        rearLeftCollider.brakeTorque = brakeForceValue;
        rearRightCollider.brakeTorque = brakeForceValue;
    }

    // Controlar Dirección
    private void ControlSteering()
    {
        steerAngle = maxTurnAngle * steerInput;
        frontLeftCollider.steerAngle = steerAngle;
        frontRightCollider.steerAngle = steerAngle;
    }

    // Comprobar si se excede el límite de velocidad
    private void CheckSpeedLimit()
    {
        float speedKmh = rb.velocity.magnitude * 3.6f; // Convierte m/s a km/h
        if (speedKmh > 40f)
        {
            OnExceedSpeedLimit?.Invoke(); // Dispara el evento
        }
    }

    // Actualizar las Mallas de las Ruedas
    private void UpdateWheelMeshes()
    {
        UpdateWheelPose(frontLeftCollider, frontLeftMesh);
        UpdateWheelPose(frontRightCollider, frontRightMesh);
        UpdateWheelPose(rearRightCollider, rearRightMesh);
        UpdateWheelPose(rearLeftCollider, rearLeftMesh);
    }

    private void UpdateWheelPose(WheelCollider wheelCol, Transform wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCol.GetWorldPose(out pos, out rot);
        wheelMesh.rotation = rot;
        wheelMesh.position = pos;
    }
}
