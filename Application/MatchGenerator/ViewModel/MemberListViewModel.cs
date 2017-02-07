using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
		/// このViewModelが表現するModel
		/// </summary>
		private ObservableCollection<IPerson> model;

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
			private set
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
		/// コレクションが変更された場合に発生する.
		/// </summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

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

		public MemberListViewModel(ObservableCollection<IPerson> modelData)
		{
			model = modelData;
            Members = model.Select(person => new MemberListItemViewModel(person)).Cast<IMemberListItemViewModel>().ToList();
            foreach (IMemberListItemViewModel memberViewModel in Members)
            {
                memberViewModel.MemberClick += Item_MemberClick;
                memberViewModel.MemberExtendedClick += Item_MemberExtendedClick;
            }
		}

		private MemberListViewModel(IList<Model.IPerson> memberData)
		{
			Members = memberData
				.Select(
					personData =>
					{
						IMemberListItemViewModel memberListItemViewModel = new MemberListItemViewModel(personData);
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

		/// <summary>
		/// メンバーのコレクションを反復処理する列挙子を返す.
		/// </summary>
		/// <returns>メンバーのコレクションの反復処理に使用できる列挙子</returns>
		public IEnumerator<IPerson> GetEnumerator()
		{
			return Model.GetEnumerator();
		}

		/// <summary>
		/// メンバーのコレクションを反復処理する列挙子を返す.
		/// </summary>
		/// <returns>メンバーのコレクションの反復処理に使用できる列挙子</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Model.GetEnumerator();
		}
	}
}
