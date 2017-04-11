using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Xunit;
using Moq;
using MatchGenerator.ViewModel;
using MatchGenerator.Model;
using MatchGenerator.FileIO;
using MatchGeneratorTest.FileIO;

namespace MatchGeneratorTest.ViewModel
{

	internal struct MainViewModelMember
	{
		public const string MefContainer = "mefContainers";
		public const string AllMembers = "allMembersField";
		public const string AttendanceMembers = "AttendanceMembersField";
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
			// Act
			IMemberListViewModel actualReturn = Instance.AllMembers;

			// Assert
			Assert.NotNull(actualReturn);
			Assert.IsType<MemberListViewModel>(actualReturn);
		}

		[Fact(DisplayName = nameof(MainViewModel.AllMembers) + ".Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void AllMembersSetterTest()
		{
			// Arrange
			IMemberListViewModel inputAllMembers = new Mock<IMemberListViewModel>().Object;
			IMemberListViewModel expectedAllMembersField = inputAllMembers;

			// Act
			Instance.AllMembers = inputAllMembers;

			// Assert
			IMemberListViewModel actualAllMembersField = (IMemberListViewModel)Instance.GetPrivateField(MainViewModelMember.AllMembers);
			Assert.Same(expectedAllMembersField, actualAllMembersField);
		}

		[Fact(DisplayName = nameof(MainViewModel.AttendanceMembers) + ".Getterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void AttendanceMembersGetterTest()
		{
			// Arrange
			IMemberListViewModel attendanceMembersFieldValue = new Mock<IMemberListViewModel>().Object;
			Instance.SetPrivateField(MainViewModelMember.AttendanceMembers, attendanceMembersFieldValue);
			IMemberListViewModel expectedReturn = attendanceMembersFieldValue;

			// Act
			IMemberListViewModel actualReturn = Instance.AttendanceMembers;

			// Assert
			Assert.Same(expectedReturn, actualReturn);
		}

		[Fact(DisplayName = nameof(MainViewModel.AttendanceMembers) + ".Setterプロパティ : 正常系")]
		[Trait("category", "ViewModel")]
		[Trait("type", "正常系")]
		public void AttendanceMembersSetterTest()
		{
			// Arrange
			IMemberListViewModel inputAttendanceMembers = new Mock<IMemberListViewModel>().Object;
			IMemberListViewModel expectedAttendanceMembersField = inputAttendanceMembers;
			// Event handler
			IList<object> actualPropertyChangedParamsSender = new List<object>();
			IList<PropertyChangedEventArgs> actualPropertyChangedParamsE = new List<PropertyChangedEventArgs>();
			Instance.PropertyChanged += (s, e) =>
			{
				actualPropertyChangedParamsSender.Add(s);
				actualPropertyChangedParamsE.Add(e);
			};
			// Expected data
			IList<object> expectedPropertyChangedParamsSender = new List<object> { Instance };
			IList<string> expectedPropertyChangedParamsE = new List<string> { "AttendanceMembers" };

			// Act
			Instance.AttendanceMembers = inputAttendanceMembers;

			// Assert
			IMemberListViewModel actualAttendanceMembersField = (IMemberListViewModel)Instance.GetPrivateField(MainViewModelMember.AttendanceMembers);
			Assert.Same(expectedAttendanceMembersField, actualAttendanceMembersField);
			// Called handler
			Assert.True(actualPropertyChangedParamsSender.SequenceEqual(expectedPropertyChangedParamsSender));
			Assert.True(actualPropertyChangedParamsE.Select(e => e.PropertyName).SequenceEqual(expectedPropertyChangedParamsE));
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
			IMemberListViewModel allMembers = new Mock<IMemberListViewModel>().Object;
			IMemberListViewModel attendanceMembers = new Mock<IMemberListViewModel>().Object;
			// Setup mocks
			Mock<IMemberImporter> importerMock = new Mock<IMemberImporter>();
			importerMock.Setup(importer => importer.Import("MemberData.csv")).Returns(members);
			MefContainer.ComposeExportedValue(importerMock.Object);
			MefContainer.SatisfyImportsOnce(Instance);
			// Expected data
			CompositionContainer expectedMefContainerField = MefContainer;
			IEnumerable<IMemberImporter> expectedMemberImportersField = MefContainer.GetExportedValues<IMemberImporter>();
			IMemberListViewModel expectedAllMembersField = allMembers;
			IMemberListViewModel expectedAttendanceMembersField = attendanceMembers;
			IList<IList<IPerson>> expectedCreateMemberListViewModelParamsMemberData = new List<IList<IPerson>> { members, new List<IPerson>() };
			// Mock of CreateMefContainer
			Instance.SetPrivateField(MainViewModelMember.CreateMefContainer,
				new Func<CompositionContainer>(() => MefContainer));
			// Mock of DefaultMemberImporterType
			Instance.SetPrivateField(MainViewModelMember.DefaultMemberImporterType,
				importerMock.Object.GetType());
			// Mock of MemberListViewModel.CreateMemberListViewModel
			int calledCount = 0;
			IList<IList<IPerson>> actualCreateMemberListViewModelParamsMemberData = new List<IList<IPerson>>();
			IList<IMemberListViewModel> createMemberListViewModelReturn = new List<IMemberListViewModel> { allMembers, attendanceMembers };
			Utils.SetStaticField<MemberListViewModel>(MemberListViewModelMember.CreateMemberListViewModel,
				new Func<IList<IPerson>, IMemberListViewModel>(persons =>
				{
					actualCreateMemberListViewModelParamsMemberData.Add(persons);
					return createMemberListViewModelReturn[calledCount++];
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
			// AttendanceMembers
			IMemberListViewModel actualAttendanceMembersField = (IMemberListViewModel)Instance.GetPrivateField(MainViewModelMember.AttendanceMembers);
			Assert.Same(expectedAttendanceMembersField, actualAttendanceMembersField);
			// Called methods
			Assert.Equal(2, actualCreateMemberListViewModelParamsMemberData.Count);
			Assert.Same(expectedCreateMemberListViewModelParamsMemberData[0], actualCreateMemberListViewModelParamsMemberData[0]);
			Assert.True(expectedCreateMemberListViewModelParamsMemberData[1].SequenceEqual(actualCreateMemberListViewModelParamsMemberData[1]));
		}
	}
}
