using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Weinkarte1
{
    public class CartForm : Form
    {
        private DataGridView dgv;
        private Button btnClose;
        private Button btnRemoveSelected;
        private BindingSource bs = new BindingSource();

        public CartForm(List<Product> cart)
        {
            this.Text = "Warenkorb";
            this.Width = 600;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;

            dgv = new DataGridView();
            dgv.Dock = DockStyle.Top;
            dgv.Height = this.ClientSize.Height - 60;
            dgv.AutoGenerateColumns = true;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = true;

            bs.DataSource = cart ?? new List<Product>();
            dgv.DataSource = bs;

            btnRemoveSelected = new Button();
            btnRemoveSelected.Text = "Entfernen";
            btnRemoveSelected.Left = 10;
            btnRemoveSelected.Top = this.ClientSize.Height - 45;
            btnRemoveSelected.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRemoveSelected.Click += BtnRemoveSelected_Click;

            btnClose = new Button();
            btnClose.Text = "Schließen";
            btnClose.Left = this.ClientSize.Width - 100;
            btnClose.Top = this.ClientSize.Height - 45;
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(dgv);
            this.Controls.Add(btnRemoveSelected);
            this.Controls.Add(btnClose);
        }

        private void BtnRemoveSelected_Click(object? sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Keine Auswahl zum Entfernen.", "Warenkorb", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Entferne ausgewählte Items aus Bound List
            var items = new List<Product>();
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                if (row.DataBoundItem is Product p)
                    items.Add(p);
            }

            var list = bs.DataSource as List<Product>;
            if (list == null) return;
            foreach (var it in items)
            {
                list.Remove(it);
            }
            bs.ResetBindings(false);
        }
    }
}
