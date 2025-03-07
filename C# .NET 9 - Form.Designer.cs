namespace [Proyecto];

partial class [Form]
{
	// Controles
	private System.Windows.Forms.Button Button;
	
	private void InitializeComponent()
	{
		this.[Button] = new System.Windows.Forms.Button();
		this.SuspendLayout();
		// [Button]
		this.myButton.Location = new System.Drawing.Point(100, 100);
		this.myButton.Name = "myButton";
		this.myButton.Size = new System.Drawing.Size(100, 50);
		this.myButton.TabIndex = 0;
		this.myButton.Text = "Click Me!";
		this.myButton.UseVisualStyleBackColor = true;
		// Form
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(284, 261);
		this.Controls.Add(this.myButton);
		this.Name = "Form1";
		this.Text = "Form1";
		this.ResumeLayout(false);
	}
}