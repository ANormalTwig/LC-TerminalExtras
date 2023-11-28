using UnityEngine;

using LethalAPI.TerminalCommands.Attributes;
using LethalAPI.TerminalCommands.Models;

namespace TerminalExtras;

public class ExtraCommands {
	[TerminalCommand("Door", clearText: false), CommandInfo("Toggles the door.")]
	public string DoorCommand() {
		InteractTrigger trigger = GameObject.Find(StartOfRound.Instance.hangarDoorsClosed ? "StartButton" : "StopButton").GetComponentInChildren<InteractTrigger>();
		trigger.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);

		return "Toggled door.";
	}

	[TerminalCommand("Teleport", clearText: false), CommandInfo("Activate the teleporter.")]
	public string TeleportCommand() {
		GameObject teleporterObject = GameObject.Find("Teleporter(Clone)");
		if (teleporterObject is null) return "You don't have a teleporter!";

		ShipTeleporter teleporter = teleporterObject.GetComponent<ShipTeleporter>();
		if (teleporter is null) return "!! Can't find ShipTeleporter component !!";

		if (!teleporter.buttonTrigger.interactable) return "Teleporter is on cooldown!";

		teleporter.buttonTrigger.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);
		return "Teleporting player.";
	}

	[TerminalCommand("Lights", clearText: false), CommandInfo("Toggle the lights.")]
	public string LightsCommand() {
		InteractTrigger trigger = GameObject.Find("LightSwitch").GetComponent<InteractTrigger>();;
		trigger.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);

		return "Toggled lights.";
	}
}

