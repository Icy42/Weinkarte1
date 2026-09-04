using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Weinkarte1
{

    public partial class Form1 : Form
    {
        // Liste mit allen Produkten als Basis für Filter
        private List<Product> allProducts = new List<Product>();
        // BindingSource, damit die DataGridView zuverlässig neu gebunden werden kann
        private BindingSource productsBindingSource = new BindingSource();
        // In-Memory-Warenkorb (temporär während Programmlauf)
        private List<Product> cart = new List<Product>();

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeProducts(List<Product> products)
        {
            // Basisliste merken und DataGridView über BindingSource binden
            allProducts = products ?? new List<Product>();
            productsBindingSource.DataSource = allProducts;
            dataGridViewProducts.DataSource = productsBindingSource;

            // ComboBoxen vorbereiten
            comboBoxCategory.Items.Clear();
            comboBoxName.Items.Clear();
            comboBoxPrice.Items.Clear();
            comboBoxStock.Items.Clear();

            comboBoxCategory.Items.Add("Alle Kategorien");
            comboBoxName.Items.Add("Alle Kategorien");
            comboBoxPrice.Items.Add("Alle Kategorien");
            comboBoxStock.Items.Add("Alle Kategorien");

            comboBoxName.Items.AddRange(allProducts.Select(p => p.Name).Distinct().ToArray());
            comboBoxCategory.Items.AddRange(allProducts.Select(p => p.Category).Distinct().ToArray());
            comboBoxPrice.Items.AddRange(allProducts.Select(p => p.Price).Distinct().ToArray());
            comboBoxStock.Items.AddRange(allProducts.Select(p => p.Country).Distinct().ToArray());

            // Standardauswahl
            comboBoxName.SelectedIndex = 0;
            comboBoxCategory.SelectedIndex = 0;
            comboBoxPrice.SelectedIndex = 0;
            comboBoxStock.SelectedIndex = 0;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // Versuche, Produkte aus einer SQL-Datenbank zu laden. Bei Fehlern werden Beispiel-Daten verwendet.
            string conn = "Server=localhost;Database=Model;Trusted_Connection=True;TrustServerCertificate=True";
            List<Product> products;
            try
            {
                products = LoadProductsFromDatabase(conn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Verbinden zur Datenbank:\n{ex.Message}\nEs werden Beispiel-Daten geladen.", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                products = GetSampleProducts();
            }

            InitializeProducts(products);

            // Erstelle Laufzeit-Buttons für Warenkorb-Funktionalität, falls Designer keine hat
            EnsureCartButtonsExist();

            // Doppelklick auf Produkt zum Hinzufügen in den Warenkorb
            dataGridViewProducts.CellDoubleClick += (s, ev) =>
            {
                if (ev.RowIndex >= 0 && dataGridViewProducts.Rows[ev.RowIndex].DataBoundItem is Product p)
                {
                    AddProductToCart(p);
                }
            };
        }

        private void EnsureCartButtonsExist()
        {
            // Button: In Warenkorb
            if (this.Controls.Find("buttonAddToCart", true).Length == 0)
            {
                var btn = new Button();
                btn.Name = "buttonAddToCart";
                btn.Text = "In Warenkorb";
                btn.AutoSize = true;
                btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btn.Left = Math.Max(10, this.ClientSize.Width - 220);
                btn.Top = 10;
                btn.Click += ButtonAddToCart_Click;
                this.Controls.Add(btn);
            }

            // Button: Warenkorb anzeigen
            if (this.Controls.Find("buttonShowCart", true).Length == 0)
            {
                var btn2 = new Button();
                btn2.Name = "buttonShowCart";
                btn2.Text = "Warenkorb anzeigen";
                btn2.AutoSize = true;
                btn2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btn2.Left = Math.Max(120, this.ClientSize.Width - 120);
                btn2.Top = 10;
                btn2.Click += ButtonShowCart_Click;
                this.Controls.Add(btn2);
            }
        }

        private void ButtonAddToCart_Click(object? sender, EventArgs e)
        {
            // Füge alle selektierten Zeilen hinzu, falls keine Auswahl: aktuelle Zeile
            var added = 0;
            foreach (DataGridViewRow row in dataGridViewProducts.SelectedRows)
            {
                if (row.DataBoundItem is Product p)
                {
                    AddProductToCart(p);
                    added++;
                }
            }

            if (added == 0 && dataGridViewProducts.CurrentRow?.DataBoundItem is Product current)
            {
                AddProductToCart(current);
                added = 1;
            }

            if (added > 0)
                MessageBox.Show($"{added} Produkt(e) zum Warenkorb hinzugefügt.", "Warenkorb", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Keine Produkte ausgewählt.", "Warenkorb", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void AddProductToCart(Product p)
        {
            // einfache Referenz speichern; Kopie falls später nötig
            cart.Add(new Product(p.Name, p.Category, p.Price, p.Country));
        }

        private void ButtonShowCart_Click(object? sender, EventArgs e)
        {
            using var form = new CartForm(cart);
            form.ShowDialog(this);
        }

        private List<Product> LoadProductsFromDatabase(string connectionString)
        {
            var list = new List<Product>();
            using var cn = new SqlConnection(connectionString);
            cn.Open();
            using var cmd = new SqlCommand("SELECT Name, Category, Price, Country FROM Weinkarte", cn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var name = rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0);
                if (string.IsNullOrWhiteSpace(name)) continue;
                var category = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1);
                var price = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2);
                var country = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3);
                list.Add(new Product(name, category, price, country));
            }
            return list;
        }

        private List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product("Elektronik", "test", "16€", "DE"),
                new Product("Plastik", "test1", "17€", "ES"),
                new Product("Stoff", "test2", "12€", "IT"),
                new Product("Polyester", "test3", "10€", "AT"),
                new Product("Luft", "test4", "11€", "CH"),
                new Product("Ueberfluessig", "test5", "17€", "NL"),
                new Product("Wein Spätlese", "Rotwein", "12€", "DE"),
                new Product("Wein Kabinett", "Weißwein", "10€", "DE"),
                new Product("Rosé Classic", "Rosé", "9€", "FR"),
                new Product("Sekt Brut", "Schaumwein", "15€", "DE"),
                new Product("Dessertwein", "Süßwein", "20€", "IT"),
                new Product("Traubenmix", "Verschnitt", "7€", "ES"),
                new Product("Reserve", "Rotwein", "22€", "PT"),
                new Product("Cuvée", "Rotwein", "18€", "FR"),
                new Product("Fumé Blanc", "Weißwein", "14€", "US"),
                new Product("Chardonnay", "Weißwein", "13€", "AU"),
                new Product("Merlot", "Rotwein", "11€", "IT"),
                new Product("Pinot Noir", "Rotwein", "19€", "FR"),
                new Product("Riesling", "Weißwein", "12€", "DE"),
                new Product("Grüner Veltliner", "Weißwein", "9€", "AT"),
                new Product("Prosecco", "Schaumwein", "8€", "IT")
            };
        }

        public void buttonFilter_Click(object sender, EventArgs e)
        {
            // Werte aus den ComboBoxen lesen; "Alle Kategorien" bedeutet: kein Filter für diese Spalte
            string selectedName = comboBoxName.SelectedItem?.ToString();
            string selectedCategory = comboBoxCategory.SelectedItem?.ToString();
            string selectedPrice = comboBoxPrice.SelectedItem?.ToString();
            string selectedStock = comboBoxStock.SelectedItem?.ToString();

            if (selectedName == "Alle Kategorien") selectedName = null;
            if (selectedCategory == "Alle Kategorien") selectedCategory = null;
            if (selectedPrice == "Alle Kategorien") selectedPrice = null;
            if (selectedStock == "Alle Kategorien") selectedStock = null;

            // Filter immer auf der ursprünglichen Produktliste anwenden
            var filteredProducts = allProducts.Where(product =>
                (selectedName == null || product.Name == selectedName) &&
                (selectedCategory == null || product.Category == selectedCategory) &&
                (selectedPrice == null || product.Price == selectedPrice) &&
                (selectedStock == null || product.Country == selectedStock)
            ).ToList();

            productsBindingSource.DataSource = filteredProducts;
            productsBindingSource.ResetBindings(false);
        }

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonFilter_Click(sender, e);
        }

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonFilter_Click(sender, e);
        }

        private void comboBoxPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonFilter_Click(sender, e);
        }

        private void comboBoxStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonFilter_Click(sender, e);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Country { get; set; }

        public Product(string name, string category, string price, string country)
        {
            Name = name;
            Category = category;
            Price = price;
            Country = country;
        }
    }
}