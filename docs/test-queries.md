# Test Queries

This is the query you want to run to return entire budgets.

```sql
select 
	b.name,
	c.name,
	bi.name,
	bi.planned,
	bi.spent,
	t.vendor,
	t.amount,
	t.transaction_date,
	t.notes
from budgets b
join categories c on c.budget_id = b.id
join budget_items bi on bi.category_id = c.id
join transactions t on t.budget_item_id = bi.id
```