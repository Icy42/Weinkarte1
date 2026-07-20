namespace Weinkarte1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Product> products = new List<Product>
            { 
            new Product("Elektronik", "test", 16, 16)
            };
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }

    public class Product
    {
        private string Name { get; set; }
        private string Category { get; set; }
        private decimal Price { get; set; }
        private int Stock { get; set; }

        public Product(string name, string category, decimal price, int stock)
        {
            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
        }
    }
}