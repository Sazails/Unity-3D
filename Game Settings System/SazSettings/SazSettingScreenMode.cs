using UnityEngine;

namespace Game.Scripts.Settings.SazSettings
{
	public static class SazSettingScreenMode
	{
		public static readonly string[] screenModeOptions =
		{
			"Windowed",
			"FullScreen Window",
			"Maximized Window",
			"FullScreen"
		};

		public static FullScreenMode ConvertScreenMode(string option)
		{
			var options = screenModeOptions;

			if (option == options[0])
				return FullScreenMode.Windowed;

			if (option == options[1])
				return FullScreenMode.FullScreenWindow;

			if (option == options[2])
				return FullScreenMode.MaximizedWindow;

			return FullScreenMode.ExclusiveFullScreen;
		}
	}
}