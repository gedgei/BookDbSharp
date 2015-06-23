namespace BookDbSharp.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tbSearchString = new System.Windows.Forms.TextBox();
			this.lvSearchResults = new System.Windows.Forms.ListView();
			this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.lvReadingQueue = new BookDbSharp.WinForms.ListViewEx();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbSearchString
			// 
			this.tbSearchString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSearchString.Location = new System.Drawing.Point(3, 8);
			this.tbSearchString.Name = "tbSearchString";
			this.tbSearchString.Size = new System.Drawing.Size(596, 22);
			this.tbSearchString.TabIndex = 1;
			this.tbSearchString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSearchString_KeyUp);
			// 
			// lvSearchResults
			// 
			this.lvSearchResults.AllowDrop = true;
			this.lvSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName});
			this.lvSearchResults.Location = new System.Drawing.Point(3, 36);
			this.lvSearchResults.MultiSelect = false;
			this.lvSearchResults.Name = "lvSearchResults";
			this.lvSearchResults.Size = new System.Drawing.Size(596, 455);
			this.lvSearchResults.TabIndex = 0;
			this.lvSearchResults.UseCompatibleStateImageBehavior = false;
			this.lvSearchResults.View = System.Windows.Forms.View.Details;
			this.lvSearchResults.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvSearchResults_ItemDrag);
			this.lvSearchResults.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvSearchResults_DragEnter);
			this.lvSearchResults.DragOver += new System.Windows.Forms.DragEventHandler(this.lvSearchResults_DragOver);
			this.lvSearchResults.DoubleClick += new System.EventHandler(this.lvSearchResults_DoubleClick);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 574;
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.tbSearchString);
			this.splitContainer.Panel1.Controls.Add(this.lvSearchResults);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.lvReadingQueue);
			this.splitContainer.Size = new System.Drawing.Size(894, 506);
			this.splitContainer.SplitterDistance = 602;
			this.splitContainer.TabIndex = 2;
			// 
			// lvReadingQueue
			// 
			this.lvReadingQueue.AllowDrop = true;
			this.lvReadingQueue.AllowRowReorder = true;
			this.lvReadingQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvReadingQueue.FullRowSelect = true;
			this.lvReadingQueue.Location = new System.Drawing.Point(3, 8);
			this.lvReadingQueue.MultiSelect = false;
			this.lvReadingQueue.Name = "lvReadingQueue";
			this.lvReadingQueue.Size = new System.Drawing.Size(273, 483);
			this.lvReadingQueue.TabIndex = 0;
			this.lvReadingQueue.UseCompatibleStateImageBehavior = false;
			this.lvReadingQueue.View = System.Windows.Forms.View.List;
			this.lvReadingQueue.ItemReordered += new System.EventHandler<BookDbSharp.WinForms.ItemReorderedEventArgs>(this.lvReadingQueue_ItemReordered);
			this.lvReadingQueue.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvReadingQueue_DragDrop);
			this.lvReadingQueue.DragOver += new System.Windows.Forms.DragEventHandler(this.lvReadingQueue_DragOver);
			this.lvReadingQueue.DoubleClick += new System.EventHandler(this.lvReadingQueue_DoubleClick);
			this.lvReadingQueue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvReadingQueue_KeyUp);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 481);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(894, 25);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(70, 20);
			this.lblStatus.Text = "Starting...";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(894, 506);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainer);
			this.Name = "MainForm";
			this.Text = "BookDb#";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox tbSearchString;
		private System.Windows.Forms.ListView lvSearchResults;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.SplitContainer splitContainer;
		private ListViewEx lvReadingQueue;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

