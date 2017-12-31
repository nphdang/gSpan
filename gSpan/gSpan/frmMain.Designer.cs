namespace gSpan
{
    partial class frmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataset = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.btLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinSup = new System.Windows.Forms.TextBox();
            this.btMine = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.tvResult = new System.Windows.Forms.TreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dataset";
            // 
            // txtDataset
            // 
            this.txtDataset.Location = new System.Drawing.Point(61, 14);
            this.txtDataset.Name = "txtDataset";
            this.txtDataset.Size = new System.Drawing.Size(274, 20);
            this.txtDataset.TabIndex = 1;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(341, 12);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 2;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(422, 12);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(75, 23);
            this.btLoad.TabIndex = 3;
            this.btLoad.Text = "Load data";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "minSup";
            // 
            // txtMinSup
            // 
            this.txtMinSup.Location = new System.Drawing.Point(61, 50);
            this.txtMinSup.Name = "txtMinSup";
            this.txtMinSup.Size = new System.Drawing.Size(41, 20);
            this.txtMinSup.TabIndex = 5;
            // 
            // btMine
            // 
            this.btMine.Location = new System.Drawing.Point(135, 48);
            this.btMine.Name = "btMine";
            this.btMine.Size = new System.Drawing.Size(104, 40);
            this.btMine.TabIndex = 6;
            this.btMine.Text = "Mine data";
            this.btMine.UseVisualStyleBackColor = true;
            this.btMine.Click += new System.EventHandler(this.btMine_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(245, 50);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(104, 40);
            this.btClose.TabIndex = 7;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // tvResult
            // 
            this.tvResult.Location = new System.Drawing.Point(14, 107);
            this.tvResult.Name = "tvResult";
            this.tvResult.Size = new System.Drawing.Size(483, 306);
            this.tvResult.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "%";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 425);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tvResult);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btMine);
            this.Controls.Add(this.txtMinSup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.txtDataset);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "gSpan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDataset;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMinSup;
        private System.Windows.Forms.Button btMine;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TreeView tvResult;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
    }
}

