namespace ktsu.io.TwemojiRenamer;

using System.Collections.ObjectModel;
using System.Text.Json;

internal class Config
{
	public string SourcePath { get; set; } = string.Empty;
	public string DestinationPath { get; set; } = string.Empty;
	public Collection<string> Codepoints { get; set; } = new();

	internal static Config Load(string path) =>
		JsonSerializer.Deserialize<Config>(File.ReadAllText(path)) ?? new();
}