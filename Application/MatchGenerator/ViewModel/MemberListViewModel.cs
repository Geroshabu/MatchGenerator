using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// メンバーのリストのViewModel
	/// </summary>
	internal class MemberListViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase, IMemberListViewModel
	{
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
			}
		}

		private IList<IMemberListItemViewModel> SelectedMembersField;
		/// <summary>
		/// 現在選択されているメンバー
		/// </summary>
		public IList<IMemberListItemViewModel> SelectedMembers
		{
			get
			{
				return SelectedMembersField;
			}

			set
			{
				SetProperty(ref SelectedMembersField, value);
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

		public MemberListViewModel()
		{
			Members = new List<MemberListItemViewModel>
			{
				new MemberListItemViewModel(new MatchGenerator.Core.Person(new string[] {"真田", "い", "M", "0", "う" })),
				new MemberListItemViewModel(new MatchGenerator.Core.Person(new string[] {"あ", "い", "M", "0", "う" })),
				new MemberListItemViewModel(new MatchGenerator.Core.Person(new string[] {"え", "お", "M", "0", "か" }))
			}
			.Select(item => { item.MemberClick += Item_MemberClick; item.MemberExtendedClick += Item_MemberExtendedClick; return item; })
			.Cast<IMemberListItemViewModel>()
			.ToList();
		}

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
