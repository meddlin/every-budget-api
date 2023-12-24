import string
import uuid
from flask import Flask
from flask_cors import CORS, cross_origin
import os
import psycopg2
from sqlalchemy import create_engine
from sqlalchemy.sql import func
from sqlalchemy import Table, Column, String, MetaData, DateTime
from sqlalchemy.dialects.postgresql import UUID

from api.data_models.budget import Budget

app = Flask(__name__)
CORS(app)

## Using SQLAlchemy: https://www.compose.com/articles/using-postgresql-through-sqlalchemy/

###
### API Routes
###

@app.route('/hello', methods=['GET', 'POST'])
@cross_origin()
def welcome():
    return "Hello World"

@cross_origin()
@app.route('/dbtest', methods=['GET', 'POST'])
def dbtest():
    conn = psycopg2.connect(
        host ="localhost",
        port="5434",
        database="everybudget_db",
        user='postgres',
        password='jailbreak'
    )

    cur = conn.cursor()
    cur.execute('SELECT * FROM budget')

    res = cur.fetchone()
    print(res)

    # conn.commit()
    cur.close()
    conn.close()

    return res

@cross_origin()
@app.route('/alchemy', methods=['GET', 'POST'])
def alchemy():
    budget = Budget('Test Budget')
    return budget.create()

@cross_origin()
@app.route('/get-budget', methods=['GET'])
def get_budget():
    res = Budget.get("cfd9087a-1305-4b23-9ac9-50fed35af9ff")
    return res

@app.route('/budget/create', methods=['POST'])
@cross_origin()
def budget_create(): 
    return "budget create -- placeholder"

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)