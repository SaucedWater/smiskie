### Register Notes

## Ensure connection first (inside public class)
```cs
public partial class RegisterForm : Form
    {
        SqlConnection connect 
            = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WINDOWS 10\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30");
```

## Password Thingy
```cs
private void signup_showPass_CheckedChanged(object sender, EventArgs e)
{
    signup_passwordtb.PasswordChar = signup_showPass.Checked ? '\0' : '*';
}
```


## Change to Login/Anther page
```cs
private void signup_loginBtn_Click(object sender, EventArgs e)
{
    Form1 loginForm = new Form1();
    loginForm.Show();
    this.Hide();
}
```

## Sign Up Btn
```cs
    private void singup_signupBtn_Click(object sender, EventArgs e)
    {
        // error checking
        if(signup_usernametb.Text == ""
            || signup_passwordtb.Text == "")
        {
            MessageBox.Show("Please fill all blank fields", 
                "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            if(connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    // check for existing users
                    string selectUsername = "SELECT COUNT(*) FROM users WHERE username = @user";

                    using (SqlCommand checkUser = new SqlCommand(selectUsername, connect))
                    {
                        checkUser.Parameters.AddWithValue("@user", signup_usernametb.Text.Trim());
                        int count =(int)checkUser.ExecuteScalar();

                        if(count >= 1)
                        {
                            MessageBox.Show(signup_usernametb.Text.Trim() + " is already taken",
"Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            DateTime today = DateTime.Today;

                            string insertData = "INSERT INTO users " +
                                "(username, password, date_register) " +
                                "VALUES(@username, @password, @dataReg)";

                            using (SqlCommand cmd = new SqlCommand(insertData, connect))
                            {
                                cmd.Parameters.AddWithValue("@username", signup_usernametb.Text.Trim());
                                cmd.Parameters.AddWithValue("@password", signup_passwordtb.Text.Trim());
                                cmd.Parameters.AddWithValue("@dataReg", today);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Registered Successfully!!!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Form1 loginForm = new Form1(); //form1 is login form btw
                                loginForm.Show();
                                this.Hide();
                            }
                        }

                    }




                }catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex,
                "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }
    }
}
```