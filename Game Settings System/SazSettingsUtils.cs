using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace Game.Scripts.Settings
{
	public static class SazSettingsUtils
	{
		#region EnabledDisabled

		private const string enabled = "Enabled";
		private const string disabled = "Disabled";

		public static string[] GetEnabledDisabledOptions =
		{
			enabled,
			disabled
		};

		public static bool ConvertEnabledDisabled(string state)
		{
			return state == enabled;
		}

		public static string ConvertEnabledDisabled(bool state)
		{
			return state ? enabled : disabled;
		}

		#endregion

		#region Dropdown

		public static void UpdateDropdownValue(TMP_Dropdown dropdown, string optionName)
		{
			var data = dropdown.options.Select(option => option.text).ToList();
			dropdown.value = data.IndexOf(optionName);
		}

		public static void UpdateDropdown(TMP_Dropdown dropdown, string[] dropdownContent)
		{
			dropdown.ClearOptions();
			dropdown.AddOptions(dropdownContent.ToList());
			dropdown.RefreshShownValue();
		}

		public static void UpdateDropdown(TMP_Dropdown dropdown, List<string> dropdownContent)
		{
			dropdown.ClearOptions();
			dropdown.AddOptions(dropdownContent);
			dropdown.RefreshShownValue();
		}

		public static string GetDropdownSelectedOption(TMP_Dropdown dropdown)
		{
			return dropdown.options[dropdown.value].text;
		}

		#endregion

		#region Sliders

		public static void UpdateSliderValue(Slider slider, float value)
		{
			slider.value = value;
		}

		public static void UpdateSlider(Slider slider, float value, float min, float max, bool wholeNumbers)
		{
			slider.wholeNumbers = wholeNumbers;
			slider.value = value;
			slider.minValue = min;
			slider.maxValue = max;
		}

		public static void UpdateSlider(Slider slider, int value, int min, int max, bool wholeNumbers)
		{
			slider.wholeNumbers = wholeNumbers;
			slider.value = value;
			slider.minValue = min;
			slider.maxValue = max;
		}

		#endregion
	}
}