using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xunit;
using Moq;
using MatchGenerator.Model;
using MatchGenerator.ViewModel;
using MatchGeneratorTest.Model;

namespace MatchGeneratorTest.ViewModel
{
	internal struct MemberListViewModelMember
	{
		public const string Model = "model";
		public const string MembersField = "MembersField";
		public const string CreateMemberListViewModel = "<CreateMemberListViewModel>k__BackingField";
	}

	public class MemberListViewModelTest
	{
		[Fact(DisplayName = nameof(MemberListViewModel) + "コンストラクタ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ConstructorTest()
		{
			// Arrange
			var input = new ObservableCollection<IPerson>();

			// Act
			MemberListViewModel actualResult = new MemberListViewModel(input);

			// Assert
			object actualModel = actualResult.GetPrivateField(MemberListViewModelMember.Model);
			Assert.Same(input, actualModel);
		}
	}

	public class MemberListViewModelInstanceTest
	{
		private MemberListViewModel Instance;

		private IList<Mock<IMemberListItemViewModel>> MembersFieldMocks;

		public MemberListViewModelInstanceTest()
		{
			IList<MatchGenerator.Model.IPerson> inputMemberData = new List<MatchGenerator.Model.IPerson>();

			Instance = (MemberListViewModel)MemberListViewModel.CreateMemberListViewModel(inputMemberData);

			MembersFieldMocks = new List<Mock<IMemberListItemViewModel>>
			{
				new Mock<IMemberListItemViewModel>(),
				new Mock<IMemberListItemViewModel>(),
				new Mock<IMemberListItemViewModel>(),
				new Mock<IMemberListItemViewModel>(),
				new Mock<IMemberListItemViewModel>(),
				new Mock<IMemberListItemViewModel>()
			};
			int count = 0;
			foreach (Mock<IMemberListItemViewModel> mock in MembersFieldMocks)
			{
				mock.SetupProperty(vm => vm.IsChecked, count % 2 == 1);
				count++;
			}
		}

		[Fact(DisplayName = nameof(MemberListViewModel.Model) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ModelGetTest()
		{
			// Arrange
			IList<IPerson> expectedReturn = new List<IPerson>();
			foreach(Mock<IMemberListItemViewModel> mock in MembersFieldMocks)
			{
				IPerson member = new Mock<IPerson>().Object;
				mock.Setup(vm => vm.Model).Returns(member);
				expectedReturn.Add(member);
			}
			Instance.SetPrivateField(MemberListViewModelMember.MembersField,
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object)));

			// Act
			IList<IPerson> actualReturn = Instance.Model;

			// Assert
			Assert.Equal(expectedReturn, actualReturn);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.Members) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void MembersGetTest()
		{
			// Arrange
			IList<IMemberListItemViewModel> MembersFieldValue =
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object));
			Instance.SetPrivateField(MemberListViewModelMember.MembersField, MembersFieldValue);
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
			IList<IMemberListItemViewModel> inputMembers =
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object));
			IList<IMemberListItemViewModel> expectedMembersField = inputMembers;
			// Event handler
			IList<object> actualPropertyChangedParamsSender = new List<object>();
			IList<System.ComponentModel.PropertyChangedEventArgs> actualPropertyChangedParamsE = new List<System.ComponentModel.PropertyChangedEventArgs>();
			Instance.PropertyChanged += (s, e) =>
			{
				actualPropertyChangedParamsSender.Add(s);
				actualPropertyChangedParamsE.Add(e);
			};
			// Expected data
			IList<object> expectedPropertyChangedParamsSender = new List<object> { Instance, Instance };
			IList<string> expectedPropertyChangedParamsE = new List<string> { "Members", "SelectedMembers" };

			// Act
			Instance.Members = inputMembers;

