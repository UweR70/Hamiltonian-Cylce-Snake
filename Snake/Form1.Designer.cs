﻿namespace Snake
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBox_Info = new System.Windows.Forms.TextBox();
            this.numUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numUpDown_Height = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Height)).BeginInit();
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(267, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 886);
            this.panel1.TabIndex = 1;
            // 
            // txtBox_Info
            // 
            this.txtBox_Info.Location = new System.Drawing.Point(8, 55);
            this.txtBox_Info.Multiline = true;
            this.txtBox_Info.Name = "txtBox_Info";
            this.txtBox_Info.ReadOnly = true;
            this.txtBox_Info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBox_Info.Size = new System.Drawing.Size(254, 842);
            this.txtBox_Info.TabIndex = 2;
            // 
            // numUpDown_Width
            // 
            this.numUpDown_Width.Location = new System.Drawing.Point(107, 9);
            this.numUpDown_Width.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.numUpDown_Width.Size = new System.Drawing.Size(34, 20);
            this.numUpDown_Width.TabIndex = 3;
            this.numUpDown_Width.Value = new decimal(new int[] {
            10,
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
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Playground width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Playground height:";
            // 
            // numUpDown_Height
            // 
            this.numUpDown_Height.Location = new System.Drawing.Point(107, 28);
            this.numUpDown_Height.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.numUpDown_Height.Size = new System.Drawing.Size(34, 20);
            this.numUpDown_Height.TabIndex = 6;
            this.numUpDown_Height.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 902);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.numUpDown_Height);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numUpDown_Width);
            this.Controls.Add(this.txtBox_Info);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBox_Info;
        private System.Windows.Forms.NumericUpDown numUpDown_Width;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUpDown_Height;
    }
}

