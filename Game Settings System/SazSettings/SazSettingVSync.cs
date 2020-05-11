namespace Game.Scripts.Settings.SazSettings
{
	public static class SazSettingVSync
	{
		public static int ConvertVSync(string option)
		{
			return (option == "Enabled" ? 1 : 0);
		}
	}
}