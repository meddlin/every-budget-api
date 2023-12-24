class Transaction:
    """Data model to hold individual monetary transactions on budgets"""
    def __init__(self, date_added, vendor, amount, note):
        self.date_added = date_added
        self.vendor = vendor
        self.amount = amount
        self.note = note
    
    ### Update a transaction with new data
    @classmethod
    def update(self) -> int:
        """Update a transaction"""

        return 0
