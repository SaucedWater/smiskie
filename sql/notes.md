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
```