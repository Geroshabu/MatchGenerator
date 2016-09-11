using System;
using System.Reflection;
using Xunit;
using MatchGenerator.Core;
using MatchGenerator.ViewModel;

namespace MatchGeneratorTest.ViewModel
{
	public class MemberListItemViewModelTest
	{
		[Fact(DisplayName = "MemberListItemViewModelコンストラクタ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ConstructorTest()
		{
			// Arrange
			Person inputModel = new Person();
			Person expectedModel = inputModel;

			// Act
			MemberListItemViewModel actualReturn = new MemberListItemViewModel(inputModel);

			// Assert
			// 影響するフィールドの確認
			FieldInfo fieldInfo = typeof(MemberListItemViewModel).GetField("Model",
				BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
			Person actualModel = (Person)fieldInfo.GetValue(actualReturn);
			Assert.Same(expectedModel, actualModel);
		}
	}

	public class MemberListItemViewModelInstanceTest
	{
		private MemberListItemViewModel Instance;
		private Person ModelField;

		public MemberListItemViewModelInstanceTest()
		{
			ModelField = new Person(new string[] { "foo", "bar", "M", "0", "foobar" });
			Instance = new MemberListItemViewModel(ModelField);
		}

		[Fact(DisplayName = "Nameプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void NameTest()
		{
			// Arrange
			string expectedName = ModelField.Name;

			// Act
			string actualName = Instance.Name;

			// Assert
			Assert.Equal(expectedName, actualName);
		}

		[Fact(DisplayName = "Descriptionプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void DescriptionTest()
		{
			// Arrange
			string expectedDescription = ModelField.Description;

			// Act
			string actualDescription = Instance.Description;

			// Assert
			Assert.Equal(expectedDescription, actualDescription);
		}
	}
}
