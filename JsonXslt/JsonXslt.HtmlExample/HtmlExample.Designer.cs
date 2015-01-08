namespace JsonXslt.HtmlExample
{
	partial class HtmlExample
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
			this.WebBrowserControl = new System.Windows.Forms.WebBrowser();
			this.ViewJsonButton = new System.Windows.Forms.Button();
			this.ViewXsltButton = new System.Windows.Forms.Button();
			this.ViewResultButton = new System.Windows.Forms.Button();
			this.ViewAsXmlButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// WebBrowserControl
			// 
			this.WebBrowserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.WebBrowserControl.Location = new System.Drawing.Point(12, 41);
			this.WebBrowserControl.MinimumSize = new System.Drawing.Size(20, 20);
			this.WebBrowserControl.Name = "WebBrowserControl";
			this.WebBrowserControl.Size = new System.Drawing.Size(655, 360);
			this.WebBrowserControl.TabIndex = 0;
			// 
			// ViewJsonButton
			// 
			this.ViewJsonButton.Location = new System.Drawing.Point(12, 12);
			this.ViewJsonButton.Name = "ViewJsonButton";
			this.ViewJsonButton.Size = new System.Drawing.Size(75, 23);
			this.ViewJsonButton.TabIndex = 1;
			this.ViewJsonButton.Text = "View JSON";
			this.ViewJsonButton.UseVisualStyleBackColor = true;
			this.ViewJsonButton.Click += new System.EventHandler(this.ViewJsonButton_Click);
			// 
			// ViewXsltButton
			// 
			this.ViewXsltButton.Location = new System.Drawing.Point(93, 12);
			this.ViewXsltButton.Name = "ViewXsltButton";
			this.ViewXsltButton.Size = new System.Drawing.Size(75, 23);
			this.ViewXsltButton.TabIndex = 2;
			this.ViewXsltButton.Text = "View XSLT";
			this.ViewXsltButton.UseVisualStyleBackColor = true;
			this.ViewXsltButton.Click += new System.EventHandler(this.ViewXsltButton_Click);
			// 
			// ViewResultButton
			// 
			this.ViewResultButton.Location = new System.Drawing.Point(262, 12);
			this.ViewResultButton.Name = "ViewResultButton";
			this.ViewResultButton.Size = new System.Drawing.Size(75, 23);
			this.ViewResultButton.TabIndex = 3;
			this.ViewResultButton.Text = "View Result";
			this.ViewResultButton.UseVisualStyleBackColor = true;
			this.ViewResultButton.Click += new System.EventHandler(this.ViewResultButton_Click);
			// 
			// ViewAsXmlButton
			// 
			this.ViewAsXmlButton.Location = new System.Drawing.Point(174, 12);
			this.ViewAsXmlButton.Name = "ViewAsXmlButton";
			this.ViewAsXmlButton.Size = new System.Drawing.Size(82, 23);
			this.ViewAsXmlButton.TabIndex = 4;
			this.ViewAsXmlButton.Text = "View As XML";
			this.ViewAsXmlButton.UseVisualStyleBackColor = true;
			this.ViewAsXmlButton.Click += new System.EventHandler(this.ViewAsXmlButton_Click);
			// 
			// HtmlExample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(679, 413);
			this.Controls.Add(this.ViewAsXmlButton);
			this.Controls.Add(this.ViewResultButton);
			this.Controls.Add(this.ViewXsltButton);
			this.Controls.Add(this.ViewJsonButton);
			this.Controls.Add(this.WebBrowserControl);
			this.Name = "HtmlExample";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "JsonXslt.HtmlExample";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser WebBrowserControl;
		private System.Windows.Forms.Button ViewJsonButton;
		private System.Windows.Forms.Button ViewXsltButton;
		private System.Windows.Forms.Button ViewResultButton;
		private System.Windows.Forms.Button ViewAsXmlButton;
	}
}

