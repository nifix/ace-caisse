namespace AceCaisse
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.launchParse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.importTotal = new System.Windows.Forms.Label();
            this.importProgressBar = new System.Windows.Forms.ProgressBar();
            this.chooseImportFile = new System.Windows.Forms.Button();
            this.importFileTextbox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listRemises = new System.Windows.Forms.ListView();
            this.export = new System.Windows.Forms.Button();
            this.pickImportFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxInput.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.launchParse);
            this.groupBoxInput.Controls.Add(this.label1);
            this.groupBoxInput.Controls.Add(this.labelStatus);
            this.groupBoxInput.Controls.Add(this.importTotal);
            this.groupBoxInput.Controls.Add(this.importProgressBar);
            this.groupBoxInput.Controls.Add(this.chooseImportFile);
            this.groupBoxInput.Controls.Add(this.importFileTextbox);
            this.groupBoxInput.Location = new System.Drawing.Point(14, 11);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(613, 119);
            this.groupBoxInput.TabIndex = 0;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Fichier d\'entrée";
            // 
            // launchParse
            // 
            this.launchParse.Location = new System.Drawing.Point(532, 17);
            this.launchParse.Margin = new System.Windows.Forms.Padding(0);
            this.launchParse.Name = "launchParse";
            this.launchParse.Size = new System.Drawing.Size(75, 23);
            this.launchParse.TabIndex = 5;
            this.launchParse.Text = "Lancer !";
            this.launchParse.UseVisualStyleBackColor = true;
            this.launchParse.Click += new System.EventHandler(this.LaunchParse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Malgun Gothic Semilight", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(402, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ce fichier doit être un export du compte 512 directement par la fenêtre de révisi" +
    "on.";
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Black;
            this.labelStatus.Location = new System.Drawing.Point(5, 84);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(411, 23);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "En attente de la selection du fichier ...";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // importTotal
            // 
            this.importTotal.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importTotal.Location = new System.Drawing.Point(423, 84);
            this.importTotal.Name = "importTotal";
            this.importTotal.Size = new System.Drawing.Size(184, 23);
            this.importTotal.TabIndex = 3;
            this.importTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // importProgressBar
            // 
            this.importProgressBar.Location = new System.Drawing.Point(6, 58);
            this.importProgressBar.MarqueeAnimationSpeed = 0;
            this.importProgressBar.Name = "importProgressBar";
            this.importProgressBar.Size = new System.Drawing.Size(601, 23);
            this.importProgressBar.Step = 1;
            this.importProgressBar.TabIndex = 2;
            // 
            // chooseImportFile
            // 
            this.chooseImportFile.Location = new System.Drawing.Point(437, 17);
            this.chooseImportFile.Name = "chooseImportFile";
            this.chooseImportFile.Size = new System.Drawing.Size(92, 23);
            this.chooseImportFile.TabIndex = 1;
            this.chooseImportFile.Text = "Selectionner ...";
            this.chooseImportFile.UseVisualStyleBackColor = true;
            this.chooseImportFile.Click += new System.EventHandler(this.ChooseImportFile_Click);
            // 
            // importFileTextbox
            // 
            this.importFileTextbox.BackColor = System.Drawing.SystemColors.Control;
            this.importFileTextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importFileTextbox.ForeColor = System.Drawing.Color.Black;
            this.importFileTextbox.Location = new System.Drawing.Point(6, 17);
            this.importFileTextbox.Name = "importFileTextbox";
            this.importFileTextbox.ReadOnly = true;
            this.importFileTextbox.Size = new System.Drawing.Size(425, 20);
            this.importFileTextbox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listRemises);
            this.panel1.Location = new System.Drawing.Point(12, 136);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(615, 341);
            this.panel1.TabIndex = 1;
            // 
            // listRemises
            // 
            this.listRemises.Location = new System.Drawing.Point(2, 0);
            this.listRemises.Name = "listRemises";
            this.listRemises.Size = new System.Drawing.Size(613, 338);
            this.listRemises.TabIndex = 0;
            this.listRemises.UseCompatibleStateImageBehavior = false;
            // 
            // export
            // 
            this.export.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.export.Location = new System.Drawing.Point(505, 483);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(122, 23);
            this.export.TabIndex = 2;
            this.export.Text = "Exporter vers Excel";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 513);
            this.Controls.Add(this.export);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxInput);
            this.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Espace ACE - Caisse v1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Button chooseImportFile;
        private System.Windows.Forms.TextBox importFileTextbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listRemises;
        private System.Windows.Forms.ProgressBar importProgressBar;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label importTotal;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.Button launchParse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog pickImportFile;
    }
}

