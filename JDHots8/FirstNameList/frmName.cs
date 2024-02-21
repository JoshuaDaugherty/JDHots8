using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstNameList
{
    public partial class frmName : Form
    {
        public frmName()
        {
            InitializeComponent();

            lvNames.View = View.Details;
            lvNames.Columns.Add( "Names:", 250);

        }

        List<string> Names = new List<string>();

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitForm();
        }

        private void ExitForm()
        {
            DialogResult dialog = MessageBox.Show(
        "Do You Really Want To Exit The Program?",
        "EXIT NOW?",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void ShowErrorMessage(string msg, string title)
        {
            MessageBox.Show(msg, title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool keepGoing = ValidateName();

            if (keepGoing)
            {
                AddNewName();
            }
        }

        private void AddNewName()
        {
            string Name = txtName.Text;
           

            Names.Add(Name);
            
            ClearForm();
            UpdateListView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Names.Count <= 0)
            {
                ShowErrorMessage("No Name To Update",
                                 "LIST IS EMPTY");
                btnUpdate.Enabled = false;
                return;
            }
            else
            {
                btnUpdate.Enabled = true;

                bool keepGoing = ValidateName();
                if (keepGoing)
                {
                    UpdateExistingNames();
                }
            }
        }

        private void UpdateExistingNames()
        {
            int selectedIndex = lvNames.SelectedIndices[0];

            Names[selectedIndex] = txtName.Text;
            

            ClearForm();
            UpdateListView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Names.Count <= 0)
            {
                ShowErrorMessage("No Name To Delete",
                                 "LIST IS EMPTY");
                btnDelete.Enabled = false;
                return;
            }
            else
            {
                btnDelete.Enabled = true;
                DeleteExistingName();
            }
        }

        private void DeleteExistingName()
        {
            int selectedIndex = lvNames.SelectedIndices[0];

            DialogResult dialog = MessageBox.Show(
                        "Do You Really Want To Delete Employee This Employee?",
                        "DELETE EMPLOYEE?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Names.RemoveAt(selectedIndex);
               

                ClearForm();
                UpdateListView();
            }
        }

        private bool ValidateName()
        {
            
           
            try
            {
                if (txtName.Text.Trim() == "")
                {
                    throw new ArgumentNullException();
                }
                

                return true;
            }
            catch (ArgumentNullException ane)
            {
                ShowErrorMessage("System Message:\t" + "\n" +
                                 ane.Message + "\n\n" +
                                 "Field Cannot Be Left Empty",
                                 "ARGUMENTNULLEXCEPTION");
                return false;
            }

           
           
        }

        private void UpdateListView()
        {
            lvNames.Items.Clear();

            for (int i = 0; i < Names.Count; i++)
            {
                ListViewItem item = new ListViewItem(Names[i]);
                lvNames.Items.Add(item);
            }

            if (Names.Count > 0)
            {
                EnableUpdateAndDeleteButtons();
            }
            else
            {
                DisableUpdateAndDeleteButtons();
            }
        }

        private void lvEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewChanged();
        }

        private void ListViewChanged()
        {
            if (lvNames.SelectedIndices.Count > 0)
            {
                int selectedIndex = lvNames.SelectedIndices[0];
                txtName.Text = Names[selectedIndex];

            }
        }

        private void frmEmployees_Load(object sender, EventArgs e)
        {
            UserMessage();
            DisableUpdateAndDeleteButtons();
        }

        private void DisableUpdateAndDeleteButtons()
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void EnableUpdateAndDeleteButtons()
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void UserMessage()
        {
            ShowErrorMessage("To Update Or Delete An Existing Record\n" +
                             "Click On That Record's First Name To 'Activate'",
                             "PLEASE READ THIS CAREFULLY!");
        }

    }
}
