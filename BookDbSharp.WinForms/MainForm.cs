using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace BookDbSharp.WinForms
{
    public partial class MainForm : Form
    {
	    private readonly BookDbApplicationService applicationService;
	    private static readonly ILog Log = LogManager.GetLogger(typeof (MainForm));

	    public MainForm(BookDbApplicationService applicationService)
	    {
		    this.applicationService = applicationService;

		    InitializeComponent();
	    }

	    #region Event handlers

	    private void MainForm_Load(object sender, EventArgs e)
	    {
		    lblStatus.Text = "Loading data...";

		    applicationService.Load();
			ShowReadingQueue(applicationService.GetReadingQueue());
			ShowList(applicationService.Books.OrderBy(x => x.Title));

		    this.ActiveControl = tbSearchString;

		    lblStatus.Text = "Refreshing library...";
		    Task.Factory.StartNew(() => applicationService.RefreshLibrary(status => this.Invoke(new MethodInvoker(() => lblStatus.Text = status))));
	    }

	    private void tbSearchString_KeyUp(object sender, KeyEventArgs e)
	    {
		    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
		    {
			    if (e.KeyCode == Keys.Escape)
				    tbSearchString.Text = string.Empty;

			    var result = applicationService.Search(tbSearchString.Text);

			    // if it's not a search, order it by name
			    if (string.IsNullOrEmpty(tbSearchString.Text))
				    result = result.OrderBy(x => x.Title);

			    ShowList(result);
		    }
	    }

	    private void lvSearchResults_DoubleClick(object sender, EventArgs e)
	    {
		    if (lvSearchResults.SelectedItems.Count == 0)
			    return;

		    var selectedItem = lvSearchResults.SelectedItems[0];
		    var book = (Book) selectedItem.Tag;

		    OpenBook(book);
	    }

	    private void lvSearchResults_ItemDrag(object sender, ItemDragEventArgs e)
	    {
		    if (lvSearchResults.SelectedItems.Count == 0)
			    return;

		    var selectedItem = lvSearchResults.SelectedItems[0];

		    base.DoDragDrop((Book) selectedItem.Tag, DragDropEffects.Move);
	    }

	    private void lvSearchResults_DragOver(object sender, DragEventArgs e)
	    {
		    e.Effect = DragDropEffects.Move;
	    }

	    private void lvSearchResults_DragEnter(object sender, DragEventArgs e)
	    {
		    if (e.Data.GetDataPresent(typeof (Book)))
		    {
			    e.Effect = DragDropEffects.Move;
		    }
		    else
		    {
			    e.Effect = DragDropEffects.None;
		    }
	    }

	    private void lvReadingQueue_DragOver(object sender, DragEventArgs e)
	    {
		    if (e.Data.GetDataPresent(typeof (Book)))
		    {
			    e.Effect = DragDropEffects.Move;
		    }
	    }

	    private void lvReadingQueue_DragDrop(object sender, DragEventArgs e)
	    {
		    if (e.Data.GetDataPresent(typeof (Book)))
		    {
			    var book = (Book) e.Data.GetData(typeof (Book));

			    // if is already in reading queue, do not add it again
			    foreach (ListViewItem existingItem in lvReadingQueue.Items)
			    {
				    if (((Book) existingItem.Tag).Id == book.Id)
					    return;
			    }

			    var insertIndex = lvReadingQueue.Items.Count;

			    Point cp = lvReadingQueue.PointToClient(new Point(e.X, e.Y));
			    ListViewItem dragToItem = lvReadingQueue.GetItemAt(cp.X, cp.Y);
			    if (dragToItem != null)
			    {
				    insertIndex = dragToItem.Index;
			    }

			    var item = new ListViewItem(book.Title);
			    item.Tag = book;

			    lvReadingQueue.Items.Insert(insertIndex, item);

			    applicationService.QueueForReading(book.Id, insertIndex);
		    }
	    }

	    private void lvReadingQueue_DoubleClick(object sender, EventArgs e)
	    {
		    if (lvReadingQueue.SelectedItems.Count == 0)
			    return;

		    var selectedItem = lvReadingQueue.SelectedItems[0];
		    var book = (Book) selectedItem.Tag;

		    OpenBook(book);
	    }

	    private void lvReadingQueue_ItemReordered(object sender, ItemReorderedEventArgs e)
	    {
		    var book = (Book) e.Item.Tag;
		    applicationService.ReorderReadingQueue(book.Id, e.NewPosition);
	    }

	    private void lvReadingQueue_KeyUp(object sender, KeyEventArgs e)
	    {
		    if (e.KeyCode == Keys.Delete)
		    {
			    if (lvReadingQueue.SelectedItems.Count == 0)
				    return;

			    var item = lvReadingQueue.SelectedItems[0];
			    var book = (Book) item.Tag;
			    applicationService.RemoveFromReadingQueue(book.Id);

			    lvReadingQueue.Items.Remove(item);
		    }
	    }

	    #endregion

	    #region Methods

	    private void ShowReadingQueue(IEnumerable<ReadingQueueItem> readingQueue)
	    {
		    foreach (var item in readingQueue)
		    {
			    var lvItem = new ListViewItem(item.Book.Title);
			    lvItem.Tag = item.Book;

			    lvReadingQueue.Items.Add(lvItem);
		    }
	    }

	    private void ShowList(IEnumerable<Book> books)
	    {
		    lvSearchResults.BeginUpdate();

		    lvSearchResults.Items.Clear();

		    foreach (var book in books)
		    {
			    ListViewItem listViewItem = lvSearchResults.Items.Add(book.Title);
			    listViewItem.Tag = book;
		    }

		    lvSearchResults.EndUpdate();
	    }

	    private static void OpenBook(Book book)
	    {
		    Process proc = new Process();
		    proc.EnableRaisingEvents = false;
		    proc.StartInfo.FileName = book.Path;
		    proc.Start();
	    }

	    #endregion
    }
}
