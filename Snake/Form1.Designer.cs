namespace Snake
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.txtBox_Info = new System.Windows.Forms.TextBox();
            this.numUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numUpDown_Height = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkBx_ShowHamiltonianCycle = new System.Windows.Forms.CheckBox();
            this.numUpDown_WaitFactor = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_WaitFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(147, 8);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(113, 35);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // txtBox_Info
            // 
            this.txtBox_Info.Location = new System.Drawing.Point(8, 104);
            this.txtBox_Info.Multiline = true;
            this.txtBox_Info.Name = "txtBox_Info";
            this.txtBox_Info.ReadOnly = true;
            this.txtBox_Info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBox_Info.Size = new System.Drawing.Size(254, 793);
            this.txtBox_Info.TabIndex = 2;
            // 
            // numUpDown_Width
            // 
            this.numUpDown_Width.BackColor = System.Drawing.SystemColors.Control;
            this.numUpDown_Width.Location = new System.Drawing.Point(96, 9);
            this.numUpDown_Width.Margin = new System.Windows.Forms.Padding(2);
            this.numUpDown_Width.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numUpDown_Width.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUpDown_Width.Name = "numUpDown_Width";
            this.numUpDown_Width.ReadOnly = true;
            this.numUpDown_Width.Size = new System.Drawing.Size(45, 20);
            this.numUpDown_Width.TabIndex = 3;
            this.numUpDown_Width.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "PlayField width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PlayField height:";
            // 
            // numUpDown_Height
            // 
            this.numUpDown_Height.BackColor = System.Drawing.SystemColors.Control;
            this.numUpDown_Height.Location = new System.Drawing.Point(96, 28);
            this.numUpDown_Height.Margin = new System.Windows.Forms.Padding(2);
            this.numUpDown_Height.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numUpDown_Height.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUpDown_Height.Name = "numUpDown_Height";
            this.numUpDown_Height.ReadOnly = true;
            this.numUpDown_Height.Size = new System.Drawing.Size(45, 20);
            this.numUpDown_Height.TabIndex = 6;
            this.numUpDown_Height.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(267, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(886, 886);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // chkBx_ShowHamiltonianCycle
            // 
            this.chkBx_ShowHamiltonianCycle.AutoSize = true;
            this.chkBx_ShowHamiltonianCycle.Checked = true;
            this.chkBx_ShowHamiltonianCycle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBx_ShowHamiltonianCycle.Location = new System.Drawing.Point(12, 55);
            this.chkBx_ShowHamiltonianCycle.Name = "chkBx_ShowHamiltonianCycle";
            this.chkBx_ShowHamiltonianCycle.Size = new System.Drawing.Size(146, 17);
            this.chkBx_ShowHamiltonianCycle.TabIndex = 8;
            this.chkBx_ShowHamiltonianCycle.Text = "Show Hamiltonian Cycle?";
            this.chkBx_ShowHamiltonianCycle.UseVisualStyleBackColor = true;
            // 
            // numUpDown_WaitFactor
            // 
            this.numUpDown_WaitFactor.Location = new System.Drawing.Point(96, 78);
            this.numUpDown_WaitFactor.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDown_WaitFactor.Name = "numUpDown_WaitFactor";
            this.numUpDown_WaitFactor.ReadOnly = true;
            this.numUpDown_WaitFactor.Size = new System.Drawing.Size(45, 20);
            this.numUpDown_WaitFactor.TabIndex = 9;
            this.numUpDown_WaitFactor.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDown_WaitFactor.ValueChanged += new System.EventHandler(this.numUpDown_WaitFactor_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Wait factor:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 902);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numUpDown_WaitFactor);
            this.Controls.Add(this.chkBx_ShowHamiltonianCycle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.numUpDown_Height);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numUpDown_Width);
            this.Controls.Add(this.txtBox_Info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_WaitFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txtBox_Info;
        private System.Windows.Forms.NumericUpDown numUpDown_Width;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUpDown_Height;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkBx_ShowHamiltonianCycle;
        private System.Windows.Forms.NumericUpDown numUpDown_WaitFactor;
        private System.Windows.Forms.Label label3;
    }
}

