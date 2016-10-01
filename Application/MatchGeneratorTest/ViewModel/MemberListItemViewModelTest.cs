using System;
using System.Reflection;
using Xunit;
using MatchGenerator.Core;
using MatchGenerator.Model;
using MatchGenerator.ViewModel;
using MatchGeneratorTest.Model;
using System.Collections.Generic;
using System.Windows.Input;

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

		public Func<bool> IsCheckedGetter = () => false;
		public int IsCheckedGetterCount = 0;
		public Action<bool> IsCheckedSetter = _ => { };
		public int IsCheckedSetterCount = 0;
		public bool IsChecked
		{
			get
			{
				IsCheckedGetterCount++;
				return IsCheckedGetter();
			}

			set
			{
				IsCheckedSetterCount++;
				IsCheckedSetter(value);
			}
		}

		public event EventHandler<MemberClickEventArgs> MemberClick;

		public ICommand MemberClickCommand
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public event EventHandler<MemberClickEventArgs> MemberExtendedClick;

		public ICommand MemberExtendedClickCommand
		{
			get
			{
				throw new NotImplementedException();
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
			Assert.NotNull(actualReturn.MemberClickCommand);
			Assert.IsType<Microsoft.Practices.Prism.Commands.DelegateCommand>(actualReturn.MemberClickCommand);
			Assert.NotNull(actualReturn.MemberExtendedClickCommand);
			Assert.IsType<Microsoft.Practices.Prism.Commands.DelegateCommand>(actualReturn.MemberExtendedClickCommand);
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

		[Fact(DisplayName = "IsChecked.Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void IsCheckedGetterTest()
		{
			// Arrange
			bool beforeIsChecked = (bool)Instance.GetPrivateField("IsCheckedField");
			bool expectedIsChecked = !beforeIsChecked;
			Instance.SetPrivateField("IsCheckedField", !beforeIsChecked);

			// Act
			bool actualIsChecked = Instance.IsChecked;

			// Assert
			Assert.Equal(expectedIsChecked, actualIsChecked);
		}

		[Fact(DisplayName = "IsChecked.Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void IsCheckedSetterTest()
		{
			// Arrange
			bool beforeIsChecked = (bool)Instance.GetPrivateField("IsCheckedField");
			bool inputIsChecked = !beforeIsChecked;
			bool expectedIsChecked = !beforeIsChecked;

			// Act
			Instance.IsChecked = inputIsChecked;

			// Assert
			bool actualIsChecked = (bool)Instance.GetPrivateField("IsCheckedField");
			Assert.Equal(expectedIsChecked, actualIsChecked);
		}

		[Theory(DisplayName = "MemberClickメソッド : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		[InlineData(true)]
		[InlineData(false)]
		public void ClickMemberTest(bool isCheckedValue)
		{
			// Arrange
			IList<object> actualMemberClickSenderParams = new List<object>();
			IList<MemberClickEventArgs> actualMemberClickEParams = new List<MemberClickEventArgs>();
			Instance.MemberClick += (s, e) =>
			{
				actualMemberClickSenderParams.Add(s);
				actualMemberClickEParams.Add(e);
			};
			Instance.SetPrivateField("IsCheckedField", isCheckedValue);
			// Expected
			object expectedMemberClickSenderParam = Instance;
			bool expectedIsChecked = isCheckedValue;

			// Act
			Instance.InvokePrivateMethod("ClickMember");

			// Assert
			Assert.Single(actualMemberClickSenderParams);
			Assert.Single(actualMemberClickEParams);
			Assert.Same(expectedMemberClickSenderParam, actualMemberClickSenderParams[0]);
			Assert.Equal(expectedIsChecked, actualMemberClickEParams[0].IsChecked);
		}

		[Fact(DisplayName = "MemberClickメソッド : 異常系 : イベントハンドラが設定されていないときに落ちないこと")]
		[Trait("category", "ViewModel")]
		[Trait("type", "異常系")]
		public void ClickMemberTest_NoEventHandler()
		{
			// Act
			Instance.InvokePrivateMethod("ClickMember");
		}

		[Theory(DisplayName = "ExtendClickMemberメソッド : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		[InlineData(true)]
		[InlineData(false)]
		public void ExtendClickMemberTest(bool isCheckedValue)
		{
			// Arrange
			IList<object> actualMemberExtendedClickSenderParams = new List<object>();
			IList<MemberClickEventArgs> actualMemberExtendedClickEParams = new List<MemberClickEventArgs>();
			Instance.MemberExtendedClick += (s, e) =>
			{
				actualMemberExtendedClickSenderParams.Add(s);
				actualMemberExtendedClickEParams.Add(e);
			};
			Instance.SetPrivateField("IsCheckedField", isCheckedValue);
			Instance.IsChecked = false;
			// Expected
			object expectedMemberExtendedClickSenderParam = Instance;
			bool expectedMemberExtendedClickEParam = isCheckedValue;
			bool expectedIsChecked = true;

			// Act
			Instance.InvokePrivateMethod("ExtendClickMember");

			// Assert
			Assert.Single(actualMemberExtendedClickSenderParams);
			Assert.Single(actualMemberExtendedClickEParams);
			Assert.Same(expectedMemberExtendedClickSenderParam, actualMemberExtendedClickSenderParams[0]);
			Assert.Equal(expectedMemberExtendedClickEParam, actualMemberExtendedClickEParams[0].IsChecked);
			Assert.Equal(expectedIsChecked, Instance.IsChecked);
		}

		[Fact(DisplayName = "ExtendClickMemberメソッド : 異常系 : イベントハンドラが設定されていないときに落ちないこと")]
		[Trait("category", "ViewModel")]
		[Trait("type", "異常系")]
		public void ExtendClickMemberTest_NoEventHandler()
		{
			// Act
			Instance.InvokePrivateMethod("ExtendClickMember");
		}
	}
}
