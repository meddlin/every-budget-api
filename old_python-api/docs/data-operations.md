# Database Config & Connecting Python

## Getting Started

Ref: [https://www.compose.com/articles/using-postgresql-through-sqlalchemy/](https://www.compose.com/articles/using-postgresql-through-sqlalchemy/)

This is a really good article explaining how to work with SQLAlchemy. It covers moving from basic 
SQL commands to simple CRUD operations with SQLAlchemy, in Python code.

However, the article doesn't cover a few things I encountered for what my code needed. So read below 
for what I encountered.

- connection string
- UUIDs in SQLAlchemy columns
- Dates in columns, and using the server default value

## Example: code snippet where it's all used

```python
def save_new(self):
    db_string = "postgresql://postgres:jailbreak@localhost:5434/everybudget_db"
    db = create_engine(db_string)
    meta = MetaData(db)
    budget_table = Table('budget', meta, 
        Column('id', UUID(as_uuid=True), primary_key=True, default=uuid.uuid4),
        Column('date_created', DateTime(timezone=True), server_default=func.now()),
        Column('name', String))
    
    res = ''
    
    with db.connect() as conn:
        insert_statement = budget_table.insert().values(name=self.title)
        res = conn.execute(insert_statement)
    
    return str(res)
```

## Connection String

Ref: [https://stackoverflow.com/questions/62688256/sqlalchemy-exc-nosuchmoduleerror-cant-load-plugin-sqlalchemy-dialectspostgre](https://stackoverflow.com/questions/62688256/sqlalchemy-exc-nosuchmoduleerror-cant-load-plugin-sqlalchemy-dialectspostgre)

As the post states, it's necessary to use `postgresql://` in the connection string. The article 
still uses `postgres://`.

## Using UUIDs in SQLAlchemy Columns

Ref: [https://stackoverflow.com/questions/183042/how-can-i-use-uuids-in-sqlalchemy](https://stackoverflow.com/questions/183042/how-can-i-use-uuids-in-sqlalchemy)

```python
    Column('id', UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)
```

## Using Date types (e.g. Timestamp)

Ref: [https://stackoverflow.com/questions/13370317/sqlalchemy-default-datetime](https://stackoverflow.com/questions/13370317/sqlalchemy-default-datetime)

We're configuring the column to use a `DateTime`, also setting `timezone=True` because we configured the
column in Postgres to be _Timestamp with timezone_, and finally informing SQLAlchemy to use the 
timestamp _from the DB server, **not** the application_ in `server_default=func.now()`.

```python
    Column('date_created', DateTime(timezone=True), server_default=func.now())
```

This takes care of the code, but Postgres needs to be configured to actually have a default value set for
the column. So, open pgAdmin, navigate to the column necessary, right-click -> Properties, switch the 
Constraints tab, and in "Default" set `CURRENT_TIMESTAMP`.