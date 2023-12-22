import decimal, datetime
from uuid import UUID

## JSON serialization of objects isn't exactly supported
##  Ref: https://codeandlife.com/2014/12/07/sqlalchemy-results-to-json-the-easy-way/

## UUID serialization (or string conversion) is from
##  here: https://arthurpemberton.com/2015/04/fixing-uuid-is-not-json-serializable

def alchemyencoder(obj):
    """JSON encoder function for SQLAlchemy special classes."""
    if isinstance(obj, datetime.date):
        return obj.isoformat()
    elif isinstance(obj, UUID):
        return str(obj)
    elif isinstance(obj, decimal.Decimal):
        return float(obj)