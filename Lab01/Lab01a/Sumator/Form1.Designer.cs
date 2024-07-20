namespace Sumator
{
	partial class Form1
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
			textBoxX = new TextBox();
			textBoxY = new TextBox();
			button_calc = new Button();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			textBoxResult = new TextBox();
			SuspendLayout();
			// 
			// textBoxX
			// 
			textBoxX.Location = new Point(65, 125);
			textBoxX.Name = "textBoxX";
			textBoxX.Size = new Size(217, 27);
			textBoxX.TabIndex = 0;
			// 
			// textBoxY
			// 
			textBoxY.Location = new Point(65, 185);
			textBoxY.Name = "textBoxY";
			textBoxY.Size = new Size(217, 27);
			textBoxY.TabIndex = 1;
			// 
			// button_calc
			// 
			button_calc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			button_calc.Location = new Point(99, 269);
			button_calc.Name = "button_calc";
			button_calc.Size = new Size(147, 41);
			button_calc.TabIndex = 2;
			button_calc.Text = "Calculate";
			button_calc.UseVisualStyleBackColor = true;
			button_calc.Click += button_calc_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(65, 102);
			label1.Name = "label1";
			label1.Size = new Size(94, 20);
			label1.TabIndex = 3;
			label1.Text = "First Number";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(65, 162);
			label2.Name = "label2";
			label2.Size = new Size(116, 20);
			label2.TabIndex = 4;
			label2.Text = "Second Number";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(66, 225);
			label3.Name = "label3";
			label3.Size = new Size(49, 20);
			label3.TabIndex = 5;
			label3.Text = "Result";
			// 
			// textBoxResult
			// 
			textBoxResult.Font = new Font("Segoe UI", 9F);
			textBoxResult.Location = new Point(121, 222);
			textBoxResult.Name = "textBoxResult";
			textBoxResult.ReadOnly = true;
			textBoxResult.Size = new Size(161, 27);
			textBoxResult.TabIndex = 6;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(359, 401);
			Controls.Add(textBoxResult);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(button_calc);
			Controls.Add(textBoxY);
			Controls.Add(textBoxX);
			Name = "Form1";
			Text = "Sumator";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBoxX;
		private TextBox textBoxY;
		private Button button_calc;
		private Label label1;
		private Label label2;
		private Label label3;
		private TextBox textBoxResult;
	}
}
