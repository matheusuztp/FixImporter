namespace FixImporter
{
    partial class FixImporter
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtData = new RichTextBox();
            lblTitle = new Label();
            lblMessage = new Label();
            btnProcess = new Button();
            lblInfo = new Label();
            cbSQL = new CheckBox();
            SuspendLayout();
            // 
            // txtData
            // 
            txtData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtData.Location = new Point(12, 58);
            txtData.Name = "txtData";
            txtData.Size = new Size(468, 272);
            txtData.TabIndex = 0;
            txtData.Text = "";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(352, 15);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Este aplicativo vai organizar as listas dos dados das planilhas excel";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(12, 40);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(290, 15);
            lblMessage.TabIndex = 2;
            lblMessage.Text = "Cole aqui uma coluna de strings da planilha analisada";
            // 
            // btnProcess
            // 
            btnProcess.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnProcess.Location = new Point(405, 336);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(75, 36);
            btnProcess.TabIndex = 3;
            btnProcess.Text = "Iniciar";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(12, 354);
            lblInfo.MaximumSize = new Size(390, 15);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(388, 15);
            lblInfo.TabIndex = 4;
            lblInfo.Text = "Informaçõessssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
            lblInfo.Visible = false;
            // 
            // cbSQL
            // 
            cbSQL.AutoSize = true;
            cbSQL.Location = new Point(12, 332);
            cbSQL.Name = "cbSQL";
            cbSQL.Size = new Size(122, 19);
            cbSQL.TabIndex = 5;
            cbSQL.Text = "Pronto para o SQL";
            cbSQL.UseVisualStyleBackColor = true;
            // 
            // FixImporter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 384);
            Controls.Add(cbSQL);
            Controls.Add(lblInfo);
            Controls.Add(btnProcess);
            Controls.Add(lblMessage);
            Controls.Add(lblTitle);
            Controls.Add(txtData);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(416, 423);
            Name = "FixImporter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FixImporter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox txtData;
        private Label lblTitle;
        private Label lblMessage;
        private Button btnProcess;
        private Label lblInfo;
        private CheckBox cbSQL;
    }
}
