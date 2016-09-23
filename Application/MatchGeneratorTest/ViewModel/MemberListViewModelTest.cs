using System;
using System.Collections.Generic;
using Xunit;
using MatchGenerator.ViewModel;

namespace MatchGeneratorTest.ViewModel
{
	public class MemberListViewModelTest
	{
		[Fact(DisplayName = nameof(MemberListViewModel) + "コンストラクタ : 正常系", Skip = "未実装")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ConstructorTest()
		{

		}
	}

	public class MemberListViewModelInstanceTest
	{
		private MemberListViewModel Instance;

		public MemberListViewModelInstanceTest()
		{
			Instance = new MemberListViewModel();
		}

		[Fact(DisplayName = nameof(MemberListViewModel.Members) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void MembersGetTest()
		{
			// Arrange
			IList<IMemberListItemViewModel> MembersFieldValue = new List<IMemberListItemViewModel>
			{
				new MemberListItemViewModelMock()
			};
			Instance.SetPrivateField("MembersField", MembersFieldValue);
			IList<IMemberListItemViewModel> expectedReturn = MembersFieldValue;

			// Act
			IList<IMemberListItemViewModel> actualReturn = Instance.Members;

			// Assert
			Assert.Same(expectedReturn, actualReturn);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.Members) + ".Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void MembersSetTest()
		{
			// Arrange
			IList<IMemberListItemViewModel> inputMembers = new List<IMemberListItemViewModel>
			{
				new MemberListItemViewModelMock()
			};
			IList<IMemberListItemViewModel> expectedMembersField = inputMembers;

			// Act
			Instance.Members = inputMembers;

			// Assert
			IList<IMemberListItemViewModel> actualMembersField = (IList<IMemberListItemViewModel>)Instance.GetPrivateField("MembersField");
			Assert.Same(expectedMembersField, actualMembersField);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.SelectedMembers) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void SelectedMembersGetTest()
		{
			// Arrange
			IList<IMemberListItemViewModel> SelectedMembersFieldValue = new List<IMemberListItemViewModel>
			{
				new MemberListItemViewModelMock()
			};
			Instance.SetPrivateField("SelectedMembersField", SelectedMembersFieldValue);
			IList<IMemberListItemViewModel> expectedReturn = SelectedMembersFieldValue;

			// Act
			IList<IMemberListItemViewModel> actualReturn = Instance.SelectedMembers;

			// Assert
			Assert.Same(expectedReturn, actualReturn);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.SelectedMembers) + ".Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void SelectedMembersSetTest()
		{
			// Arrange
			IList<IMemberListItemViewModel> inputSelectedMembers = new List<IMemberListItemViewModel>
			{
				new MemberListItemViewModelMock()
			};
			IList<IMemberListItemViewModel> expectedSelectedMembersField = inputSelectedMembers;

			// Act
			Instance.Members = inputSelectedMembers;

			// Assert
			IList<IMemberListItemViewModel> actualSelectedMembersField = (IList<IMemberListItemViewModel>)Instance.GetPrivateField("MembersField");
			Assert.Same(expectedSelectedMembersField, actualSelectedMembersField);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.LastClickedMember) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void LastClickedMemberGetTest()
		{
			// Arrange
			IMemberListItemViewModel LastClickedMemberFieldValue = new MemberListItemViewModelMock();
			Instance.SetPrivateField("LastClickedMemberField", LastClickedMemberFieldValue);
			IMemberListItemViewModel expectedReturn = LastClickedMemberFieldValue;

			// Act
			IMemberListItemViewModel actualReturn = Instance.LastClickedMember;

			// Assert
			Assert.Same(expectedReturn, actualReturn);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.LastClickedMember) + ".Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void LastClickedMemberSetTest()
		{
			// Arrange
			IMemberListItemViewModel inputLastClickedMember = new MemberListItemViewModelMock();
			IMemberListItemViewModel expectedLastClickedMemberField = inputLastClickedMember;

			// Act
			Instance.LastClickedMember = inputLastClickedMember;

			// Assert
			IMemberListItemViewModel actualLastClickedMemberField = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMemberField);
		}

		[Fact(DisplayName = "Item_MemberClickメソッド : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberClickTest()
		{
			// Arrange
			object inputSender = new MemberListItemViewModelMock();
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = false };
			IMemberListItemViewModel expectedLastClickedMemberField = (IMemberListItemViewModel)inputSender;

			// Act
			Instance.InvokePrivateMethod("Item_MemberClick", inputSender, inputE);

			// Assert
			IMemberListItemViewModel actualLastClickedMemberField = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMemberField);
		}

		[Fact(DisplayName = "Item_MemberClickメソッド : 異常系 : senderが期待したものではない(使い方間違ってる)")]
		[Trait("category", "ViewModel")]
		[Trait("type", "異常系")]
		public void Item_MemberClickTest_SenderTypeError()
		{
			// Arrange
			object inputSender = new object();
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = false };

			// Act & Assert
			Assert.Throws<ArgumentException>(
				() =>
				{
					Instance.InvokePrivateMethod("Item_MemberClick", inputSender, inputE);
				});
		}
	}
}
