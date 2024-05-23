-- Create a workshop database.
create database workshopDB;
go
-- Change to use new workshop DB.
use workshopDB; 
go

-- Create all tables
-- Incase if there is any issue first time running it then checking next time if table already created then do not create 

if not exists(select 1 from information_schema.tables where table_name = 'customer')
begin 
	create table customer(
	customer_id int identity primary key,
	first_name varchar(20),
	last_name varchar(20),
	customer_address varchar(200)
	)
end


if not exists(select 1 from information_schema.tables where table_name = 'vehicle')
begin
	create table vehicle(
	vehicle_number varchar(10) primary key,
	customer_id int constraint fk_vehicle_customer_id foreign key references customer(customer_id), 
	vehicle_make varchar(20), 
	vehicle_model varchar(20),
	vehicle_year int
	)
end


if not exists(select 1 from information_schema.tables where table_name = 'slots')
begin
	create table slots(
	slot_id int primary key, 
	assigned_hours int 
	)

	Delete from slots; 
	insert into slots(slot_id, assigned_hours)
	values (1,2),(2,3),(3,2),(4,3)
end

if not exists(select 1 from information_schema.tables where table_name = 'vehicle_services')
begin
	create table vehicle_services(
	service_id int identity primary key,
	vehicle_number varchar(10) constraint fk_vehicle_service_vehicle_number foreign key references vehicle(vehicle_number), 
	service_date datetime, 
	slot_id int constraint fk_vehicle_service_slot_id foreign key references slots(slot_id), 
	) 
end



