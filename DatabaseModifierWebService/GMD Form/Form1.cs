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
        GMD_Form.ServiceReference1.WebService1SoapClient service;
        DatabaseModifier.User currentUser = new DatabaseModifier.User();

        string convFN, convLN, convMN;
        long convSSN;
        int convPIN;

        public void convertInput(TextBox firstName, TextBox lastName, TextBox socialSecurity, TextBox mobileNum, TextBox PIN, ref string convFirstName, ref string convLastName, 
            ref long convSocialSecurity, ref string convMobileNum, ref int convPIN)
        {
            convFirstName = Convert.ToString(firstName);
            convLastName = Convert.ToString(lastName);
            convSocialSecurity = Convert.ToInt64(socialSecurity.Text.ToString());
            convMobileNum = Convert.ToString(mobileNum);
            convPIN = int.Parse(PIN.Text.ToString());
        }

        //Function for adding new users
        public User addUser(string FN, string LN, string mobileNum, long SSN, int PIN)
        {
            User newUser = new User();
            newUser.firstName = FN;
            newUser.lastName = LN;
            newUser.mobileNumber = mobileNum;
            newUser.SSN = SSN;
            newUser.PIN = PIN;
            return newUser;
        }



        public Form1()
        {
            InitializeComponent();
            service = new ServiceReference1.WebService1SoapClient();
        }



        //RegisterButton:
        private void register_Click(object sender, EventArgs e)
        {
            convertInput(registerFirstName, registerLastName, registerSocialSecurityNumber, registerMobileNum, registerPIN, ref convFN, ref convLN, ref convSSN, ref convMN, ref convPIN);
            currentUser = addUser(convFN, convLN, convMN, convSSN, convPIN);

            //Need to figure out a better way to do this or some way to fix it:
            int returnState = service.RegisterUser(ref currentUser);

            //IF New user state:
            if (returnState == 1)
            {
                MessageBox.Show("Welcome " + currentUser.firstName + "! You are officially registered in our sytem", "Welcome");
            }
            //IF Existing user state:
            else if (returnState == 2)
            {
                MessageBox.Show("Welcome " + currentUser.firstName + ",\nyou are logged in to our sytem!", "Logged In");
            }
            
        }

        //UpdateLoyaltyButton:
        private void loyaltyUpdate_Click(object sender, EventArgs e)
        {
            if (currentUser.ID == 0)
            {
                MessageBox.Show("You must register/login first!\nPlease try again!");
            }
            else
            {
                service.addLoyalty(ref currentUser);
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
                service.getLoyaltyBalance(ref currentUser);
                MessageBox.Show("You have " + currentUser.loyaltyVal + " points!", "Sucessfully Checked Points");
            }
        }
    }
}
