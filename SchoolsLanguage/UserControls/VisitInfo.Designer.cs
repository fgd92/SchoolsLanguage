namespace SchoolsLanguage.UserControls
{
    partial class VisitInfo
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_showFiles = new System.Windows.Forms.Button();
            this.lbl_nameService = new System.Windows.Forms.Label();
            this.lbl_date = new System.Windows.Forms.Label();
            this.lbl_countFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_showFiles
            // 
            this.btn_showFiles.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_showFiles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_showFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_showFiles.Location = new System.Drawing.Point(247, 0);
            this.btn_showFiles.Name = "btn_showFiles";
            this.btn_showFiles.Size = new System.Drawing.Size(58, 100);
            this.btn_showFiles.TabIndex = 0;
            this.btn_showFiles.Text = "→";
            this.btn_showFiles.UseVisualStyleBackColor = true;
            this.btn_showFiles.Click += new System.EventHandler(this.btn_showFiles_Click);
            // 
            // lbl_nameService
            // 
            this.lbl_nameService.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_nameService.Location = new System.Drawing.Point(0, 0);
            this.lbl_nameService.Name = "lbl_nameService";
            this.lbl_nameService.Size = new System.Drawing.Size(247, 37);
            this.lbl_nameService.TabIndex = 1;
            this.lbl_nameService.Text = "Name";
            // 
            // lbl_date
            // 
            this.lbl_date.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_date.Location = new System.Drawing.Point(0, 37);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(241, 27);
            this.lbl_date.TabIndex = 2;
            this.lbl_date.Text = "Date";
            this.lbl_date.Click += new System.EventHandler(this.lbl_date_Click);
            // 
            // lbl_countFile
            // 
            this.lbl_countFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_countFile.Location = new System.Drawing.Point(0, 64);
            this.lbl_countFile.Name = "lbl_countFile";
            this.lbl_countFile.Size = new System.Drawing.Size(247, 36);
            this.lbl_countFile.TabIndex = 3;
            this.lbl_countFile.Text = "Count file";
            // 
            // VisitInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbl_countFile);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.lbl_nameService);
            this.Controls.Add(this.btn_showFiles);
            this.Name = "VisitInfo";
            this.Size = new System.Drawing.Size(305, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_showFiles;
        private System.Windows.Forms.Label lbl_nameService;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.Label lbl_countFile;
    }
}
