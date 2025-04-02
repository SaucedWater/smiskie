### other fun stuff! (sort of)


# Change page using buttons
```cs
private void dashboard_btn_Click(object sender, EventArgs e)
{
    dashboard1.Visible = true;
    addEmployee1.Visible = false;
    salary1.Visible = false;
}

private void addEmployee_btn_Click(object sender, EventArgs e)
{
    dashboard1.Visible = false;
    addEmployee1.Visible = true;
    salary1.Visible = false;
}

private void salary_btn_Click(object sender, EventArgs e)
{
    dashboard1.Visible = false;
    addEmployee1.Visible = false;
    salary1.Visible = true;
}
```