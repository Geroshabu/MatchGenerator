using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

		/// <summary>
		/// インスタンスのプライベートなメソッドを実行する
		/// </summary>
		public static object InvokePrivateMethod(this object instance, string methodName, params object[] args)
		{
			PrivateObject po = new PrivateObject(instance);
			return po.Invoke(methodName, args);
		}

		private static IDictionary<string, object> originalFieldValue = new Dictionary<string, object>();

		/// <summary>
		/// クラスの静的フィールドの値を設定する.
		/// 静的フィールドはテストケースが終わってもそのままなので,
		/// <see cref="RestoreStaticField"/>で元に戻すこと.
		/// </summary>
		public static void SetStaticField<T>(string fieldName, object value)
		{
			string key = $"{typeof(T)}+{fieldName}";
			if (originalFieldValue.ContainsKey(key)) { throw new InvalidOperationException(); }

			PrivateType pt = new PrivateType(typeof(T));
			originalFieldValue.Add(key, pt.GetStaticField(fieldName));
			pt.SetStaticField(fieldName, value);
		}

		/// <summary>
		/// クラスの静的フィールドの値を元に戻す.
		/// </summary>
		public static void RestoreStaticField<T>(string fieldName)
		{
			string key = $"{typeof(T)}+{fieldName}";
			if (!originalFieldValue.ContainsKey(key)) { return; }

			PrivateType pt = new PrivateType(typeof(T));
			pt.SetStaticField(fieldName, originalFieldValue[key]);
			originalFieldValue.Remove(key);
		}
	}
}
