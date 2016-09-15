using System;
using System.Reflection;
using Xunit;
using MatchGenerator.Core;
using MatchGenerator.Model;
using MatchGenerator.ViewModel;
using MatchGeneratorTest.Model;
using System.Collections.Generic;

namespace MatchGeneratorTest.ViewModel
{
	/// <summary>
	/// <see cref="MemberListItemViewModel"/>のMock
	/// </summary>
	internal class MemberListItemViewModelMock : Microsoft.Practices.Prism.Mvvm.BindableBase, IMemberListItemViewModel
	{
		public IPerson ModelField = null;

		public Func<string> DescriptionGetter = () => null;
		public int DescriptionGetterCount = 0;
		public string Description
		{
			get
			{
				DescriptionGetterCount++;
				return DescriptionGetter();
			}
		}

		public Func<string> NameGetter = () => null;
		public int NameGetterCount = 0;
		public string Name
		{
			get
			{
				NameGetterCount++;
				return NameGetter();
			}
		}
	}

	public class MemberListItemViewModelTest
	{
		[Fact(DisplayName = "MemberListItemViewModelコンストラクタ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ConstructorTest()
		{
			// Arrange
			IPerson inputModel = new PersonMock();
			IPerson expectedModel = inputModel;

			// Act
			MemberListItemViewModel actualReturn = new MemberListItemViewModel(inputModel);

			// Assert
			// 影響するフィールドの確認
			Assert.Same(expectedModel, actualReturn.GetPrivateField("Model"));
		}
	}

	public class MemberListItemViewModelInstanceTest
	{
		private MemberListItemViewModel Instance;
		private IPerson ModelField;

		public MemberListItemViewModelInstanceTest()
		{
			ModelField = new PersonMock();
			Instance = new MemberListItemViewModel(ModelField);
			Instance.SetPrivateField("Model", ModelField);
		}

		[Fact(DisplayName = "Nameプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void NameTest()
		{
			// Arrange
			string expectedName = "foobar";
			((PersonMock)ModelField).NameFunc = () => "foobar";

			// Act
			string actualName = Instance.Name;

			// Assert
			// 戻り値
			Assert.Equal(expectedName, actualName);
			// 内部でメソッドが呼ばれた回数
			Assert.Equal(1, ((PersonMock)ModelField).NameCount);
		}

		[Fact(DisplayName = "Descriptionプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void DescriptionTest()
		{
			// Arrange
			string expectedDescription = "foobar";
			((PersonMock)ModelField).DescriptionFunc = () => "foobar";

			// Act
			string actualDescription = Instance.Description;

			// Assert
			// 戻り値
			Assert.Equal(expectedDescription, actualDescription);
			// 内部でメソッドが呼ばれた回数
			Assert.Equal(1, ((PersonMock)ModelField).DescriptionCount);
		}
	}
}
