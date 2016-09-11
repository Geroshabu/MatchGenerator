using System.Reflection;

namespace MatchGeneratorTest
{
	internal static class Utils
	{
		/// <summary>
		/// インスタンスのプライベートなフィールドに値を設定する
		/// </summary>
		public static void SetPrivateField(this object instance, string fieldName, object value)
		{
			FieldInfo fieldInfo = instance.GetType().GetField(fieldName,
				BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
			fieldInfo.SetValue(instance, value);
		}

		/// <summary>
		/// インスタンスのプライベートなフィールドから値を取得する
		/// </summary>
		public static object GetPrivateField(this object instance, string fieldName)
		{
			FieldInfo fieldInfo = instance.GetType().GetField(fieldName,
				BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance);
			return fieldInfo.GetValue(instance);
		}

		/// <summary>
		/// インスタンスの自動実装プロパティのBackingFieldに値を設定する
		/// </summary>
		public static void SetBackingField(this object instance, string propertyName, object value)
		{
			instance.SetPrivateField($"<{propertyName}>k__BackingField", value);
		}

		/// <summary>
		/// インスタンスの自動実装プロパティのBackingFieldから値を取得する
		/// </summary>
		public static object GetBackingField(this object instance, string propertyName)
		{
			return instance.GetPrivateField($"<{propertyName}>k__BackingField");
		}
	}
}
