namespace SchoolsLanguage.UserControls
{
    partial class Buttons
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
            this.btn_showClients = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_showClients
            // 
            this.btn_showClients.Location = new System.Drawing.Point(4, 4);
            this.btn_showClients.Name = "btn_showClients";
            this.btn_showClients.Size = new System.Drawing.Size(133, 63);
            this.btn_showClients.TabIndex = 0;
            this.btn_showClients.Text = "Клиенты";
            this.btn_showClients.UseVisualStyleBackColor = true;
            this.btn_showClients.Click += new System.EventHandler(this.btn_showClients_Click);
            // 
            // Buttons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_showClients);
            this.Name = "Buttons";
            this.Size = new System.Drawing.Size(671, 362);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_showClients;
    }
}
