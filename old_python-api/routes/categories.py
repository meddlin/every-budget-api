
from flask_cors import cross_origin


@cross_origin()
@app.route('/get', methods=['GET', 'POST'])
def get():
    return ''