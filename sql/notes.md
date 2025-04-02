#### Microsoft SQL Notes (Vscode C#)
### Regular SQL Query
```sql
CREATE TABLE users 
(
	id INT PRIMARY KEY IDENTITY(1,1),
	username VARCHAR(MAX) NULL,
	password VARCHAR(MAX) NULL,
	date_register DATETIME NULL
)

SELECT * FROM users

CREATE TABLE employees
(
	id INT PRIMARY KEY IDENTITY(1,1),
	employee_id VARCHAR(MAX) NULL,
	full_name VARCHAR (MAX) NULL,
	gender VARCHAR (MAX) NULL,
	contact_number VARCHAR (MAX) NULL,
	position VARCHAR (MAX) NULL,
	image VARCHAR (MAX) NULL,
	salary INT NULL,
	insert_date DATE NULL,
	update_date DATE NULL,
	delete_date DATE NULL
)

select * from employees
```