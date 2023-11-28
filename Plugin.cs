using BepInEx;

using LethalAPI.TerminalCommands;
using LethalAPI.TerminalCommands.Models;
using LethalAPI.TerminalCommands.Commands;

namespace TerminalExtras;

public static class PluginInfo {
	public const string GUID = "twig.terminalextras";
	public const string Name = "Terminal Extras";
	public const string Version = "1.0.0";
}

[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
internal class Plugin: BaseUnityPlugin {
	private TerminalModRegistry commands;

	public void Awake() {
		commands = TerminalRegistry.RegisterFrom(new ExtraCommands());
	}
}

