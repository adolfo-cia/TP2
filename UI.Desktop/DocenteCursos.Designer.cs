namespace UI.Desktop
{
    partial class DocenteCursos
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
            this.tcDocenteCursos = new System.Windows.Forms.ToolStripContainer();
            this.tsDocenteCursos = new System.Windows.Forms.ToolStrip();
            this.tlDocenteCursos = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDocenteCursos = new System.Windows.Forms.DataGridView();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.tcDocenteCursos.ContentPanel.SuspendLayout();
            this.tcDocenteCursos.TopToolStripPanel.SuspendLayout();
            this.tcDocenteCursos.SuspendLayout();
            this.tlDocenteCursos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocenteCursos)).BeginInit();
            this.SuspendLayout();
            // 
            // tcDocenteCursos
            // 
            // 
            // tcDocenteCursos.ContentPanel
            // 
            this.tcDocenteCursos.ContentPanel.Controls.Add(this.tlDocenteCursos);
            this.tcDocenteCursos.ContentPanel.Size = new System.Drawing.Size(502, 251);
            this.tcDocenteCursos.ContentPanel.Load += new System.EventHandler(this.tcDocenteCursos_ContentPanel_Load);
            this.tcDocenteCursos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDocenteCursos.Location = new System.Drawing.Point(0, 0);
            this.tcDocenteCursos.Name = "tcDocenteCursos";
            this.tcDocenteCursos.Size = new System.Drawing.Size(502, 276);
            this.tcDocenteCursos.TabIndex = 0;
            this.tcDocenteCursos.Text = "toolStripContainer1";
            // 
            // tcDocenteCursos.TopToolStripPanel
            // 
            this.tcDocenteCursos.TopToolStripPanel.Controls.Add(this.tsDocenteCursos);
            // 
            // tsDocenteCursos
            // 
            this.tsDocenteCursos.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDocenteCursos.Location = new System.Drawing.Point(3, 0);
            this.tsDocenteCursos.Name = "tsDocenteCursos";
            this.tsDocenteCursos.Size = new System.Drawing.Size(111, 25);
            this.tsDocenteCursos.TabIndex = 0;
            // 
            // tlDocenteCursos
            // 
            this.tlDocenteCursos.ColumnCount = 2;
            this.tlDocenteCursos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDocenteCursos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlDocenteCursos.Controls.Add(this.dgvDocenteCursos, 0, 0);
            this.tlDocenteCursos.Controls.Add(this.btnActualizar, 0, 1);
            this.tlDocenteCursos.Controls.Add(this.btnSalir, 1, 1);
            this.tlDocenteCursos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDocenteCursos.Location = new System.Drawing.Point(0, 0);
            this.tlDocenteCursos.Name = "tlDocenteCursos";
            this.tlDocenteCursos.RowCount = 2;
            this.tlDocenteCursos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDocenteCursos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlDocenteCursos.Size = new System.Drawing.Size(502, 251);
            this.tlDocenteCursos.TabIndex = 0;
            // 
            // dgvDocenteCursos
            // 
            this.dgvDocenteCursos.AllowUserToAddRows = false;
            this.dgvDocenteCursos.AllowUserToDeleteRows = false;
            this.dgvDocenteCursos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlDocenteCursos.SetColumnSpan(this.dgvDocenteCursos, 2);
            this.dgvDocenteCursos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocenteCursos.Location = new System.Drawing.Point(3, 3);
            this.dgvDocenteCursos.Name = "dgvDocenteCursos";
            this.dgvDocenteCursos.ReadOnly = true;
            this.dgvDocenteCursos.Size = new System.Drawing.Size(496, 216);
            this.dgvDocenteCursos.TabIndex = 0;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.Location = new System.Drawing.Point(343, 225);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(424, 225);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // DocenteCursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 276);
            this.Controls.Add(this.tcDocenteCursos);
            this.Name = "DocenteCursos";
            this.Text = "DocenteCursos";
            this.Load += new System.EventHandler(this.DocenteCursos_Load);
            this.tcDocenteCursos.ContentPanel.ResumeLayout(false);
            this.tcDocenteCursos.TopToolStripPanel.ResumeLayout(false);
            this.tcDocenteCursos.TopToolStripPanel.PerformLayout();
            this.tcDocenteCursos.ResumeLayout(false);
            this.tcDocenteCursos.PerformLayout();
            this.tlDocenteCursos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocenteCursos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tcDocenteCursos;
        private System.Windows.Forms.ToolStrip tsDocenteCursos;
        private System.Windows.Forms.TableLayoutPanel tlDocenteCursos;
        private System.Windows.Forms.DataGridView dgvDocenteCursos;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalir;
    }
}