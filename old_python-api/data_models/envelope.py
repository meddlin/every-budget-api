class Envelope:
    """A specific piece of a Category"""
    def __init__(self, title, spent, budget, transactions):
        self.title = title
        self.spent = spent
        self.budget = budget
        self.transactions = transactions
    
    @classmethod
    def update(self) -> int:
        """Update an envelope"""
        return 0