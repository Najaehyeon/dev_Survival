using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera officeRoom;
    public CinemachineVirtualCamera serverRoom;
    public CinemachineVirtualCamera restRoom;

    private void Start()
    {
        OfficeRoomCamera();
    }

    public void OfficeRoomCamera()
    {
        officeRoom.MoveToTopOfPrioritySubqueue();
    }

    public void ServerRoomCamera()
    {
        serverRoom.MoveToTopOfPrioritySubqueue();
    }

    public void RestRoomCamera()
    {
        restRoom.MoveToTopOfPrioritySubqueue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "OfficeRoom")
        {
            OfficeRoomCamera();
        }
        else if (collision.gameObject.name == "ServerRoom")
        {
            ServerRoomCamera();
        }
        else if (collision.gameObject.name == "RestRoom")
        {
            RestRoomCamera();
        }
    }

}
