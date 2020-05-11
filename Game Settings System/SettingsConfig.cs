using System;

namespace Game.Scripts.Settings
{
	[Serializable]
	public class SettingsConfig
	{
		public string screenResolution;
		public string screenMode;
		public string antiAliasing;
		public string shadows;
		public string shadowResolution;
		public int shadowDistance;
		public bool vSync;
	}
}