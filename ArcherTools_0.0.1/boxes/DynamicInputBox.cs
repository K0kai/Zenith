namespace ArcherTools_0._0._1.boxes
{
    public class DynamicInputBoxForm : Form
    {
        private List<TextBox> boxes = new List<TextBox>();
        private Button okButton;
        private Button cancelButton;
        private List<string> userInputs = new List<String>();

        public List<string> UserInputs => userInputs;


        public DynamicInputBoxForm(string prompt, List<string> boxNames)
        {
            boxes = new List<TextBox>();
            int numberOfBoxes = boxNames.Count;
            userInputs = new List<string>();

            BackColor = ToolHub._mainForm.BackColor;
            this.Text = "Input";
            this.ClientSize = new System.Drawing.Size(300, 40 + 45 * numberOfBoxes);

            var promptLabel = new Label
            {
                ForeColor = ToolHub._mainForm.ForeColor,
                Text = prompt,
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(260, 20)
            };
            this.Controls.Add(promptLabel);

            for (int i = 0; i < numberOfBoxes; i++)
            {
                var textBox = new WatermarkTextBox()
                {
                    Text = boxNames[i],
                    WatermarkText = boxNames[i],
                    Location = new System.Drawing.Point(10, 40 + i * 30),
                    Width = 260
                };

                boxes.Add(textBox);
                this.Controls.Add(textBox);
            }

            okButton = new Button
            {
                ForeColor = ToolHub._mainForm.ForeColor,
                Text = "OK",
                Location = new System.Drawing.Point(200, 40 + numberOfBoxes * 30)
            };
            okButton.Click += OkButton_Click;
            this.Controls.Add(okButton);

            cancelButton = new Button
            {
                ForeColor = ToolHub._mainForm.ForeColor,
                Text = "Cancel",
                Location = new System.Drawing.Point(120, 40 + numberOfBoxes * 30)
            };
            cancelButton.Click += CancelButton_Click;
            this.Controls.Add(cancelButton);
            this.StartPosition = FormStartPosition.CenterParent;


        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            foreach (var textBox in boxes)
            {
                userInputs.Add(textBox.Text);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {

        }

        public static List<string> Show(string prompt, List<string> numberOfInputs)
        {
            using (var inputBox = new DynamicInputBoxForm(prompt, numberOfInputs))
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    return inputBox.UserInputs;
                }
                else
                {
                    return null;
                }
            }
        }

        public class WatermarkTextBox : TextBox
        {
            private string watermarkText;

            public string WatermarkText
            {
                get { return watermarkText; }
                set
                {
                    watermarkText = value;
                    this.Invalidate();
                }

            }

            public WatermarkTextBox()
            {
                watermarkText = "Enter text...";
                this.ForeColor = Color.Gray;
                this.GotFocus += RemoveWatermarkText;
                this.LostFocus += AddWatermarkText;
                this.TextChanged += WatermarkTextChanged;
            }

            private void RemoveWatermarkText(object sender, EventArgs e)
            {
                if (this.Text == watermarkText)
                {
                    this.Text = "";
                    this.ForeColor = Color.Black;
                }
            }

            private void AddWatermarkText(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(this.Text))
                {
                    this.Text = watermarkText;
                    this.ForeColor = Color.Gray;
                }
            }

            private void WatermarkTextChanged(object sender, EventArgs e)
            {
                if (this.Text == watermarkText)
                {
                    this.ForeColor = Color.Gray;
                }
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                if (string.IsNullOrEmpty(this.Text) && !this.Focused)
                {
                    using (Brush brush = new SolidBrush(Color.Gray))
                    {
                        e.Graphics.DrawString(watermarkText, this.Font, brush, new PointF(0, 0));
                    }
                }
            }
        }
    }
}
