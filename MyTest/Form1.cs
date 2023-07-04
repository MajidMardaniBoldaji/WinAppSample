using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CmsDbEntities1 Db = new CmsDbEntities1();
        private void Form1_Load(object sender, EventArgs e)
        {
            newForm();
        }
        string State = "";
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (State == "add")
            {

                Contacts contact = new Contacts()
                {
                    Name = txtName.Text.Trim(),
                    FatherName = txtFatherName.Text,
                    Gander = comboGander.Text,
                    Address = txtAddress.Text,
                    City = txtCity.Text,
                    Company = txtCompany.Text,
                    Description = txtDescription.Text,
                    Email = txtEmail.Text,
                    ContactNo1 = txtContactNo1.Text,
                    ContactNo2 = txtContactNo2.Text,
                    ContactNo3 = txtContactNo3.Text
                };
                Db.Contacts.Add(contact);
                Db.SaveChanges();
                MessageBox.Show("Contact is stored inDatabase", "Add Contact");
                newForm();
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    var res = Db.Contacts.FirstOrDefault(p => p.Id == selectedID);
                    if (res != null)
                    {
                        res.Name = txtName.Text;
                        res.FatherName = txtFatherName.Text;
                        res.Gander = comboGander.Text.Trim();
                        res.Address = txtAddress.Text;
                        res.City = txtCity.Text;
                        res.Company = txtCompany.Text;
                        res.Description = txtDescription.Text;
                        res.Email = txtEmail.Text;
                        res.ContactNo1 = txtContactNo1.Text;
                        res.ContactNo2 = txtContactNo2.Text;
                        res.ContactNo3 = txtContactNo3.Text;
                        Db.SaveChanges();
                        MessageBox.Show("Contact is Edited inDatabase", "Edit Contact");
                        newForm();
                    }
                    // Save changes
                }

            } //else

            BindGrid();

        }//End btnSave

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            State = "add";
            enableInputs();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            State = "edit";
            enableInputs();
        }
        public void newForm()
        {
            txtName.Text = "";
            txtFatherName.Text = "";
            comboGander.SelectedIndex = 0;
            txtCity.Text = "";
            txtCompany.Text = "";
            txtAddress.Text = "";
            txtDescription.Text = "";
            txtEmail.Text = "";
            txtContactNo1.Text = "";
            txtContactNo2.Text = "";
            txtContactNo2.Text = "";
            txtContactNo3.Text = "";
            //..... Disable .........
            txtName.Enabled = false;
            txtFatherName.Enabled = false;
            comboGander.Enabled = false;
            txtCity.Enabled = false;
            txtAddress.Enabled = false;
            txtCompany.Enabled = false;
            txtDescription.Enabled = false;
            txtEmail.Enabled = false;
            txtContactNo1.Enabled = false;
            txtContactNo2.Enabled = false;
            txtContactNo2.Enabled = false;
            txtContactNo3.Enabled = false;
            btnSave.Enabled = false;
            btnAddNew.Enabled = true;
            btnEdit.Enabled = true;
            BindGrid();

        }
        private void enableInputs()
        {
            txtName.Enabled = true;
            txtFatherName.Enabled = true;
            comboGander.Enabled = true;
            txtCity.Enabled = true;
            txtCompany.Enabled = true;
            txtAddress.Enabled = true;
            txtDescription.Enabled = true;
            txtEmail.Enabled = true;
            txtContactNo1.Enabled = true;
            txtContactNo2.Enabled = true;
            txtContactNo2.Enabled = true;
            txtContactNo3.Enabled = true;
            btnAddNew.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
        }
        private void BindGrid()
        {
            dgContacts.DataSource = Db.Contacts.ToList();
            var cities = Db.Contacts.Select(x => x.City).Distinct();
            comboCitySearch.DataSource = cities.ToList();
            var copmpanies = Db.Contacts.Select(x => x.Company).Distinct();
            comboCompanySearch.DataSource = copmpanies.ToList();
            comboCitySearch.Text = "City";
            comboCompanySearch.Text = "Company";
            comboGander.Text = "Gander";

        }
        private void BindGrid(string param, string type)
        {

            if (type == "city")
            {
                var query = from p in Db.Contacts
                            where p.City == param
                            select p;
                dgContacts.DataSource = query.ToList();

            }
            else if (type == "company")
            {
                var query = from p in Db.Contacts
                            where p.Company == param
                            select p;
                dgContacts.DataSource = query.ToList();

            }
            else if (type == "gander")
            {
                var query = from p in Db.Contacts
                            where p.Gander == param
                            select p;
                dgContacts.DataSource = query.ToList();
            }

        }
        int selectedID = 0;
        private void dgContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            State = "edit";
            enableInputs();
            if (dgContacts.RowCount >= 1)
            {

                selectedID = Convert.ToInt32(dgContacts.CurrentRow.Cells["Id"].Value);
                txtName.Text = dgContacts.CurrentRow.Cells["Name"].Value.ToString();
                txtFatherName.Text = dgContacts.CurrentRow.Cells["FatherName"].Value.ToString();
                comboGander.Text = dgContacts.CurrentRow.Cells["Gander"].Value.ToString();
                txtAddress.Text = dgContacts.CurrentRow.Cells["Address"].Value.ToString();
                txtCity.Text = dgContacts.CurrentRow.Cells["City"].Value.ToString();
                txtCompany.Text = dgContacts.CurrentRow.Cells["Company"].Value.ToString();
                txtDescription.Text = dgContacts.CurrentRow.Cells["Description"].Value.ToString();
                txtEmail.Text = dgContacts.CurrentRow.Cells["Email"].Value.ToString();
                txtContactNo1.Text = dgContacts.CurrentRow.Cells["ContactNo1"].Value.ToString();
                txtContactNo2.Text = dgContacts.CurrentRow.Cells["ContactNo2"].Value.ToString();
                txtContactNo3.Text = dgContacts.CurrentRow.Cells["ContactNo3"].Value.ToString();


            }//if
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                string name = dgContacts.CurrentRow.Cells["Name"].Value.ToString();
                int contactID = int.Parse(dgContacts.CurrentRow.Cells["Id"].Value.ToString());

                DialogResult result = MessageBox.Show($"Do you want to Delete {name} ?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    using (CmsDbEntities1 db = new CmsDbEntities1())
                    {
                        Contacts contact = db.Contacts.FirstOrDefault(c => c.Id == contactID);
                        db.Contacts.Remove(contact);
                        db.SaveChanges();
                    }
                    BindGrid();
                    newForm();
                }
            }
        }

        private void brnShowData_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void cmbGanderSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changeCombo == true)
            {
                BindGrid(cmbGanderSearch.Text.Trim(), "gander");
                changeCombo = false;
            }


        }

        private void comboCitySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changeCombo == true)
            {
                BindGrid(comboCitySearch.Text.Trim(), "city");
                changeCombo = false;
            }
        }

        private void comboCompanySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(changeCombo)
            {
                BindGrid(comboCompanySearch.Text.Trim(), "company");
                changeCombo = false;
            }

        }
        bool changeCombo = false;
        private void cmbGanderSearch_MouseClick(object sender, MouseEventArgs e)
        {
            changeCombo = true;
        }

        private void comboCitySearch_MouseClick(object sender, MouseEventArgs e)
        {
            changeCombo = true;
        }

        private void comboCompanySearch_MouseClick(object sender, MouseEventArgs e)
        {
            changeCombo = true;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            State = "";
            newForm();
        }
    }
}
