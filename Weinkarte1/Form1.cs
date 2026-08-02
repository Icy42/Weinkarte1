namespace Weinkarte1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            List<Product> products = new List<Product>
            {
            new Product("Elektronik", "test", "16€", "16"),
            new Product("Plastik", "test1", "17€", "18"),
            new Product("Stoff", "test2", "12€", "11"),
            new Product("Polyester", "test3", "10€", "15"),
            new Product("Luft", "test4", "11€", "12"),
            new Product("Ueberfluessig", "test5", "17€", "16")
            };

            //DataGridView mit den Produkten befüllen

            dataGridViewProducts.DataSource = products;

            //""Alle Kategorien" in jede ComboBox schreiben"
            comboBoxName.Items.Add("Alle Kategorien");
            comboBoxCategory.Items.Add("Alle Kategorien");
            comboBoxPrice.Items.Add("Alle Kategorien");
            comboBoxStock.Items.Add("Alle Kategorien");

            //ComboBox befüllen
            comboBoxName.Items.AddRange(products.Select(products => products.Name).Distinct().ToArray());
            comboBoxCategory.Items.AddRange(products.Select(products => products.Category).Distinct().ToArray());
            comboBoxPrice.Items.AddRange(products.Select(products => products.Price).Distinct().ToArray());
            comboBoxStock.Items.AddRange(products.Select(products => products.Country).Distinct().ToArray());
        }

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxPrice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxStock_SelectedIndexChanged(object sender, EventArgs e)
        {

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