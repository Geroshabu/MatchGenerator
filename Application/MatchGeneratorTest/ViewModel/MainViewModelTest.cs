using Xunit;
using MatchGenerator.ViewModel;

namespace MatchGeneratorTest.ViewModel
{

	internal struct MainViewModelMember
	{
		public const string AllMembers = "AllMembersField";
		public const string InitializeData = "InitializeData";
	}

	public class MainViewModelTest
	{
		[Fact(DisplayName = "MainViewModelコンストラクタ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ConstructorTest()
		{
			// Act
			MainViewModel actualReturn = new MainViewModel();

			// Assert
			Assert.NotNull(actualReturn.InitializeCommand);
		}
	}

	public class MainViewModelInstanceTest
	{
		private MainViewModel Instance;

		public MainViewModelInstanceTest()
		{
			Instance = new MainViewModel();
		}

		[Fact(DisplayName = nameof(MainViewModel.AllMembers) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void AllMembersGetterTest()
		{
			// Arrange
			IMemberListViewModel AllMembersFieldValue = new MemberListViewModelMock();
			Instance.SetPrivateField(MainViewModelMember.AllMembers, AllMembersFieldValue);
			IMemberListViewModel expectedReturn = AllMembersFieldValue;

			// Act
			IMemberListViewModel actualReturn = Instance.AllMembers;

			// Assert
			Assert.Same(expectedReturn, actualReturn);
		}

		[Fact(DisplayName = nameof(MainViewModel.AllMembers) + ".Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void AllMembersSetterTest()
		{
			// Arrange
			IMemberListViewModel inputAllMembers = new MemberListViewModelMock();
			IMemberListViewModel expectedAllMembersField = inputAllMembers;

			// Act
			Instance.AllMembers = inputAllMembers;

			// Assert
			IMemberListViewModel actualAllMembersField = (IMemberListViewModel)Instance.GetPrivateField(MainViewModelMember.AllMembers);
			Assert.Same(expectedAllMembersField, actualAllMembersField);
		}

		[Fact(DisplayName = MainViewModelMember.InitializeData + "メソッド : 正常系", Skip = "モックに入れ替えられるようになってから")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void InitializeDataTest()
		{
			// Act
			Instance.InvokePrivateMethod(MainViewModelMember.InitializeData, null);

			// Assert
			Assert.NotNull(Instance.InitializeCommand);
		}
	}
}
