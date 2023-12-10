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
		return "Teleporting player";
	}

	[TerminalCommand("Lights", clearText: false), CommandInfo("Toggle the lights.")]
	public string LightsCommand() {
		InteractTrigger trigger = GameObject.Find("LightSwitch").GetComponent<InteractTrigger>();
		trigger.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);

		return "Toggled lights";
	}
    [TerminalCommand("Doors", clearText: false), CommandInfo("Toggles the door.")]
    public string DoorsCommand()
    {
		return DoorCommand();
    }

    [TerminalCommand("Light", clearText: false), CommandInfo("Toggle the lights.")]
	public string LightCommand()
	{
		return LightsCommand();
	}

	[TerminalCommand("Tp", clearText: false), CommandInfo("Activate the teleporter.")]
	public string TeleportCommandShort()
	{
		return this.TeleportCommand();
    }

    [TerminalCommand("Launch", clearText: false), CommandInfo("Pull the lever, Kronk!")]
    public string LaunchCommand() {
		const string alreadyTransitMessage = "Unable to comply. The ship is already in transit.";
		GameObject leverObject = GameObject.Find("StartGameLever");
        if (leverObject is null) return "!! Can't find StartGameLever !!";
        StartMatchLever lever = leverObject.GetComponent<StartMatchLever>();
        if (lever is null) return "!! Can't find StartMatchLever componen !!";

        // Doors are enabled (on a moon), ship is either not landed or is leaving
        if (StartOfRound.Instance.shipDoorsEnabled && !(StartOfRound.Instance.shipHasLanded || StartOfRound.Instance.shipIsLeaving)) { return alreadyTransitMessage; }
		// Doors are disabled (in space), ship is in transit to another moon
		if (!StartOfRound.Instance.shipDoorsEnabled && StartOfRound.Instance.travellingToNewLevel) { return alreadyTransitMessage; }

		bool newState = !lever.leverHasBeenPulled;
        lever.PullLever();
        lever.LeverAnimation();
        if (newState) lever.StartGame(); else lever.EndGame();
        return "Initiating " + (lever.leverHasBeenPulled ? "landing" : "launch") + " sequence.";
    }

    [TerminalCommand("Go", clearText: false), CommandInfo("Pull the lever, Kronk!")]
	public string GoCommand()
	{
		return LaunchCommand();
	}
}


