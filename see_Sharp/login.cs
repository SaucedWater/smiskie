//using Microsoft.Data.SqlClient;


// Start of Login Button
private void login_loginnBtn_Click(object sender, EventArgs e) 
{
    // check for blanks
    if(login_usernametb.Text == "" 
        || login_passwordtb.Text == "")
    {
        MessageBox.Show("Please fill in the boxes!", "Error Message",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    else
    {
        // check for open connection
        if(connect.State == ConnectionState.Closed)
        {
            try
            {
                //open connection
                connect.Open();

                // select username AND password
                string selectData = "SELECT * FROM users WHERE username = @username " +
                    "AND password = @password";

                // 
                using(SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    cmd.Parameters.AddWithValue("@username", login_usernametb.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", login_passwordtb.Text.Trim());

                    // SQL Adapter
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // check for exisitng data
                    if(table.Rows.Count >= 1)
                    {
                        MessageBox.Show("Login Successfully!"
                            , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //load main form
                        MainForm mForm = new MainForm();
                        mForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username/Password"   
                            , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex,
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }

    }
}