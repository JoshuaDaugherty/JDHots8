using System;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class frmAddressBook : Form
    {
        public frmAddressBook()
        {
            InitializeComponent();
        }

        const string UNF = "User Not Found";
        const string FN = "First Name:\t";
        const string LN = "Last Name:\t";
        const string PN = "Phone Number\t";

        string[] firstNames = { "Markel", "Luiza", "Bryony", "Giraldo", "Lowri" };
        string[] lastNames = { "Diggory", "Gunnar", "Hester", "Addy", "Hari" };
        string[] phoneNumber = { "555-8390", "555-4618", "555-4440", "555-1687", "555-7763" };


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtForL.Text = "";
            lblResult.Text = "";
            txtForL.Focus();
        }

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool keepGoing = CheckForNoInput();
            if(keepGoing)
            {
                PerformTheSearch();
            }
        }

        private bool CheckForNoInput()
        {
            bool retVal = true;
            string term = txtForL.Text.Trim();

            if (term == "")
            {
                ShowErrorMessage("You Must Enter a First or Last Name", "Seach Term Empty");

                ClearForm();
                retVal = false;
            }

            return retVal;
        }

        private void PerformTheSearch()
        {
            string term = txtForL.Text.Trim();
            bool isFound = false;
            int indexNumber = -1;
            string outputStr = "";

            for(int lcv = 0; lcv < firstNames.Length; lcv++)
            {
                if (firstNames[lcv].ToLower().Contains(term.ToLower()) || lastNames[lcv].ToLower().Contains(term.ToLower()))
                {
                    isFound = true;
                    indexNumber = lcv;
                    break;
                }
            }

            if (isFound)
            {
                outputStr += ($"{FN} {firstNames[indexNumber]}\r\t\r\t\n");
                outputStr += ($"{LN} {lastNames[indexNumber]}\r\t\r\t\n");
                outputStr += ($"{PN} {phoneNumber[indexNumber]}\r\t\r\t\n");
            }
            else
            {
                outputStr += ($"{FN} {UNF[indexNumber]}\r\t\r\t\n");
                outputStr += ($"{LN} {UNF[indexNumber]}\r\t\r\t\n");
                outputStr += ($"{PN} {UNF[indexNumber]}\r\t\r\t\n");
            }

            lblResult.Text = outputStr;
        }
    }
}
