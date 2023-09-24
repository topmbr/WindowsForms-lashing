using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp14
{
    public partial class Form1 : Form
    {
        private BindingList<Product> products = new BindingList<Product>();

        public Form1()
        {
            InitializeComponent();
            dataGridView.DataSource = products;
        }

        private void AddProduct(string name, string category, decimal price)
        {
            products.Add(new Product { Name = name, Category = category, Price = price });
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string category = categoryTextBox.Text;
            decimal price = decimal.Parse(priceTextBox.Text);
            AddProduct(name, category, price);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView.SelectedRows[0].Index;
                products.RemoveAt(selectedIndex);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView.SelectedRows[0].Index;
                string name = nameTextBox.Text;
                string category = categoryTextBox.Text;
                decimal price = decimal.Parse(priceTextBox.Text);
                products[selectedIndex].Name = name;
                products[selectedIndex].Category = category;
                products[selectedIndex].Price = price;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Заполняем ComboBox для фильтрации
            filterComboBox.Items.Add("Все");
            filterComboBox.Items.Add("Категория");
            filterComboBox.Items.Add("Цена больше");
            filterComboBox.Items.Add("Цена меньше");

            // Заполняем ComboBox для сортировки
            sortComboBox.Items.Add("По имени");
            sortComboBox.Items.Add("По категории");
            sortComboBox.Items.Add("По цене");
        }

        private void ApplyFilter()
        {
            string filterText = filterComboBox.Text;
            string searchText = filterTextBox.Text;

            if (filterText == "Все")
            {
                dataGridView.DataSource = products;
            }
            else if (filterText == "Категория")
            {
                dataGridView.DataSource = new BindingList<Product>(products.Where(p => p.Category.Contains(searchText)).ToList());
            }
            else if (filterText == "Цена больше")
            {
                decimal minPrice = decimal.Parse(searchText);
                dataGridView.DataSource = new BindingList<Product>(products.Where(p => p.Price >= minPrice).ToList());
            }
            else if (filterText == "Цена меньше")
            {
                decimal maxPrice = decimal.Parse(searchText);
                dataGridView.DataSource = new BindingList<Product>(products.Where(p => p.Price <= maxPrice).ToList());
            }
        }

        private void ApplySort()
        {
            string sortText = sortComboBox.Text;

            if (sortText == "По имени")
            {
                dataGridView.DataSource = new BindingList<Product>(products.OrderBy(p => p.Name).ToList());
            }
            else if (sortText == "По категории")
            {
                dataGridView.DataSource = new BindingList<Product>(products.OrderBy(p => p.Category).ToList());
            }
            else if (sortText == "По цене")
            {
                dataGridView.DataSource = new BindingList<Product>(products.OrderBy(p => p.Price).ToList());
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            ApplyFilter();
            ApplySort();
        }
    }
}
