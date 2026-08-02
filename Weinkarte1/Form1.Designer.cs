namespace Weinkarte1
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
            comboBoxCategory = new ComboBox();
            dataGridViewProducts = new DataGridView();
            comboBoxName = new ComboBox();
            comboBoxPrice = new ComboBox();
            comboBoxStock = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            SuspendLayout();
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(12, 102);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new Size(121, 23);
            comboBoxCategory.TabIndex = 0;
            comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewProducts.Location = new Point(12, 149);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.Size = new Size(502, 289);
            dataGridViewProducts.TabIndex = 1;
            dataGridViewProducts.CellContentClick += dataGridViewProducts_CellContentClick;
            // 
            // comboBoxName
            // 
            comboBoxName.FormattingEnabled = true;
            comboBoxName.Location = new Point(139, 102);
            comboBoxName.Name = "comboBoxName";
            comboBoxName.Size = new Size(121, 23);
            comboBoxName.TabIndex = 2;
            comboBoxName.SelectedIndexChanged += comboBoxName_SelectedIndexChanged;
            // 
            // comboBoxPrice
            // 
            comboBoxPrice.FormattingEnabled = true;
            comboBoxPrice.Location = new Point(266, 102);
            comboBoxPrice.Name = "comboBoxPrice";
            comboBoxPrice.Size = new Size(121, 23);
            comboBoxPrice.TabIndex = 3;
            comboBoxPrice.SelectedIndexChanged += comboBoxPrice_SelectedIndexChanged;
            // 
            // comboBoxStock
            // 
            comboBoxStock.FormattingEnabled = true;
            comboBoxStock.Location = new Point(393, 102);
            comboBoxStock.Name = "comboBoxStock";
            comboBoxStock.Size = new Size(121, 23);
            comboBoxStock.TabIndex = 4;
            comboBoxStock.SelectedIndexChanged += comboBoxStock_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBoxStock);
            Controls.Add(comboBoxPrice);
            Controls.Add(comboBoxName);
            Controls.Add(dataGridViewProducts);
            Controls.Add(comboBoxCategory);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBoxCategory;
        private DataGridView dataGridViewProducts;
        private ComboBox comboBoxName;
        private ComboBox comboBoxPrice;
        private ComboBox comboBoxStock;
    }
}
