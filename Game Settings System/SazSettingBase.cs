using System;
using System.IO;
using Game.Scripts.Settings.SazSettings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Game.Scripts.Settings.SazSettingsUtils;

namespace Game.Scripts.Settings
{
	public class SazSettingBase : MonoBehaviour
	{
		[Header("Config")] 
		public SettingsConfig config;
		private SettingsConfig defaultConfig;

		[Header("Components")] 
		public TMP_Dropdown screenResolutionDropdown;
		public TMP_Dropdown screenModeDropdown;
		public TMP_Dropdown antiAliasingDropdown;
		public TMP_Dropdown shadowsDropdown;
		public TMP_Dropdown shadowResolutionDropdown;
		public TMP_Dropdown vSyncDropdown;
		public Slider shadowDistanceSlider;

		private void Awake()
		{
			defaultConfig = new SettingsConfig()
			{
				screenResolution = SazSettingScreenResolution.CurrentScreenResolution(),
				screenMode = SazSettingScreenMode.screenModeOptions[3],
				antiAliasing = SazSettingAntiAliasing.antiAliasingOptions[1],
				shadows = SazSettingShadows.shadowOptions[2],
				shadowResolution = SazSettingShadowResolution.shadowResolutionOptions[2],
				shadowDistance = 110,
				vSync = false
			};
		}

		private void Start()
		{
			UpdateUI();
			LoadConfigFromDisk();
			UpdateUISelectedValues();
			UpdateSettings();
		}

		// Normally called by the apply settings button
		public void SaveAndApplySettings()
		{
			SaveConfigToDisk();
			UpdateSettings();
			UpdateUISelectedValues();
		}

		private void SaveConfigToDisk()
		{
			config.screenResolution = GetDropdownSelectedOption(screenResolutionDropdown);
			config.screenMode = GetDropdownSelectedOption(screenModeDropdown);
			config.antiAliasing = GetDropdownSelectedOption(antiAliasingDropdown);
			config.shadows = GetDropdownSelectedOption(shadowsDropdown);
			config.shadowResolution = GetDropdownSelectedOption(shadowResolutionDropdown);
			config.shadowDistance = (int) shadowDistanceSlider.value;
			config.vSync = ConvertEnabledDisabled(GetDropdownSelectedOption(vSyncDropdown));
			
			SaveLoad.SaveAsJson(config, SaveLoad.configPath, "settings.config");
			UpdateUISelectedValues();
		}

		private void LoadConfigFromDisk()
		{
			try
			{
				config = JsonUtility.FromJson<SettingsConfig>(File.ReadAllText(SaveLoad.configPath + "settings.config.json"));
			}
			catch
			{
				config = defaultConfig;					
				SaveLoad.SaveAsJson(config, SaveLoad.configPath, "settings.config");
			}
		}

		private void UpdateSettings()
		{
			var resolution = SazSettingScreenResolution.ConvertResolution(config.screenResolution);
			var screenMode = SazSettingScreenMode.ConvertScreenMode(config.screenMode);
			Screen.SetResolution(resolution.width, resolution.height, screenMode);

			QualitySettings.antiAliasing = SazSettingAntiAliasing.ConvertAntiAliasing(config.antiAliasing);
			QualitySettings.shadows = SazSettingShadows.ConvertShadowResolution(config.shadows);
			QualitySettings.shadowResolution = SazSettingShadowResolution.ConvertShadowResolution(config.shadowResolution);
			QualitySettings.shadowDistance = config.shadowDistance;
			QualitySettings.vSyncCount = SazSettingVSync.ConvertVSync(ConvertEnabledDisabled(config.vSync));
		}

		private void UpdateUI()
		{
			UpdateDropdown(screenResolutionDropdown, SazSettingScreenResolution.ScreenResolutionOptions());
			UpdateDropdown(screenModeDropdown, SazSettingScreenMode.screenModeOptions);
			UpdateDropdown(antiAliasingDropdown, SazSettingAntiAliasing.antiAliasingOptions);
			UpdateDropdown(shadowsDropdown, GetEnabledDisabledOptions);
			UpdateDropdown(shadowResolutionDropdown, SazSettingShadowResolution.shadowResolutionOptions);
			UpdateSlider(shadowDistanceSlider, 110, 0, 180, true);
			UpdateDropdown(vSyncDropdown, GetEnabledDisabledOptions);
		}

		private void UpdateUISelectedValues()
		{
			UpdateDropdownValue(screenResolutionDropdown, config.screenResolution);
			UpdateDropdownValue(screenModeDropdown, config.screenMode);
			UpdateDropdownValue(antiAliasingDropdown, config.antiAliasing);
			UpdateDropdownValue(shadowsDropdown, config.shadows);
			UpdateDropdownValue(shadowResolutionDropdown, config.shadowResolution);
			UpdateSliderValue(shadowDistanceSlider, config.shadowDistance);
			UpdateDropdownValue(vSyncDropdown, ConvertEnabledDisabled(config.vSync));
		}
	}
}