			// Assert
			ObservableCollection<IMemberListItemViewModel> actualMembersField =
				(ObservableCollection<IMemberListItemViewModel>)Instance.GetPrivateField(MemberListViewModelMember.MembersField);
			Assert.Equal(expectedMembersField, actualMembersField);
			// Called handler
			Assert.True(actualPropertyChangedParamsSender.SequenceEqual(expectedPropertyChangedParamsSender));
			Assert.True(actualPropertyChangedParamsE.Select(e => e.PropertyName).SequenceEqual(expectedPropertyChangedParamsE));
		}

		[Fact(DisplayName = nameof(MemberListViewModel.Members) + ".Setterプロパティ : 正常系 : コレクション変更イベントが設定されること")]
		[Trait("category", "ViewModel"), Trait("type", "正常系")]
		public void MemberSetTest_CollectionChangedEvent()
		{
			// Arrange
			IList<IMemberListItemViewModel> inputMembers = new List<IMemberListItemViewModel>();
			IMemberListItemViewModel addedMember = new Mock<IMemberListItemViewModel>().Object;
			// Event handler
			IList<object> actualSenderParamsOfCollectionChanged = new List<object>();
			IList<NotifyCollectionChangedEventArgs> actualEParamsOfCollectionChanged = new List<NotifyCollectionChangedEventArgs>();
			Instance.CollectionChanged += (s, e) =>
			{
				actualSenderParamsOfCollectionChanged.Add(s);
				actualEParamsOfCollectionChanged.Add(e);
			};
			// Expected data
			IList<object> expectedSenderParamsOfCollectionChanged = new List<object>();
			IList<IMemberListItemViewModel> expectedNewItemsOfEParamOfCollectionChanged = new List<IMemberListItemViewModel> { addedMember };

			// Act
			Instance.Members = inputMembers;
			ObservableCollection<IMemberListItemViewModel> membersField =
				(ObservableCollection<IMemberListItemViewModel>)Instance.GetPrivateField(MemberListViewModelMember.MembersField);
			membersField.Add(addedMember);

			// Assert
			expectedSenderParamsOfCollectionChanged.Add(membersField);
			Assert.Equal(expectedSenderParamsOfCollectionChanged, actualSenderParamsOfCollectionChanged);
			Assert.Equal(expectedNewItemsOfEParamOfCollectionChanged, 
				actualEParamsOfCollectionChanged.Select(e => e.NewItems[0]).Cast<IMemberListItemViewModel>().ToList());
		}

		[Fact(DisplayName = nameof(MemberListViewModel.SelectedMembers) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void SelectedMembersGetTest()
		{
			// Arrange
			Instance.SetPrivateField(MemberListViewModelMember.MembersField,
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object)));
			// Expected data
			IList<IMemberListItemViewModel> expectedReturn = new List<IMemberListItemViewModel>
			{
				MembersFieldMocks[1].Object,
				MembersFieldMocks[3].Object,
				MembersFieldMocks[5].Object
			};

			// Act
			IList<IMemberListItemViewModel> actualReturn = Instance.SelectedMembers;

			// Assert
			Assert.True(expectedReturn.SequenceEqual(actualReturn));
		}

		[Fact(DisplayName = nameof(MemberListViewModel.SelectedMembers) + ".Getterプロパティ : 正常系 : 全メンバが空")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void SelectedMembersGetTest_EmptyMembers()
		{
			// Arrange
			ObservableCollection<IMemberListItemViewModel> membersFieldValue = new ObservableCollection<IMemberListItemViewModel>();
			Instance.SetPrivateField(MemberListViewModelMember.MembersField, membersFieldValue);

			// Act
			IList<IMemberListItemViewModel> actualReturn = Instance.SelectedMembers;

			// Assert
			Assert.Empty(actualReturn);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.SelectedMembers) + ".Getterプロパティ : 正常系 : 何も選択されていない")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void SelectedMembersGetTest_NoMemberSelected()
		{
			// Arrange
			Instance.SetPrivateField(MemberListViewModelMember.MembersField,
				new ObservableCollection<IMemberListItemViewModel>(
					MembersFieldMocks.Select(mock =>
					{
						mock.Setup(vm => vm.IsChecked).Returns(false);
						return mock.Object;
					})));

			// Act
			IList<IMemberListItemViewModel> actualReturn = Instance.SelectedMembers;

			// Assert
			Assert.Empty(actualReturn);
		}

		[Fact(DisplayName = nameof(MemberListViewModel.SelectedMembers) + ".Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void SelectedMembersSetTest()
		{
			// Arrange
			IList<Mock<IMemberListItemViewModel>> inputSelectedMembersMocks = new List<Mock<IMemberListItemViewModel>>
			{
				MembersFieldMocks[1],
				MembersFieldMocks[2],
				MembersFieldMocks[4]
			};
			IList<Mock<IMemberListItemViewModel>> notSelectedMembersMocks = new List<Mock<IMemberListItemViewModel>>
			{
				MembersFieldMocks[0],
				MembersFieldMocks[3],
				MembersFieldMocks[5]
			};
			IList<object> actualPropertyChangedParamsSender = new List<object>();
			IList<System.ComponentModel.PropertyChangedEventArgs> actualPropertyChangedParamsE = new List<System.ComponentModel.PropertyChangedEventArgs>();
			Instance.PropertyChanged += (s, e) =>
			{
				actualPropertyChangedParamsSender.Add(s);
				actualPropertyChangedParamsE.Add(e);
			};
			// Field values
			Instance.SetPrivateField(MemberListViewModelMember.MembersField,
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object)));
			// Expected data
			IList<object> expectedPropertyChangedParamsSender = new List<object> { Instance };
			IList<string> expectedPropertyChangedParamsE = new List<string> { "SelectedMembers" };

			// Act
			Instance.SelectedMembers = inputSelectedMembersMocks.Select(mock => mock.Object).ToList();

			// Assert
			// Called method
			foreach(Mock<IMemberListItemViewModel> mock in inputSelectedMembersMocks)
			{
				mock.VerifySet(vm => vm.IsChecked = true);
			}
			foreach(Mock<IMemberListItemViewModel> mock in notSelectedMembersMocks)
			{
				mock.VerifySet(vm => vm.IsChecked = false);
			}
			// Called handler
			Assert.True(actualPropertyChangedParamsSender.SequenceEqual(expectedPropertyChangedParamsSender));
			Assert.True(actualPropertyChangedParamsE.Select(e => e.PropertyName).SequenceEqual(expectedPropertyChangedParamsE));
		}

		[Fact(DisplayName = nameof(MemberListViewModel.LastClickedMember) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void LastClickedMemberGetTest()
		{
			// Arrange
			IMemberListItemViewModel LastClickedMemberFieldValue = new Mock<IMemberListItemViewModel>().Object;
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
			IMemberListItemViewModel inputLastClickedMember = new Mock<IMemberListItemViewModel>().Object;
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
			object inputSender = new Mock<IMemberListItemViewModel>().Object;
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
			// Field values
			Instance.SetPrivateField(MemberListViewModelMember.MembersField,
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object)));
			IMemberListItemViewModel valueLastClickedMember = MembersFieldMocks[1].Object;
			Instance.SetPrivateField("LastClickedMemberField", valueLastClickedMember);
			// Input data
			object inputSender = MembersFieldMocks[3].Object;
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Expected data
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldMocks[3].Object;

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Called method
			IList<Mock<IMemberListItemViewModel>> calledMemberMocks = new List<Mock<IMemberListItemViewModel>>
			{
				MembersFieldMocks[1], MembersFieldMocks[2], MembersFieldMocks[3]
			};
			foreach (Mock<IMemberListItemViewModel> mock in calledMemberMocks)
			{
				mock.VerifySet(vm => vm.IsChecked = true);
			}
			// Not called method
			foreach (Mock<IMemberListItemViewModel> mock in MembersFieldMocks.Except(calledMemberMocks))
			{
				mock.VerifySet(vm => vm.IsChecked = It.IsAny<bool>(), Times.Never);
			}
		}

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 正常系 : リストの下から上へ連続選択解除")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberExtendedClickTest_Up()
		{
			// Arrange
			// Field values
			Instance.SetPrivateField(MemberListViewModelMember.MembersField,
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object)));
			IMemberListItemViewModel valueLastClickedMember = MembersFieldMocks[4].Object;
			Instance.SetPrivateField("LastClickedMemberField", valueLastClickedMember);
			// Input data
			object inputSender = MembersFieldMocks[1].Object;
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Expected data
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldMocks[1].Object;

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Called method
			IList<Mock<IMemberListItemViewModel>> calledMemberMocks = new List<Mock<IMemberListItemViewModel>>
			{
				MembersFieldMocks[4],
				MembersFieldMocks[3],
				MembersFieldMocks[2],
				MembersFieldMocks[1]
			};
			foreach (Mock<IMemberListItemViewModel> mock in calledMemberMocks)
			{
				mock.VerifySet(vm => vm.IsChecked = false);
			}
			// Not called method
			foreach (Mock<IMemberListItemViewModel> mock in MembersFieldMocks.Except(calledMemberMocks))
			{
				mock.VerifySet(vm => vm.IsChecked = It.IsAny<bool>(), Times.Never);
			}
		}

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 正常系 : 連続選択した項目が前回選択した項目と同じ")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberExtendedClickTest_SameItem()
		{
			// Arrange
			// Field values
			Instance.SetPrivateField(MemberListViewModelMember.MembersField,
				new ObservableCollection<IMemberListItemViewModel>(MembersFieldMocks.Select(mock => mock.Object)));
			IMemberListItemViewModel valueLastClickedMember = MembersFieldMocks[1].Object;
			Instance.SetPrivateField("LastClickedMemberField", valueLastClickedMember);
			// Input data
			object inputSender = MembersFieldMocks[1].Object;
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Expected data
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldMocks[1].Object;

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Called method
			IList<Mock<IMemberListItemViewModel>> calledMemberMock = new List<Mock<IMemberListItemViewModel>>
			{
				MembersFieldMocks[1]
			};
			foreach (Mock<IMemberListItemViewModel> mock in calledMemberMock)
			{
				mock.VerifySet(vm => vm.IsChecked = true);
			}
			// Not called method
			foreach (Mock<IMemberListItemViewModel> mock in MembersFieldMocks.Except(calledMemberMock))
			{
				mock.VerifySet(vm => vm.IsChecked = It.IsAny<bool>(), Times.Never);
			}
		}

		[Fact(DisplayName = "Item_MemberExtendedClickメソッド : 正常系 : 一度も選択していない状態で連続選択クリック")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void Item_MemberExtendedClickTest_FirstClick()
		{
			// Arrange
			// Field values
			Instance.SetPrivateField("LastClickedMemberField", null);
			// Input data
			object inputSender = MembersFieldMocks[1].Object;
			MemberClickEventArgs inputE = new MemberClickEventArgs { IsChecked = true };
			// Expected data
			IMemberListItemViewModel expectedLastClickedMemberField = MembersFieldMocks[1].Object;

			// Act
			Instance.InvokePrivateMethod("Item_MemberExtendedClick", inputSender, inputE);

			// Assert
			// Members
			IMemberListItemViewModel actualLastClickedMember = (IMemberListItemViewModel)Instance.GetPrivateField("LastClickedMemberField");
			Assert.Same(expectedLastClickedMemberField, actualLastClickedMember);
			// Not called method
			foreach (Mock<IMemberListItemViewModel> mock in MembersFieldMocks)
			{
				mock.VerifySet(vm => vm.IsChecked = It.IsAny<bool>(), Times.Never);
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

		[Fact(DisplayName = nameof(IEnumerable<IPerson>) + "<" + nameof(IPerson) + ">." + nameof(IEnumerable<IPerson>.GetEnumerator) + "メソッド : 正常系")]
		[Trait("category", "ViewModel"), Trait("type", "正常系")]
		public void GetEnumeratorOfIPersonTest()
		{
			// Arrange
			// Field values
			IList<IPerson> modelListData = new List<IPerson>
			{
				new Mock<IPerson>().Object,
				new Mock<IPerson>().Object
			};
			ObservableCollection<IMemberListItemViewModel> memberFieldValue = new ObservableCollection<IMemberListItemViewModel>(
				modelListData
				.Select(person =>
				{
					Mock<IMemberListItemViewModel> viewModelMock = new Mock<IMemberListItemViewModel>();
					viewModelMock.Setup(vm => vm.Model).Returns(person);
					return viewModelMock.Object;
				}));
			Instance.SetPrivateField(MemberListViewModelMember.MembersField, memberFieldValue);

			// Act & Assert
			Assert.True(Instance.SequenceEqual(modelListData));
		}

		[Fact(DisplayName = nameof(System.Collections.IEnumerable) + "." + nameof(System.Collections.IEnumerable.GetEnumerator) + "メソッド : 正常系")]
		[Trait("category", "ViewModel"), Trait("type", "正常系")]
		public void GetEnumeratorTest()
		{
			// Arrange
			// Field values
			IList<IPerson> modelListData = new List<IPerson>
			{
				new Mock<IPerson>().Object,
				new Mock<IPerson>().Object
			};
			ObservableCollection<IMemberListItemViewModel> memberFieldValue = new ObservableCollection<IMemberListItemViewModel>(
				modelListData
				.Select(person =>
				{
					Mock<IMemberListItemViewModel> viewModelMock = new Mock<IMemberListItemViewModel>();
					viewModelMock.Setup(vm => vm.Model).Returns(person);
					return viewModelMock.Object;
				}));
			Instance.SetPrivateField(MemberListViewModelMember.MembersField, memberFieldValue);

			// Act & Assert
			int count = 0;
			foreach(IPerson person in (System.Collections.IEnumerable)Instance)
			{
				Assert.Same(modelListData[count++], person);
			}
		}
	}
}
