using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace InVestme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        // Declearing all Constants and Variable
        const decimal UNDER_TWOFIFTY_ONEYEAR = 1.005M, UNDER_TWOFIFTY_THREEYEAR = 1.00625M, UNDER_TWOFIFTY_FIVEYEAR = 1.007125M,
            UNDER_TWOFIFTY_TENYEAR = 1.010125M, OVER_TWOFIFTY_ONEYEAR = 1.006M, OVER_TWOFIFTY_THREEYEAR = 1.00725M,
            OVER_TWOFIFTY_FIVEYEAR = 1.008125M, OVER_TWOFIFTY_TENYEAR = 1.01025M, OVER_MILL = 25000M;


        const int ONE_YEAR_MONTHS = 12, THREE_YEAR_MONTHS = 36, FIVE_YEAR_MONTH = 60, TEN_YEAR_MONTHS = 120;
        const string PROGRAM_FILE = "alltransactions.txt";
        int term;
        string transactionNumber, customerName, customerEmail, customerTelephoneNumber;
        decimal startingBalance, endBalance;
        string oneYear = "1 Year", threeYear = "3 Years", fiveYear = "5 Years", tenYear = "10 Years";

        private void Form1_Load(object sender, EventArgs e)
        {
            StartingPosition();
            
        }

        // Clears form for new Transaction
        private void clearButton_Click(object sender, EventArgs e)
        {
            inititalInvestmentTextBox.Focus();
            StartingPosition();
            ClearCustomerVariables();
            ClearDetails();
            searchResult.Text = "";
            summaryGroupBox.Visible = false;
            investmentCreationGroupBox.Visible = true;

        }
        // Exits from
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Clears form for new Transaction
        private void logoPictureBox_Click(object sender, EventArgs e)
        {
            inititalInvestmentTextBox.Focus();
            StartingPosition();
            ClearCustomerVariables();
            ClearDetails();
            searchResult.Text = "";
            summaryGroupBox.Visible = false;
            investmentCreationGroupBox.Visible = true;
        }
        // Method to clear parts of Form
        private void StartingPosition()
        {
            investmentDetailsConfirmationGroupbox.Visible = false;
            customerDetailInputGroupBox.Visible = false;
            telephoneNumberTextBox.Text = "";
            emailAddressNameTextBox.Text = "";
            nameTextBox.Text = "";
            searchResultsListbox.Visible = false;
            inititalInvestmentTextBox.Focus();
            ClearCustomerVariables();
            ClearDetails();
            searchResult.Text = "";
            searchTextBox.Text = "";
            outputListBox.Visible = false;
            proceedButton.Visible = false;
        }

        private void searchTransNoButtons_Click(object sender, EventArgs e)
        {
                    // Disables Visibility on relevant objects  
                confirmTransactionButton.Visible = false;
                investmentCreationGroupBox.Visible = true;
                customerDetailInputGroupBox.Visible = false;
                ClearDetails();
                investmentDetailsConfirmationGroupbox.Visible = false;
                outputListBox.Visible = false;
                proceedButton.Visible = false;

            // Declear local Stringd
            string email, transNumber, investTerm, name, teleNumber;
                decimal investedAmmount, investEarnings;
                if (searchTextBox.Text != "")
                {
                    try
                    {
                        // Open File
                        StreamReader transactionfile;
                        transactionfile = File.OpenText(PROGRAM_FILE);

                        // loops file records and display Relevant Recrod
                        while (!transactionfile.EndOfStream)
                        {
                            // Read 7 lines of file and Stores in relevant variable
                            transNumber = transactionfile.ReadLine();
                            email = transactionfile.ReadLine();
                            name = transactionfile.ReadLine();
                            teleNumber = transactionfile.ReadLine();
                            investedAmmount = decimal.Parse(transactionfile.ReadLine());
                            investEarnings = decimal.Parse(transactionfile.ReadLine());
                            investTerm = transactionfile.ReadLine();
                            // check for search term and display relevant record
                            if (transNumber == searchTextBox.Text)
                            {
                                transactionNumberDisplayLabel.Text = transNumber;
                                emailDisplayLabel.Text = email;
                                nameDisplayLabel.Text = name;
                                telephoneNumebrDisplayLabel.Text = teleNumber;
                                investmentAmountDisplayLaebel.Text = investedAmmount.ToString("C");
                                interestEarnedDisplayLabel.Text = investEarnings.ToString("C");
                                investmentTermDisplayLablel.Text = investTerm;

                                searchResult.Text = " Search Results";
                                searchResultsListbox.Visible = false;
                                investmentDetailsConfirmationGroupbox.Visible = true;
                            }
                            // Gives no results
                            else if (transNumber != searchTextBox.Text )
                            {
                                searchResult.Text = "No Matching Records found";
                            }

                        }
                        // Closes File
                        transactionfile.Close();


                    }
                    catch 
                    {
                    ErrorMessage("Error Reading File", "File Error");
                    }

                }
                else
                {
                    ErrorMessage("Please Enter Transaction Number to serach", "error");
                }
            

        }
        // Method to Clears Variables
        private void ClearCustomerVariables()
        {
            transactionNumber = "";
            customerName = "";
            customerEmail = "";
            customerTelephoneNumber = "";
            startingBalance = 0;
            endBalance = 0;
            term = 0;
        }
        
        // Clears form for new transaction
        private void newInvestmentButton_Click(object sender, EventArgs e)
        {
            summaryGroupBox.Visible = false;
            investmentCreationGroupBox.Visible = true;
            investmentDetailsConfirmationGroupbox.Visible = false;
            StartingPosition();
        }
        // Search for emails
        private void searchByEmail_Click(object sender, EventArgs e)
        {
            confirmTransactionButton.Visible = false;
            outputListBox.Visible = false;
            proceedButton.Visible = false;
            investmentCreationGroupBox.Visible = true;
            customerDetailInputGroupBox.Visible = false;
            investmentDetailsConfirmationGroupbox.Visible = false;
            ClearDetails();
            searchResultsListbox.Items.Clear();
            // declear local variables
            string email, transNumber, investTerm, name, teleNumber;
            decimal investedAmmount, investEarnings;
            if (searchTextBox.Text != "")
            {
                try
                {
                    // Opnens File
                    StreamReader transactionfile;
                    transactionfile = File.OpenText(PROGRAM_FILE);
                   
                    // loops file records and display Relevant Recrod
                    while (!transactionfile.EndOfStream)
                    {
                        transNumber = transactionfile.ReadLine();
                        email = transactionfile.ReadLine();
                        name = transactionfile.ReadLine();
                        teleNumber = transactionfile.ReadLine();
                        investedAmmount = decimal.Parse(transactionfile.ReadLine());
                        investEarnings = decimal.Parse(transactionfile.ReadLine());
                        investTerm = transactionfile.ReadLine();
                        // check for search term and display relevant records
                        if (email == searchTextBox.Text)
                        {
                            searchResultsListbox.Items.Add("Transation Number  "+ "Name             " + "Phone Number " + " Principal " + "      Total Interest"+ " Term");
                            searchResultsListbox.Items.Add(transNumber + "                    " + name + "         " + teleNumber + "           " + 
                                investedAmmount.ToString("C") + "   "  + investEarnings.ToString("C") +"         "+investTerm+ " Year(s)" );
                            
                            searchResult.Text = " Search Results";
                            investmentDetailsConfirmationGroupbox.Visible = true;
                            searchResultsListbox.Visible = true;
                        }
                        // Gives no results
                        else if (email != searchTextBox.Text) 
                        {
                            searchResult.Text = "No Matching Records found";
                        }

                    }
                    transactionfile.Close();


                }
                catch 
                {
                    ErrorMessage("Error Reading File", "File Error");
                }

            }
            else
            {
                ErrorMessage("Please Enter Email to serach", "error");
            }
        }

        

        private void summaryButton_Click_1(object sender, EventArgs e)
        {
            summaryGroupBox.Visible = true;
            investmentCreationGroupBox.Visible = false;
            exitButton.Visible = true;

            string filetransactionNumber;
            decimal totalInvested = 0, totalEarned = 0;
            int totalterm = 0, count = 0;
            summaryListBox.Items.Clear();
            try
            {
                // Reads File and Displays Summary of records
                StreamReader transactionfile;
                transactionfile = File.OpenText(PROGRAM_FILE);

                while (!transactionfile.EndOfStream)
                {
                    filetransactionNumber = transactionfile.ReadLine();
                    summaryListBox.Items.Add(filetransactionNumber);

                    transactionfile.ReadLine();
                    transactionfile.ReadLine();
                    transactionfile.ReadLine();

                    totalInvested += decimal.Parse(transactionfile.ReadLine());
                    totalEarned += decimal.Parse(transactionfile.ReadLine());
                    totalterm += int.Parse(transactionfile.ReadLine());
                    count += 1;
                }
                transactionfile.Close();

                totalInvestedDisplayLable.Text = totalInvested.ToString("C");
                totalEarningsDisplayLabel.Text = totalEarned.ToString("C");
                averageDisplayTerm.Text = (totalterm / count).ToString();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void confirmDetailsButton_Click_1(object sender, EventArgs e)
        {
            // Makes Sure a name is enterd
           if(nameTextBox.Text != "")
            {
                                          
                    try
                    {// makes sure numerical input for phone number
                    int.Parse(telephoneNumberTextBox.Text);
                    // Check email format
                    char at = '@', dot = '.' ;
                        if (emailAddressNameTextBox.Text.IndexOf(at) >= 0 && emailAddressNameTextBox.Text.IndexOf(dot) >= 0)
                        {
                            
                            // Takes in and Stores Detailss
                            customerName = nameTextBox.Text;
                            customerEmail = emailAddressNameTextBox.Text;
                            customerTelephoneNumber = telephoneNumberTextBox.Text;


                            investmentAmountDisplayLaebel.Text = startingBalance.ToString("C");
                            interestEarnedDisplayLabel.Text = Earnings(endBalance).ToString("C");
                            investmentTermDisplayLablel.Text = term.ToString();
                            nameDisplayLabel.Text = customerName;
                            telephoneNumebrDisplayLabel.Text = customerTelephoneNumber;
                            emailDisplayLabel.Text = customerEmail;
                            investmentDetailsConfirmationGroupbox.Visible = true;
                            searchResultsListbox.Visible = false;
                            confirmTransactionButton.Visible = true;


                    }

                        else
                        {
                        ErrorMessage("Please Enter a valid email address", "Input Error");
                        }



                    }
                    catch
                    {
                ErrorMessage("Please Enter numbers only", "Input Error");
                    }
            }
            else
            {
                ErrorMessage("Please Enter Customers Name", "Input Error");
            }
        }

        private void confirmTransactionButton_Click(object sender, EventArgs e)
        {
            // Confirms Detail from user
            DialogResult dialogResult = MessageBox.Show("Please confirm the details then Press Yes to confirm the Transction" +
                Environment.NewLine + "Name: " + customerName + Environment.NewLine + "Email: " + customerEmail + Environment.NewLine +
                "Telephone Number: " + customerTelephoneNumber + Environment.NewLine + "Investment Amount: " + startingBalance.ToString("C")
                + Environment.NewLine + "Finishing Balance: " + endBalance.ToString("C") + Environment.NewLine + "Term: " + term + Environment.NewLine
                + "Interet Earned: " + Earnings(endBalance).ToString("C"), "Confirm Transaction?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (dialogResult == DialogResult.Yes)
            {  
                try
                {
                    // Creates file and stores deatils
                    if (!File.Exists(PROGRAM_FILE))
                    {
                        StreamWriter transactionfile;
                        transactionfile = File.CreateText(PROGRAM_FILE);

                        transactionfile.WriteLine(transactionNumber);
                        transactionfile.WriteLine(customerEmail);
                        transactionfile.WriteLine(customerName);
                        transactionfile.WriteLine(customerTelephoneNumber);
                        transactionfile.WriteLine(startingBalance);
                        transactionfile.WriteLine(Earnings(endBalance));
                        transactionfile.WriteLine(term);
                        transactionfile.Close();

                        MessageBox.Show("Investment Has been Confirmed", "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearDetails();
                        ClearCustomerVariables();
                        StartingPosition();
                    }
                    else
                    {
                        // Store details if file Exists
                        StreamWriter transactionfile;
                        transactionfile = File.AppendText(PROGRAM_FILE);

                        transactionfile.WriteLine(transactionNumber);
                        transactionfile.WriteLine(customerEmail);
                        transactionfile.WriteLine(customerName);
                        transactionfile.WriteLine(customerTelephoneNumber);
                        transactionfile.WriteLine(startingBalance);
                        transactionfile.WriteLine(Earnings(endBalance));
                        transactionfile.WriteLine(term);
                        transactionfile.Close();

                        MessageBox.Show("Investment Has been Confirmed", "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearDetails();
                        ClearCustomerVariables();
                        StartingPosition();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

            }
        }

        private void ClearDetails()
        {
            // Clears parts of the form
            transactionNumberDisplayLabel.Text = "";
            emailDisplayLabel.Text = "";
            nameDisplayLabel.Text = "";
            telephoneNumebrDisplayLabel.Text = "";
            investmentAmountDisplayLaebel.Text = "";
            interestEarnedDisplayLabel.Text = "";
            investmentTermDisplayLablel.Text = "";
            outputListBox.Items.Clear();
            inititalInvestmentTextBox.Text = "";
           
        }
        

       private void AddToListBox(decimal interest, string term)
        {
            // Display Interest                       
            outputListBox.Items.Add(endBalance.ToString("C") + " At  " +
                ((interest * 100) - 100).ToString() + "% For  " + term);
        }

        private void CalculateInterest (int months, decimal interest)
        {
            // Calculates interest
            int counter;
            endBalance = startingBalance;
            for (counter = 0; counter < months; counter +=1 )
            {
                
                endBalance = endBalance * interest;
               
            }
            term = InvestmentTerm(months);
          
        }


        private void CalculateInterestOverMillion(int months, decimal interest)
        {
            // Calculates interest
            int counter;
            endBalance = startingBalance;
            for (counter = 0; counter < months; counter += 1)
            {
                endBalance = endBalance * interest;
            }
            endBalance = endBalance + OVER_MILL;
            term = InvestmentTerm(months);

        }
        // Changes term to Years
        private int InvestmentTerm(int months)
        {
            return months / 12;
        }
        // Calculates Earningd
        private decimal Earnings ( decimal finishingBalance)
        {
            return finishingBalance - startingBalance;
        }

        // Generates random number
        private void GenereateTransactionNumber()
        {
            do
            {

                int a, b, c, d, e, f;
                Random rand = new Random();
                a = rand.Next(10);
                b = rand.Next(10);
                c = rand.Next(10);
                d = rand.Next(10);
                e = rand.Next(10);
                f = rand.Next(10);

                transactionNumber = a.ToString() + b.ToString() + c.ToString() + d.ToString() + e.ToString() + f.ToString();
            }
            while (UniqueCheck(transactionNumber,PROGRAM_FILE) == false);
        }

        // Checks the variable enterd is uniquw
     private Boolean UniqueCheck (string toCheck, string fileName)
     {
            Boolean Unique = false;
            StreamReader transactionfile;
            transactionfile = File.OpenText(fileName);

            while(!transactionfile.EndOfStream)
            {
                if(toCheck.Equals(transactionfile.ReadLine()))
                {
                    transactionfile.Close();
                    return Unique;
                }
            }
            transactionfile.Close();
            return Unique = true;


     }

        // error message
        private void ErrorMessage(string dataType, string messageName)
        {
            MessageBox.Show(dataType, messageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void proceedButton_Click(object sender, EventArgs e)
        {
            // Stores the relevant Variables for the selected items
            if (outputListBox.SelectedIndex != -1)
            {
                
                if (startingBalance < 250000)
                {
                    switch (outputListBox.SelectedIndex)
                    {
                        case 0:
                            CalculateInterest(ONE_YEAR_MONTHS, UNDER_TWOFIFTY_ONEYEAR);                          
                            break;

                        case 1:
                            CalculateInterest(THREE_YEAR_MONTHS, UNDER_TWOFIFTY_THREEYEAR);
                            break;
                        case 2:
                            CalculateInterest(FIVE_YEAR_MONTH, UNDER_TWOFIFTY_FIVEYEAR);                            
                            break;
                        case 3:
                            CalculateInterest(TEN_YEAR_MONTHS, UNDER_TWOFIFTY_TENYEAR);                            
                            break;

                    }

                }
                else if (startingBalance >= 250000 && startingBalance < 1000000)
                {
                    switch (outputListBox.SelectedIndex)
                    {
                        case 0:
                            CalculateInterest(ONE_YEAR_MONTHS, OVER_TWOFIFTY_ONEYEAR);                           
                            break;

                        case 1:
                            CalculateInterest(THREE_YEAR_MONTHS, OVER_TWOFIFTY_THREEYEAR);                          
                            break;
                        case 2:
                            CalculateInterest(FIVE_YEAR_MONTH, OVER_TWOFIFTY_FIVEYEAR);                            
                            break;
                        case 3:
                            CalculateInterest(TEN_YEAR_MONTHS, OVER_TWOFIFTY_TENYEAR);                            
                            break;

                    }

                }
                else if (startingBalance >= 1000000)
                {
                    switch (outputListBox.SelectedIndex)
                    {
                        case 0:
                            CalculateInterestOverMillion(ONE_YEAR_MONTHS, OVER_TWOFIFTY_ONEYEAR);
                            break;

                        case 1:
                            CalculateInterestOverMillion(THREE_YEAR_MONTHS, OVER_TWOFIFTY_THREEYEAR);                            
                            break;
                        case 2:
                            CalculateInterestOverMillion(FIVE_YEAR_MONTH, OVER_TWOFIFTY_FIVEYEAR);                            
                            break;
                        case 3:
                            CalculateInterestOverMillion(TEN_YEAR_MONTHS, OVER_TWOFIFTY_TENYEAR);
                            break;

                    }

                }
                GenereateTransactionNumber();
                transactionNumberDisplayLabel.Text = transactionNumber;
                customerDetailInputGroupBox.Visible = true;
                customerDetailInputGroupBox.Visible = true;
                investmentDetailsConfirmationGroupbox.Visible = true;
                confirmTransactionButton.Visible = false;
                investmentAmountDisplayLaebel.Text = "";
                interestEarnedDisplayLabel.Text = "";
                investmentTermDisplayLablel.Text = "";
                nameDisplayLabel.Text = "";
                telephoneNumebrDisplayLabel.Text = "";
                emailDisplayLabel.Text = "";
                nameTextBox.Focus();
            }
            else
            {
                ErrorMessage("Please Select a Term ", "Input Error" );
            }

        }


        private void displayButton_Click(object sender, EventArgs e)
        {
            // clears form labels and display the needed part
            ClearCustomerVariables();
            outputListBox.Items.Clear();
            searchTextBox.Text = "";
            searchResultsListbox.Visible = false;
            investmentDetailsConfirmationGroupbox.Visible = false;
            searchResult.Text = "";
            customerDetailInputGroupBox.Visible = false;
            outputListBox.Items.Clear();
            try
            { 
                
            // Convert Investment to decimal
            startingBalance = decimal.Parse(inititalInvestmentTextBox.Text);

                if (startingBalance > 0)
                {
                    // Calculates and display Interent in Listbox
                    if (startingBalance < 250000)
                    {

                        CalculateInterest(ONE_YEAR_MONTHS, UNDER_TWOFIFTY_ONEYEAR);
                        AddToListBox(UNDER_TWOFIFTY_ONEYEAR, oneYear);
                        CalculateInterest(THREE_YEAR_MONTHS, UNDER_TWOFIFTY_THREEYEAR);
                        AddToListBox(UNDER_TWOFIFTY_THREEYEAR, threeYear);
                        CalculateInterest(FIVE_YEAR_MONTH, UNDER_TWOFIFTY_FIVEYEAR);
                        AddToListBox(UNDER_TWOFIFTY_FIVEYEAR, fiveYear);
                        CalculateInterest(TEN_YEAR_MONTHS, UNDER_TWOFIFTY_TENYEAR);
                        AddToListBox(UNDER_TWOFIFTY_TENYEAR, tenYear);

                    }

                    else if (startingBalance >= 250000 && startingBalance < 1000000)
                    {
                        CalculateInterest(ONE_YEAR_MONTHS, OVER_TWOFIFTY_ONEYEAR);
                        AddToListBox(OVER_TWOFIFTY_ONEYEAR, oneYear);
                        CalculateInterest(THREE_YEAR_MONTHS, OVER_TWOFIFTY_THREEYEAR);
                        AddToListBox(OVER_TWOFIFTY_THREEYEAR, threeYear);
                        CalculateInterest(FIVE_YEAR_MONTH, OVER_TWOFIFTY_FIVEYEAR);
                        AddToListBox(OVER_TWOFIFTY_FIVEYEAR, fiveYear);
                        CalculateInterest(TEN_YEAR_MONTHS, OVER_TWOFIFTY_TENYEAR);
                        AddToListBox(OVER_TWOFIFTY_TENYEAR, tenYear);

                    }
                    else if (startingBalance >= 1000000)
                    {
                        CalculateInterest(ONE_YEAR_MONTHS, OVER_TWOFIFTY_ONEYEAR);
                        AddToListBox(OVER_TWOFIFTY_ONEYEAR, oneYear);
                        CalculateInterestOverMillion(THREE_YEAR_MONTHS, OVER_TWOFIFTY_THREEYEAR);
                        AddToListBox(OVER_TWOFIFTY_THREEYEAR, threeYear);
                        CalculateInterestOverMillion(FIVE_YEAR_MONTH, OVER_TWOFIFTY_FIVEYEAR);
                        AddToListBox(OVER_TWOFIFTY_FIVEYEAR, fiveYear);
                        CalculateInterestOverMillion(TEN_YEAR_MONTHS, OVER_TWOFIFTY_TENYEAR);
                        AddToListBox(OVER_TWOFIFTY_TENYEAR, tenYear);

                    }
                    outputListBox.Visible = true;
                    proceedButton.Visible = true;
                }
                else
                {
                    ErrorMessage("Please Type A Positve Number", "Input Error");
                }
            }
            catch
            {
                ErrorMessage("Numerical Input Expected", "Input Error");
                inititalInvestmentTextBox.Focus();
                inititalInvestmentTextBox.SelectAll();
            }
        }
    }
}
