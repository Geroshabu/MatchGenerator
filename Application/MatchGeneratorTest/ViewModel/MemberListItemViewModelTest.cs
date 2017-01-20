using System;
using System.Reflection;
using System.Linq;
using Xunit;
using Moq;
using MatchGenerator.Core;
using MatchGenerator.Model;
using MatchGenerator.ViewModel;
using MatchGeneratorTest.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace MatchGeneratorTest.ViewModel
{
	internal struct MemberListItemViewModelMember
	{
		public const string Model = "Model";
		public const string IsCheckedField = "IsCheckedField";
		public const string CopyMemberListItemViewModel = "<CopyMemberListItemViewModel>k__BackingField";
	}

	public class MemberListItemViewModelTest
	{
		[Fact(DisplayName = "MemberListItemViewModelコンストラクタ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ConstructorTest()
		{
			// Arrange
			IPerson inputModel = new Mock<IPerson>().Object;
			IPerson expectedModel = inputModel;

			// Act
			MemberListItemViewModel actualReturn = new MemberListItemViewModel(inputModel);

			// Assert
			// 影響するフィールドの確認
			Assert.Same(expectedModel, actualReturn.Model);
			Assert.NotNull(actualReturn.MemberClickCommand);
			Assert.IsType<Microsoft.Practices.Prism.Commands.DelegateCommand>(actualReturn.MemberClickCommand);
			Assert.NotNull(actualReturn.MemberExtendedClickCommand);
			Assert.IsType<Microsoft.Practices.Prism.Commands.DelegateCommand>(actualReturn.MemberExtendedClickCommand);
		}

		[Theory(DisplayName = nameof(MemberListItemViewModel) + "コピーコンストラクタ : 正常系")]
		[InlineData(false)]
		[InlineData(true)]
		[Trait("category", "ViewModel"), Trait("type", "正常系")]
		public void CopyConstructorTest(bool isCheckedFieldValue)
		{
			// Arrange
			IPerson inputModel = new Mock<IPerson>().Object;
			MemberListItemViewModel inputOther = new MemberListItemViewModel(inputModel);
			inputOther.SetPrivateField(MemberListItemViewModelMember.IsCheckedField, isCheckedFieldValue);
			// Expected data
			IPerson expectedModel = (IPerson)inputOther.GetBackingField(MemberListItemViewModelMember.Model);

			// Act
			IMemberListItemViewModel actualReturn = MemberListItemViewModel.CopyMemberListItemViewModel(inputOther);

			// Assert
			// 影響するフィールドの確認
			Assert.Same(inputModel, actualReturn.Model);
			Assert.Equal(isCheckedFieldValue, (bool)actualReturn.GetPrivateField(MemberListItemViewModelMember.IsCheckedField));
			Assert.NotNull(actualReturn.MemberClickCommand);
			Assert.IsType<Microsoft.Practices.Prism.Commands.DelegateCommand>(actualReturn.MemberClickCommand);
			Assert.NotNull(actualReturn.MemberExtendedClickCommand);
			Assert.IsType<Microsoft.Practices.Prism.Commands.DelegateCommand>(actualReturn.MemberExtendedClickCommand);
		}
	}

	public class MemberListItemViewModelInstanceTest : IDisposable
	{
		private MemberListItemViewModel Instance;
		private IPerson ModelField;

		public MemberListItemViewModelInstanceTest()
		{
			ModelField = new Mock<IPerson>().Object;
			Instance = new MemberListItemViewModel(ModelField);
			Instance.SetBackingField(MemberListItemViewModelMember.Model, ModelField);
		}

		public void Dispose()
		{
			Utils.RestoreStaticField<MemberListItemViewModel>(MemberListItemViewModelMember.CopyMemberListItemViewModel);
		}

		[Fact(DisplayName = "Nameプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void NameTest()
		{
			// Arrange
			string expectedName = "foobar";
			// Mock of Model
			Mock<IPerson> modelFieldMock = new Mock<IPerson>();
			modelFieldMock.Setup(model => model.Name).Returns("foobar");
			Instance.SetBackingField(MemberListItemViewModelMember.Model, modelFieldMock.Object);

			// Act
			string actualName = Instance.Name;

			// Assert
			// 戻り値
			Assert.Equal(expectedName, actualName);
			// 内部でメソッドが呼ばれた回数
			modelFieldMock.VerifyGet(model => model.Name, Times.Once);
		}

		[Fact(DisplayName = "Descriptionプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void DescriptionTest()
		{
			// Arrange
			string expectedDescription = "foobar";
			Mock<IPerson> modelFieldMock = new Mock<IPerson>();
			modelFieldMock.Setup(model => model.Description).Returns("foobar");
			Instance.SetBackingField(MemberListItemViewModelMember.Model, modelFieldMock.Object);

			// Act
			string actualDescription = Instance.Description;

			// Assert
			// 戻り値
			Assert.Equal(expectedDescription, actualDescription);
			// 内部でメソッドが呼ばれた回数
			modelFieldMock.VerifyGet(model => model.Description, Times.Once);
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

		[Theory(DisplayName = "ClickMemberメソッド : 正常系")]
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

		[Fact(DisplayName = "ClickMemberメソッド : 異常系 : イベントハンドラが設定されていないときに落ちないこと")]
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
			// Expected
			object expectedMemberExtendedClickSenderParam = Instance;
			bool expectedMemberExtendedClickEParam = !isCheckedValue;
			bool expectedIsChecked = !isCheckedValue;

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

		[Fact(DisplayName = "Cloneメソッド : 正常系 : 期待する戻り値が得られること")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void CloneTest()
		{
			// Arrange
			IMemberListItemViewModel returnValue = new Mock<IMemberListItemViewModel>().Object;
			IList<MemberListItemViewModel> actualOtherParamsCopyMemberListItemViewModel = new List<MemberListItemViewModel>();
			Utils.SetStaticField<MemberListItemViewModel>(MemberListItemViewModelMember.CopyMemberListItemViewModel,
				new Func<MemberListItemViewModel, IMemberListItemViewModel>(
					other =>
					{
						actualOtherParamsCopyMemberListItemViewModel.Add(other);
						return returnValue;
					}));
			// Expected data
			IList<MemberListItemViewModel> expectedOtherParamsCopyMemberListItemViewModel = new List<MemberListItemViewModel> { Instance };

			// Act
			object actualReturn = Instance.Clone();

			// Assert
			Assert.Same(returnValue, actualReturn);
			Assert.True(expectedOtherParamsCopyMemberListItemViewModel.SequenceEqual(actualOtherParamsCopyMemberListItemViewModel));
		}
	}

	public class MemberListItemViewModelInstancesTest
	{
		private IList<MemberListItemViewModel> Instances = new List<MemberListItemViewModel>();

		private IList<IPerson> ModelFields = new List<IPerson>();

		public MemberListItemViewModelInstancesTest()
		{
			for (int i = 0; i < 2; i++)
			{
				IPerson modelField = new Mock<IPerson>().Object;
				MemberListItemViewModel instance = new MemberListItemViewModel(modelField);
				instance.SetBackingField(MemberListItemViewModelMember.Model, modelField);
				ModelFields.Add(modelField);
				Instances.Add(instance);
			}
		}

		[Fact(DisplayName = "EqualsModelメソッド : 正常系 : 等しいときにtrueを返す")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void EqualsModelTestEqual()
		{
			// Arrange
			Instances[1].SetBackingField(MemberListItemViewModelMember.Model, ModelFields[0]);
			object inputOther = Instances[1];

			// Act
			bool actualReturn = Instances[0].EqualsModel(inputOther);

			// Assert
			Assert.Equal(true, actualReturn);
		}

		[Fact(DisplayName = "EqualsModelメソッド : 正常系 : 等しくないときにfalseを返す")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void EqualsModelTestNotEqual()
		{
			// Act
			bool actualReturn = Instances[0].EqualsModel(Instances[1]);

			// Assert
			Assert.Equal(false, actualReturn);
		}

		[Fact(DisplayName = "EqualsModelメソッド : 正常系 : 型が違うときにfalseを返す")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void EqualsModelTestDifferentType()
		{
			// Arrange
			object inputOther = new object();

			// Act
			bool actualReturn = Instances[0].EqualsModel(inputOther);

			// Assert
			Assert.Equal(false, actualReturn);
		}
	}
}
