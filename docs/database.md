# Database Schemas and Relationships

Database: 'every-budget'

## Schemas

NOTE: Before entering `uuid` types into Postgres tables we need to 
install an extension for it.

Ref: [https://stackoverflow.com/questions/22446478/extension-exists-but-uuid-generate-v4-fails](https://stackoverflow.com/questions/22446478/extension-exists-but-uuid-generate-v4-fails)

```sql
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
```

### Categories

- id -> UUID
- date_created -> timestamp
- date_updated -> timestamp
- name -> varchar

```sql
CREATE TABLE IF NOT EXISTS public.categories
(
    id uuid NOT NULL,
    date_created timestamp with time zone NOT NULL,
    date_updated timestamp with time zone NOT NULL,
    name character varying COLLATE pg_catalog."default",
    CONSTRAINT categories_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.categories
    OWNER to postgres;
```

#### Sample `INSERT`

```sql
INSERT INTO public.categories(
	id, date_created, date_updated, name)
	VALUES (uuid_generate_v4(), current_timestamp, current_timestamp, 'Test');
```

### BudgetItems

- id -> UUID
- date_created -> timestamp
- date_updated -> timestamp
- category_id -> UUID
- name -> varchar
- planned -> numeric
- spent -> numeric
- description -> varchar

```sql
CREATE TABLE IF NOT EXISTS public.budget_items
(
    id uuid NOT NULL,
    date_created timestamp with time zone NOT NULL,
    date_updated timestamp with time zone NOT NULL,
    category_id uuid,
    name character varying COLLATE pg_catalog."default",
    planned numeric(12,2),
    spent numeric(12,2),
    description character varying COLLATE pg_catalog."default",
    CONSTRAINT budget_items_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.budget_items
    OWNER to postgres;
```

### Transactions

