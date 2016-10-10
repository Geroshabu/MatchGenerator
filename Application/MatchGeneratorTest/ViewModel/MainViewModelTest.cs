using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition.Hosting;
using Xunit;
using MatchGenerator.ViewModel;

namespace MatchGeneratorTest.ViewModel
{

	internal struct MainViewModelMember
	{
		public const string AllMembers = "AllMembersField";
		public const string DefaultMemberImporterType = "<DefaultMemberImporterType>k__BackingField";
		public const string InitializeData = "InitializeData";
		public const string ReadMemberFromFile = "ReadMemberFromFile";
		public const string CreateMefContainer = "CreateMefContainerBody";
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
			Assert.NotNull(actualReturn.ReadMemberFromFileCommand);
		}
	}

	public class MainViewModelInstanceTest
	{
		private MainViewModel Instance;

		public MainViewModelInstanceTest()
		{
			Instance = new MainViewModel();
		}

		[Fact(DisplayName = nameof(MainViewModelMember.DefaultMemberImporterType) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void DefaultMemberImporterTypeTest()
		{
			// Arrange
			Type expectedReturn = typeof(MatchGenerator.FileIO.DefaultImporter);

			// Act
			Type actualReturn = (Type)Instance.GetPrivateField(MainViewModelMember.DefaultMemberImporterType);

			// Assert
			Assert.Equal(expectedReturn, actualReturn);
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

		[Fact(DisplayName = MainViewModelMember.ReadMemberFromFile + "メソッド : 正常系", Skip = "ダイアログの表示をモックに入れ替えられるようになってから")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ReadMemberFromFileTest()
		{

		}

		[Fact(DisplayName = MainViewModelMember.CreateMefContainer + "メソッド : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void CreateMefContainerTest()
		{
			// Act
			CompositionContainer actualReturn = (CompositionContainer)Instance.InvokePrivateMethod(MainViewModelMember.CreateMefContainer, null);

			// Assert
			// 動作に必要な最低限のインスタンス(自身のインスタンス)が取得できるか.
			IEnumerable<MatchGenerator.FileIO.IMemberImporter> importers = actualReturn.GetExportedValues<MatchGenerator.FileIO.IMemberImporter>();
			Assert.True(importers.Any(importer => importer is MatchGenerator.FileIO.DefaultImporter));
		}
	}
}
