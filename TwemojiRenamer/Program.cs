namespace ktsu.io.TwemojiRenamer;

using EmojiHelper;
using ktsu.io.CaseConverter;

internal class Program
{
	internal static void Main(string[] args)
	{
		if (args.Length != 1)
		{
			Console.WriteLine("Provide config file path");
			return;
		}

		string configPath = args[0];
		if (!File.Exists(configPath))
		{
			Console.WriteLine("Config file not found");
			return;
		}

		var config = Config.Load(configPath);
		foreach (string codepoint in config.Codepoints)
		{
			string oldPath = Path.Combine(config.SourcePath, $"{codepoint}.svg");
			string name = EmojiHelper.GetEmojiName(codepoint);
			string filename = name.ToKebabCase();
			string newPath = Path.Combine(config.DestinationPath, $"{filename}.svg");

			try
			{
				File.Copy(oldPath, newPath, overwrite: true);
				Console.WriteLine($"Copied {codepoint} to {filename}");
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine($"File not found: {oldPath}");
			}
			catch (DirectoryNotFoundException)
			{
				Console.WriteLine($"Directory not found: {oldPath} => {newPath}");
				Console.WriteLine(Environment.CurrentDirectory);
			}
		}
	}
}
