# Flask API Design


## Design Example Tutorial 

This is a really good walk-thru of an example Flask
application. It also explains how to properly structure 
a Flask application.

Many tutorials show a simple, single-file `app.py` structure, but this one shows how to properly separate 
responsibilities, use Blueprints, and add routes.

Ref: [https://hackersandslackers.com/flask-blueprints/](https://hackersandslackers.com/flask-blueprints/)


## Good Example of a Route

This article doesn't offer much over the previous, but
does include this example of a route method (see below).
Uses: a route param, an API route on a Blueprint, and
the `Company.query.filter` line uses SQLAlchemy.

Ref: [http://allynh.com/blog/adding-an-api-to-your-flask-project/](http://allynh.com/blog/adding-an-api-to-your-flask-project/)

```python
@bp.route('/get_company/<int:id>')
@login_required
def get_company(id):
    c = Company.query.filter_by(id=id).first_or_404()
    message = "Welcome to the API :)"
    content = {
        "name"          : c.name,
        "ceo_name"      : c.company_ceo,
        "business type" : c.business_type
    }
    status_dict = {
        "status": 200,
        "success": True,
        "message": message,
        "contentType":'application/json',
        "content": content
    }
  
    return jsonify(status_dict), status_dict["status"]
```

## More Info on Flask Route Params

Reference material for more information on Flask's 
route parameters. Using them, adding types, etc.

Ref: [https://pythonbasics.org/flask-tutorial-routes/#flask-route-params](https://pythonbasics.org/flask-tutorial-routes/#flask-route-params)

```python
@app.route('/sale/<transaction_id>')
def get_sale(transaction_id=0):
  return "The transaction is "+str(transaction_id)
```