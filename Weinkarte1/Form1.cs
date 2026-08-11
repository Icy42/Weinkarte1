using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Weinkarte1
{

    public partial class Form1 : Form
    {
        // Liste mit allen Produkten als Basis für Filter
        private List<Product> allProducts = new List<Product>();
        // BindingSource, damit die DataGridView zuverlässig neu gebunden werden kann
        private BindingSource productsBindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // Pfad anpassen, falls Sie Excel wirklich laden wollen
            string path = "products.xlsx";
            var products = LoadProductsFromExcel(path);

            // Basisliste merken und DataGridView über BindingSource binden
            allProducts = products;
            productsBindingSource.DataSource = allProducts;
            dataGridViewProducts.DataSource = productsBindingSource;

            // ComboBoxen mit einer Option "Alle Kategorien" + Werte füllen
            comboBoxCategory.Items.Add("Alle Kategorien");
            comboBoxName.Items.Add("Alle Kategorien");
            comboBoxPrice.Items.Add("Alle Kategorien");
            comboBoxStock.Items.Add("Alle Kategorien");

            comboBoxName.Items.AddRange(products.Select(p => p.Name).Distinct().ToArray());
            comboBoxCategory.Items.AddRange(products.Select(p => p.Category).Distinct().ToArray());
            comboBoxPrice.Items.AddRange(products.Select(p => p.Price).Distinct().ToArray());
            comboBoxStock.Items.AddRange(products.Select(p => p.Country).Distinct().ToArray());

            // Standardauswahl: Alle Kategorien
            comboBoxName.SelectedIndex = 0;
            comboBoxCategory.SelectedIndex = 0;
            comboBoxPrice.SelectedIndex = 0;
            comboBoxStock.SelectedIndex = 0;
        }

        private List<Product> LoadProductsFromExcel(string path)
        {
            // Bei Bedarf: tatsächliches Einlesen aus Excel (XLWorkbook) verwenden.
            // Aktuell wird die Beispiel-Liste aus dem ursprünglichen Code zurückgegeben:
            List<Product> products = new List<Product>
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

            return products; 
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