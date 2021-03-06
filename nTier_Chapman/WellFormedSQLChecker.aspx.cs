﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using DataAccess;


namespace chapman
{

    public partial class _Default : System.Web.UI.Page
    {

        blSQLapp obj_blSQLapp = new blSQLapp();
        daSQLapp obj_daSQLapp = new daSQLapp();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCheckSyntax_Click(object sender, EventArgs e)
        {
            Errors1.Text = "No Errors Found.";
            string inputStr = TextBox1.Text;                                                            // set the input string equal to the input in Textbox1

            if (inputStr.Length == 0)
            {
                Errors1.Text = "No SQL provided.";                                                      // if the user enters no input, then output to error box, letting user know.
            }
            else
            {
                inputStr = ScrubSQLstring(inputStr);                                                    // pass inputStr to Scrub function, set to inputStr
                char[] delimiters = new char[] { ' ' };                                                   // set delimiter characters
                // pass scrubbed input, and element count to Build Token array, located in the utilities.cs file.
                var checker = new SQLutilities();                                                       // create instance of our SQLutilities class
                string[] tokenArray = BuildTokenArray(inputStr);                                        // pass our instance the input string IOT have it build an array consisting of the components(tokens) of the SQL input.
                checker.CheckSQLsyntax(tokenArray);                                                     // pass the array of tokens to the CheckSQLsyntax() method
                if (checker.errorFound)
                {                                                                // check to see if errorFound was tripped in our instances.
                    Errors1.Text = checker.getError();                                                  // if they were, then set errors to text box <<<<<<<<<<<<<<<<<<<<<<DEBUGGING STEP>>>>>>>>>>>>>>>>>>>
                }
                List<string> lst = tokenArray.OfType<string>().ToList();                                // take the array that we got back and turn it into a list

                GridView1.DataSource = lst;                                                             // set the datasource
                GridView1.DataBind();                                                                   // bind it
            }
        }

        protected string ScrubSQLstring(string inputStr)
        {
            string scrubbedInput = "";                                                                  // Declare variable for the scrubbed input I am creating.
            while (inputStr.Contains("  "))
            {                                                           // While our input string contains more than one space...
                inputStr = inputStr.Replace("  ", " ");                                                 // then replace those two spaces with one space....repeat as necessary
            }
            while (inputStr.Contains(" ,"))
            {                                                           // While our input string contains a space before a comma
                inputStr = inputStr.Replace(" ,", ",");                                                 // replace with just a comma.
            }
            while (inputStr.Contains(" ;"))
            {                                                           // While our input string contains a space before a semicolon
                inputStr = inputStr.Replace(" ;", ";");                                                 // replace with just a semicolon.
            }
            scrubbedInput = inputStr.Trim();                                                            // Trim leaing and trailing spaces from input and save it to scrubbedInput.
            return scrubbedInput;
        }
        public string[] BuildTokenArray(string inputStr)
        {
            char[] delimiters = new char[] { ' ' };                         // delimiters to split on.
            string[] tokens = inputStr.Split(delimiters);                 // Returns a string array that contains the substrings in this instance that are delimited by elements of a Unicode character array, defined in the previous step.
            return tokens;                                                // return array of elements.
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}