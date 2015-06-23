using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace BookDbSharp.WinForms
{
	public class ItemReorderedEventArgs : EventArgs
	{
		public ListViewItem Item { get; private set; }
		public int OldPosition { get; private set; }
		public int NewPosition { get; private set; }

		public ItemReorderedEventArgs(ListViewItem item, int oldPosition, int newPosition)
		{
			Item = item;
			OldPosition = oldPosition;
			NewPosition = newPosition;
		}
	}

	public class ListViewEx : ListView
	{
		public event EventHandler<ItemReorderedEventArgs> ItemReordered;

		private const string REORDER = "Reorder";

		private bool allowRowReorder = true;
		public bool AllowRowReorder
		{
			get
			{
				return this.allowRowReorder;
			}
			set
			{
				this.allowRowReorder = value;
				base.AllowDrop = value;
			}
		}

		public new SortOrder Sorting
		{
			get
			{
				return SortOrder.None;
			}
			set
			{
				base.Sorting = SortOrder.None;
			}
		}

		public ListViewEx()
			: base()
		{
			this.AllowRowReorder = true;
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);

			if (IsReordering(e))
			{
				if (base.SelectedItems.Count == 0)
				{
					return;
				}

				Point cp = base.PointToClient(new Point(e.X, e.Y));
				ListViewItem dragToItem = base.GetItemAt(cp.X, cp.Y);
				if (dragToItem == null)
				{
					return;
				}
				int dropIndex = dragToItem.Index;
				int newPosition = dragToItem.Index;
				int oldPosition = base.SelectedItems[0].Index;

				//TODO: what if there are multiple items reordered?
				var movedItem = base.SelectedItems[0];

				if (dropIndex > oldPosition)
				{
					dropIndex++;
				}

				ArrayList insertItems = new ArrayList(base.SelectedItems.Count);
				foreach (ListViewItem item in base.SelectedItems)
				{
					insertItems.Add(item.Clone());
				}
				for (int i = insertItems.Count - 1; i >= 0; i--)
				{
					ListViewItem insertItem = (ListViewItem) insertItems[i];
					base.Items.Insert(dropIndex, insertItem);
				}
				foreach (ListViewItem removeItem in base.SelectedItems)
				{
					base.Items.Remove(removeItem);
				}

				OnItemReordered(movedItem, oldPosition, newPosition);
			}
		}

		private void OnItemReordered(ListViewItem item, int oldPosition, int newPosition)
		{
			var handlers = ItemReordered;

			if(handlers != null)
				handlers(this, new ItemReorderedEventArgs(item, oldPosition, newPosition));
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			bool isReordering = IsReordering(e);

			if (isReordering)
			{
				if (!this.AllowRowReorder)
				{
					e.Effect = DragDropEffects.None;
					return;
				}

				Point cp = base.PointToClient(new Point(e.X, e.Y));
				ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
				if (hoverItem == null)
				{
					e.Effect = DragDropEffects.None;
					return;
				}
				foreach (ListViewItem moveItem in base.SelectedItems)
				{
					if (moveItem.Index == hoverItem.Index)
					{
						e.Effect = DragDropEffects.None;
						hoverItem.EnsureVisible();
						return;
					}
				}
			}

			if (isReordering)
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}

			base.OnDragOver(e);
		}

		private bool IsReordering(DragEventArgs e)
		{
			if (!this.AllowRowReorder)
			{
				return false;
			}

			if (!e.Data.GetDataPresent(DataFormats.Text))
			{
				return false;
			}

			String text = (String) e.Data.GetData(REORDER.GetType());
			if (text.CompareTo(REORDER) != 0)
			{
				return false;
			}

			return true;
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			if (IsReordering(e))
			{
				String text = (String)e.Data.GetData(REORDER.GetType());
				if (text.CompareTo(REORDER) == 0)
				{
					e.Effect = DragDropEffects.Move;
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}

			base.OnDragEnter(e);
		}

		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			base.OnItemDrag(e);
			if (!this.AllowRowReorder)
			{
				return;
			}
			base.DoDragDrop(REORDER, DragDropEffects.Move);
		}
	}

}
