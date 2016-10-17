using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchGenerator.Model;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// メンバーのリストのViewModel
	/// </summary>
	internal class MemberListViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase, IMemberListViewModel
	{
		/// <summary>
		/// このViewModelが表現するModelを取得する
		/// </summary>
		public IList<IPerson> Model
		{
			get
			{
				return Members.Select(vm => vm.Model).ToList();
			}
		}

		private IList<IMemberListItemViewModel> MembersField;
		/// <summary>
		/// リストに表示するすべてのメンバー
		/// </summary>
		public IList<IMemberListItemViewModel> Members
		{
			get
			{
				return MembersField;
			}

			set
			{
				SetProperty(ref MembersField, value);
				OnPropertyChanged(nameof(SelectedMembers));
			}
		}
		
		/// <summary>
		/// 現在選択されているメンバー
		/// </summary>
		public IList<IMemberListItemViewModel> SelectedMembers
		{
			get
			{
				return MembersField.Where(item => item.IsChecked).ToList();
			}

			set
			{
				foreach (IMemberListItemViewModel item in value)
				{
					item.IsChecked = true;
				}
				foreach(IMemberListItemViewModel otherItem in Members.Except(value))
				{
					otherItem.IsChecked = false;
				}
				OnPropertyChanged(nameof(SelectedMembers));
			}
		}

		private IMemberListItemViewModel LastClickedMemberField;
		/// <summary>
		/// 最後にチェックボックスがクリックされたメンバー
		/// </summary>
		public IMemberListItemViewModel LastClickedMember
		{
			get
			{
				return LastClickedMemberField;
			}

			set
			{
				SetProperty(ref LastClickedMemberField, value);
			}
		}

		private MemberListViewModel(IList<Model.IPerson> memberData)
		{
			Members = memberData
				.Select(
					personData =>
					{
						IMemberListItemViewModel memberListItemViewModel = MemberListItemViewModel.CreateMemberListItemViewModel(personData);
						memberListItemViewModel.MemberClick += Item_MemberClick;
						memberListItemViewModel.MemberExtendedClick += Item_MemberExtendedClick;
						return memberListItemViewModel;
					})
				.ToList();
		}

		public static Func<IList<Model.IPerson>, IMemberListViewModel> CreateMemberListViewModel { get; } =
			memberData => new MemberListViewModel(memberData);

		/// <summary>
		/// メンバーリストのメンバーがクリックされたときのイベントハンドラ
		/// </summary>
		/// <param name="sender">クリックされたメンバーのViewModel (<see cref="IMemberListItemViewModel"/>)</param>
		/// <param name="e">イベントデータ</param>
		/// <exception cref="ArgumentException"><paramref name="sender"/>が<see cref="IMemberListItemViewModel"/>でない.</exception>
		private void Item_MemberClick(object sender, MemberClickEventArgs e)
		{
			if (!(sender is IMemberListItemViewModel)) { throw new ArgumentException(); }

			LastClickedMember = sender as IMemberListItemViewModel;
		}

		/// <summary>
		/// メンバーリストのメンバーが連続選択クリックされたときのイベントハンドラ
		/// </summary>
		/// <param name="sender">クリックされたメンバーのViewModel (<see cref="IMemberListItemViewModel"/>)</param>
		/// <param name="e">イベントデータ</param>
		/// <exception cref="ArgumentException"><paramref name="sender"/>が<see cref="IMemberListItemViewModel"/>でない.</exception>
		private void Item_MemberExtendedClick(object sender, MemberClickEventArgs e)
		{
			if (!(sender is IMemberListItemViewModel))
			{
				throw new ArgumentException();
			}

			if (LastClickedMember != null)
			{
				int clickedIndex = Members.IndexOf((IMemberListItemViewModel)sender);
				int lastClickedIndex = Members.IndexOf(LastClickedMember);

				int firstIndex = Math.Min(clickedIndex, lastClickedIndex);
				int selectCount = Math.Abs(clickedIndex - lastClickedIndex) + 1;
				bool nextIsCheckedState = LastClickedMember.IsChecked;

				IEnumerable<IMemberListItemViewModel> targetMembers =
					Members.Skip(firstIndex).Take(selectCount);
				foreach (IMemberListItemViewModel member in targetMembers)
				{
					member.IsChecked = nextIsCheckedState;
				}
			}

			LastClickedMember = sender as IMemberListItemViewModel;
		}
	}
}
