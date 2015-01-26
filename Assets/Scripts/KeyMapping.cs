using System;
using System.Collections;
using UnityEngine;
using InControl;


// This custom profile is enabled by adding it to the Custom Profiles list
// on the InControlManager component, or you can attach it yourself like so:
// InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
// 
public class KeyMapping : UnityInputDeviceProfile
{
	public KeyMapping()
	{
		Name = "KeyMapping";
		Meta = "A keyboard and mouse combination profile for mini golf.";
		
		// This profile only works on desktops.
		SupportedPlatforms = new[]
		{
			"Windows",
			"Mac",
			"Linux"
		};
		
		Sensitivity = 1.0f;
		LowerDeadZone = 0.0f;
		UpperDeadZone = 1.0f;
		
		ButtonMappings = new[]
		{
			new InputControlMapping
			{
				Handle = "Swing - Mouse",
				Target = InputControlType.Action1,
				Source = MouseButton0
			},
			new InputControlMapping
			{
				Handle = "Swing - Keyboard",
				Target = InputControlType.Action1,
				// KeyCodeButton fires when any of the provided KeyCode params are down.
				Source = KeyCodeButton( KeyCode.Space, KeyCode.Return )
			},
			new InputControlMapping
			{
				Handle = "Fly Up",
				Target = InputControlType.DPadUp,
				Source = KeyCodeButton ( KeyCode.E )
			},
			new InputControlMapping
			{
				Handle = "Fly Down",
				Target = InputControlType.DPadDown,
				Source = KeyCodeButton ( KeyCode.Q )
			},
			new InputControlMapping
			{
				Handle = "Rotate Club Right",
				Target = InputControlType.RightTrigger,
				Source = KeyCodeButton ( KeyCode.RightArrow )
			},
			new InputControlMapping
			{
				Handle = "Rotate Club Left",
				Target = InputControlType.LeftTrigger,
				Source = KeyCodeButton ( KeyCode.LeftArrow )
			},
			new InputControlMapping
			{
				Handle = "Turn Right",
				Target = InputControlType.RightBumper,
				Source = KeyCodeButton ( KeyCode.C )
			},
			new InputControlMapping
			{
				Handle = "Turn Left",
				Target = InputControlType.LeftBumper,
				Source = KeyCodeButton ( KeyCode.Z )
			},
			new InputControlMapping
			{
				Handle = "Face Target",
				Target = InputControlType.Action3,
				Source = MouseButton1
			},
			new InputControlMapping
			{
				Handle = "Reset Screen",
				Target = InputControlType.Action4,
				Source = KeyCodeButton ( KeyCode.R )
			},
			new InputControlMapping
			{
				Handle = "Options",
				Target = InputControlType.Start,
				// KeyCodeComboButton requires that all KeyCode params are down simultaneously.
				Source = KeyCodeButton( KeyCode.Tab )
			},
		};
		
		AnalogMappings = new[]
		{
			new InputControlMapping
			{
				Handle = "Move Camera X",
				Target = InputControlType.LeftStickX,
				// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
				Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
			},
			new InputControlMapping
			{
				Handle = "Move Camera Y",
				Target = InputControlType.LeftStickY,
				// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
				Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
			},
			new InputControlMapping
			{
				Handle = "Look X",
				Target = InputControlType.RightStickX,
				Source = MouseXAxis,
				Raw    = true,
				Scale  = 1.0f
			},
			new InputControlMapping
			{
				Handle = "Look Y",
				Target = InputControlType.RightStickY,
				Source = MouseYAxis,
				Raw    = true,
				Scale  = 1.0f
			},
		};
	}
}


