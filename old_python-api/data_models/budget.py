import uuid, datetime, decimal
import json
import psycopg2
from data_models.encoder import alchemyencoder
from sqlalchemy import create_engine
from sqlalchemy.sql import func
from sqlalchemy import Table, Column, String, MetaData, DateTime
from sqlalchemy.dialects.postgresql import UUID

class Budget:
    def __init__(self, title):
        self.title = title
        self.categories = []

    ### This method requires an instance of Budget
    @classmethod
    def create(self):
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

    ### This method doesn't require an instance of Budget...but this is some confusing
    ###     usage, with the DB connection floating around.
    ### NOTE: See how `self` is no longer used in the method parameters
    @staticmethod
    def get(budget_id):
        db_string = "postgresql://postgres:jailbreak@localhost:5434/everybudget_db"
        db = create_engine(db_string)
        meta = MetaData(db)
        budget_table = Table('budget', meta, 
            Column('id', UUID(as_uuid=True), primary_key=True, default=uuid.uuid4),
            Column('date_created', DateTime(timezone=True), server_default=func.now()),
            Column('name', String))
        
        ## Weird 'no module found' error was solved by correctly importing user-defined 
        ##   modules. Ref: https://careerkarma.com/blog/python-modulenotfounderror/
        ##   encoder.alchemyencoder for that one
        ##  Apparently, we have to import relative to the working directory, NOT the
        ##      file we're importing *into*. 

        with db.connect() as conn:
            select_statement = budget_table.select().where(budget_table.c.id==budget_id)
            result_set = conn.execute(select_statement)
            data = json.dumps([dict(r) for r in result_set], default=alchemyencoder)
            print(data)
            for r in result_set:
                print(r)
                # return all rows as a JSON array of objects
                
        
        return data