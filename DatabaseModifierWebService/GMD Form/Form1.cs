using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseModifier;



namespace GMD_Form
{
    public partial class Form1 : Form
    {
        GMD_Form.DMSoapLocal.WebService1SoapClient service;
        DMSoapLocal.User currentUser = new DMSoapLocal.User();

        string convFN, convLN, convMN;
        long convSSN;
        int convPIN;

        public void convertInput(TextBox firstName, TextBox lastName, TextBox socialSecurity, TextBox mobileNum, TextBox PIN, ref string convFirstName, ref string convLastName, 
            ref long convSocialSecurity, ref string convMobileNum, ref int convPIN)
        {
            convFirstName = Convert.ToString(firstName.Text);
            convLastName = Convert.ToString(lastName.Text);
            convSocialSecurity = Convert.ToInt64(socialSecurity.Text.ToString());
            convMobileNum = Convert.ToString(mobileNum.Text);
            convPIN = int.Parse(PIN.Text.ToString());
        }

        //Function for adding new users
        public DMSoapLocal.User addUser(string FN, string LN, string mobileNum, long SSN, int PIN)
        {
            DMSoapLocal.User newUser = new DMSoapLocal.User();
            newUser.firstName = FN;
            newUser.lastName = LN;
            newUser.mobileNumber = mobileNum;
            newUser.SSN = SSN;
            newUser.PIN = PIN;
            return newUser;
        }

        public DMSoapLocal.User addUserMN(string MN)
        {
            DMSoapLocal.User newUser = new DMSoapLocal.User();
            newUser.mobileNumber = MN;

            return newUser;
        }



        public Form1()
        {
            InitializeComponent();
            service = new DMSoapLocal.WebService1SoapClient();
            
        }



        //RegisterButton:
        private void register_Click(object sender, EventArgs e)
        {
            convertInput(registerFirstName, registerLastName, registerSocialSecurityNumber, registerMobileNum, registerPIN, ref convFN, ref convLN, ref convSSN, ref convMN, ref convPIN);
            currentUser = addUser(convFN, convLN, convMN, convSSN, convPIN);

            currentUser = service.RegisterUserWeb(currentUser);
            int returnState = currentUser.option;

            //DEBUG OUTPUT            MessageBox.Show("FN: " + currentUser.firstName + "\nLN: " + currentUser.lastName + "\nMN: " + currentUser.mobileNumber + "\nSSN " + currentUser.SSN + "\nPIN " + currentUser.PIN + "\nDEBUG OUTPUT");
            if (returnState == 1)
            {
                MessageBox.Show("Welcome " + currentUser.firstName + "! you are officially registered in our sytem!", "Welcome");
            }
            //if existing user state:
            else if (returnState == 2)
            {
                MessageBox.Show("Welcome " + currentUser.firstName + ",\nyou are logged in to our sytem!", "Logged In");
            }

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            currentUser = addUserMN(loginMobileNumber.Text.ToString());
            

            currentUser = service.loginUser(currentUser);
            int returnState = currentUser.option;

            if (returnState == 1)
            {
                MessageBox.Show("You are not registered. Check your mobile number or register.", "Not Registered");
            }

            else if (returnState == 2)
            {
                MessageBox.Show("You are logged in " + currentUser.firstName + "!", "Logged In");
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            currentUser = new DMSoapLocal.User();
            registerFirstName.Clear();
            registerLastName.Clear();
            registerMobileNum.Clear();
            registerSocialSecurityNumber.Clear();
            registerPIN.Clear();
            loginMobileNumber.Clear();
            MessageBox.Show("You have been logged out");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //UpdateLoyaltyButton:
        private void loyaltyUpdate_Click(object sender, EventArgs e)
        {
            if (currentUser.ID == 0)
            {
                MessageBox.Show("Please Login!", "You must register/login first!\nPlease try again!");
            }
            else
            {
                currentUser = service.addLoyalty(currentUser);
                MessageBox.Show("You have " + currentUser.loyaltyVal + " points!", "Sucessfully Updated Points");
            }
        }

        //Check Loyalty Button:
        private void checkLoyaltyValue_Click(object sender, EventArgs e)
        {
            if(currentUser.ID == 0)
            {
                MessageBox.Show("You must register/login first!\nPlease try again!");
            }
            else
            {
                currentUser = service.getLoyaltyBalance(currentUser);
                MessageBox.Show("You have " + currentUser.loyaltyVal + " points!", "Sucessfully Checked Points");
            }
        }
    }
}
