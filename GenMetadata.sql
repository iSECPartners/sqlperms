-- class definitions
select 
	z.Tag
	, z.Parent
	, z.ClassId [EventClass!1!Id]
	, z.Category [EventClass!1!Category]
	, z.Name  [EventClass!1!Name]
	, z.Name  [EventClass!1!ClassName]
	, z.ColId  [Col!2!Id]
	, z.ColName [Col!2!Name]
from (

select 
		1 as Tag
		, NULL as Parent
		, e.trace_event_id as ClassId
		, c.name as Category
		, e.name as Name
		, NULL as ColId
		, NULL as ColName
	from sys.trace_events e
	join sys.trace_categories c on e.category_id = c.category_id
	where c.name = N'Security Audit'
	
UNION ALL
select 
		2 as Tag
		, 1 as Parent
		, e.trace_event_id as ClassId
		, NULL as Category
		, NULL as Name
		, c.trace_column_id
		, c.name
	from sys.trace_events e
	join sys.trace_event_bindings ec on e.trace_event_id = ec.trace_event_id
	join sys.trace_columns c on ec.trace_column_id = c.trace_column_id
	join sys.trace_categories cat on e.category_id = cat.category_id
	where cat.name = N'Security Audit'
) z
order by z.ClassId, z.Tag
for xml explicit

-- Columns query (so we don't have to keep redefining the membername and have discrepencies
select 
		c.trace_column_id [@ColId]
		, c.name [@ColName]
		, c.Name [@MemberName]
		, CASE c.name WHEN N'EventSequence' THEN 'bigint' ELSE c.type_name END [@ColType]
		, case c.type_name when N'nvarchar' then c.max_size /2
			else c.max_size
			end [@ColMaxSize]
	from sys.trace_columns c
	for xml path ('Column')


select 
		e.trace_event_id as [@Id]
		--, c.name as [@Category]
		, e.name as [@Name]
		--, NULL as [@ColId]
		--, NULL as [@ColName]
	from sys.trace_events e
	join sys.trace_categories c on e.category_id = c.category_id
	where c.name = N'Security Audit'
		order by 2
	--for xml path (N'EventClass')