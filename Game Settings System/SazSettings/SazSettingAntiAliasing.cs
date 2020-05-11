namespace Game.Scripts.Settings.SazSettings
{
	public static class SazSettingAntiAliasing
	{
		public static readonly string[] antiAliasingOptions =
		{
			"Disabled",
			"x2",
			"x4",
			"x8"
		};

		public static int ConvertAntiAliasing(string option)
		{
			//MsaaQuality
			var options = antiAliasingOptions;

			if (option == options[0])
				return 0;

			if (option == options[2])
				return 4;

			if (option == options[3])
				return 8;

			return 2;
		}
	}
}