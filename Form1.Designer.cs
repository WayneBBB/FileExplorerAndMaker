namespace SimpleFileManager
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            backButton = new System.Windows.Forms.Button();
            goButton = new System.Windows.Forms.Button();
            listView1 = new System.Windows.Forms.ListView();
            iconList = new System.Windows.Forms.ImageList(components);
            filePathTextBox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            fileNameLabel = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            fileTypeLabel = new System.Windows.Forms.Label();
            CreateSIFD = new System.Windows.Forms.Button();
            LoadPictures = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // backButton
            // 
            backButton.Location = new System.Drawing.Point(2, 10);
            backButton.Margin = new System.Windows.Forms.Padding(2);
            backButton.Name = "backButton";
            backButton.Size = new System.Drawing.Size(65, 22);
            backButton.TabIndex = 0;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += backButton_Click;
            // 
            // goButton
            // 
            goButton.Location = new System.Drawing.Point(1070, 10);
            goButton.Margin = new System.Windows.Forms.Padding(2);
            goButton.Name = "goButton";
            goButton.Size = new System.Drawing.Size(65, 22);
            goButton.TabIndex = 1;
            goButton.Text = "Go";
            goButton.UseVisualStyleBackColor = true;
            goButton.Click += goButton_Click;
            // 
            // listView1
            // 
            listView1.LargeImageList = iconList;
            listView1.Location = new System.Drawing.Point(2, 40);
            listView1.Margin = new System.Windows.Forms.Padding(2);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(1133, 516);
            listView1.SmallImageList = iconList;
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.ItemSelectionChanged += listView1_ItemSelectionChanged;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            listView1.MouseDoubleClick += listView1_MouseDoubleClick;
            // 
            // iconList
            // 
            iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            iconList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iconList.ImageStream");
            iconList.TransparentColor = System.Drawing.Color.Transparent;
            iconList.Images.SetKeyName(0, "folder.png");
            iconList.Images.SetKeyName(1, "folder2.png");
            iconList.Images.SetKeyName(2, "file.png");
            iconList.Images.SetKeyName(3, "doc.png");
            iconList.Images.SetKeyName(4, "pdf.png");
            iconList.Images.SetKeyName(5, "mp3.png");
            iconList.Images.SetKeyName(6, "mp4.png");
            iconList.Images.SetKeyName(7, "exe.png");
            iconList.Images.SetKeyName(8, "unknown.png");
            iconList.Images.SetKeyName(9, "png.png");
            iconList.Images.SetKeyName(10, "folder64.png");
            // 
            // filePathTextBox
            // 
            filePathTextBox.Location = new System.Drawing.Point(74, 13);
            filePathTextBox.Margin = new System.Windows.Forms.Padding(2);
            filePathTextBox.Name = "filePathTextBox";
            filePathTextBox.Size = new System.Drawing.Size(992, 23);
            filePathTextBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 572);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 15);
            label1.TabIndex = 4;
            label1.Text = "FileName";
            // 
            // fileNameLabel
            // 
            fileNameLabel.AutoSize = true;
            fileNameLabel.Location = new System.Drawing.Point(96, 572);
            fileNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new System.Drawing.Size(17, 15);
            fileNameLabel.TabIndex = 5;
            fileNameLabel.Text = "--";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(205, 576);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 15);
            label3.TabIndex = 6;
            label3.Text = "File Type";
            // 
            // fileTypeLabel
            // 
            fileTypeLabel.AutoSize = true;
            fileTypeLabel.Location = new System.Drawing.Point(276, 576);
            fileTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            fileTypeLabel.Name = "fileTypeLabel";
            fileTypeLabel.Size = new System.Drawing.Size(17, 15);
            fileTypeLabel.TabIndex = 7;
            fileTypeLabel.Text = "--";
            // 
            // CreateSIFD
            // 
            CreateSIFD.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            CreateSIFD.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 0, 64);
            CreateSIFD.FlatAppearance.BorderSize = 4;
            CreateSIFD.Location = new System.Drawing.Point(978, 565);
            CreateSIFD.Margin = new System.Windows.Forms.Padding(4);
            CreateSIFD.Name = "CreateSIFD";
            CreateSIFD.Size = new System.Drawing.Size(88, 26);
            CreateSIFD.TabIndex = 8;
            CreateSIFD.Text = "Create SIFD";
            CreateSIFD.UseVisualStyleBackColor = false;
            CreateSIFD.Click += button1_Click;
            // 
            // LoadPictures
            // 
            LoadPictures.BackColor = System.Drawing.Color.Cyan;
            LoadPictures.Location = new System.Drawing.Point(848, 568);
            LoadPictures.Name = "LoadPictures";
            LoadPictures.Size = new System.Drawing.Size(111, 23);
            LoadPictures.TabIndex = 9;
            LoadPictures.Text = "Load Pictures";
            LoadPictures.UseVisualStyleBackColor = false;
            LoadPictures.Click += LoadPictures_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1122, 610);
            Controls.Add(LoadPictures);
            Controls.Add(CreateSIFD);
            Controls.Add(fileTypeLabel);
            Controls.Add(label3);
            Controls.Add(fileNameLabel);
            Controls.Add(label1);
            Controls.Add(filePathTextBox);
            Controls.Add(listView1);
            Controls.Add(goButton);
            Controls.Add(backButton);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label fileTypeLabel;
        private System.Windows.Forms.Button CreateSIFD;
        private System.Windows.Forms.Button LoadPictures;
    }
}

