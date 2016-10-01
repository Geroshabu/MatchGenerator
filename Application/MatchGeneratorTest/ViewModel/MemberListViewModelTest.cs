using System;
using System.Collections.Generic;
using System.Linq;
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

		private IList<IMemberListItemViewModel> MembersFieldValue;

		public MemberListViewModelInstanceTest()
		{
			Instance = new MemberListViewModel();
			MembersFieldValue = new List<IMemberListItemViewModel>
			{
				new MemberListItemViewModelMock(),
				new MemberListItemViewModelMock(),
				new MemberListItemViewModelMock(),
				new MemberListItemViewModelMock(),
				new MemberListItemViewModelMock()
			};

			Instance.SetPrivateField("MembersField", MembersFieldValue);
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

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 正常系 : リストの上から下へ連続選択")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberExtendedClickTest_Down()
		{
			// Arrange
			((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedGetter = () => true;
			((MemberListItemViewModelMock)MembersFieldValue[3]).IsCheckedGetter = () => true;
			IMemberListItemViewModel valueLastClickedMember = MembersFieldValue[1];
			Instance.SetPrivateField("LastClickedMemberField", valueLastClickedMember);
			// Input data
			object inputSender = MembersFieldValue[3];
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Mocks
			bool actualMember1IsChecked = false;
			((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedSetter = value => { actualMember1IsChecked = value; };
			bool actualMember2IsChecked = false;
			((MemberListItemViewModelMock)MembersFieldValue[2]).IsCheckedSetter = value => { actualMember2IsChecked = value; };
			bool actualMember3IsChecked = false;
			((MemberListItemViewModelMock)MembersFieldValue[3]).IsCheckedSetter = value => { actualMember3IsChecked = value; };
			// Expected data
			IList<IMemberListItemViewModel> expectedSelectedMembersField = MembersFieldValue.Skip(1).Take(3).ToList();
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldValue[3];

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Called method
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[0]).IsCheckedSetterCount);
			Assert.Equal(1, ((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedSetterCount);
			Assert.Equal(1, ((MemberListItemViewModelMock)MembersFieldValue[2]).IsCheckedSetterCount);
			Assert.Equal(1, ((MemberListItemViewModelMock)MembersFieldValue[3]).IsCheckedSetterCount);
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[4]).IsCheckedSetterCount);
			Assert.Equal(true, actualMember1IsChecked);
			Assert.Equal(true, actualMember2IsChecked);
			Assert.Equal(true, actualMember3IsChecked);
		}

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 正常系 : リストの下から上へ連続選択解除")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberExtendedClickTest_Up()
		{
			// Arrange
			((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedGetter = () => true;
			((MemberListItemViewModelMock)MembersFieldValue[3]).IsCheckedGetter = () => false;
			IMemberListItemViewModel valueLastClickedMember = MembersFieldValue[3];
			Instance.SetPrivateField("LastClickedMemberField", valueLastClickedMember);
			// Input data
			object inputSender = MembersFieldValue[1];
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Mocks
			bool actualMember1IsChecked = true;
			((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedSetter = value => { actualMember1IsChecked = value; };
			bool actualMember2IsChecked = true;
			((MemberListItemViewModelMock)MembersFieldValue[2]).IsCheckedSetter = value => { actualMember2IsChecked = value; };
			bool actualMember3IsChecked = true;
			((MemberListItemViewModelMock)MembersFieldValue[3]).IsCheckedSetter = value => { actualMember3IsChecked = value; };
			// Expected data
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldValue[1];

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Called method
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[0]).IsCheckedSetterCount);
			Assert.Equal(1, ((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedSetterCount);
			Assert.Equal(1, ((MemberListItemViewModelMock)MembersFieldValue[2]).IsCheckedSetterCount);
			Assert.Equal(1, ((MemberListItemViewModelMock)MembersFieldValue[3]).IsCheckedSetterCount);
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[4]).IsCheckedSetterCount);
			Assert.Equal(false, actualMember1IsChecked);
			Assert.Equal(false, actualMember2IsChecked);
			Assert.Equal(false, actualMember3IsChecked);
		}

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 正常系 : 連続選択した項目が前回選択した項目と同じ")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberExtendedClickTest_SameItem()
		{
			// Arrange
			((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedGetter = () => true;
			IMemberListItemViewModel valueLastClickedMember = MembersFieldValue[1];
			Instance.SetPrivateField("LastClickedMemberField", valueLastClickedMember);
			// Input data
			object inputSender = MembersFieldValue[1];
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Mocks
			bool actualMember1IsChecked = false;
			((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedSetter = value => { actualMember1IsChecked = value; };
			// Expected data
			IList<IMemberListItemViewModel> expectedSelectedMembersField = MembersFieldValue.Skip(1).Take(1).ToList();
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldValue[1];

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Called method
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[0]).IsCheckedSetterCount);
			Assert.Equal(1, ((MemberListItemViewModelMock)MembersFieldValue[1]).IsCheckedSetterCount);
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[2]).IsCheckedSetterCount);
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[3]).IsCheckedSetterCount);
			Assert.Equal(0, ((MemberListItemViewModelMock)MembersFieldValue[4]).IsCheckedSetterCount);
			Assert.Equal(true, actualMember1IsChecked);
		}

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 正常系 : 一度も選択していない状態で連続選択クリック")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberExtendedClickTest_FirstClick()
		{
			// Arrange
			Instance.SetPrivateField("LastClickedMemberField", null);
			// Input data
			object inputSender = MembersFieldValue[1];
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Expected data
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldValue[1];

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Called method
			foreach (IMemberListItemViewModel item in MembersFieldValue)
			{
				Assert.Equal(0, ((MemberListItemViewModelMock)item).IsCheckedGetterCount);
			}
		}

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 異常系 : senderが期待したものではない(使い方間違ってる)")]
		[Trait("category", "ViewModel")]
		[Trait("type", "異常系")]
		public void Item_MemberExtendedClickTest_SenderTypeError()
		{
			// Arrange
			object inputSender = new object();
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = false };

			// Act & Assert
			Assert.Throws<ArgumentException>(
				() =>
				{
					Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);
				});
		}
	}
}
