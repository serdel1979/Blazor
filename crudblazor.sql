create database DBCrudBlazor;

use DBCrudBlazor;

create table Departamento(
Id int primary key identity(1,1),
Nombre varchar(50) not null,
);

create table Empleado(
Id int primary key identity(1,1),
NombreCompleto varchar(50) not null,
IdDepartamento int references Departamento(Id)not null,
Sueldo int not null,
FechaContrato date not null);

insert into Departamento(Nombre) values
('Administración'),
('Marketing'),
('Ventas'),
('Comercio')

select * from Departamento

insert into Empleado(NombreCompleto, IdDepartamento, FechaContrato, Sueldo) values
('Mario Mordini',2,GETDATE(),3211);