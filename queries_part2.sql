begin tran --rollback, commit

select * from Employees
select * from Addresses
select * from Items
select * from Orders
select * from Customers

begin tran --rollback, commit

select ID , COUNT(ID) from Employees GROUP BY ID
select SalesPerson,  count(SalesPerson) as a from Orders group by SalesPerson

insert into Orders(CustomerCode,ItemCode,OrderArea,OrderDate,OrderQuantity,SalesPerson, Total)
values (2, 2, 1, '2020-06-01 08:10:00', 9 ,N'רן',9 *(select Price from Items where ItemCode = 2))

select i.ItemName, o.OrderQuantity, o.Total -- Q1
 from Items i inner join orders o 
on i.ItemCode = o.ItemCode 
where o.OrderQuantity > 100

select  o.OrderQuantity, o.Total  -- Q2
from Items i inner join orders o 
on i.ItemCode = o.ItemCode
where o.OrderDate > '2020-05-01' 
and o.OrderDate < '2020-07-01'
and o.OrderQuantity > 10

select  ROW_NUMBER() over (ORDER BY ItemCode) N'מספר שורה' ,ItemCode  N'קוד פריט', ItemName  N'שם פריט' --Q3
from Items order by ItemCode asc  -- /desc

select * from Customers where CustomerName like N'ג%' --Q4