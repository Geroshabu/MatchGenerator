using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Xunit;
using MatchGenerator.ViewModel;
using MatchGenerator.Model;
using MatchGenerator.FileIO;
using MatchGeneratorTest.FileIO;

namespace MatchGeneratorTest.ViewModel
{

	internal struct MainViewModelMember
	{
		public const string MefContainer = "mefContainers";
		public const string AllMembers = "AllMembersField";
		public const string DefaultMemberImporterType = "<DefaultMemberImporterType>k__BackingField";
		public const string MemberImporters = "<memberImporters>k__BackingField";
		public const string InitializeData = "InitializeData";
		public const string ReadMemberFromFile = "ReadMemberFromFile";
		public const string CreateMefContainer = "<CreateMefContainer>k__BackingField";
		public const string CreateMefContainerBody = "CreateMefContainerBody";
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

		[Fact(DisplayName = MainViewModelMember.ReadMemberFromFile + "メソッド : 正常系", Skip = "ダイアログの表示をモックに入れ替えられるようになってから")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void ReadMemberFromFileTest()
		{

		}

		[Fact(DisplayName = MainViewModelMember.CreateMefContainerBody + "メソッド : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void CreateMefContainerTest()
		{
			// Act
			CompositionContainer actualReturn = (CompositionContainer)Instance.InvokePrivateMethod(MainViewModelMember.CreateMefContainerBody);

			// Assert
			// 動作に必要な最低限のインスタンス(自身のインスタンス)が取得できるか.
			IEnumerable<MatchGenerator.FileIO.IMemberImporter> importers = actualReturn.GetExportedValues<MatchGenerator.FileIO.IMemberImporter>();
			Assert.True(importers.Any(importer => importer is MatchGenerator.FileIO.DefaultImporter));
		}
	}

	public class MainViewModelInstanceUseMefTest : IDisposable
	{
		private CompositionContainer MefContainer;

		private MainViewModel Instance;

		public MainViewModelInstanceUseMefTest()
		{
			AggregateCatalog mefCatalog = new AggregateCatalog();
			mefCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
			MefContainer = new CompositionContainer(mefCatalog);

			Instance = new MainViewModel();
		}

		public void Dispose()
		{
			Utils.RestoreStaticField<MemberListViewModel>(MemberListViewModelMember.CreateMemberListViewModel);
		}

		[Fact(DisplayName = MainViewModelMember.InitializeData + "メソッド : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void InitializeDataTest()
		{
			// Arrange
			IList<IPerson> members = new List<IPerson>();
			IMemberListViewModel allMembers = new MemberListViewModelMock();
			// Expected data
			CompositionContainer expectedMefContainerField = MefContainer;
			IEnumerable<IMemberImporter> expectedMemberImportersField = MefContainer.GetExportedValues<IMemberImporter>();
			IMemberListViewModel expectedAllMembersField = allMembers;
			IList<string> expectedImportParamsFileName = new List<string> { "MemberData.csv" };
			IList<IList<IPerson>> expectedCreateMemberListViewModelParamsMemberData = new List<IList<IPerson>> { members };
			// Mock of CreateMefContainer
			Instance.SetPrivateField(MainViewModelMember.CreateMefContainer,
				new Func<CompositionContainer>(() => MefContainer));
			// Mock of DefaultMemberImporterType
			Instance.SetPrivateField(MainViewModelMember.DefaultMemberImporterType,
				typeof(DefaultImporterMock));
			// Mock of IMemberImporter.Import
			IEnumerable<IMemberImporter> importers = MefContainer.GetExportedValues<IMemberImporter>();
			IMemberImporter importer = importers.Single(i => i.GetType().Equals(typeof(DefaultImporterMock)));
			((DefaultImporterMock)importer).ImportFunc = _ => members;
			// Mock of MemberListViewModel.CreateMemberListViewModel
			IList<IList<IPerson>> actualCreateMemberListViewModelParamsMemberData = new List<IList<IPerson>>();
			Utils.SetStaticField<MemberListViewModel>(MemberListViewModelMember.CreateMemberListViewModel,
				new Func<IList<IPerson>, IMemberListViewModel>(persons =>
				{
					actualCreateMemberListViewModelParamsMemberData.Add(persons);
					return allMembers;
				}));

			// Act
			Instance.InvokePrivateMethod(MainViewModelMember.InitializeData);

			// Assert
			// MefContainer
			CompositionContainer actualMefContainerField = (CompositionContainer)Instance.GetPrivateField(MainViewModelMember.MefContainer);
			Assert.Same(expectedMefContainerField, actualMefContainerField);
			// MemberImporters
			IEnumerable<IMemberImporter> actualMemberImportersField = (IEnumerable<IMemberImporter>)Instance.GetPrivateField(MainViewModelMember.MemberImporters);
			Assert.True(expectedMemberImportersField.SequenceEqual(actualMemberImportersField));
			// AllMembers
			IMemberListViewModel actualAllMembersField = (IMemberListViewModel)Instance.GetPrivateField(MainViewModelMember.AllMembers);
			Assert.Same(expectedAllMembersField, actualAllMembersField);
			// Called methods
			IList<string> actualImportParamsFileName = ((DefaultImporterMock)importer).ImportParamsFileName;
			Assert.True(expectedImportParamsFileName.SequenceEqual(actualImportParamsFileName));
			Assert.True(expectedCreateMemberListViewModelParamsMemberData.SequenceEqual(actualCreateMemberListViewModelParamsMemberData));
		}
	}
}
