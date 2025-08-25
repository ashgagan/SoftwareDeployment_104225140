using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactValidationLibrary;
using System.IO;
using Newtonsoft.Json; // We'll install this package next

namespace ContactManagerPro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Contact> contacts = new List<Contact>();

        // Contact class
        public class Contact
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        // Simple validator 
        public static class ContactValidator
        {
            public static bool IsValid(string name, string email, string phone)
            {
                return AdvancedValidator.GetValidationMessage(name, email, phone) == "Valid";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ContactValidator.IsValid(txtName.Text, txtEmail.Text, txtPhone.Text))
            {
                Contact newContact = new Contact
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text
                };

                contacts.Add(newContact);
                RefreshContactList();
                ClearTextBoxes();
                MessageBox.Show("Contact added!");
            }
            else
            {
                MessageBox.Show("Please fill all fields correctly.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
                File.WriteAllText("contacts.json", json);
                MessageBox.Show("Contacts saved to contacts.json!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("contacts.json"))
                {
                    string json = File.ReadAllText("contacts.json");
                    contacts = JsonConvert.DeserializeObject<List<Contact>>(json) ?? new List<Contact>();
                    RefreshContactList();
                    MessageBox.Show("Contacts loaded!");
                }
                else
                {
                    MessageBox.Show("No contacts.json file found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}");
            }
        }

        private void RefreshContactList()
        {
            listContacts.Items.Clear();
            foreach (var contact in contacts)
            {
                listContacts.Items.Add($"{contact.Name} - {contact.Email} - {contact.Phone}");
            }
        }

        private void ClearTextBoxes()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
