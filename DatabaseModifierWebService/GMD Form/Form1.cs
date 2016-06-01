using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD_Form
{
    public partial class Form1 : Form
    {
        GMD_Form.ServiceReference1.WebService1SoapClient service;
        long currentUserID = -1;
        long currentRefID = -1;

        string convFN, convLN;
        long convSSN, convMN;
        int convPIN;

        public void convertInput(TextBox firstName, TextBox lastName, TextBox socialSecurity, TextBox mobileNum, TextBox PIN, ref string convFirstName, ref string convLastName, 
            ref long convSocialSecurity, ref long convMobileNum, ref int convPIN)
        {
            convFirstName = Convert.ToString(firstName);
            convLastName = Convert.ToString(lastName);
            convSocialSecurity = Convert.ToInt64(socialSecurity);
            convMobileNum = Convert.ToInt64(mobileNum);
            convPIN = Convert.ToInt32(PIN);
        }

        public Form1()
        {
            InitializeComponent();
            service = new ServiceReference1.WebService1SoapClient();
        }

        private void register_Click(object sender, EventArgs e)
        {
            convertInput(registerFirstName, registerLastName, registerSocialSecurityNumber, registerMobileNum, registerPIN, ref convFN, ref convLN, ref convSSN, ref convMN, ref convPIN);
            currentRefID = service.RegisterUser(convFN, convLN, convSSN, convMN, convPIN, ref currentUserID);
            MessageBox.Show(currentRefID.ToString());
        }
    }
}
