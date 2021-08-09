# Unity Input System Camera Free Look
A script that can be attached to a **[Cinemachine Virtual Camera]** or the **[Main Camera]** to rotate and move it in game mode (and build) using the Unity Input System.

**How it works:**

**(1)** If you are NOT using Cinemachine: Change "CinemachineVirtualCamera" to "Camera" (line 7).  
**(2)** Add the FreeLook component to your MainCamera or Cinemachine Virtual Camera  
**(3)** Assign the InputActionAsset to be used to the "Actions" field in the inspector  
**(4)** Check that your InputActionAsset has the following actions (you can use different names)  
  
**Up:** Button  
**Down:** Button  
**Move:** Vector2  
**MouseLook:** Vector2  
**GamepadLook:** Vector2
  
**(5)** Check that the names of the individual actions match the fields CameraUpActionName, CameraDownActionName, CameraMoveActionName, CameraMouseLookActionName and CameraGamepadLookActionName  
**(6)** Adjust movement and rotation speed  
**(7)** Have fun with the script
