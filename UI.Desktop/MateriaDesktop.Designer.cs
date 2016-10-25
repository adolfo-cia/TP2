namespace UI.Desktop
{
    partial class MateriaDesktop
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblID = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblHSSemanales = new System.Windows.Forms.Label();
            this.lblHSTotales = new System.Windows.Forms.Label();
            this.lblIDPlan = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtHSSemanales = new System.Windows.Forms.TextBox();
            this.txtHSTotales = new System.Windows.Forms.TextBox();
            this.cbPlan = new System.Windows.Forms.ComboBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblIDEspecialidad = new System.Windows.Forms.Label();
            this.cbEspecialidad = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.24188F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.75813F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Controls.Add(this.lblID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDescripcion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHSSemanales, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblHSTotales, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblIDPlan, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDescripcion, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtHSSemanales, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtHSTotales, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbPlan, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAceptar, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCancelar, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblIDEspecialidad, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbEspecialidad, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.08696F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.91304F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 184);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(3, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(3, 47);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // lblHSSemanales
            // 
            this.lblHSSemanales.AutoSize = true;
            this.lblHSSemanales.Location = new System.Drawing.Point(3, 92);
            this.lblHSSemanales.Name = "lblHSSemanales";
            this.lblHSSemanales.Size = new System.Drawing.Size(74, 13);
            this.lblHSSemanales.TabIndex = 2;
            this.lblHSSemanales.Text = "HSSemanales";
            // 
            // lblHSTotales
            // 
            this.lblHSTotales.AutoSize = true;
            this.lblHSTotales.Location = new System.Drawing.Point(280, 0);
            this.lblHSTotales.Name = "lblHSTotales";
            this.lblHSTotales.Size = new System.Drawing.Size(57, 13);
            this.lblHSTotales.TabIndex = 3;
            this.lblHSTotales.Text = "HSTotales";
            // 
            // lblIDPlan
            // 
            this.lblIDPlan.AutoSize = true;
            this.lblIDPlan.Location = new System.Drawing.Point(280, 47);
            this.lblIDPlan.Name = "lblIDPlan";
            this.lblIDPlan.Size = new System.Drawing.Size(39, 13);
            this.lblIDPlan.TabIndex = 4;
            this.lblIDPlan.Text = "IDPlan";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(84, 3);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(190, 20);
            this.txtID.TabIndex = 5;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(84, 50);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(190, 20);
            this.txtDescripcion.TabIndex = 6;
            // 
            // txtHSSemanales
            // 
            this.txtHSSemanales.Location = new System.Drawing.Point(84, 95);
            this.txtHSSemanales.Name = "txtHSSemanales";
            this.txtHSSemanales.Size = new System.Drawing.Size(190, 20);
            this.txtHSSemanales.TabIndex = 7;
            // 
            // txtHSTotales
            // 
            this.txtHSTotales.Location = new System.Drawing.Point(366, 3);
            this.txtHSTotales.Name = "txtHSTotales";
            this.txtHSTotales.Size = new System.Drawing.Size(165, 20);
            this.txtHSTotales.TabIndex = 8;
            // 
            // cbPlan
            // 
            this.cbPlan.FormattingEnabled = true;
            this.cbPlan.Location = new System.Drawing.Point(366, 50);
            this.cbPlan.Name = "cbPlan";
            this.cbPlan.Size = new System.Drawing.Size(165, 21);
            this.cbPlan.TabIndex = 9;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(280, 140);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 10;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(366, 140);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblIDEspecialidad
            // 
            this.lblIDEspecialidad.AutoSize = true;
            this.lblIDEspecialidad.Location = new System.Drawing.Point(280, 92);
            this.lblIDEspecialidad.Name = "lblIDEspecialidad";
            this.lblIDEspecialidad.Size = new System.Drawing.Size(78, 13);
            this.lblIDEspecialidad.TabIndex = 12;
            this.lblIDEspecialidad.Text = "IDEspecialidad";
            // 
            // cbEspecialidad
            // 
            this.cbEspecialidad.FormattingEnabled = true;
            this.cbEspecialidad.Location = new System.Drawing.Point(366, 95);
            this.cbEspecialidad.Name = "cbEspecialidad";
            this.cbEspecialidad.Size = new System.Drawing.Size(165, 21);
            this.cbEspecialidad.TabIndex = 13;
            this.cbEspecialidad.SelectedIndexChanged += new System.EventHandler(this.cbEspecialidad_SelectedIndexChanged);
            // 
            // MateriaDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 184);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MateriaDesktop";
            this.Text = "MateriaDesktop";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblHSSemanales;
        private System.Windows.Forms.Label lblHSTotales;
        private System.Windows.Forms.Label lblIDPlan;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtHSSemanales;
        private System.Windows.Forms.TextBox txtHSTotales;
        private System.Windows.Forms.ComboBox cbPlan;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblIDEspecialidad;
        private System.Windows.Forms.ComboBox cbEspecialidad;
    }
}